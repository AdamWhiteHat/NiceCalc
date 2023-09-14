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
			this.splitContainer_TopBottom = new System.Windows.Forms.SplitContainer();
			this.tbInput = new NiceCalc.AutoCompleteTextBox();
			this.cbCopyInputToOutput = new System.Windows.Forms.CheckBox();
			this.splitContainer_LeftRight = new System.Windows.Forms.SplitContainer();
			this.flowLayoutPanel_RightToolbar = new System.Windows.Forms.FlowLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.cbUseFractions = new System.Windows.Forms.CheckBox();
			this.numericPrecision = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.flowToolbarPanel_Row1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnFactor = new System.Windows.Forms.Button();
			this.btnDivisor = new System.Windows.Forms.Button();
			this.btnSqrt = new System.Windows.Forms.Button();
			this.btnFactorial = new System.Windows.Forms.Button();
			this.flowToolbarPanel_Row2 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnCeil = new System.Windows.Forms.Button();
			this.btnFloor = new System.Windows.Forms.Button();
			this.btnAbs = new System.Windows.Forms.Button();
			this.btnRound = new System.Windows.Forms.Button();
			this.flowToolbarPanel_Row3 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnLcm = new System.Windows.Forms.Button();
			this.btnGcd = new System.Windows.Forms.Button();
			this.btnLogn = new System.Windows.Forms.Button();
			this.btnLn = new System.Windows.Forms.Button();
			this.flowToolbarPanel_Row4 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnSin = new System.Windows.Forms.Button();
			this.btnCos = new System.Windows.Forms.Button();
			this.btnTan = new System.Windows.Forms.Button();
			this.btnIsPrime = new System.Windows.Forms.Button();
			this.flowToolbarPanel_Row5 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnPreviousPrime = new System.Windows.Forms.Button();
			this.btnNextPrime = new System.Windows.Forms.Button();
			this.cbCtrlEnterForTotal = new System.Windows.Forms.CheckBox();
			this.tbOutput = new System.Windows.Forms.RichTextBox();
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
			this.flowToolbarPanel_Row1.SuspendLayout();
			this.flowToolbarPanel_Row2.SuspendLayout();
			this.flowToolbarPanel_Row3.SuspendLayout();
			this.flowToolbarPanel_Row4.SuspendLayout();
			this.flowToolbarPanel_Row5.SuspendLayout();
			this.SuspendLayout();
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
			this.splitContainer_TopBottom.Panel1.Cursor = System.Windows.Forms.Cursors.Default;
			this.splitContainer_TopBottom.Panel1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.splitContainer_TopBottom.Panel1MinSize = 150;
			// 
			// splitContainer_TopBottom.Panel2
			// 
			this.splitContainer_TopBottom.Panel2.BackColor = System.Drawing.SystemColors.Control;
			this.splitContainer_TopBottom.Panel2.Controls.Add(this.tbOutput);
			this.splitContainer_TopBottom.Panel2.Cursor = System.Windows.Forms.Cursors.Default;
			this.splitContainer_TopBottom.Panel2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.splitContainer_TopBottom.Panel2MinSize = 150;
			this.splitContainer_TopBottom.Size = new System.Drawing.Size(591, 400);
			this.splitContainer_TopBottom.SplitterDistance = 221;
			this.splitContainer_TopBottom.SplitterWidth = 3;
			this.splitContainer_TopBottom.TabIndex = 3;
			// 
			// tbInput
			// 
			this.tbInput.AcceptsTab = true;
			this.tbInput.AutoCompleteCustomSource = null;
			this.tbInput.Cursor = System.Windows.Forms.Cursors.Default;
			this.tbInput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbInput.Location = new System.Drawing.Point(4, 3);
			this.tbInput.Name = "tbInput";
			this.tbInput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.tbInput.Size = new System.Drawing.Size(583, 215);
			this.tbInput.TabIndex = 1;
			this.tbInput.Text = "";
			// 
			// cbCopyInputToOutput
			// 
			this.cbCopyInputToOutput.AutoSize = true;
			this.cbCopyInputToOutput.BackColor = System.Drawing.SystemColors.Control;
			this.cbCopyInputToOutput.Checked = true;
			this.cbCopyInputToOutput.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbCopyInputToOutput.Location = new System.Drawing.Point(5, 228);
			this.cbCopyInputToOutput.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cbCopyInputToOutput.Name = "cbCopyInputToOutput";
			this.cbCopyInputToOutput.Size = new System.Drawing.Size(138, 19);
			this.cbCopyInputToOutput.TabIndex = 1;
			this.cbCopyInputToOutput.Text = "Copy input to output";
			this.cbCopyInputToOutput.UseVisualStyleBackColor = false;
			// 
			// splitContainer_LeftRight
			// 
			this.splitContainer_LeftRight.Cursor = System.Windows.Forms.Cursors.VSplit;
			this.splitContainer_LeftRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer_LeftRight.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer_LeftRight.Location = new System.Drawing.Point(0, 0);
			this.splitContainer_LeftRight.Margin = new System.Windows.Forms.Padding(0);
			this.splitContainer_LeftRight.Name = "splitContainer_LeftRight";
			// 
			// splitContainer_LeftRight.Panel1
			// 
			this.splitContainer_LeftRight.Panel1.Controls.Add(this.splitContainer_TopBottom);
			this.splitContainer_LeftRight.Panel1.Cursor = System.Windows.Forms.Cursors.Default;
			this.splitContainer_LeftRight.Panel1MinSize = 200;
			// 
			// splitContainer_LeftRight.Panel2
			// 
			this.splitContainer_LeftRight.Panel2.Controls.Add(this.flowLayoutPanel_RightToolbar);
			this.splitContainer_LeftRight.Panel2.Cursor = System.Windows.Forms.Cursors.Default;
			this.splitContainer_LeftRight.Panel2MinSize = 200;
			this.splitContainer_LeftRight.Size = new System.Drawing.Size(800, 400);
			this.splitContainer_LeftRight.SplitterDistance = 591;
			this.splitContainer_LeftRight.SplitterWidth = 5;
			this.splitContainer_LeftRight.TabIndex = 2;
			// 
			// flowLayoutPanel_RightToolbar
			// 
			this.flowLayoutPanel_RightToolbar.Controls.Add(this.panel1);
			this.flowLayoutPanel_RightToolbar.Controls.Add(this.flowToolbarPanel_Row1);
			this.flowLayoutPanel_RightToolbar.Controls.Add(this.flowToolbarPanel_Row2);
			this.flowLayoutPanel_RightToolbar.Controls.Add(this.flowToolbarPanel_Row3);
			this.flowLayoutPanel_RightToolbar.Controls.Add(this.flowToolbarPanel_Row4);
			this.flowLayoutPanel_RightToolbar.Controls.Add(this.flowToolbarPanel_Row5);
			this.flowLayoutPanel_RightToolbar.Controls.Add(this.cbCtrlEnterForTotal);
			this.flowLayoutPanel_RightToolbar.Controls.Add(this.cbCopyInputToOutput);
			this.flowLayoutPanel_RightToolbar.Cursor = System.Windows.Forms.Cursors.Default;
			this.flowLayoutPanel_RightToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel_RightToolbar.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel_RightToolbar.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel_RightToolbar.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel_RightToolbar.Name = "flowLayoutPanel_RightToolbar";
			this.flowLayoutPanel_RightToolbar.Padding = new System.Windows.Forms.Padding(1);
			this.flowLayoutPanel_RightToolbar.Size = new System.Drawing.Size(204, 400);
			this.flowLayoutPanel_RightToolbar.TabIndex = 1;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.cbUseFractions);
			this.panel1.Controls.Add(this.numericPrecision);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new System.Drawing.Point(1, 4);
			this.panel1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(1);
			this.panel1.Size = new System.Drawing.Size(198, 28);
			this.panel1.TabIndex = 5;
			// 
			// cbUseFractions
			// 
			this.cbUseFractions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbUseFractions.BackColor = System.Drawing.SystemColors.Control;
			this.cbUseFractions.Location = new System.Drawing.Point(152, 6);
			this.cbUseFractions.Name = "cbUseFractions";
			this.cbUseFractions.Size = new System.Drawing.Size(44, 19);
			this.cbUseFractions.TabIndex = 1;
			this.cbUseFractions.Text = "a/b";
			this.cbUseFractions.UseVisualStyleBackColor = false;
			// 
			// numericPrecision
			// 
			this.numericPrecision.Location = new System.Drawing.Point(62, 2);
			this.numericPrecision.Margin = new System.Windows.Forms.Padding(0);
			this.numericPrecision.Maximum = new decimal(new int[] {
            9999999,
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
			this.numericPrecision.TabIndex = 0;
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
			this.label1.Location = new System.Drawing.Point(3, 7);
			this.label1.Margin = new System.Windows.Forms.Padding(1);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Precision:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// flowToolbarPanel_Row1
			// 
			this.flowToolbarPanel_Row1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowToolbarPanel_Row1.Controls.Add(this.btnFactor);
			this.flowToolbarPanel_Row1.Controls.Add(this.btnDivisor);
			this.flowToolbarPanel_Row1.Controls.Add(this.btnSqrt);
			this.flowToolbarPanel_Row1.Controls.Add(this.btnFactorial);
			this.flowToolbarPanel_Row1.Cursor = System.Windows.Forms.Cursors.Default;
			this.flowToolbarPanel_Row1.Location = new System.Drawing.Point(1, 35);
			this.flowToolbarPanel_Row1.Margin = new System.Windows.Forms.Padding(0);
			this.flowToolbarPanel_Row1.Name = "flowToolbarPanel_Row1";
			this.flowToolbarPanel_Row1.Size = new System.Drawing.Size(198, 33);
			this.flowToolbarPanel_Row1.TabIndex = 6;
			// 
			// btnFactor
			// 
			this.btnFactor.AutoSize = true;
			this.btnFactor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnFactor.Location = new System.Drawing.Point(0, 3);
			this.btnFactor.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.btnFactor.Name = "btnFactor";
			this.btnFactor.Size = new System.Drawing.Size(48, 25);
			this.btnFactor.TabIndex = 1;
			this.btnFactor.Text = "factor";
			this.btnFactor.UseVisualStyleBackColor = true;
			this.btnFactor.Click += new System.EventHandler(this.BtnFunctionClick);
			// 
			// btnDivisor
			// 
			this.btnDivisor.AutoSize = true;
			this.btnDivisor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnDivisor.Location = new System.Drawing.Point(48, 3);
			this.btnDivisor.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.btnDivisor.Name = "btnDivisor";
			this.btnDivisor.Size = new System.Drawing.Size(52, 25);
			this.btnDivisor.TabIndex = 2;
			this.btnDivisor.Text = "divisor";
			this.btnDivisor.UseVisualStyleBackColor = true;
			this.btnDivisor.Click += new System.EventHandler(this.BtnFunctionClick);
			// 
			// btnSqrt
			// 
			this.btnSqrt.AutoSize = true;
			this.btnSqrt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnSqrt.Location = new System.Drawing.Point(100, 3);
			this.btnSqrt.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.btnSqrt.Name = "btnSqrt";
			this.btnSqrt.Size = new System.Drawing.Size(37, 25);
			this.btnSqrt.TabIndex = 3;
			this.btnSqrt.Text = "sqrt";
			this.btnSqrt.UseVisualStyleBackColor = true;
			this.btnSqrt.Click += new System.EventHandler(this.BtnFunctionClick);
			// 
			// btnFactorial
			// 
			this.btnFactorial.AutoSize = true;
			this.btnFactorial.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnFactorial.Location = new System.Drawing.Point(137, 3);
			this.btnFactorial.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.btnFactorial.Name = "btnFactorial";
			this.btnFactorial.Size = new System.Drawing.Size(20, 25);
			this.btnFactorial.TabIndex = 0;
			this.btnFactorial.Text = "!";
			this.btnFactorial.UseVisualStyleBackColor = true;
			this.btnFactorial.Click += new System.EventHandler(this.BtnFunctionClick);
			// 
			// flowToolbarPanel_Row2
			// 
			this.flowToolbarPanel_Row2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowToolbarPanel_Row2.Controls.Add(this.btnCeil);
			this.flowToolbarPanel_Row2.Controls.Add(this.btnFloor);
			this.flowToolbarPanel_Row2.Controls.Add(this.btnAbs);
			this.flowToolbarPanel_Row2.Controls.Add(this.btnRound);
			this.flowToolbarPanel_Row2.Location = new System.Drawing.Point(1, 68);
			this.flowToolbarPanel_Row2.Margin = new System.Windows.Forms.Padding(0);
			this.flowToolbarPanel_Row2.Name = "flowToolbarPanel_Row2";
			this.flowToolbarPanel_Row2.Size = new System.Drawing.Size(198, 33);
			this.flowToolbarPanel_Row2.TabIndex = 7;
			// 
			// btnCeil
			// 
			this.btnCeil.AutoSize = true;
			this.btnCeil.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnCeil.Location = new System.Drawing.Point(0, 3);
			this.btnCeil.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.btnCeil.Name = "btnCeil";
			this.btnCeil.Size = new System.Drawing.Size(35, 25);
			this.btnCeil.TabIndex = 0;
			this.btnCeil.Text = "ceil";
			this.btnCeil.UseVisualStyleBackColor = true;
			this.btnCeil.Click += new System.EventHandler(this.BtnFunctionClick);
			// 
			// btnFloor
			// 
			this.btnFloor.AutoSize = true;
			this.btnFloor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnFloor.Location = new System.Drawing.Point(35, 3);
			this.btnFloor.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.btnFloor.Name = "btnFloor";
			this.btnFloor.Size = new System.Drawing.Size(42, 25);
			this.btnFloor.TabIndex = 1;
			this.btnFloor.Text = "floor";
			this.btnFloor.UseVisualStyleBackColor = true;
			this.btnFloor.Click += new System.EventHandler(this.BtnFunctionClick);
			// 
			// btnAbs
			// 
			this.btnAbs.AutoSize = true;
			this.btnAbs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnAbs.Location = new System.Drawing.Point(77, 3);
			this.btnAbs.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.btnAbs.Name = "btnAbs";
			this.btnAbs.Size = new System.Drawing.Size(35, 25);
			this.btnAbs.TabIndex = 2;
			this.btnAbs.Text = "abs";
			this.btnAbs.UseVisualStyleBackColor = true;
			this.btnAbs.Click += new System.EventHandler(this.BtnFunctionClick);
			// 
			// btnRound
			// 
			this.btnRound.AutoSize = true;
			this.btnRound.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnRound.Location = new System.Drawing.Point(112, 3);
			this.btnRound.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.btnRound.Name = "btnRound";
			this.btnRound.Size = new System.Drawing.Size(49, 25);
			this.btnRound.TabIndex = 3;
			this.btnRound.Text = "round";
			this.btnRound.UseVisualStyleBackColor = true;
			this.btnRound.Click += new System.EventHandler(this.BtnFunctionClick);
			// 
			// flowToolbarPanel_Row3
			// 
			this.flowToolbarPanel_Row3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowToolbarPanel_Row3.Controls.Add(this.btnLcm);
			this.flowToolbarPanel_Row3.Controls.Add(this.btnGcd);
			this.flowToolbarPanel_Row3.Controls.Add(this.btnLogn);
			this.flowToolbarPanel_Row3.Controls.Add(this.btnLn);
			this.flowToolbarPanel_Row3.Location = new System.Drawing.Point(1, 101);
			this.flowToolbarPanel_Row3.Margin = new System.Windows.Forms.Padding(0);
			this.flowToolbarPanel_Row3.Name = "flowToolbarPanel_Row3";
			this.flowToolbarPanel_Row3.Size = new System.Drawing.Size(198, 33);
			this.flowToolbarPanel_Row3.TabIndex = 8;
			// 
			// btnLcm
			// 
			this.btnLcm.AutoSize = true;
			this.btnLcm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnLcm.Location = new System.Drawing.Point(0, 3);
			this.btnLcm.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.btnLcm.Name = "btnLcm";
			this.btnLcm.Size = new System.Drawing.Size(37, 25);
			this.btnLcm.TabIndex = 0;
			this.btnLcm.Text = "lcm";
			this.btnLcm.UseVisualStyleBackColor = true;
			this.btnLcm.Click += new System.EventHandler(this.BtnFunctionClick);
			// 
			// btnGcd
			// 
			this.btnGcd.AutoSize = true;
			this.btnGcd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnGcd.Location = new System.Drawing.Point(37, 3);
			this.btnGcd.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.btnGcd.Name = "btnGcd";
			this.btnGcd.Size = new System.Drawing.Size(37, 25);
			this.btnGcd.TabIndex = 1;
			this.btnGcd.Text = "gcd";
			this.btnGcd.UseVisualStyleBackColor = true;
			this.btnGcd.Click += new System.EventHandler(this.BtnFunctionClick);
			// 
			// btnLogn
			// 
			this.btnLogn.AutoSize = true;
			this.btnLogn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnLogn.Location = new System.Drawing.Point(74, 3);
			this.btnLogn.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.btnLogn.Name = "btnLogn";
			this.btnLogn.Size = new System.Drawing.Size(41, 25);
			this.btnLogn.TabIndex = 2;
			this.btnLogn.Text = "logn";
			this.btnLogn.UseVisualStyleBackColor = true;
			this.btnLogn.Click += new System.EventHandler(this.BtnFunctionClick);
			// 
			// btnLn
			// 
			this.btnLn.AutoSize = true;
			this.btnLn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnLn.Location = new System.Drawing.Point(115, 3);
			this.btnLn.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.btnLn.Name = "btnLn";
			this.btnLn.Size = new System.Drawing.Size(27, 25);
			this.btnLn.TabIndex = 3;
			this.btnLn.Text = "ln";
			this.btnLn.UseVisualStyleBackColor = true;
			this.btnLn.Click += new System.EventHandler(this.BtnFunctionClick);
			// 
			// flowToolbarPanel_Row4
			// 
			this.flowToolbarPanel_Row4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowToolbarPanel_Row4.Controls.Add(this.btnSin);
			this.flowToolbarPanel_Row4.Controls.Add(this.btnCos);
			this.flowToolbarPanel_Row4.Controls.Add(this.btnTan);
			this.flowToolbarPanel_Row4.Controls.Add(this.btnIsPrime);
			this.flowToolbarPanel_Row4.Location = new System.Drawing.Point(1, 134);
			this.flowToolbarPanel_Row4.Margin = new System.Windows.Forms.Padding(0);
			this.flowToolbarPanel_Row4.Name = "flowToolbarPanel_Row4";
			this.flowToolbarPanel_Row4.Size = new System.Drawing.Size(198, 33);
			this.flowToolbarPanel_Row4.TabIndex = 9;
			// 
			// btnSin
			// 
			this.btnSin.AutoSize = true;
			this.btnSin.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnSin.Location = new System.Drawing.Point(0, 3);
			this.btnSin.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.btnSin.Name = "btnSin";
			this.btnSin.Size = new System.Drawing.Size(32, 25);
			this.btnSin.TabIndex = 0;
			this.btnSin.Text = "sin";
			this.btnSin.UseVisualStyleBackColor = true;
			this.btnSin.Click += new System.EventHandler(this.BtnFunctionClick);
			// 
			// btnCos
			// 
			this.btnCos.AutoSize = true;
			this.btnCos.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnCos.Location = new System.Drawing.Point(32, 3);
			this.btnCos.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.btnCos.Name = "btnCos";
			this.btnCos.Size = new System.Drawing.Size(35, 25);
			this.btnCos.TabIndex = 1;
			this.btnCos.Text = "cos";
			this.btnCos.UseVisualStyleBackColor = true;
			this.btnCos.Click += new System.EventHandler(this.BtnFunctionClick);
			// 
			// btnTan
			// 
			this.btnTan.AutoSize = true;
			this.btnTan.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnTan.Location = new System.Drawing.Point(67, 3);
			this.btnTan.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.btnTan.Name = "btnTan";
			this.btnTan.Size = new System.Drawing.Size(34, 25);
			this.btnTan.TabIndex = 2;
			this.btnTan.Text = "tan";
			this.btnTan.UseVisualStyleBackColor = true;
			this.btnTan.Click += new System.EventHandler(this.BtnFunctionClick);
			// 
			// btnIsPrime
			// 
			this.btnIsPrime.AutoSize = true;
			this.btnIsPrime.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnIsPrime.Location = new System.Drawing.Point(101, 3);
			this.btnIsPrime.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.btnIsPrime.Name = "btnIsPrime";
			this.btnIsPrime.Size = new System.Drawing.Size(56, 25);
			this.btnIsPrime.TabIndex = 3;
			this.btnIsPrime.Text = "isprime";
			this.btnIsPrime.UseVisualStyleBackColor = true;
			this.btnIsPrime.Click += new System.EventHandler(this.BtnFunctionClick);
			// 
			// flowToolbarPanel_Row5
			// 
			this.flowToolbarPanel_Row5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowToolbarPanel_Row5.Controls.Add(this.btnPreviousPrime);
			this.flowToolbarPanel_Row5.Controls.Add(this.btnNextPrime);
			this.flowToolbarPanel_Row5.Location = new System.Drawing.Point(1, 167);
			this.flowToolbarPanel_Row5.Margin = new System.Windows.Forms.Padding(0);
			this.flowToolbarPanel_Row5.Name = "flowToolbarPanel_Row5";
			this.flowToolbarPanel_Row5.Size = new System.Drawing.Size(198, 33);
			this.flowToolbarPanel_Row5.TabIndex = 10;
			// 
			// btnPreviousPrime
			// 
			this.btnPreviousPrime.AutoSize = true;
			this.btnPreviousPrime.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnPreviousPrime.Location = new System.Drawing.Point(0, 3);
			this.btnPreviousPrime.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.btnPreviousPrime.Name = "btnPreviousPrime";
			this.btnPreviousPrime.Size = new System.Drawing.Size(93, 25);
			this.btnPreviousPrime.TabIndex = 0;
			this.btnPreviousPrime.Text = "previousprime";
			this.btnPreviousPrime.UseVisualStyleBackColor = true;
			this.btnPreviousPrime.Click += new System.EventHandler(this.BtnFunctionClick);
			// 
			// btnNextPrime
			// 
			this.btnNextPrime.AutoSize = true;
			this.btnNextPrime.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnNextPrime.Location = new System.Drawing.Point(93, 3);
			this.btnNextPrime.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.btnNextPrime.Name = "btnNextPrime";
			this.btnNextPrime.Size = new System.Drawing.Size(70, 25);
			this.btnNextPrime.TabIndex = 1;
			this.btnNextPrime.Text = "nextprime";
			this.btnNextPrime.UseVisualStyleBackColor = true;
			this.btnNextPrime.Click += new System.EventHandler(this.BtnFunctionClick);
			// 
			// cbCtrlEnterForTotal
			// 
			this.cbCtrlEnterForTotal.AutoSize = true;
			this.cbCtrlEnterForTotal.BackColor = System.Drawing.SystemColors.Control;
			this.cbCtrlEnterForTotal.Checked = true;
			this.cbCtrlEnterForTotal.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbCtrlEnterForTotal.Location = new System.Drawing.Point(5, 203);
			this.cbCtrlEnterForTotal.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cbCtrlEnterForTotal.Name = "cbCtrlEnterForTotal";
			this.cbCtrlEnterForTotal.Size = new System.Drawing.Size(125, 19);
			this.cbCtrlEnterForTotal.TabIndex = 0;
			this.cbCtrlEnterForTotal.Text = "Ctrl+Enter for total";
			this.cbCtrlEnterForTotal.UseVisualStyleBackColor = false;
			// 
			// tbOutput
			// 
			this.tbOutput.Cursor = System.Windows.Forms.Cursors.Default;
			this.tbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbOutput.Location = new System.Drawing.Point(4, 3);
			this.tbOutput.Name = "tbOutput";
			this.tbOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.tbOutput.Size = new System.Drawing.Size(583, 170);
			this.tbOutput.TabIndex = 1;
			this.tbOutput.Text = "";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 400);
			this.Controls.Add(this.splitContainer_LeftRight);
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MinimumSize = new System.Drawing.Size(403, 345);
			this.Name = "MainForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "NiceCalc";
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.splitContainer_TopBottom.Panel1.ResumeLayout(false);
			this.splitContainer_TopBottom.Panel2.ResumeLayout(false);
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
			this.flowToolbarPanel_Row1.ResumeLayout(false);
			this.flowToolbarPanel_Row1.PerformLayout();
			this.flowToolbarPanel_Row2.ResumeLayout(false);
			this.flowToolbarPanel_Row2.PerformLayout();
			this.flowToolbarPanel_Row3.ResumeLayout(false);
			this.flowToolbarPanel_Row3.PerformLayout();
			this.flowToolbarPanel_Row4.ResumeLayout(false);
			this.flowToolbarPanel_Row4.PerformLayout();
			this.flowToolbarPanel_Row5.ResumeLayout(false);
			this.flowToolbarPanel_Row5.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.SplitContainer splitContainer_TopBottom;
		private System.Windows.Forms.CheckBox cbCopyInputToOutput;
		private System.Windows.Forms.SplitContainer splitContainer_LeftRight;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_RightToolbar;
		private System.Windows.Forms.CheckBox cbCtrlEnterForTotal;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.NumericUpDown numericPrecision;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.FlowLayoutPanel flowToolbarPanel_Row1;
		private System.Windows.Forms.Button btnFactor;
		private System.Windows.Forms.Button btnDivisor;
		private System.Windows.Forms.Button btnSqrt;
		private System.Windows.Forms.FlowLayoutPanel flowToolbarPanel_Row2;
		private System.Windows.Forms.Button btnCeil;
		private System.Windows.Forms.Button btnFloor;
		private System.Windows.Forms.Button btnAbs;
		private System.Windows.Forms.Button btnRound;
		private System.Windows.Forms.FlowLayoutPanel flowToolbarPanel_Row3;
		private System.Windows.Forms.Button btnGcd;
		private System.Windows.Forms.Button btnLcm;
		private System.Windows.Forms.Button btnLogn;
		private System.Windows.Forms.Button btnLn;
		private System.Windows.Forms.Button btnFactorial;
		private System.Windows.Forms.FlowLayoutPanel flowToolbarPanel_Row4;
		private System.Windows.Forms.Button btnIsPrime;
		private System.Windows.Forms.Button btnSin;
		private System.Windows.Forms.Button btnCos;
		private System.Windows.Forms.Button btnTan;
		private System.Windows.Forms.FlowLayoutPanel flowToolbarPanel_Row5;
		private System.Windows.Forms.Button btnPreviousPrime;
		private System.Windows.Forms.Button btnNextPrime;
		private CheckBox cbUseFractions;
		private AutoCompleteTextBox tbInput;
		private RichTextBox tbOutput;
	}
}