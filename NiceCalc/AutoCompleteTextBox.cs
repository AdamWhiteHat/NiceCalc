using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Windows.Forms;

namespace NiceCalc
{
    public class AutoCompleteTextBox : RichTextBox
    {
        public event EventHandler ExecuteExpression;
        public event EventHandler ClearOutput;

        public bool IsSuggestionBoxVisible { get { return _listBox.Visible; } }

        public string[] AutoCompleteCustomSource
        {
            get { return _autoCompleteCustomSource; }
            set { _autoCompleteCustomSource = value; }
        }
        private string[] _autoCompleteCustomSource;

        private bool _isAdded;
        private ListBox _listBox;
        private static readonly char[] Delimiters = new char[] { ' ', '\r', '\n', '\t', '(', ')' };

        public AutoCompleteTextBox()
            : base()
        {
            InitializeComponent();
            AcceptsTab = true;
            // this.AutoCompleteSource = 
            base.ShortcutsEnabled = true;
        }

        private void InitializeComponent()
        {
            _listBox = new ListBox();
            _listBox.Visible = false;
            _listBox.MouseClick += listBox_Click;

            ScrollBars = RichTextBoxScrollBars.Vertical;

            _isAdded = false;

            KeyDown += this_KeyDown;
        }

        protected void Raise_ExecuteExpression()
        {
            ExecuteExpression?.Invoke(this, EventArgs.Empty);
        }

        protected void Raise_ClearOutput()
        {
            ClearOutput?.Invoke(this, EventArgs.Empty);
        }

        string typedThusFar = string.Empty;

        private void this_KeyDown(object sender, KeyEventArgs e)
        {
            bool isHandled = false;

            switch (e.KeyCode)
            {
                case Keys.Escape:
                    if (IsSuggestionBoxVisible)
                    {
                        HideListBox();
                        isHandled = true;
                    }
                    else
                    {
                        Clear();
                        isHandled = true;
                    }
                    break;
                case Keys.Tab:
                    if (IsSuggestionBoxVisible)
                    {
                        e.SuppressKeyPress = true;
                        e.Handled = true;
                        InsertSuggestedWord((string)_listBox.SelectedItem);
                        isHandled = true;
                    }
                    break;

                case Keys.Enter:
                    if (IsSuggestionBoxVisible)
                    {
                        e.SuppressKeyPress = true;
                        e.Handled = true;
                        InsertSuggestedWord((string)_listBox.SelectedItem);
                        isHandled = true;
                    }
                    else if (e.Shift || (e.Control && MainForm.CurrentSettings.CtrlEnterForTotal) || (!e.Control && !MainForm.CurrentSettings.CtrlEnterForTotal))
                    {
                        e.SuppressKeyPress = true;
                        e.Handled = true;
                        Raise_ExecuteExpression();
                        isHandled = true;
                    }
                    break;

                case Keys.F5:
                    if (!IsSuggestionBoxVisible)
                    {
                        e.SuppressKeyPress = true;
                        e.Handled = true;
                        Raise_ExecuteExpression();
                        isHandled = true;
                    }
                    break;

                case Keys.Down:
                    ListBox_Next();
                    isHandled = true;
                    return;

                case Keys.Up:
                    ListBox_Previous();
                    isHandled = true;
                    return;

                case Keys.Left:
                case Keys.Right:
                    HideListBox();
                    isHandled = true;
                    return;

                case Keys.Z:
                    if (e.Control && !IsSuggestionBoxVisible)
                    {
                        if (CanUndo)
                        {
                            Undo();
                        }
                        isHandled = true;
                    }
                    break;

                case Keys.Y:
                    if (e.Control && !IsSuggestionBoxVisible)
                    {
                        if (CanRedo)
                        {
                            Redo();
                        }
                        isHandled = true;
                    }
                    break;

                case Keys.A:
                    if (e.Control && !IsSuggestionBoxVisible)
                    {
                        SelectAll();
                        isHandled = true;
                    }
                    break;
            }

            if (isHandled)
            {
                typedThusFar = string.Empty;
                return;
            }

            if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
            {
                string pressed = ((char)e.KeyValue).ToString().ToLower();
                typedThusFar += pressed;
                UpdateListBox(typedThusFar);
                return;
            }

            HideListBox();
            typedThusFar = string.Empty;
        }

        private void listBox_Click(object sender, MouseEventArgs e)
        {
            InsertSuggestedWord((string)_listBox.SelectedItem);
        }

