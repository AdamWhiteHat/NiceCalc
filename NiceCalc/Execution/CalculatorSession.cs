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

        public IToken Eval(string expression)
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

        private IToken EvaluateAndAssign(string identifier, string assignmentExpression)
        {
            IToken results = Evaluate(assignmentExpression);
            if (results.TokenType != TokenType.Number)
            {
                return results;
            }

            NumberToken numericResults = (NumberToken)results;

            if (Variables.ContainsKey(identifier))
            {
                NumberToken value = Variables[identifier];
                if (value != numericResults)
                {
                    Variables[identifier] = numericResults;
                }
            }
            else
            {
                Variables.Add(identifier, numericResults);
            }

            if (BoundList != null)
            {
                string formattedString = $"{identifier} = {numericResults}";

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

            return numericResults;
        }


        private IToken Evaluate(string expression)
        {
            if (!string.IsNullOrWhiteSpace(expression))
            {
                if (Syntax.IsNumeric(expression))
                {
                    return NumberToken.Factory.Parse(expression); // No-op. Expression is just a number (likely to be assigned to variable).
                }

                if (Variables.ContainsKey(expression))
                {
                    return Variables[expression];
                }
            }

            List<IToken> tokens = Tokenizer.Tokenize(expression);

            // Replace Variable tokens with their stored value
            bool unevaluatable = false;
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
                    else
                    {
                        unevaluatable = true;
                        throw new ParsingException($"Variable '{vTok.Name}' undefined.", stringToken: vTok.Name);
                    }
                }

                index = tokens.FindIndex(index + 1, tok => tok.TokenType == TokenType.Variable);
            }

            if (unevaluatable)
            {

            }

            IToken results = InfixNotation.Evaluate(tokens, PreferredOutputFormat);
            NumberToken? numericResults = results as NumberToken?;
            if (numericResults != null)
            {
                return new NumberToken(BigDecimal.Round(numericResults.Value.RealValue, BigDecimal.Precision));
            }
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
