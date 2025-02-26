using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms.ComponentModel;
using System.Windows.Forms.Design;
using System.Runtime;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NiceCalc
{
    public class TextboxContextMenu : ContextMenuStrip
    {
        [Category("Menu")]
        public event EventHandler CutMenuClicked;
        [Category("Menu")]
        public event EventHandler CopyMenuClicked;
        [Category("Menu")]
        public event EventHandler PasteMenuClicked;
        [Category("Menu")]
        public event EventHandler SelectAllMenuClicked;
        [Category("Menu")]
        public event EventHandler UndoMenuClicked;
        [Category("Menu")]
        public event EventHandler RedoMenuClicked;
        [Category("Menu")]
        public event EventHandler ChangeFontMenuClicked;

        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripMenuItem selectAllToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem changeFontToolStripMenuItem;

        private static Size _menuItemSize = new Size(185, 24);
        private static Size _separatorSize = new Size(182, 6);

        public TextboxContextMenu(IContainer container)
            : base(container)
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.cutToolStripMenuItem = new ToolStripMenuItem("Cu&t", null, new EventHandler(OnCutMenuClicked)) { Size = _menuItemSize, Name = nameof(cutToolStripMenuItem) };
            this.copyToolStripMenuItem = new ToolStripMenuItem("&Copy", null, new EventHandler(OnCopyMenuClicked)) { Size = _menuItemSize, Name = nameof(copyToolStripMenuItem) };
            this.pasteToolStripMenuItem = new ToolStripMenuItem("&Paste", null, new EventHandler(OnPasteMenuClicked)) { Size = _menuItemSize, Name = nameof(pasteToolStripMenuItem) };
            this.selectAllToolStripMenuItem = new ToolStripMenuItem("&Select All", null, new EventHandler(OnSelectAllMenuClicked)) { Size = _menuItemSize, Name = nameof(selectAllToolStripMenuItem) };
            this.toolStripSeparator1 = new ToolStripSeparator() { Size = _separatorSize, Name = nameof(toolStripSeparator1) };
            this.undoToolStripMenuItem = new ToolStripMenuItem("&Undo", null, new EventHandler(OnUndoMenuClicked)) { Size = _menuItemSize, Name = nameof(undoToolStripMenuItem) };
            this.redoToolStripMenuItem = new ToolStripMenuItem("&Redo", null, new EventHandler(OnRedoMenuClicked)) { Size = _menuItemSize, Name = nameof(redoToolStripMenuItem) };
            this.toolStripSeparator2 = new ToolStripSeparator() { Size = _separatorSize, Name = nameof(toolStripSeparator2) };
            this.changeFontToolStripMenuItem = new ToolStripMenuItem("Change &Font...", null, new EventHandler(OnChangeFontMenuClicked)) { Size = _menuItemSize, Name = nameof(changeFontToolStripMenuItem) };
            this.SuspendLayout();

            this.Name = nameof(TextboxContextMenu);
            this.ShowImageMargin = false;
            this.Size = new System.Drawing.Size(186, 212);
            this.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Items.AddRange(new ToolStripItem[]
            {
                this.cutToolStripMenuItem,
                this.copyToolStripMenuItem,
                this.pasteToolStripMenuItem,
                this.selectAllToolStripMenuItem,
                this.toolStripSeparator1,
                this.undoToolStripMenuItem,
                this.redoToolStripMenuItem,
                this.toolStripSeparator2,
                this.changeFontToolStripMenuItem
            });
            this.ResumeLayout(false);
        }

        private void OnCutMenuClicked(object sender, EventArgs e)
        {
            CutMenuClicked?.Invoke(this, EventArgs.Empty);
        }

        private void OnCopyMenuClicked(object sender, EventArgs e)
        {
            CopyMenuClicked?.Invoke(this, EventArgs.Empty);
        }

        private void OnPasteMenuClicked(object sender, EventArgs e)
        {
            PasteMenuClicked?.Invoke(this, EventArgs.Empty);
        }

        private void OnSelectAllMenuClicked(object sender, EventArgs e)
        {
            SelectAllMenuClicked?.Invoke(this, EventArgs.Empty);
        }

        private void OnUndoMenuClicked(object sender, EventArgs e)
        {
            UndoMenuClicked?.Invoke(this, EventArgs.Empty);
        }

        private void OnRedoMenuClicked(object sender, EventArgs e)
        {
            RedoMenuClicked?.Invoke(this, EventArgs.Empty);
        }

        private void OnChangeFontMenuClicked(object sender, EventArgs e)
        {
            ChangeFontMenuClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
