using ExtendedNumerics;
using Microsoft.Build.Framework.XamlTypes;
using NiceCalc.Interpreter;
using NiceCalc.Interpreter.Language;
using NiceCalc.Tokenization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NiceCalc.Execution
{
	public class CalculatorSession : IDisposable
	{
		[Bindable(true)]
		[TypeConverter("System.Windows.Forms.Design.DataSourceConverter,System.Design")]
		[Category("Data")]
		public ObservableDictionary<string, string> Variables { get; set; }

		public NumericType PreferredOutputFormat { get; set; }

		private int LineNumber { get; set; }
		private IList BoundList = null;

		private CalculatorSession()
		{
			Reset();
			PreferredOutputFormat = NumericType.Real;
		}

		public CalculatorSession(NumericType preferredOutputFormat)
			: this()
		{
			PreferredOutputFormat = preferredOutputFormat;
		}

		public void Dispose()
		{
			Reset();
		}

		public void Reset()
		{
			LineNumber = 0;
			Variables = new ObservableDictionary<string, string>();
			BoundList = null;
		}

		public string Eval(string expression)
		{
			LineNumber++;

			// Check for variable assignment expressions
			if (expression.Contains(Syntax.AssignmentOperator))
			{
				if (expression.Count(c => c == Syntax.AssignmentOperator) != 1)
				{
					throw new CalculatorException($"Multiple assignment operators ('{Syntax.AssignmentOperator}') on line # {LineNumber}.");
				}

				string[] parts = expression.Split(new char[] { Syntax.AssignmentOperator });

				string variableIdentifier = parts[0].Trim();
				string variableExpression = parts[1].Trim();

				if (char.IsDigit(variableIdentifier[0]) || variableIdentifier.Any(c => !(char.IsLetterOrDigit(c) || c == '_')))
				{
					throw new CalculatorException($"Variable identifiers may contain only letters, numbers and underscore characters and cannot begin with a number. Illegal identifier: '{variableIdentifier}'");
				}

				if (Syntax.ReservedIdentifiers.Contains(variableIdentifier))
				{
					throw new CalculatorException($"'{variableIdentifier}' is a reserved identifier. Please choose something else.");
				}

				return EvaluateAndAssign(variableExpression, variableIdentifier);
			}

			return Evaluate(expression);
		}

		public void BindToList(IList bindingList)
		{
			BoundList = bindingList;
		}

		private string EvaluateAndAssign(string expression, string assignmentIdentifier)
		{
			string results = Evaluate(expression);

			string formattedString = $"{assignmentIdentifier} = {results.Trim()}";

			if (Variables.ContainsKey(assignmentIdentifier))
			{
				string value = Variables[assignmentIdentifier];

				if (value.Trim() != results.Trim())
				{
					Variables[assignmentIdentifier] = results.Trim();
				}
			}
			else
			{
				Variables.Add(assignmentIdentifier, results.Trim());
			}

			if (BoundList != null)
			{
				var found = BoundList.Cast<string>().FirstOrDefault(itm => itm.ToString().Contains(assignmentIdentifier));

				if (found != default(string))
				{
					int index = BoundList.IndexOf(found);
					if (index != -1)
					{
						BoundList.RemoveAt(index);
						BoundList.Insert(index, formattedString);
					}
				}
				else
				{
					BoundList.Add(formattedString);
				}
			}

			return results;
		}



		private string Evaluate(string expression)
		{
			if (!string.IsNullOrWhiteSpace(expression))
			{
				string expr = expression;
				if (expression[0] == '-')
				{
					expr = new string(expression.Skip(1).ToArray());
				}

				if (expr.All(c => Syntax.Numbers.Contains(c)))
				{
					return expression; // No-op. Expression is just a number (likely to be assigned to variable).
				}
			}

			/*
			//ParsingConfig.Default.ExpressionPromoter
			//ParsingConfig.Default.TypeConverters
			//System.Linq.Dynamic.Core.Parser.IExpressionPromoter
			//IExpressionPromoter: Expression promoter is used to promote object or value types to their destination type when an automatic promotion is available such as: int to int?

			Dictionary<Type, TypeConverter> typeConverters = new Dictionary<Type, TypeConverter>();
			typeConverters.Add(typeof(BigDecimal), new BigDecimalTypeConverter());
			typeConverters.Add(typeof(Fraction), new FractionTypeConverter());

			ParsingConfig config = ParsingConfig.Default;
			config.TypeConverters = typeConverters;
		



			LambdaExpression lambda = System.Linq.Dynamic.Core.DynamicExpressionParser.ParseLambda(parsingConfig: config, parameters: GetVariablesInScope(), resultType: null, expression: expression);
			Delegate func = lambda.Compile();

			object output = string.Empty;

			if (Variables.Any())
			{
				if (PreferredOutputFormat == NumericType.Real)
				{
					output = func.DynamicInvoke(GetVariableValuesAsReals());
				}
				else if (PreferredOutputFormat == NumericType.Rational)
				{
					output = func.DynamicInvoke(GetVariableValuesAsRationals());
				}
			}
			else
			{
				output = func.DynamicInvoke();
			}

			string result = "";

			if (output != null)
			{
				result = output.ToString();
			}

			return result;
			*/


			List<string> tokens = Tokenizer.Tokenize(expression);

			bool skipEvaluation = false;
			if (tokens.All(tok => Variables.ContainsKey(tok)))
			{
				skipEvaluation = true;
			}

			List<string> variableInlinedExpression = PopulateVariableValues(tokens, Variables);

			if (skipEvaluation)
			{
				return string.Join("", variableInlinedExpression);
			}

			string results = InfixNotation.Evaluate(variableInlinedExpression, PreferredOutputFormat);
			return results;
		}

		private BigDecimal[] GetVariableValuesAsReals()
		{
			return Variables.Select(kvp => BigDecimal.Parse(kvp.Value)).ToArray();
		}

		private Fraction[] GetVariableValuesAsRationals()
		{
			return Variables.Select(kvp => Fraction.Parse(kvp.Value)).ToArray();
		}

		private ParameterExpression[] GetVariablesInScope()
		{
			//Variables
			//PreferredOutputFormat

			Type parameterType = typeof(BigDecimal);

			if (PreferredOutputFormat == NumericType.Rational)
			{
				parameterType = typeof(Fraction);
			}

			return Variables.Select(kvp => ParameterExpression.Parameter(parameterType, kvp.Key)).ToArray();
		}

		private static List<string> PopulateVariableValues(List<string> tokens, ObservableDictionary<string, string> variableDictionary)
		{
			foreach (var kvp in variableDictionary)
			{
				int index = tokens.IndexOf(kvp.Key);
				while (index != -1)
				{
					tokens[index] = kvp.Value;
					index = tokens.IndexOf(kvp.Key);
				}
			}

			return tokens;
		}

		

	}


	public class BigDecimalTypeConverter : TypeConverter
	{
		public BigDecimalTypeConverter()
			: base()
		{ }

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			switch (sourceType.Name)
			{
				case "Int16":
				case "Int32":
				case "Int64":
				case "Single":
				case "Double":
				case "Decimal":
				case "BigInteger":
				case "String":
					return true;
				default:
					return base.CanConvertFrom(context, sourceType);
			}
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			switch (destinationType.Name)
			{
				case "Int16":
				case "Int32":
				case "Int64":
				case "Single":
				case "Double":
				case "BigInteger":
				case "String":
					return true;
				default:
					return base.CanConvertTo(context, destinationType);
			}
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is Int16 || value is Int32) { return new BigDecimal((Int32)value); }
			else if (value is Int64) { return new BigDecimal((Int64)value); }
			else if (value is Single) { return new BigDecimal((Single)value); }
			else if (value is double) { return new BigDecimal((double)value); }
			else if (value is BigInteger) { return new BigDecimal((BigInteger)value); }
			else if (value is string) { return BigDecimal.Parse((string)value); }
			else { return base.ConvertFrom(context, culture, value); }
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			BigDecimal from = (BigDecimal)value;
			switch (destinationType.Name)
			{
				case "Int16":
					return (Int16)from;
				case "Int32":
					return (Int32)from;
				case "Int64":
					return (Int64)((Int32)from);
				case "Single":
					return (Single)from;
				case "Double":
					return (double)from;
				case "BigInteger":
					return (BigInteger)from;
				case "String":
					return from.ToString();
				default:
					return base.ConvertTo(context, culture, value, destinationType);
			}
		}

		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			switch (value.GetType().Name)
			{
				case "Int16":
				case "Int32":
				case "Int64":
				case "Single":
				case "Double":
				case "BigInteger":
				case "String":
					return true;
				default:
					return base.IsValid(context, value);
			}
		}
	}


	public class FractionTypeConverter : TypeConverter
	{
		public FractionTypeConverter()
			: base()
		{ }

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			switch (sourceType.Name)
			{
				case "Int16":
				case "Int32":
				case "Int64":
				case "Single":
				case "Double":
				case "Decimal":
				case "BigInteger":
				case "String":
					return true;
				default:
					return base.CanConvertFrom(context, sourceType);
			}
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			switch (destinationType.Name)
			{
				case "Int32":
				case "Single":
				case "Double":
				case "BigInteger":
				case "String":
					return true;
				default:
					return base.CanConvertTo(context, destinationType);
			}
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is Int16 || value is Int32) { return new Fraction((Int32)value); }
			else if (value is Int64) { return new Fraction((Int32)value); }
			else if (value is Single) { return new Fraction((Single)value); }
			else if (value is double) { return new Fraction((double)value); }
			else if (value is BigInteger) { return new Fraction((BigInteger)value); }
			else if (value is string) { return Fraction.Parse((string)value); }
			else { return base.ConvertFrom(context, culture, value); }
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			Fraction from = (Fraction)value;
			switch (destinationType.Name)
			{
				case "Int32":
					{
						BigInteger result = from.Numerator / from.Denominator;
						return (int)result;
					}
				case "Single":
					return (Single)from;
				case "Double":
					return (double)from;
				case "BigInteger":
					return (BigInteger)from;
				case "String":
					return from.ToString();
				default:
					return base.ConvertTo(context, culture, value, destinationType);
			}
		}

		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			switch (value.GetType().Name)
			{
				case "Int16":
				case "Int32":
				case "Int64":
				case "Single":
				case "Double":
				case "BigInteger":
				case "String":
					return true;
				default:
					return base.IsValid(context, value);
			}
		}
	}

}
