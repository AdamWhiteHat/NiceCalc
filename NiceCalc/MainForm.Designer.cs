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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer_TopBottom = new System.Windows.Forms.SplitContainer();
            this.tbInput = new NiceCalc.AutoCompleteTextBox();
            this._contextMenu = new NiceCalc.TextboxContextMenu(this.components);
            this.tbOutput = new System.Windows.Forms.RichTextBox();
            this.cbCopyInputToOutput = new System.Windows.Forms.CheckBox();
            this.splitContainer_LeftRight = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel_RightToolbar = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel_Precision = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.numericPrecision = new System.Windows.Forms.NumericUpDown();
            this.cbPreferFractionsResult = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel_Row0 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnEquals = new System.Windows.Forms.Button();
            this.btnSemicolon = new System.Windows.Forms.Button();
            this.btnFactorial = new System.Windows.Forms.Button();
            this.flowToolbarPanel_Row1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCeil = new System.Windows.Forms.Button();
            this.btnFloor = new System.Windows.Forms.Button();
            this.btnRound = new System.Windows.Forms.Button();
            this.btnAbs = new System.Windows.Forms.Button();
            this.flowToolbarPanel_Row2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnGcd = new System.Windows.Forms.Button();
            this.btnLcm = new System.Windows.Forms.Button();
            this.flowToolbarPanel_Row3 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSqrt = new System.Windows.Forms.Button();
            this.btnPow = new System.Windows.Forms.Button();
            this.btnLogn = new System.Windows.Forms.Button();
            this.flowToolbarPanel_Row4 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPi = new System.Windows.Forms.Button();
            this.btnE = new System.Windows.Forms.Button();
            this.btnLn = new System.Windows.Forms.Button();
            this.btnSin = new System.Windows.Forms.Button();
            this.btnCos = new System.Windows.Forms.Button();
            this.btnTan = new System.Windows.Forms.Button();
            this.flowToolbarPanel_Row5 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPreviousPrime = new System.Windows.Forms.Button();
            this.btnNextPrime = new System.Windows.Forms.Button();
            this.btnIsPrime = new System.Windows.Forms.Button();
            this.btnFactor = new System.Windows.Forms.Button();
            this.btnDivisors = new System.Windows.Forms.Button();
            this.cbCtrlEnterForTotal = new System.Windows.Forms.CheckBox();
            this.groupVariables = new System.Windows.Forms.GroupBox();
            this.listBoxVariables = new System.Windows.Forms.ListBox();
            this.cbExpandPanel = new System.Windows.Forms.CheckBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.fontSelectionDialog = new System.Windows.Forms.FontDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_TopBottom)).BeginInit();
            this.splitContainer_TopBottom.Panel1.SuspendLayout();
            this.splitContainer_TopBottom.Panel2.SuspendLayout();
            this.splitContainer_TopBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_LeftRight)).BeginInit();
            this.splitContainer_LeftRight.Panel1.SuspendLayout();
            this.splitContainer_LeftRight.Panel2.SuspendLayout();
            this.splitContainer_LeftRight.SuspendLayout();
            this.flowLayoutPanel_RightToolbar.SuspendLayout();
            this.flowLayoutPanel_Precision.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPrecision)).BeginInit();
            this.flowLayoutPanel_Row0.SuspendLayout();
            this.flowToolbarPanel_Row1.SuspendLayout();
            this.flowToolbarPanel_Row2.SuspendLayout();
            this.flowToolbarPanel_Row3.SuspendLayout();
            this.flowToolbarPanel_Row4.SuspendLayout();
            this.flowToolbarPanel_Row5.SuspendLayout();
            this.groupVariables.SuspendLayout();
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
            this.splitContainer_TopBottom.Panel1.Padding = new System.Windows.Forms.Padding(4);
            this.splitContainer_TopBottom.Panel1MinSize = 150;
            // 
            // splitContainer_TopBottom.Panel2
            // 
            this.splitContainer_TopBottom.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer_TopBottom.Panel2.Controls.Add(this.tbOutput);
            this.splitContainer_TopBottom.Panel2.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer_TopBottom.Panel2.Padding = new System.Windows.Forms.Padding(4);
            this.splitContainer_TopBottom.Panel2MinSize = 150;
            this.splitContainer_TopBottom.Size = new System.Drawing.Size(501, 532);
            this.splitContainer_TopBottom.SplitterDistance = 292;
            this.splitContainer_TopBottom.SplitterWidth = 10;
            this.splitContainer_TopBottom.TabIndex = 3;
            // 
            // tbInput
            // 
            this.tbInput.AcceptsTab = true;
            this.tbInput.AutoCompleteCustomSource = new System.Collections.Generic.List<string> {
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
        "pow",
        "previousprime",
        "round",
        "sign",
        "sin",
        "sqrt",
        "tan",
        "trunc"};
            this.tbInput.ContextMenuStrip = this._contextMenu;
            this.tbInput.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbInput.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbInput.Location = new System.Drawing.Point(4, 4);
            this.tbInput.Margin = new System.Windows.Forms.Padding(4);
            this.tbInput.Name = "tbInput";
            this.tbInput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.tbInput.Size = new System.Drawing.Size(493, 284);
            this.tbInput.TabIndex = 25;
            this.tbInput.Text = "";
            // 
            // _contextMenu
            // 
            this._contextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._contextMenu.Name = "TextboxContextMenu";
            this._contextMenu.ShowImageMargin = false;
            this._contextMenu.Size = new System.Drawing.Size(146, 184);
            this._contextMenu.CutMenuClicked += new System.EventHandler(this._contextMenu_CutMenuClicked);
            this._contextMenu.CopyMenuClicked += new System.EventHandler(this._contextMenu_CopyMenuClicked);
            this._contextMenu.PasteMenuClicked += new System.EventHandler(this._contextMenu_PasteMenuClicked);
            this._contextMenu.SelectAllMenuClicked += new System.EventHandler(this._contextMenu_SelectAllMenuClicked);
            this._contextMenu.UndoMenuClicked += new System.EventHandler(this._contextMenu_UndoMenuClicked);
            this._contextMenu.RedoMenuClicked += new System.EventHandler(this._contextMenu_RedoMenuClicked);
            this._contextMenu.ChangeFontMenuClicked += new System.EventHandler(this._contextMenu_ChangeFontMenuClicked);
            // 
            // tbOutput
            // 
            this.tbOutput.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbOutput.Font = new System.Drawing.Font("Consolas", 10.2F);
            this.tbOutput.Location = new System.Drawing.Point(4, 4);
            this.tbOutput.Margin = new System.Windows.Forms.Padding(4);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.tbOutput.Size = new System.Drawing.Size(493, 222);
            this.tbOutput.TabIndex = 26;
            this.tbOutput.Text = "";
            // 
            // cbCopyInputToOutput
            // 
            this.cbCopyInputToOutput.AutoSize = true;
            this.cbCopyInputToOutput.BackColor = System.Drawing.SystemColors.Control;
            this.cbCopyInputToOutput.Checked = true;
            this.cbCopyInputToOutput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCopyInputToOutput.Location = new System.Drawing.Point(5, 249);
            this.cbCopyInputToOutput.Margin = new System.Windows.Forms.Padding(4);
            this.cbCopyInputToOutput.Name = "cbCopyInputToOutput";
            this.cbCopyInputToOutput.Size = new System.Drawing.Size(162, 22);
            this.cbCopyInputToOutput.TabIndex = 24;
            this.cbCopyInputToOutput.Text = "Copy input to output";
            this.cbCopyInputToOutput.UseVisualStyleBackColor = false;
            // 
            // splitContainer_LeftRight
            // 
            this.splitContainer_LeftRight.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer_LeftRight.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer_LeftRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_LeftRight.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer_LeftRight.ForeColor = System.Drawing.SystemColors.ControlText;
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
            this.splitContainer_LeftRight.Panel2MinSize = 225;
            this.splitContainer_LeftRight.Size = new System.Drawing.Size(749, 532);
            this.splitContainer_LeftRight.SplitterDistance = 501;
            this.splitContainer_LeftRight.SplitterWidth = 10;
            this.splitContainer_LeftRight.TabIndex = 2;
            // 
            // flowLayoutPanel_RightToolbar
            // 
            this.flowLayoutPanel_RightToolbar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel_RightToolbar.Controls.Add(this.flowLayoutPanel_Precision);
            this.flowLayoutPanel_RightToolbar.Controls.Add(this.flowLayoutPanel_Row0);
            this.flowLayoutPanel_RightToolbar.Controls.Add(this.flowToolbarPanel_Row1);
            this.flowLayoutPanel_RightToolbar.Controls.Add(this.flowToolbarPanel_Row2);
            this.flowLayoutPanel_RightToolbar.Controls.Add(this.flowToolbarPanel_Row3);
            this.flowLayoutPanel_RightToolbar.Controls.Add(this.flowToolbarPanel_Row4);
            this.flowLayoutPanel_RightToolbar.Controls.Add(this.flowToolbarPanel_Row5);
            this.flowLayoutPanel_RightToolbar.Controls.Add(this.cbCtrlEnterForTotal);
            this.flowLayoutPanel_RightToolbar.Controls.Add(this.cbCopyInputToOutput);
            this.flowLayoutPanel_RightToolbar.Controls.Add(this.groupVariables);
            this.flowLayoutPanel_RightToolbar.Cursor = System.Windows.Forms.Cursors.Default;
            this.flowLayoutPanel_RightToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_RightToolbar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowLayoutPanel_RightToolbar.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel_RightToolbar.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel_RightToolbar.MinimumSize = new System.Drawing.Size(225, 2);
            this.flowLayoutPanel_RightToolbar.Name = "flowLayoutPanel_RightToolbar";
            this.flowLayoutPanel_RightToolbar.Padding = new System.Windows.Forms.Padding(1);
            this.flowLayoutPanel_RightToolbar.Size = new System.Drawing.Size(238, 532);
            this.flowLayoutPanel_RightToolbar.TabIndex = 1;
            // 
            // flowLayoutPanel_Precision
            // 
            this.flowLayoutPanel_Precision.AutoSize = true;
            this.flowLayoutPanel_Precision.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel_Precision.Controls.Add(this.label1);
            this.flowLayoutPanel_Precision.Controls.Add(this.numericPrecision);
            this.flowLayoutPanel_Precision.Controls.Add(this.cbPreferFractionsResult);
            this.flowLayoutPanel_Precision.Location = new System.Drawing.Point(1, 1);
            this.flowLayoutPanel_Precision.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel_Precision.Name = "flowLayoutPanel_Precision";
            this.flowLayoutPanel_Precision.Size = new System.Drawing.Size(210, 26);
            this.flowLayoutPanel_Precision.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Precision: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericPrecision
            // 
            this.numericPrecision.Location = new System.Drawing.Point(81, 1);
            this.numericPrecision.Margin = new System.Windows.Forms.Padding(1);
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
            this.numericPrecision.Size = new System.Drawing.Size(76, 24);
            this.numericPrecision.TabIndex = 0;
            this.numericPrecision.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericPrecision.ThousandsSeparator = true;
            this.numericPrecision.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // cbPreferFractionsResult
            // 
            this.cbPreferFractionsResult.AutoSize = true;
            this.cbPreferFractionsResult.BackColor = System.Drawing.SystemColors.Control;
            this.cbPreferFractionsResult.Location = new System.Drawing.Point(159, 1);
            this.cbPreferFractionsResult.Margin = new System.Windows.Forms.Padding(1);
            this.cbPreferFractionsResult.Name = "cbPreferFractionsResult";
            this.cbPreferFractionsResult.Size = new System.Drawing.Size(50, 22);
            this.cbPreferFractionsResult.TabIndex = 1;
            this.cbPreferFractionsResult.Text = "a/b";
            this.cbPreferFractionsResult.UseVisualStyleBackColor = false;
            // 
            // flowLayoutPanel_Row0
            // 
            this.flowLayoutPanel_Row0.AutoSize = true;
            this.flowLayoutPanel_Row0.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel_Row0.Controls.Add(this.btnEquals);
            this.flowLayoutPanel_Row0.Controls.Add(this.btnSemicolon);
            this.flowLayoutPanel_Row0.Controls.Add(this.btnFactorial);
            this.flowLayoutPanel_Row0.Location = new System.Drawing.Point(1, 27);
            this.flowLayoutPanel_Row0.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel_Row0.MinimumSize = new System.Drawing.Size(2, 32);
            this.flowLayoutPanel_Row0.Name = "flowLayoutPanel_Row0";
            this.flowLayoutPanel_Row0.Size = new System.Drawing.Size(76, 32);
            this.flowLayoutPanel_Row0.TabIndex = 28;
            // 
            // btnEquals
            // 
            this.btnEquals.AutoSize = true;
            this.btnEquals.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnEquals.Location = new System.Drawing.Point(1, 1);
            this.btnEquals.Margin = new System.Windows.Forms.Padding(1);
            this.btnEquals.Name = "btnEquals";
            this.btnEquals.Size = new System.Drawing.Size(27, 28);
            this.btnEquals.TabIndex = 2;
            this.btnEquals.Text = "=";
            this.btnEquals.UseVisualStyleBackColor = true;
            this.btnEquals.Click += new System.EventHandler(this.BtnFunctionSimple_Click);
            // 
            // btnSemicolon
            // 
            this.btnSemicolon.AutoSize = true;
            this.btnSemicolon.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSemicolon.Location = new System.Drawing.Point(30, 1);
            this.btnSemicolon.Margin = new System.Windows.Forms.Padding(1);
            this.btnSemicolon.Name = "btnSemicolon";
            this.btnSemicolon.Size = new System.Drawing.Size(22, 28);
            this.btnSemicolon.TabIndex = 3;
            this.btnSemicolon.Text = ";";
            this.btnSemicolon.UseVisualStyleBackColor = true;
            this.btnSemicolon.Click += new System.EventHandler(this.BtnFunctionSimple_Click);
            // 
            // btnFactorial
            // 
            this.btnFactorial.AutoSize = true;
            this.btnFactorial.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFactorial.Location = new System.Drawing.Point(54, 1);
            this.btnFactorial.Margin = new System.Windows.Forms.Padding(1);
            this.btnFactorial.Name = "btnFactorial";
            this.btnFactorial.Size = new System.Drawing.Size(21, 28);
            this.btnFactorial.TabIndex = 8;
            this.btnFactorial.Text = "!";
            this.btnFactorial.UseVisualStyleBackColor = true;
            this.btnFactorial.Click += new System.EventHandler(this.BtnFunctionSimple_Click);
            // 
            // flowToolbarPanel_Row1
            // 
            this.flowToolbarPanel_Row1.AutoSize = true;
            this.flowToolbarPanel_Row1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowToolbarPanel_Row1.Controls.Add(this.btnCeil);
            this.flowToolbarPanel_Row1.Controls.Add(this.btnFloor);
            this.flowToolbarPanel_Row1.Controls.Add(this.btnRound);
            this.flowToolbarPanel_Row1.Controls.Add(this.btnAbs);
            this.flowToolbarPanel_Row1.Cursor = System.Windows.Forms.Cursors.Default;
            this.flowToolbarPanel_Row1.Location = new System.Drawing.Point(1, 59);
            this.flowToolbarPanel_Row1.Margin = new System.Windows.Forms.Padding(0);
            this.flowToolbarPanel_Row1.MinimumSize = new System.Drawing.Size(2, 32);
            this.flowToolbarPanel_Row1.Name = "flowToolbarPanel_Row1";
            this.flowToolbarPanel_Row1.Size = new System.Drawing.Size(194, 32);
            this.flowToolbarPanel_Row1.TabIndex = 6;
            // 
            // btnCeil
            // 
            this.btnCeil.AutoSize = true;
            this.btnCeil.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCeil.Location = new System.Drawing.Point(1, 1);
            this.btnCeil.Margin = new System.Windows.Forms.Padding(1);
            this.btnCeil.Name = "btnCeil";
            this.btnCeil.Size = new System.Drawing.Size(40, 28);
            this.btnCeil.TabIndex = 4;
            this.btnCeil.Text = "ceil";
            this.btnCeil.UseVisualStyleBackColor = true;
            this.btnCeil.Click += new System.EventHandler(this.BtnFunctionParameterize_Click);
            // 
            // btnFloor
            // 
            this.btnFloor.AutoSize = true;
            this.btnFloor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFloor.Location = new System.Drawing.Point(43, 1);
            this.btnFloor.Margin = new System.Windows.Forms.Padding(1);
            this.btnFloor.Name = "btnFloor";
            this.btnFloor.Size = new System.Drawing.Size(48, 28);
            this.btnFloor.TabIndex = 5;
            this.btnFloor.Text = "floor";
            this.btnFloor.UseVisualStyleBackColor = true;
            this.btnFloor.Click += new System.EventHandler(this.BtnFunctionParameterize_Click);
            // 
            // btnRound
            // 
            this.btnRound.AutoSize = true;
            this.btnRound.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRound.Location = new System.Drawing.Point(93, 1);
            this.btnRound.Margin = new System.Windows.Forms.Padding(1);
            this.btnRound.Name = "btnRound";
            this.btnRound.Size = new System.Drawing.Size(56, 28);
            this.btnRound.TabIndex = 7;
            this.btnRound.Text = "round";
            this.btnRound.UseVisualStyleBackColor = true;
            this.btnRound.Click += new System.EventHandler(this.BtnFunctionParameterize_Click);
            // 
            // btnAbs
            // 
            this.btnAbs.AutoSize = true;
            this.btnAbs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAbs.Location = new System.Drawing.Point(151, 1);
            this.btnAbs.Margin = new System.Windows.Forms.Padding(1);
            this.btnAbs.Name = "btnAbs";
            this.btnAbs.Size = new System.Drawing.Size(42, 28);
            this.btnAbs.TabIndex = 6;
            this.btnAbs.Text = "abs";
            this.btnAbs.UseVisualStyleBackColor = true;
            this.btnAbs.Click += new System.EventHandler(this.BtnFunctionParameterize_Click);
            // 
            // flowToolbarPanel_Row2
            // 
            this.flowToolbarPanel_Row2.AutoSize = true;
            this.flowToolbarPanel_Row2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowToolbarPanel_Row2.Controls.Add(this.btnGcd);
            this.flowToolbarPanel_Row2.Controls.Add(this.btnLcm);
            this.flowToolbarPanel_Row2.Location = new System.Drawing.Point(1, 91);
            this.flowToolbarPanel_Row2.Margin = new System.Windows.Forms.Padding(0);
            this.flowToolbarPanel_Row2.MinimumSize = new System.Drawing.Size(2, 32);
            this.flowToolbarPanel_Row2.Name = "flowToolbarPanel_Row2";
            this.flowToolbarPanel_Row2.Size = new System.Drawing.Size(88, 32);
            this.flowToolbarPanel_Row2.TabIndex = 7;
            // 
            // btnGcd
            // 
            this.btnGcd.AutoSize = true;
            this.btnGcd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnGcd.Location = new System.Drawing.Point(1, 1);
            this.btnGcd.Margin = new System.Windows.Forms.Padding(1);
            this.btnGcd.Name = "btnGcd";
            this.btnGcd.Size = new System.Drawing.Size(42, 28);
            this.btnGcd.TabIndex = 9;
            this.btnGcd.Text = "gcd";
            this.btnGcd.UseVisualStyleBackColor = true;
            this.btnGcd.Click += new System.EventHandler(this.BtnFunctionParameterize_Click);
            // 
            // btnLcm
            // 
            this.btnLcm.AutoSize = true;
            this.btnLcm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLcm.Location = new System.Drawing.Point(45, 1);
            this.btnLcm.Margin = new System.Windows.Forms.Padding(1);
            this.btnLcm.Name = "btnLcm";
            this.btnLcm.Size = new System.Drawing.Size(42, 28);
            this.btnLcm.TabIndex = 10;
            this.btnLcm.Text = "lcm";
            this.btnLcm.UseVisualStyleBackColor = true;
            this.btnLcm.Click += new System.EventHandler(this.BtnFunctionParameterize_Click);
            // 
            // flowToolbarPanel_Row3
            // 
            this.flowToolbarPanel_Row3.AutoSize = true;
            this.flowToolbarPanel_Row3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowToolbarPanel_Row3.Controls.Add(this.btnSqrt);
            this.flowToolbarPanel_Row3.Controls.Add(this.btnPow);
            this.flowToolbarPanel_Row3.Controls.Add(this.btnLogn);
            this.flowToolbarPanel_Row3.Location = new System.Drawing.Point(89, 91);
            this.flowToolbarPanel_Row3.Margin = new System.Windows.Forms.Padding(0);
            this.flowToolbarPanel_Row3.MinimumSize = new System.Drawing.Size(2, 32);
            this.flowToolbarPanel_Row3.Name = "flowToolbarPanel_Row3";
            this.flowToolbarPanel_Row3.Size = new System.Drawing.Size(141, 32);
            this.flowToolbarPanel_Row3.TabIndex = 8;
            // 
            // btnSqrt
            // 
            this.btnSqrt.AutoSize = true;
            this.btnSqrt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSqrt.Location = new System.Drawing.Point(1, 1);
            this.btnSqrt.Margin = new System.Windows.Forms.Padding(1);
            this.btnSqrt.Name = "btnSqrt";
            this.btnSqrt.Size = new System.Drawing.Size(43, 28);
            this.btnSqrt.TabIndex = 11;
            this.btnSqrt.Text = "sqrt";
            this.btnSqrt.UseVisualStyleBackColor = true;
            this.btnSqrt.Click += new System.EventHandler(this.BtnFunctionParameterize_Click);
            // 
            // btnPow
            // 
            this.btnPow.AutoSize = true;
            this.btnPow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPow.Location = new System.Drawing.Point(46, 1);
            this.btnPow.Margin = new System.Windows.Forms.Padding(1);
            this.btnPow.Name = "btnPow";
            this.btnPow.Size = new System.Drawing.Size(46, 28);
            this.btnPow.TabIndex = 14;
            this.btnPow.Text = "pow";
            this.btnPow.UseVisualStyleBackColor = true;
            this.btnPow.Click += new System.EventHandler(this.BtnFunctionParameterize_Click);
            // 
            // btnLogn
            // 
            this.btnLogn.AutoSize = true;
            this.btnLogn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLogn.Location = new System.Drawing.Point(94, 1);
            this.btnLogn.Margin = new System.Windows.Forms.Padding(1);
            this.btnLogn.Name = "btnLogn";
            this.btnLogn.Size = new System.Drawing.Size(46, 28);
            this.btnLogn.TabIndex = 13;
            this.btnLogn.Text = "logn";
            this.btnLogn.UseVisualStyleBackColor = true;
            this.btnLogn.Click += new System.EventHandler(this.BtnFunctionParameterize_Click);
            // 
            // flowToolbarPanel_Row4
            // 
            this.flowToolbarPanel_Row4.AutoSize = true;
            this.flowToolbarPanel_Row4.Controls.Add(this.btnPi);
            this.flowToolbarPanel_Row4.Controls.Add(this.btnE);
            this.flowToolbarPanel_Row4.Controls.Add(this.btnLn);
            this.flowToolbarPanel_Row4.Controls.Add(this.btnSin);
            this.flowToolbarPanel_Row4.Controls.Add(this.btnCos);
            this.flowToolbarPanel_Row4.Controls.Add(this.btnTan);
            this.flowToolbarPanel_Row4.Location = new System.Drawing.Point(1, 123);
            this.flowToolbarPanel_Row4.Margin = new System.Windows.Forms.Padding(0);
            this.flowToolbarPanel_Row4.MinimumSize = new System.Drawing.Size(2, 32);
            this.flowToolbarPanel_Row4.Name = "flowToolbarPanel_Row4";
            this.flowToolbarPanel_Row4.Size = new System.Drawing.Size(214, 32);
            this.flowToolbarPanel_Row4.TabIndex = 9;
            // 
            // btnPi
            // 
            this.btnPi.AutoSize = true;
            this.btnPi.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPi.Location = new System.Drawing.Point(1, 1);
            this.btnPi.Margin = new System.Windows.Forms.Padding(1);
            this.btnPi.Name = "btnPi";
            this.btnPi.Size = new System.Drawing.Size(29, 28);
            this.btnPi.TabIndex = 14;
            this.btnPi.Text = "pi";
            this.btnPi.UseVisualStyleBackColor = true;
            this.btnPi.Click += new System.EventHandler(this.BtnFunctionSimple_Click);
            // 
            // btnE
            // 
            this.btnE.AutoSize = true;
            this.btnE.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnE.Location = new System.Drawing.Point(32, 1);
            this.btnE.Margin = new System.Windows.Forms.Padding(1);
            this.btnE.Name = "btnE";
            this.btnE.Size = new System.Drawing.Size(26, 28);
            this.btnE.TabIndex = 18;
            this.btnE.Text = "e";
            this.btnE.UseVisualStyleBackColor = true;
            this.btnE.Click += new System.EventHandler(this.BtnFunctionSimple_Click);
            // 
            // btnLn
            // 
            this.btnLn.AutoSize = true;
            this.btnLn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLn.Location = new System.Drawing.Point(60, 1);
            this.btnLn.Margin = new System.Windows.Forms.Padding(1);
            this.btnLn.Name = "btnLn";
            this.btnLn.Size = new System.Drawing.Size(29, 28);
            this.btnLn.TabIndex = 12;
            this.btnLn.Text = "ln";
            this.btnLn.UseVisualStyleBackColor = true;
            this.btnLn.Click += new System.EventHandler(this.BtnFunctionParameterize_Click);
            // 
            // btnSin
            // 
            this.btnSin.AutoSize = true;
            this.btnSin.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSin.Location = new System.Drawing.Point(91, 1);
            this.btnSin.Margin = new System.Windows.Forms.Padding(1);
            this.btnSin.Name = "btnSin";
            this.btnSin.Size = new System.Drawing.Size(37, 28);
            this.btnSin.TabIndex = 15;
            this.btnSin.Text = "sin";
            this.btnSin.UseVisualStyleBackColor = true;
            this.btnSin.Click += new System.EventHandler(this.BtnFunctionParameterize_Click);
            // 
            // btnCos
            // 
            this.btnCos.AutoSize = true;
            this.btnCos.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCos.Location = new System.Drawing.Point(130, 1);
            this.btnCos.Margin = new System.Windows.Forms.Padding(1);
            this.btnCos.Name = "btnCos";
            this.btnCos.Size = new System.Drawing.Size(43, 28);
            this.btnCos.TabIndex = 16;
            this.btnCos.Text = "cos";
            this.btnCos.UseVisualStyleBackColor = true;
            this.btnCos.Click += new System.EventHandler(this.BtnFunctionParameterize_Click);
            // 
            // btnTan
            // 
            this.btnTan.AutoSize = true;
            this.btnTan.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnTan.Location = new System.Drawing.Point(175, 1);
            this.btnTan.Margin = new System.Windows.Forms.Padding(1);
            this.btnTan.Name = "btnTan";
            this.btnTan.Size = new System.Drawing.Size(38, 28);
            this.btnTan.TabIndex = 17;
            this.btnTan.Text = "tan";
            this.btnTan.UseVisualStyleBackColor = true;
            this.btnTan.Click += new System.EventHandler(this.BtnFunctionParameterize_Click);
            // 
            // flowToolbarPanel_Row5
            // 
            this.flowToolbarPanel_Row5.AutoSize = true;
            this.flowToolbarPanel_Row5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowToolbarPanel_Row5.Controls.Add(this.btnPreviousPrime);
            this.flowToolbarPanel_Row5.Controls.Add(this.btnNextPrime);
            this.flowToolbarPanel_Row5.Controls.Add(this.btnIsPrime);
            this.flowToolbarPanel_Row5.Controls.Add(this.btnFactor);
            this.flowToolbarPanel_Row5.Controls.Add(this.btnDivisors);
            this.flowToolbarPanel_Row5.Location = new System.Drawing.Point(1, 155);
            this.flowToolbarPanel_Row5.Margin = new System.Windows.Forms.Padding(0);
            this.flowToolbarPanel_Row5.MinimumSize = new System.Drawing.Size(2, 32);
            this.flowToolbarPanel_Row5.Name = "flowToolbarPanel_Row5";
            this.flowToolbarPanel_Row5.Size = new System.Drawing.Size(197, 60);
            this.flowToolbarPanel_Row5.TabIndex = 10;
            // 
            // btnPreviousPrime
            // 
            this.btnPreviousPrime.AutoSize = true;
            this.btnPreviousPrime.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPreviousPrime.Location = new System.Drawing.Point(1, 1);
            this.btnPreviousPrime.Margin = new System.Windows.Forms.Padding(1);
            this.btnPreviousPrime.Name = "btnPreviousPrime";
            this.btnPreviousPrime.Size = new System.Drawing.Size(111, 28);
            this.btnPreviousPrime.TabIndex = 18;
            this.btnPreviousPrime.Text = "previousprime";
            this.btnPreviousPrime.UseVisualStyleBackColor = true;
            this.btnPreviousPrime.Click += new System.EventHandler(this.BtnFunctionParameterize_Click);
            // 
            // btnNextPrime
            // 
            this.btnNextPrime.AutoSize = true;
            this.btnNextPrime.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNextPrime.Location = new System.Drawing.Point(114, 1);
            this.btnNextPrime.Margin = new System.Windows.Forms.Padding(1);
            this.btnNextPrime.Name = "btnNextPrime";
            this.btnNextPrime.Size = new System.Drawing.Size(82, 28);
            this.btnNextPrime.TabIndex = 19;
            this.btnNextPrime.Text = "nextprime";
            this.btnNextPrime.UseVisualStyleBackColor = true;
            this.btnNextPrime.Click += new System.EventHandler(this.BtnFunctionParameterize_Click);
            // 
            // btnIsPrime
            // 
            this.btnIsPrime.AutoSize = true;
            this.btnIsPrime.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnIsPrime.Location = new System.Drawing.Point(1, 31);
            this.btnIsPrime.Margin = new System.Windows.Forms.Padding(1);
            this.btnIsPrime.Name = "btnIsPrime";
            this.btnIsPrime.Size = new System.Drawing.Size(66, 28);
            this.btnIsPrime.TabIndex = 22;
            this.btnIsPrime.Text = "isprime";
            this.btnIsPrime.UseVisualStyleBackColor = true;
            this.btnIsPrime.Click += new System.EventHandler(this.BtnFunctionParameterize_Click);
            // 
            // btnFactor
            // 
            this.btnFactor.AutoSize = true;
            this.btnFactor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFactor.Location = new System.Drawing.Point(69, 31);
            this.btnFactor.Margin = new System.Windows.Forms.Padding(1);
            this.btnFactor.Name = "btnFactor";
            this.btnFactor.Size = new System.Drawing.Size(56, 28);
            this.btnFactor.TabIndex = 20;
            this.btnFactor.Text = "factor";
            this.btnFactor.UseVisualStyleBackColor = true;
            this.btnFactor.Click += new System.EventHandler(this.BtnFunctionParameterize_Click);
            // 
            // btnDivisors
            // 
            this.btnDivisors.AutoSize = true;
            this.btnDivisors.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDivisors.Location = new System.Drawing.Point(127, 31);
            this.btnDivisors.Margin = new System.Windows.Forms.Padding(1);
            this.btnDivisors.Name = "btnDivisors";
            this.btnDivisors.Size = new System.Drawing.Size(69, 28);
            this.btnDivisors.TabIndex = 21;
            this.btnDivisors.Text = "divisors";
            this.btnDivisors.UseVisualStyleBackColor = true;
            this.btnDivisors.Click += new System.EventHandler(this.BtnFunctionParameterize_Click);
            // 
            // cbCtrlEnterForTotal
            // 
            this.cbCtrlEnterForTotal.AutoSize = true;
            this.cbCtrlEnterForTotal.BackColor = System.Drawing.SystemColors.Control;
            this.cbCtrlEnterForTotal.Checked = true;
            this.cbCtrlEnterForTotal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCtrlEnterForTotal.Location = new System.Drawing.Point(5, 219);
            this.cbCtrlEnterForTotal.Margin = new System.Windows.Forms.Padding(4);
            this.cbCtrlEnterForTotal.Name = "cbCtrlEnterForTotal";
            this.cbCtrlEnterForTotal.Size = new System.Drawing.Size(151, 22);
            this.cbCtrlEnterForTotal.TabIndex = 23;
            this.cbCtrlEnterForTotal.Text = "Ctrl+Enter for total";
            this.cbCtrlEnterForTotal.UseVisualStyleBackColor = false;
            // 
            // groupVariables
            // 
            this.groupVariables.BackColor = System.Drawing.Color.Gainsboro;
            this.groupVariables.Controls.Add(this.listBoxVariables);
            this.groupVariables.Controls.Add(this.cbExpandPanel);
            this.groupVariables.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupVariables.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupVariables.Location = new System.Drawing.Point(5, 279);
            this.groupVariables.Margin = new System.Windows.Forms.Padding(4);
            this.groupVariables.MaximumSize = new System.Drawing.Size(192, 231);
            this.groupVariables.MinimumSize = new System.Drawing.Size(192, 30);
            this.groupVariables.Name = "groupVariables";
            this.groupVariables.Padding = new System.Windows.Forms.Padding(0);
            this.groupVariables.Size = new System.Drawing.Size(192, 231);
            this.groupVariables.TabIndex = 26;
            this.groupVariables.TabStop = false;
            // 
            // listBoxVariables
            // 
            this.listBoxVariables.BackColor = System.Drawing.Color.Snow;
            this.listBoxVariables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxVariables.FormattingEnabled = true;
            this.listBoxVariables.IntegralHeight = false;
            this.listBoxVariables.ItemHeight = 18;
            this.listBoxVariables.Location = new System.Drawing.Point(0, 39);
            this.listBoxVariables.Margin = new System.Windows.Forms.Padding(0);
            this.listBoxVariables.Name = "listBoxVariables";
            this.listBoxVariables.ScrollAlwaysVisible = true;
            this.listBoxVariables.Size = new System.Drawing.Size(191, 188);
            this.listBoxVariables.TabIndex = 26;
            // 
            // cbExpandPanel
            // 
            this.cbExpandPanel.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbExpandPanel.BackColor = System.Drawing.SystemColors.Control;
            this.cbExpandPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cbExpandPanel.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbExpandPanel.Checked = true;
            this.cbExpandPanel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbExpandPanel.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cbExpandPanel.FlatAppearance.BorderSize = 0;
            this.cbExpandPanel.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.cbExpandPanel.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight;
            this.cbExpandPanel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cbExpandPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cbExpandPanel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbExpandPanel.ImageIndex = 1;
            this.cbExpandPanel.ImageList = this.imageList1;
            this.cbExpandPanel.Location = new System.Drawing.Point(0, 0);
            this.cbExpandPanel.Margin = new System.Windows.Forms.Padding(0);
            this.cbExpandPanel.MaximumSize = new System.Drawing.Size(253, 27);
            this.cbExpandPanel.Name = "cbExpandPanel";
            this.cbExpandPanel.Size = new System.Drawing.Size(192, 27);
            this.cbExpandPanel.TabIndex = 25;
            this.cbExpandPanel.Text = "Bound Variables";
            this.cbExpandPanel.UseVisualStyleBackColor = true;
            this.cbExpandPanel.CheckedChanged += new System.EventHandler(this.cbExpandPanel_CheckedChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "ExpandRightArrow");
            this.imageList1.Images.SetKeyName(1, "ExpandDownArrow");
            // 
            // fontSelectionDialog
            // 
            this.fontSelectionDialog.Font = new System.Drawing.Font("Consolas", 10.2F);
            this.fontSelectionDialog.FontMustExist = true;
            this.fontSelectionDialog.ShowEffects = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 532);
            this.Controls.Add(this.splitContainer_LeftRight);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(458, 363);
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
            this.flowLayoutPanel_Precision.ResumeLayout(false);
            this.flowLayoutPanel_Precision.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPrecision)).EndInit();
            this.flowLayoutPanel_Row0.ResumeLayout(false);
            this.flowLayoutPanel_Row0.PerformLayout();
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
            this.groupVariables.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.SplitContainer splitContainer_TopBottom;
		private System.Windows.Forms.CheckBox cbCopyInputToOutput;
		private System.Windows.Forms.SplitContainer splitContainer_LeftRight;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_RightToolbar;
		private System.Windows.Forms.CheckBox cbCtrlEnterForTotal;
		private System.Windows.Forms.NumericUpDown numericPrecision;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.FlowLayoutPanel flowToolbarPanel_Row1;
		private System.Windows.Forms.Button btnFactor;
		private System.Windows.Forms.Button btnDivisors;
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
        private NiceCalc.TextboxContextMenu _contextMenu;
        private CheckBox cbPreferFractionsResult;
		private AutoCompleteTextBox tbInput;
		private RichTextBox tbOutput;
		private Button btnEquals;
		private Button btnSemicolon;
		private Button btnPi;
		private ImageList imageList1;
		private CheckBox cbExpandPanel;
		private GroupBox groupVariables;
		private ListBox listBoxVariables;
		private Button btnE;
        private FlowLayoutPanel flowLayoutPanel_Precision;
        private FlowLayoutPanel flowLayoutPanel_Row0;
        private Button btnPow;
        private FontDialog fontSelectionDialog;
    }
}