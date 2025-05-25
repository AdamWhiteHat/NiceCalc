using ExtendedNumerics;
using System;
using System.Drawing;
using System.Numerics;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using NiceCalc.Interpreter;
using NiceCalc.Interpreter.Language;
using NiceCalc.Execution;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Management;
using NiceCalc.Tokenization;
using System.ComponentModel;
using System.Collections.Specialized;

namespace NiceCalc
{
    public partial class MainForm : Form
    {
        public CalculatorSession CalculatorSession;

        public static Settings CurrentSettings;
        private ObservableDictionary<string, string> _variables;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Shown(object sender, System.EventArgs e)
        {
            List<string> autocompleteItems = new List<string>
            {
                "#",
                "abs",
                "ceil",
                "cos",
                "divisors",
                "factor",
                "factorial",
                "floor",
                "gcd",
                "isprime",
                "lcm",
                "ln",
                "logn",
                "nextprime",
                "nthroot",
                "pi",
                "previousprime",
                "round",
                "sign",
                "sin",
                "sqrt",
                "tan",
                "trunc"
            };
            tbInput.AutoCompleteCustomSource = autocompleteItems;

            CurrentSettings = new Settings();
            CurrentSettings.Load();
            LoadCurrentSetting(CurrentSettings);
            RegisterSettingsEventHandles();
            CalculatorSession = new CalculatorSession(cbPreferFractionsResult.Checked ? NumericType.Rational : NumericType.Real);

            CalculatorSession.Variables.CollectionChanged += Variables_CollectionChanged;

            ResetBoundVariables();

            tbInput.ExecuteExpression += TbInput_ExecuteExpression;
            tbInput.ClearOutput += TbInput_ClearOutput;
        }

