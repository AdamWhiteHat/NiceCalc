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
		public bool IsSuggestionBoxVisible { get { return _listBox.Visible; } }

		public string[] AutoCompleteCustomSource
		{
			get { return _autoCompleteCustomSource; }
			set { _autoCompleteCustomSource = value; }
		}
		private string[] _autoCompleteCustomSource;

		private bool _isAdded;
		private ListBox _listBox;
		private string _formerValue;
		private static readonly char[] Delimiters = new char[] { ' ', '\r', '\n', '\t', '(', ')' };
		//public List<string> SelectedValues => new List<string>(Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

		public AutoCompleteTextBox()
			: base()
		{
			InitializeComponent();
			AcceptsTab = true;
		}

		private void InitializeComponent()
		{
			_listBox = new ListBox();
			_listBox.Visible = false;
			_listBox.MouseClick += listBox_Click;
			_listBox.KeyDown += listBox_KeyPress;

			ScrollBars = RichTextBoxScrollBars.Vertical;

			_isAdded = false;
			_formerValue = string.Empty;

			KeyDown += this_KeyDown;
			KeyUp += this_KeyUp;

		}

		private void this_KeyDown(object sender, KeyEventArgs e)
		{
			if (!EnableCustomAutoCompleteListbox())
			{
				return;
			}

			if (_listBox.Visible)
			{
				switch (e.KeyCode)
				{
					case Keys.Escape:
						{
							e.Handled = true;
							e.SuppressKeyPress = true;
							HideListBox();
							return;
						}
					case Keys.D9:
						{
							if (e.Shift)
							{
								InsertSuggestedWord((string)_listBox.SelectedItem);
								int pos = SelectionStart;
								this.Text = this.Text.Insert(pos, ")");
								SelectionStart = pos;
							}
							break;
						}
					case Keys.Tab:
					case Keys.Enter:
						{
							InsertSuggestedWord((string)_listBox.SelectedItem);
							int pos = SelectionStart;
							this.Text = this.Text.Insert(pos, "()");
							SelectionStart = pos + 1;

							e.SuppressKeyPress = true;
							e.Handled = true;
							break;
						}
					case Keys.Down:
						{
							_listBox.SelectedIndex = System.Math.Min(_listBox.SelectedIndex + 1, _listBox.Items.Count - 1);
							break;
						}
					case Keys.Up:
						{
							_listBox.SelectedIndex = System.Math.Max(0, _listBox.SelectedIndex - 1);
							break;
						}
				}
			}
		}

		private void this_KeyUp(object sender, KeyEventArgs e)
		{
			char pressedKey = (char)e.KeyValue;

			if (_listBox.Visible)
			{
				if (e.KeyCode == Keys.Escape)
				{
					e.Handled = true;
					e.SuppressKeyPress = true;
					HideListBox();
					return;
				}
			}
			else if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
			{
				UpdateListBox();
			}
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

		private void listBox_Click(object sender, MouseEventArgs e)
		{
			InsertSuggestedWord((string)_listBox.SelectedItem);
		}

		protected override bool IsInputKey(Keys keyData)
		{
			switch (keyData)
			{
				case Keys.Escape:
				case Keys.Tab:
					return true;
				default:
					return base.IsInputKey(keyData);
			}
		}

		protected override bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			if (keyData == Keys.Escape)
			{
				if (_listBox.Visible)
				{
					_listBox.Visible = false;
					return true;
				}
			}
			return base.ProcessCmdKey(ref m, keyData);
		}

		private void listBox_KeyPress(object sender, KeyEventArgs e)
		{
			if (!EnableCustomAutoCompleteListbox() || !_listBox.Visible)
			{
				return;
			}
			if (e.KeyCode == Keys.Escape)
			{
				e.Handled = true;
				e.SuppressKeyPress = true;
				HideListBox();
				return;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				InsertSuggestedWord((string)_listBox.SelectedItem);
			}
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
		}

		private void UpdateListBox()
		{
			if (!EnableCustomAutoCompleteListbox())
			{
				return;
			}

			if (Text == _formerValue)
			{
				return;
			}
			_formerValue = Text;

			string word = GetIncompleteWord(Text, SelectionStart);
			if (word.Length > 0)
			{
				string[] autoCompleteWords = AutoCompleteCustomSource.Cast<string>().ToArray();
				string[] matches = Array.FindAll(autoCompleteWords,
									x => (x.StartsWith(word, StringComparison.OrdinalIgnoreCase)/* && !SelectedValues.Contains(x)*/));

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
			_formerValue = Text;
		}


	}
}
