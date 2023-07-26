using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace NiceCalc
{
	public class ParsingException : Exception
	{
		public string Token { get; private set; } = null;
		public List<string> Stack { get; private set; } = null;
		public string MethodName { get; private set; }
		public string SourceFile { get; private set; }
		public int LineNumber { get; private set; }

		public ParsingException(string message,
								[CallerMemberName] string sourceMemberName = "",
								[CallerFilePath] string sourceFilePath = "",
								[CallerLineNumber] int sourceLineNumber = 0)
			: base(message)
		{
			MethodName = sourceMemberName;
			SourceFile = sourceFilePath;
			LineNumber = sourceLineNumber;
			Token = null;
			Stack = null;
		}


		public ParsingException(string message, char token,
								[CallerMemberName] string sourceMemberName = "",
								[CallerFilePath] string sourceFilePath = "",
								[CallerLineNumber] int sourceLineNumber = 0)
			: this(message, token.ToString(), sourceMemberName, sourceFilePath, sourceLineNumber)
		{ }
		public ParsingException(string message, string token,
								[CallerMemberName] string sourceMemberName = "",
								[CallerFilePath] string sourceFilePath = "",
								[CallerLineNumber] int sourceLineNumber = 0)
			: base(message)
		{
			MethodName = sourceMemberName;
			SourceFile = sourceFilePath;
			LineNumber = sourceLineNumber;
			Token = token;
			Stack = null;
		}


		public ParsingException(string message, char token, Stack<char> stack,
						[CallerMemberName] string sourceMemberName = "",
						[CallerFilePath] string sourceFilePath = "",
						[CallerLineNumber] int sourceLineNumber = 0)
			: this(message, token.ToString(), stack, sourceMemberName, sourceFilePath, sourceLineNumber)
		{ }
		public ParsingException(string message, string token, Stack<char> stack,
						[CallerMemberName] string sourceMemberName = "",
						[CallerFilePath] string sourceFilePath = "",
						[CallerLineNumber] int sourceLineNumber = 0)
			: this(message, token, stack.Select(c => c.ToString()).ToList(), sourceMemberName, sourceFilePath, sourceLineNumber)
		{ }


		public ParsingException(string message, char token, Stack<string> stack,
								[CallerMemberName] string sourceMemberName = "",
								[CallerFilePath] string sourceFilePath = "",
								[CallerLineNumber] int sourceLineNumber = 0)
			: this(message, token.ToString(), stack, sourceMemberName, sourceFilePath, sourceLineNumber)
		{ }
		public ParsingException(string message, string token, Stack<string> stack,
							[CallerMemberName] string sourceMemberName = "",
							[CallerFilePath] string sourceFilePath = "",
							[CallerLineNumber] int sourceLineNumber = 0)
			: this(message, token, stack.ToList(), sourceMemberName, sourceFilePath, sourceLineNumber)
		{ }


		public ParsingException(string message, string token, List<string> stack,
							[CallerMemberName] string sourceMemberName = "",
							[CallerFilePath] string sourceFilePath = "",
							[CallerLineNumber] int sourceLineNumber = 0)
			: base(message)
		{
			MethodName = sourceMemberName;
			SourceFile = sourceFilePath;
			LineNumber = sourceLineNumber;
			Token = token;

			if (stack.Any())
			{
				Stack = stack;
			}
			else
			{
				Stack = null;
			}
		}


		public override string ToString()
		{
			string type = this.GetType().Name;
			string message = this.Message;
			string method = this.MethodName;
			string fileName = Path.GetFileName(this.SourceFile);
			string lineNumber = this.LineNumber.ToString();
			string tokenMessage = string.Empty;
			string stackMessage = string.Empty;

			if (Token != null)
			{
				tokenMessage = $" Token: '{Token}'.";
			}
			if (Stack != null)
			{
				stackMessage = $" Stack: [{string.Join(", ", Stack)}].";
			}

			string result = $"{type}: \"{message}\". Location: At method '{method}' in file '{fileName}', line # {lineNumber}.{tokenMessage}{stackMessage}";
			return result;
		}
	}
}