        private void Variables_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Replace)
            {
                var newItems = e.NewItems.Cast<KeyValuePair<string, NumberToken>>().Select(kvp => kvp.Key).ToList();
                tbInput.AutoCompleteCustomSource.AddRange(newItems);
            }
            if (e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Reset || e.Action == NotifyCollectionChangedAction.Replace)
            {
                var oldItems = e.OldItems.Cast<KeyValuePair<string, NumberToken>>().Select(kvp => kvp.Key).ToList();
                foreach (string item in oldItems)
                {
                    tbInput.AutoCompleteCustomSource.Remove(item);
                }
            }

        }

        private void TbInput_ExecuteExpression(object sender, EventArgs e)
        {
            Calculate();
        }

        private void TbInput_ClearOutput(object sender, EventArgs e)
        {
            tbOutput.Clear();
        }

        private void numericPrecision_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Calculate();
            }
        }

        private void ResetBoundVariables()
        {
            listBoxVariables.Items.Clear();
            CalculatorSession.BindToList(this.listBoxVariables.Items);
        }

        public void Calculate()
        {
            ProcessLines(tbInput.Lines);
        }

        private void ProcessLines(string[] lines)
        {
            tbOutput.Clear();
            tbOutput.ClearUndo();

            if (CalculatorSession == null)
            {
                CalculatorSession = new CalculatorSession(cbPreferFractionsResult.Checked ? NumericType.Rational : NumericType.Real);

                //listBoxVariables.DataBindings.Add(new Binding("Items", CalculatorSession.Variables, null));
                //listBoxVariables.DataSource = CalculatorSession.Variables.GetList(); // new BindingSource((IEnumerable<KeyValuePair<string, string>>)CalculatorSession.Variables, null);
                //listBoxVariables.FormattingEnabled = true;
                //listBoxVariables.Format += ListBoxVariables_Format;

                //_variables = calculatorSessionBindingSource.List;
            }

            foreach (string immutableLine in lines)
            {
                string line = immutableLine;

                if (string.IsNullOrWhiteSpace(line))
                {
                    tbOutput.AppendText(Environment.NewLine);
                    continue;
                }

                // Simple comment syntax support. 
                // Comment string must come at the beginning of the line (less whitespace)
                // and the comment will extend to the end of the line.
                if (Syntax.SingleLineComments.Any(comment => line.Trim().StartsWith(comment)))
                {
                    continue;
                }

                    List<string> expressions = new List<string>();

                    bool multiExpressionedLine = line.Contains(';');
                    if (multiExpressionedLine)
                    {
                        expressions.AddRange(line.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));
                    }
                    else
                    {
                        expressions.Add(line);
                    }

                    foreach (string expression in expressions)
                    {
                        int leftSpaces = expression.AsEnumerable().TakeWhile(c => char.IsWhiteSpace(c)).Count();
                        int rightSpaces = expression.AsEnumerable().Reverse().TakeWhile(c => char.IsWhiteSpace(c)).Count();

                        if (!multiExpressionedLine && CurrentSettings.CopyInputToOutput)
                        {
                            tbOutput.AppendText(expression + " = ");
                        }

                        try
                        {
                            string toEval = expression.Replace(" ", "").Replace("\t", "");

                            IToken resultToken = CalculatorSession.Eval(toEval);
                            string result = "";

                            if (resultToken.TokenType == TokenType.Number && CurrentSettings.PreferFractionsResult)
                            {
                                NumberToken numericResults = (NumberToken)resultToken;
                                result = numericResults.RationalValue.ToString();
                            }
                            else
                            {
                                result = resultToken.ToString();
                            }

                            if (leftSpaces > 0)
                            {
                                tbOutput.AppendText(new string(Enumerable.Repeat(' ', leftSpaces).ToArray()));
                            }
                            tbOutput.AppendText(result);
                            if (rightSpaces > 0)
                            {
                                tbOutput.AppendText(new string(Enumerable.Repeat(' ', rightSpaces).ToArray()));
                            }

                            if (multiExpressionedLine)
                            {
                                tbOutput.AppendText(";");
                            }
                        }
                        catch (Exception ex)
                        {
                            tbOutput.AppendText(ex.ToString());
                        }
                    }


            }
        }

        private void ListBoxVariables_Format(object sender, ListControlConvertEventArgs e)
        {
            KeyValuePair<string, string> item = (KeyValuePair<string, string>)e.ListItem;
            e.Value = string.Format("{0}({1})", item.Key, item.Value);
        }

        void BtnFunctionSimple_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            int start = tbInput.SelectionStart;
            int end = start + tbInput.SelectionLength;

            string text = tbInput.Text;

            int insertionLength = btn.Text.Length;
            text = text.Insert(end, btn.Text);

            tbInput.Text = text;

            tbInput.SelectionStart = end + insertionLength;
            tbInput.SelectionLength = 0;

            tbInput.Focus();
        }

        void BtnFunctionParameterize_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            int start = tbInput.SelectionStart;
            int end = start + tbInput.SelectionLength;

            string text = tbInput.Text;

            text = text.Insert(end, ")");
            text = text.Insert(start, btn.Text + "(");

            tbInput.Text = text;

            int restoreLocation = text.IndexOf('(', start) + 1;
            tbInput.SelectionStart = restoreLocation;
            tbInput.SelectionLength = 0;

            tbInput.Focus();
        }

        private void cbExpandPanel_CheckedChanged(object sender, EventArgs e)
        {
            if (cbExpandPanel.Checked)
            {
                groupVariables.Size = groupVariables.MaximumSize;
                cbExpandPanel.ImageIndex = 1;
            }
            else
            {
                groupVariables.Size = groupVariables.MinimumSize;
                cbExpandPanel.ImageIndex = 0;
            }
        }

        #region CurrentSettings <=> UI Controls

        private void LoadCurrentSetting(Settings currentSettings)
        {
            this.Width = currentSettings.WindowWidth;
            this.Height = currentSettings.WindowHeight;
            Point location = new Point(currentSettings.WindowLocationX, currentSettings.WindowLocationY);
            this.Location = location;

            cbCopyInputToOutput.Checked = currentSettings.CopyInputToOutput;
            cbCtrlEnterForTotal.Checked = currentSettings.CtrlEnterForTotal;
            cbPreferFractionsResult.Checked = currentSettings.PreferFractionsResult;

            numericPrecision.Value = currentSettings.Precision;
            BigDecimal.Precision = currentSettings.Precision;
            BigDecimal.AlwaysNormalize = true;
            BigDecimal.AlwaysTruncate = true;

            string fontName = currentSettings.FontName;
            float fontSize = currentSettings.FontSize;

            Font loadedFont = new Font(fontName, fontSize);
            tbInput.Font = loadedFont;
            tbOutput.Font = loadedFont;

            int splitterDistance = splitContainer_LeftRight.Size.Width - splitContainer_LeftRight.SplitterWidth - CurrentSettings.RightPanelWidth;
            splitContainer_LeftRight.SplitterDistance = splitterDistance;
        }

        private void RegisterSettingsEventHandles()
        {
            cbCopyInputToOutput.CheckedChanged += CbCopyInputToOutput_CheckedChanged;
            cbCtrlEnterForTotal.CheckedChanged += CbCtrlEnterForTotal_CheckedChanged;
            cbPreferFractionsResult.CheckedChanged += CbUseFractions_CheckedChanged;
            numericPrecision.ValueChanged += NumericPrecision_ValueChanged;
            FormClosing += MainForm_FormClosing;
        }

        private void CbUseFractions_CheckedChanged(object sender, EventArgs e)
        {
            CurrentSettings.PreferFractionsResult = cbPreferFractionsResult.Checked;
            CalculatorSession.PreferredOutputFormat = cbPreferFractionsResult.Checked ? NumericType.Rational : NumericType.Real;
        }

        private void CbCopyInputToOutput_CheckedChanged(object sender, EventArgs e)
        {
            CurrentSettings.CopyInputToOutput = cbCopyInputToOutput.Checked;
        }

        private void CbCtrlEnterForTotal_CheckedChanged(object sender, EventArgs e)
        {
            CurrentSettings.CtrlEnterForTotal = cbCtrlEnterForTotal.Checked;
        }

        private void NumericPrecision_ValueChanged(object sender, EventArgs e)
        {
            CurrentSettings.Precision = (int)numericPrecision.Value;
            BigDecimal.Precision = CurrentSettings.Precision;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CurrentSettings.WindowLocationX = this.Location.X;
            CurrentSettings.WindowLocationY = this.Location.Y;
            CurrentSettings.WindowWidth = this.Width;
            CurrentSettings.WindowHeight = this.Height;

            CurrentSettings.CopyInputToOutput = cbCopyInputToOutput.Checked;
            CurrentSettings.CtrlEnterForTotal = cbCtrlEnterForTotal.Checked;
            CurrentSettings.PreferFractionsResult = cbPreferFractionsResult.Checked;

            CurrentSettings.FontName = tbInput.Font.Name;
            CurrentSettings.FontSize = tbInput.Font.Size;

            int rightPanelWidth = splitContainer_LeftRight.Size.Width - splitContainer_LeftRight.SplitterDistance - splitContainer_LeftRight.SplitterWidth;
            CurrentSettings.RightPanelWidth = rightPanelWidth;

            CurrentSettings.Save();
        }

        #endregion

        #region Context Menu Event Handlers

        private void _contextMenu_CutMenuClicked(object sender, EventArgs e)
        {
            tbInput.Cut();
        }

        private void _contextMenu_CopyMenuClicked(object sender, EventArgs e)
        {
            tbInput.Copy();
        }

        private void _contextMenu_PasteMenuClicked(object sender, EventArgs e)
        {
            tbInput.Paste();
        }

        private void _contextMenu_SelectAllMenuClicked(object sender, EventArgs e)
        {
            tbInput.SelectAll();
        }

        private void _contextMenu_UndoMenuClicked(object sender, EventArgs e)
        {
            tbInput.Undo();
        }

        private void _contextMenu_RedoMenuClicked(object sender, EventArgs e)
        {
            tbInput.Redo();
        }

        private void _contextMenu_ChangeFontMenuClicked(object sender, EventArgs e)
        {
            if (fontSelectionDialog.ShowDialog() == DialogResult.OK)
            {
                Font selectedFont = fontSelectionDialog.Font;
                tbInput.Font = selectedFont;
                tbOutput.Font = selectedFont;
                CurrentSettings.FontName = selectedFont.Name;
                CurrentSettings.FontSize = selectedFont.Size;
            }
        }

        #endregion

    }
}
