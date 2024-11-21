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
        public ObservableDictionary<string, NumberToken> Variables { get; set; }

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
            Variables = new ObservableDictionary<string, NumberToken>(StringComparer.OrdinalIgnoreCase);
            BoundList = null;
        }

        public NumberToken Eval(string expression)
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

                string identifier = parts[0].Trim();
                string assignmentExpression = parts[1].Trim();

                if (char.IsDigit(identifier[0]) || identifier.Any(c => !(char.IsLetterOrDigit(c) || c == '_')))
                {
                    throw new CalculatorException($"Variable identifiers may contain only letters, numbers and underscore characters and cannot begin with a number. Illegal identifier: '{identifier}'");
                }

                if (Syntax.ReservedIdentifiers.Contains(identifier))
                {
                    throw new CalculatorException($"'{identifier}' is a reserved identifier. Please choose something else.");
                }

                return EvaluateAndAssign(identifier, assignmentExpression);
            }

            return Evaluate(expression);
        }

        public void BindToList(IList bindingList)
        {
            BoundList = bindingList;
        }

        private NumberToken EvaluateAndAssign(string identifier, string assignmentExpression)
        {
            NumberToken results = Evaluate(assignmentExpression);

            if (Variables.ContainsKey(identifier))
            {
                NumberToken value = Variables[identifier];
                if (value != results)
                {
                    Variables[identifier] = results;
                }
            }
            else
            {
                Variables.Add(identifier, results);
            }

            if (BoundList != null)
            {
                string formattedString = $"{identifier} = {results}";

                var found = BoundList.Cast<string>().FirstOrDefault(itm => itm.Contains(identifier));
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


        private NumberToken Evaluate(string expression)
        {
            if (!string.IsNullOrWhiteSpace(expression))
            {
                string expr = expression;
                if (expression[0] == '-')
                {
                    expr = new string(expression.Skip(1).ToArray());
                }

                if (Syntax.IsNumeric(expr))
                {
                    return NumberToken.Factory.Parse(expression); // No-op. Expression is just a number (likely to be assigned to variable).
                }

                if (Variables.ContainsKey(expression))
                {
                    return Variables[expression];
                }
            }


            List<IToken> tokens = Tokenizer.Tokenize(expression);

            int index = tokens.FindIndex(tok => tok.TokenType == TokenType.Variable);
            while (index != -1)
            {
                VariableToken vTok = tokens[index] as VariableToken;
                if (vTok != null)
                {
                    if (Variables.ContainsKey(vTok.Name))
                    {
                        NumberToken fromVariable = Variables[vTok.Name];
                        tokens[index] = fromVariable;
                    }
                }

                index = tokens.FindIndex(index, tok => tok.TokenType == TokenType.Variable);
            }


            NumberToken results = InfixNotation.Evaluate(tokens, PreferredOutputFormat);
            results = new NumberToken(BigDecimal.Round(results.RealValue, BigDecimal.Precision));
            return results;
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

}
