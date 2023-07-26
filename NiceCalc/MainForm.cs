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

			CurrentSettings = new Settings("Setting.json");
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
		}

		private void RegisterSettingsEventHandles()
		{
			numericPrecision.ValueChanged += NumericPrecision_ValueChanged; ;
			cbCopyInputToOutput.CheckedChanged += CbCopyInputToOutput_CheckedChanged;
			cbCtrlEnterForTotal.CheckedChanged += CbCtrlEnterForTotal_CheckedChanged;
			FormClosing += MainForm_FormClosing;
		}

		private void NumericPrecision_ValueChanged(object? sender, EventArgs e)
		{
			CurrentSettings.Precision = (int)numericPrecision.Value;
			BigDecimal.Precision = CurrentSettings.Precision;
		}

		private void CbCtrlEnterForTotal_CheckedChanged(object? sender, EventArgs e)
		{
			CurrentSettings.CtrlEnterForTotal = cbCtrlEnterForTotal.Checked;
		}

		private void CbCopyInputToOutput_CheckedChanged(object? sender, EventArgs e)
		{
			CurrentSettings.CopyInputToOutput = cbCopyInputToOutput.Checked;
		}

		private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
		{
			CurrentSettings.WindowWidth = this.Width;
			CurrentSettings.WindowHeight = this.Height;
			CurrentSettings.Save();
		}

		#endregion

	}
}