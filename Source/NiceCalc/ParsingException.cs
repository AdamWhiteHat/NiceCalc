using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using NiceCalc.Tokenization;

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


        public ParsingException(string message, char charToken,
                                [CallerMemberName] string sourceMemberName = "",
                                [CallerFilePath] string sourceFilePath = "",
                                [CallerLineNumber] int sourceLineNumber = 0)
            : this(message, charToken.ToString(), sourceMemberName, sourceFilePath, sourceLineNumber)
        { }
        public ParsingException(string message, string stringToken,
                                [CallerMemberName] string sourceMemberName = "",
                                [CallerFilePath] string sourceFilePath = "",
                                [CallerLineNumber] int sourceLineNumber = 0)
            : base(message)
        {
            MethodName = sourceMemberName;
            SourceFile = sourceFilePath;
            LineNumber = sourceLineNumber;
            Token = stringToken;
            Stack = null;
        }


        public ParsingException(string message, char charToken, Stack<char> charStack,
                        [CallerMemberName] string sourceMemberName = "",
                        [CallerFilePath] string sourceFilePath = "",
                        [CallerLineNumber] int sourceLineNumber = 0)
            : this(message, charToken.ToString(), charStack, sourceMemberName, sourceFilePath, sourceLineNumber)
        { }
        public ParsingException(string message, string stringToken, Stack<char> charStack,
                        [CallerMemberName] string sourceMemberName = "",
                        [CallerFilePath] string sourceFilePath = "",
                        [CallerLineNumber] int sourceLineNumber = 0)
            : this(message, stringToken, charStack.Select(c => c.ToString()).ToList(), sourceMemberName, sourceFilePath, sourceLineNumber)
        { }


        public ParsingException(string message, char charToken, Stack<string> charStack,
                                [CallerMemberName] string sourceMemberName = "",
                                [CallerFilePath] string sourceFilePath = "",
                                [CallerLineNumber] int sourceLineNumber = 0)
            : this(message, charToken.ToString(), charStack, sourceMemberName, sourceFilePath, sourceLineNumber)
        { }
        public ParsingException(string message, string stringToken, Stack<string> stringStack,
                            [CallerMemberName] string sourceMemberName = "",
                            [CallerFilePath] string sourceFilePath = "",
                            [CallerLineNumber] int sourceLineNumber = 0)
            : this(message, stringToken, stringStack.ToList(), sourceMemberName, sourceFilePath, sourceLineNumber)
        { }

        public ParsingException(string message, IToken token, Stack<IToken> stack,
                            [CallerMemberName] string sourceMemberName = "",
                            [CallerFilePath] string sourceFilePath = "",
                            [CallerLineNumber] int sourceLineNumber = 0)
            : this(message, token.ToString(), stack.Select(tok => tok.ToString()).ToList(), sourceMemberName, sourceFilePath, sourceLineNumber)
        {
        }


        public ParsingException(string message, char token, List<string> stack,
                    [CallerMemberName] string sourceMemberName = "",
                    [CallerFilePath] string sourceFilePath = "",
                    [CallerLineNumber] int sourceLineNumber = 0)
            : this(message, token.ToString(), stack, sourceMemberName, sourceFilePath, sourceLineNumber)
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
