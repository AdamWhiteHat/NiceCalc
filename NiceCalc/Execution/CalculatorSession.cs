using ExtendedNumerics;
using Microsoft.Build.Framework.XamlTypes;
using NiceCalc.Interpreter;
using NiceCalc.Interpreter.Language;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
			string variableInlinedExpression = PopulateVariableValues(expression, Variables);
			string results = InfixNotation.Evaluate(variableInlinedExpression, PreferredOutputFormat);
			return results;
		}

		private static string PopulateVariableValues(string expression, ObservableDictionary<string, string> variableDictionary)
		{
			List<string> tokens = Tokenize(expression);

			foreach (var kvp in variableDictionary)
			{
				int index = tokens.IndexOf(kvp.Key);
				while (index != -1)
				{
					tokens[index] = kvp.Value;
					index = tokens.IndexOf(kvp.Key);
				}
			}

			string result = string.Join("", tokens);
			return result;
		}

		private static readonly string NonIdentifierTokens = Syntax.Operators + "() ";
		private static List<string> Tokenize(string expression)
		{
			List<string> tokens = new List<string>();

			List<char> number = new List<char>();
			List<char> identifier = new List<char>();

			foreach (char c in expression)
			{
				if (NonIdentifierTokens.Contains(c))
				{
					if (number.Any())
					{
						tokens.Add(new string(number.ToArray()));
						number.Clear();
					}
					if (identifier.Any())
					{
						tokens.Add(new string(identifier.ToArray()));
						identifier.Clear();
					}
					tokens.Add(c.ToString());
				}
				else if (Syntax.Numbers.Contains(c))
				{
					if (identifier.Any())
					{
						identifier.Add(c);
					}
					else
					{
						number.Add(c);
					}
				}
				else
				{
					if (number.Any())
					{
						tokens.Add(new string(number.ToArray()));
						number.Clear();
					}
					identifier.Add(c);
				}
			}

			if (number.Any())
			{
				tokens.Add(new string(number.ToArray()));
				number.Clear();
			}
			if (identifier.Any())
			{
				tokens.Add(new string(identifier.ToArray()));
				identifier.Clear();
			}

			return tokens;
		}

	}
}
