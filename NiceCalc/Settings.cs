using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NiceCalc
{
	public class Settings : INotifyPropertyChanged
	{
		public static string DefaultFilename = "Settings.json";
		public event PropertyChangedEventHandler? PropertyChanged;

		public bool CopyInputToOutput
		{
			get { return _copyInputToOutput; }
			set
			{
				if (_copyInputToOutput != value)
				{
					_copyInputToOutput = value;
					OnPropertyChanged();
				}
			}
		}
		private bool _copyInputToOutput;

		public bool CtrlEnterForTotal
		{
			get { return _ctrlEnterForTotal; }
			set
			{
				if (_ctrlEnterForTotal != value)
				{
					_ctrlEnterForTotal = value;
					OnPropertyChanged();
				}
			}
		}
		private bool _ctrlEnterForTotal;

		public int Precision
		{
			get { return _precision; }
			set
			{
				if (_precision != value)
				{
					_precision = value;
					OnPropertyChanged();
				}
			}
		}
		private int _precision;

		public int RightPanelWidth
		{
			get { return _rightPanelWidth; }
			set
			{
				if (_rightPanelWidth != value)
				{
					_rightPanelWidth = value;
					OnPropertyChanged();
				}
			}
		}
		private int _rightPanelWidth;

		public int WindowWidth
		{
			get { return _windowWidth; }
			set
			{
				if (_windowWidth != value)
				{
					_windowWidth = value;
					OnPropertyChanged();
				}
			}
		}
		private int _windowWidth;

		public int WindowHeight
		{
			get { return _windowHeight; }
			set
			{
				if (_windowHeight != value)
				{
					_windowHeight = value;
					OnPropertyChanged();
				}
			}
		}
		private int _windowHeight;

		protected bool IsDirty { get; set; } = false;
		protected string SettingsFilename { get; set; }

		public Settings()
		{ }

		public virtual void Save()
		{
			if (IsDirty == true)
			{
				string json = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
				File.WriteAllText(SettingsFilename, json);
				IsDirty = false;
			}
		}

		public virtual void Load(string settingsFilename = null)
		{
			string filename = DefaultFilename;
			if (!string.IsNullOrWhiteSpace(settingsFilename))
			{
				filename = settingsFilename;
			}
			SettingsFilename = Path.GetFullPath(filename);

			string json = File.ReadAllText(SettingsFilename);
			Settings loaded = JsonConvert.DeserializeObject<Settings>(json);
			SetProperties(loaded);
			IsDirty = false;
		}

		protected virtual void SetProperties(Settings from)
		{
			this.CopyInputToOutput = from.CopyInputToOutput;
			this.CtrlEnterForTotal = from.CtrlEnterForTotal;
			this.Precision = from.Precision;
			this.WindowWidth = from.WindowWidth;
			this.WindowHeight = from.WindowHeight;
			this.RightPanelWidth = from.RightPanelWidth;
		}

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			this.IsDirty = true;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
