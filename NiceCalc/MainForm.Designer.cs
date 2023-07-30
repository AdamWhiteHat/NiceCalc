using System.Drawing;
using System.Windows.Forms;

namespace NiceCalc
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.tbInput = new System.Windows.Forms.TextBox();
			this.tbOutput = new System.Windows.Forms.TextBox();
			this.splitContainer_TopBottom = new System.Windows.Forms.SplitContainer();
			this.cbCopyInputToOutput = new System.Windows.Forms.CheckBox();
			this.splitContainer_LeftRight = new System.Windows.Forms.SplitContainer();
			this.flowLayoutPanel_RightToolbar = new System.Windows.Forms.FlowLayoutPanel();
			this.cbCtrlEnterForTotal = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.numericPrecision = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer_TopBottom)).BeginInit();
			this.splitContainer_TopBottom.Panel1.SuspendLayout();
			this.splitContainer_TopBottom.Panel2.SuspendLayout();
			this.splitContainer_TopBottom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer_LeftRight)).BeginInit();
			this.splitContainer_LeftRight.Panel1.SuspendLayout();
			this.splitContainer_LeftRight.Panel2.SuspendLayout();
			this.splitContainer_LeftRight.SuspendLayout();
			this.flowLayoutPanel_RightToolbar.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericPrecision)).BeginInit();
			this.SuspendLayout();
			// 
			// tbInput
			// 
			this.tbInput.AcceptsReturn = true;
			this.tbInput.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.tbInput.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.tbInput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbInput.Location = new System.Drawing.Point(3, 3);
			this.tbInput.Margin = new System.Windows.Forms.Padding(0);
			this.tbInput.MaxLength = 65535;
			this.tbInput.Multiline = true;
			this.tbInput.Name = "tbInput";
			this.tbInput.PlaceholderText = "Input";
			this.tbInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbInput.Size = new System.Drawing.Size(444, 216);
			this.tbInput.TabIndex = 0;
			this.tbInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbInput_KeyDown);
			// 
			// tbOutput
			// 
			this.tbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbOutput.Location = new System.Drawing.Point(3, 3);
			this.tbOutput.Margin = new System.Windows.Forms.Padding(0);
			this.tbOutput.MaxLength = 65535;
			this.tbOutput.Multiline = true;
			this.tbOutput.Name = "tbOutput";
			this.tbOutput.PlaceholderText = "Results";
			this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbOutput.Size = new System.Drawing.Size(444, 168);
			this.tbOutput.TabIndex = 1;
			// 
			// splitContainer_TopBottom
			// 
			this.splitContainer_TopBottom.BackColor = System.Drawing.SystemColors.Control;
			this.splitContainer_TopBottom.Cursor = System.Windows.Forms.Cursors.HSplit;
			this.splitContainer_TopBottom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer_TopBottom.Location = new System.Drawing.Point(0, 0);
			this.splitContainer_TopBottom.Margin = new System.Windows.Forms.Padding(0);
			this.splitContainer_TopBottom.Name = "splitContainer_TopBottom";
			this.splitContainer_TopBottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer_TopBottom.Panel1
			// 
			this.splitContainer_TopBottom.Panel1.Controls.Add(this.tbInput);
			this.splitContainer_TopBottom.Panel1.Padding = new System.Windows.Forms.Padding(3);
			this.splitContainer_TopBottom.Panel1MinSize = 150;
			// 
			// splitContainer_TopBottom.Panel2
			// 
			this.splitContainer_TopBottom.Panel2.BackColor = System.Drawing.SystemColors.Control;
			this.splitContainer_TopBottom.Panel2.Controls.Add(this.tbOutput);
			this.splitContainer_TopBottom.Panel2.Padding = new System.Windows.Forms.Padding(3);
			this.splitContainer_TopBottom.Panel2MinSize = 150;
			this.splitContainer_TopBottom.Size = new System.Drawing.Size(450, 400);
			this.splitContainer_TopBottom.SplitterDistance = 222;
			this.splitContainer_TopBottom.TabIndex = 3;
			// 
			// cbCopyInputToOutput
			// 
			this.cbCopyInputToOutput.AutoSize = true;
			this.cbCopyInputToOutput.BackColor = System.Drawing.SystemColors.Control;
			this.cbCopyInputToOutput.Checked = true;
			this.cbCopyInputToOutput.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbCopyInputToOutput.Location = new System.Drawing.Point(7, 7);
			this.cbCopyInputToOutput.Name = "cbCopyInputToOutput";
			this.cbCopyInputToOutput.Size = new System.Drawing.Size(138, 19);
			this.cbCopyInputToOutput.TabIndex = 2;
			this.cbCopyInputToOutput.Text = "Copy input to output";
			this.cbCopyInputToOutput.UseVisualStyleBackColor = false;
			// 
			// splitContainer_LeftRight
			// 
			this.splitContainer_LeftRight.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer_LeftRight.Cursor = System.Windows.Forms.Cursors.VSplit;
			this.splitContainer_LeftRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer_LeftRight.Location = new System.Drawing.Point(0, 0);
			this.splitContainer_LeftRight.Margin = new System.Windows.Forms.Padding(0);
			this.splitContainer_LeftRight.Name = "splitContainer_LeftRight";
			// 
			// splitContainer_LeftRight.Panel1
			// 
			this.splitContainer_LeftRight.Panel1.Controls.Add(this.splitContainer_TopBottom);
			this.splitContainer_LeftRight.Panel1MinSize = 200;
			// 
			// splitContainer_LeftRight.Panel2
			// 
			this.splitContainer_LeftRight.Panel2.Controls.Add(this.flowLayoutPanel_RightToolbar);
			this.splitContainer_LeftRight.Panel2MinSize = 160;
			this.splitContainer_LeftRight.Size = new System.Drawing.Size(667, 400);
			this.splitContainer_LeftRight.SplitterDistance = 450;
			this.splitContainer_LeftRight.SplitterWidth = 5;
			this.splitContainer_LeftRight.TabIndex = 2;
			// 
			// flowLayoutPanel_RightToolbar
			// 
			this.flowLayoutPanel_RightToolbar.Controls.Add(this.cbCopyInputToOutput);
			this.flowLayoutPanel_RightToolbar.Controls.Add(this.cbCtrlEnterForTotal);
			this.flowLayoutPanel_RightToolbar.Controls.Add(this.panel1);
			this.flowLayoutPanel_RightToolbar.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel_RightToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel_RightToolbar.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel_RightToolbar.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel_RightToolbar.Name = "flowLayoutPanel_RightToolbar";
			this.flowLayoutPanel_RightToolbar.Padding = new System.Windows.Forms.Padding(4);
			this.flowLayoutPanel_RightToolbar.Size = new System.Drawing.Size(212, 400);
			this.flowLayoutPanel_RightToolbar.TabIndex = 1;
			// 
			// cbCtrlEnterForTotal
			// 
			this.cbCtrlEnterForTotal.AutoSize = true;
			this.cbCtrlEnterForTotal.BackColor = System.Drawing.SystemColors.Control;
			this.cbCtrlEnterForTotal.Checked = true;
			this.cbCtrlEnterForTotal.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbCtrlEnterForTotal.Location = new System.Drawing.Point(7, 32);
			this.cbCtrlEnterForTotal.Name = "cbCtrlEnterForTotal";
			this.cbCtrlEnterForTotal.Size = new System.Drawing.Size(125, 19);
			this.cbCtrlEnterForTotal.TabIndex = 3;
			this.cbCtrlEnterForTotal.Text = "Ctrl+Enter for total";
			this.cbCtrlEnterForTotal.UseVisualStyleBackColor = false;
			// 
			// panel1
			// 
			this.panel1.AutoSize = true;
			this.panel1.Controls.Add(this.numericPrecision);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new System.Drawing.Point(7, 57);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(1);
			this.panel1.Size = new System.Drawing.Size(146, 26);
			this.panel1.TabIndex = 5;
			// 
			// numericPrecision
			// 
			this.numericPrecision.AutoSize = true;
			this.numericPrecision.Location = new System.Drawing.Point(61, 2);
			this.numericPrecision.Margin = new System.Windows.Forms.Padding(0);
			this.numericPrecision.Maximum = new decimal(new int[] {
			100000000,
			0,
			0,
			0});
			this.numericPrecision.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.numericPrecision.Name = "numericPrecision";
			this.numericPrecision.Size = new System.Drawing.Size(84, 23);
			this.numericPrecision.TabIndex = 1;
			this.numericPrecision.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numericPrecision.ThousandsSeparator = true;
			this.numericPrecision.Value = new decimal(new int[] {
			20,
			0,
			0,
			0});
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(2, 4);
			this.label1.Margin = new System.Windows.Forms.Padding(1);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Precision:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(667, 400);
			this.Controls.Add(this.splitContainer_LeftRight);
			this.MinimumSize = new System.Drawing.Size(403, 345);
			this.Name = "MainForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "NiceCalc";
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.splitContainer_TopBottom.Panel1.ResumeLayout(false);
			this.splitContainer_TopBottom.Panel1.PerformLayout();
			this.splitContainer_TopBottom.Panel2.ResumeLayout(false);
			this.splitContainer_TopBottom.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer_TopBottom)).EndInit();
			this.splitContainer_TopBottom.ResumeLayout(false);
			this.splitContainer_LeftRight.Panel1.ResumeLayout(false);
			this.splitContainer_LeftRight.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer_LeftRight)).EndInit();
			this.splitContainer_LeftRight.ResumeLayout(false);
			this.flowLayoutPanel_RightToolbar.ResumeLayout(false);
			this.flowLayoutPanel_RightToolbar.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericPrecision)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private TextBox tbInput;
		private TextBox tbOutput;
		private SplitContainer splitContainer_TopBottom;
		private CheckBox cbCopyInputToOutput;
		private SplitContainer splitContainer_LeftRight;
		private FlowLayoutPanel flowLayoutPanel_RightToolbar;
		private Timer timer1;
		private CheckBox cbCtrlEnterForTotal;
		private Panel panel1;
		private NumericUpDown numericPrecision;
		private Label label1;
	}
}