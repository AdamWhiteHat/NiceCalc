using ExtendedNumerics;
using System;
using System.Drawing;
using System.Numerics;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

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
			AutoCompleteStringCollection autoCompleteSource = new AutoCompleteStringCollection();
			autoCompleteSource.AddRange(new string[]
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
				});
			tbInput.AutoCompleteCustomSource = autoCompleteSource;

			CurrentSettings = new Settings();
			CurrentSettings.Load();
			LoadCurrentSetting();
			RegisterSettingsEventHandles();
		}

		private void tbInput_KeyDown(object sender, KeyEventArgs e)
		{
			if (
				e.KeyCode == Keys.F5 ||
				e.KeyCode == Keys.Enter
			  )
			{
				if (CurrentSettings.CtrlEnterForTotal)
				{
					if (!(e.Control || e.Shift))
					{
						return;
					}
				}

				if (e.Control)
				{
					return;
				}

				e.Handled = true;
				e.SuppressKeyPress = true;
				ProcessLines(tbInput.Lines);
			}
			else if (e.KeyCode == Keys.Escape)
			{
				tbInput.Clear();
				tbOutput.Clear();
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
						object total = InfixNotation.Evaluate(line);

						tbOutput.AppendText(total.ToString());
					}
					catch (Exception ex)
					{
						tbOutput.AppendText(ex.ToString());
					}
				}
				tbOutput.AppendText(Environment.NewLine);
			}
		}

		#region CurrentSettings <=> UI Controls

		private void LoadCurrentSetting()
		{
			this.Width = CurrentSettings.WindowWidth;
			this.Height = CurrentSettings.WindowHeight;
			cbCopyInputToOutput.Checked = CurrentSettings.CopyInputToOutput;
			cbCtrlEnterForTotal.Checked = CurrentSettings.CtrlEnterForTotal;
			numericPrecision.Value = CurrentSettings.Precision;
			BigDecimal.Precision = CurrentSettings.Precision;

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
			CurrentSettings.WindowWidth = this.Width;
			CurrentSettings.WindowHeight = this.Height;

			int rightPanelWidth = splitContainer_LeftRight.Size.Width - splitContainer_LeftRight.SplitterDistance - splitContainer_LeftRight.SplitterWidth;
			CurrentSettings.RightPanelWidth = rightPanelWidth;

			CurrentSettings.Save();
		}

		#endregion

	}
}