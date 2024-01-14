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
		public static NumberToken Evaluate(List<Token> tokens, NumericType type)
		{
			Queue<Token> postFixTokenQueue = ShuntingYardConverter.Convert(tokens);
			NumberToken result = PostfixNotation.Evaluate(postFixTokenQueue, type);
			return result;
		}


	}
}
