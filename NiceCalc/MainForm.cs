using ExtendedNumerics;
using System;
using System.Drawing;
using System.Numerics;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using NiceCalc.Interpreter;
using NiceCalc.Interpreter.Language;

namespace NiceCalc
{
	public partial class MainForm : Form
	{
		private Settings CurrentSettings;

		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Shown(object sender, System.EventArgs e)
		{
			string[] autocompleteItems = new string[]
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
					"previousprime",
					"round",
					"sign",
					"sin",
					"sqrt",
					"tan",
					"trunc"
				};
			tbInput.AutoCompleteCustomSource = autocompleteItems;

			tbInput.KeyDown += tbInput_KeyDown;

			CurrentSettings = new Settings();
			CurrentSettings.Load();
			LoadCurrentSetting(CurrentSettings);
			RegisterSettingsEventHandles();
		}

		private void tbInput_KeyDown(object sender, KeyEventArgs e)
		{
			bool executeExpression = false;

			if (e.KeyCode == Keys.F5)
			{
				executeExpression = true;
			}
			else if ((e.KeyCode == Keys.Enter) && e.Shift)
			{
				executeExpression = true;
			}
			else if ((e.KeyCode == Keys.Enter) && !(CurrentSettings.CtrlEnterForTotal ^ e.Control))
			{
				executeExpression = true;
			}
			else if (e.KeyCode == Keys.Escape)
			{
				tbInput.Clear();
				tbOutput.Clear();
			}

			if (executeExpression)
			{
				e.Handled = true;
				e.SuppressKeyPress = true;
				ProcessLines(tbInput.Lines);
			}
		}

		private void ProcessLines(string[] lines)
		{
			tbOutput.Clear();
			tbOutput.ClearUndo();
			foreach (string line in lines)
			{
				if (!string.IsNullOrWhiteSpace(line))
				{
					if (CurrentSettings.CopyInputToOutput)
					{
						tbOutput.AppendText(line + " = ");
					}

					try
					{
						NumericType type = NumericType.Real;

						if (cbUseFractions.Checked)
						{
							type = NumericType.Rational;
						}

						string total = InfixNotation.Evaluate(line, type);

						tbOutput.AppendText(total);
					}
					catch (Exception ex)
					{
						tbOutput.AppendText(ex.ToString());
					}
				}
				tbOutput.AppendText(Environment.NewLine);
			}
		}

		void BtnFunctionClick(object sender, EventArgs e)
		{
			Button btn = (Button)sender;

			int start = tbInput.SelectionStart;
			int end = tbInput.SelectionStart + tbInput.SelectionLength;

			string text = tbInput.Text;

			text = text.Insert(end, ")");
			text = text.Insert(start, btn.Text + "(");

			tbInput.Text = text;
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
			numericPrecision.Value = currentSettings.Precision;
			BigDecimal.Precision = currentSettings.Precision;

			int splitterDistance = splitContainer_LeftRight.Size.Width - splitContainer_LeftRight.SplitterWidth - CurrentSettings.RightPanelWidth;
			splitContainer_LeftRight.SplitterDistance = splitterDistance;
		}

		private void RegisterSettingsEventHandles()
		{
			cbCopyInputToOutput.CheckedChanged += CbCopyInputToOutput_CheckedChanged;
			cbCtrlEnterForTotal.CheckedChanged += CbCtrlEnterForTotal_CheckedChanged;
			numericPrecision.ValueChanged += NumericPrecision_ValueChanged; ;
			FormClosing += MainForm_FormClosing;
		}

		private void CbCopyInputToOutput_CheckedChanged(object? sender, EventArgs e)
		{
			CurrentSettings.CopyInputToOutput = cbCopyInputToOutput.Checked;
		}

		private void CbCtrlEnterForTotal_CheckedChanged(object? sender, EventArgs e)
		{
			CurrentSettings.CtrlEnterForTotal = cbCtrlEnterForTotal.Checked;
		}

		private void NumericPrecision_ValueChanged(object? sender, EventArgs e)
		{
			CurrentSettings.Precision = (int)numericPrecision.Value;
			BigDecimal.Precision = CurrentSettings.Precision;
		}

		private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
		{
			CurrentSettings.WindowLocationX = this.Location.X;
			CurrentSettings.WindowLocationY = this.Location.Y;
			CurrentSettings.WindowWidth = this.Width;
			CurrentSettings.WindowHeight = this.Height;

			int rightPanelWidth = splitContainer_LeftRight.Size.Width - splitContainer_LeftRight.SplitterDistance - splitContainer_LeftRight.SplitterWidth;
			CurrentSettings.RightPanelWidth = rightPanelWidth;

			CurrentSettings.Save();
		}

		#endregion

	}
}