        private void ListBox_Next()
        {
            _listBox.SelectedIndex = System.Math.Min(_listBox.SelectedIndex + 1, _listBox.Items.Count - 1);
        }

        private void ListBox_Previous()
        {
            _listBox.SelectedIndex = System.Math.Max(0, _listBox.SelectedIndex - 1);
        }

        internal bool EnableCustomAutoCompleteListbox()
        {
            return (Multiline
                    && AutoCompleteCustomSource != null
                    && AutoCompleteCustomSource.Length > 0);
        }

        private void HideListBox()
        {
            _listBox.Visible = false;
        }

        private void ShowListBox()
        {
            if (!EnableCustomAutoCompleteListbox())
            {
                return;
            }

            if (!_isAdded)
            {
                Parent.Controls.Add(_listBox);
                _listBox.Left = Left;
                _listBox.Top = Top + (Height / 2);
                _isAdded = true;
            }

            Point pos = GetPositionFromCharIndex(SelectionStart);
            _listBox.Left = Left + pos.X;
            _listBox.Top = Top + pos.Y + Font.Height + 1;

            /*
			Console.WriteLine($"Top: {Top}");
			Console.WriteLine($"Left: {Left}");
			Console.WriteLine($"");
			Console.WriteLine($"Pos.X: {pos.X}");
			Console.WriteLine($"Pos.Y: {pos.Y}");
			Console.WriteLine($"");
			Console.WriteLine($"listBox.Height: {_listBox.Height}");
			Console.WriteLine($"Font.Height: {Font.Height}");
			*/

            _listBox.Visible = true;
            _listBox.BringToFront();
            _listBox.Focus();
        }

        private void UpdateListBox(string incompleteWord)
        {
            if (!EnableCustomAutoCompleteListbox())
            {
                return;
            }

            if (incompleteWord.Length > 0)
            {
                string[] autoCompleteWords = AutoCompleteCustomSource.Cast<string>().ToArray();
                string[] matches = Array.FindAll(autoCompleteWords,
                                    x => (x.StartsWith(incompleteWord, StringComparison.OrdinalIgnoreCase)/* && !SelectedValues.Contains(x)*/));

                if (matches.Length > 0)
                {
                    ShowListBox();
                    _listBox.Items.Clear();
                    Array.ForEach(matches, x => _listBox.Items.Add(x));
                    _listBox.SelectedIndex = 0;
                    _listBox.Height = 0;
                    _listBox.Width = 0;
                    Focus();
                    using (Graphics graphics = _listBox.CreateGraphics())
                    {
                        for (int i = 0; i < _listBox.Items.Count; i++)
                        {
                            _listBox.Height += _listBox.GetItemHeight(i);

                            // if item width is larger than the current one
                            // set it to the new max item width
                            // GetItemRectangle does not work for me
                            // we add a little extra space by using '_'
                            int itemWidth = (int)graphics.MeasureString(((String)_listBox.Items[i]) + "_", _listBox.Font).Width;
                            _listBox.Width = System.Math.Max(_listBox.Width, itemWidth);
                        }
                    }
                }
                else
                {
                    HideListBox();
                }
            }
            else
            {
                HideListBox();
            }
        }

        private void InsertSuggestedWord(string newTag)
        {
            string text = Text;

            int pos = SelectionStart;
            int posStart = text.LastIndexOfAny(Delimiters, (pos < 1) ? 0 : pos - 1);
            posStart = (posStart == -1) ? 0 : posStart + 1;
            int posEnd = text.IndexOfAny(Delimiters, pos);

            string firstPart = text.Substring(0, posStart) + newTag;
            string updatedText = firstPart + ((posEnd == -1) ? string.Empty : text.Substring(posEnd, text.Length - posEnd));

            Text = updatedText;
            SelectionStart = firstPart.Length;

            HideListBox();

            pos = SelectionStart;
            this.Text = this.Text.Insert(pos, "()");
            SelectionStart = pos + 1;
        }

        private static string GetIncompleteWord(string text, int caretPosition)
        {
            int pos = caretPosition;
            int posStart = text.LastIndexOfAny(Delimiters, (pos < 1) ? 0 : pos - 1);
            posStart = (posStart == -1) ? 0 : posStart + 1;
            int posEnd = text.IndexOfAny(Delimiters, pos);
            posEnd = (posEnd == -1) ? text.Length : posEnd;
            int length = ((posEnd - posStart) < 0) ? 0 : posEnd - posStart;

            return text.Substring(posStart, length);
        }
    }
}
