/*
 *
 * Developed by Adam White
 *  https://csharpcodewhisperer.blogspot.com
 * 
 */
using System;
using System.Linq;
using System.Numerics;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms.VisualStyles;
using ExtendedNumerics;
using System.ComponentModel;
using NiceCalc.Execution;
using NiceCalc.Interpreter.Language;
using Newtonsoft.Json.Linq;
using NiceCalc.Tokenization;

namespace NiceCalc.Interpreter
{
	public static class InfixNotation
	{
		/// <summary>
		/// Tokenizes, Parses and then Evaluates a (mostly) infix numerical expression.
		/// "Mostly" means it also handles other syntactical constructions
		/// that are not strictly infix, such as function calls, e.g. "sqrt(42)"
		/// and the factorial notation, e.g. "42!"
		/// </summary>
		public static string Evaluate(List<string> infixNotationString, NumericType type)
		{
			List<string> functionTokenizedString = Tokenizer.Preprocess.TokenizeFunctions(infixNotationString);
			Queue<string> postFixNotationString = ShuntingYardConverter.Convert(functionTokenizedString);
			string result = PostfixNotation.Evaluate(postFixNotationString, type);
			return result.Replace("/", " / ");
		}

		
	}
}
