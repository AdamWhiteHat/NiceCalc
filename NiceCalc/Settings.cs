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
	public class Settings
	{
		public bool CopyInputToOutput { get; set; }
		public bool CtrlEnterForTotal { get; set; }
		public int Precision { get; set; }
		public int WindowHeight { get; set; }
		public int WindowWidth { get; set; }

		protected string SettingsFilename { get; protected set; }

		public Settings(string settingsFilename)
		{
			SettingsFilename = Path.GetFullPath(settingsFilename);
			Load();
		}

		public virtual void Save()
		{
			string json = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
			File.WriteAllText(json, SettingsFilename);
		}

		protected virtual void Load()
		{
			string json = File.ReadAllText(SettingsFilename);
			Settings loaded = JsonConvert.DeserializeObject<Settings>(json);
			SetProperties(loaded);
		}

		protected virtual void SetProperties(Settings from)
		{
			this.CtrlEnterForTotal = from.CtrlEnterForTotal;
			this.CopyInputToOutput = from.CopyInputToOutput;
			this.Precision = from.Precision;
			this.WindowWidth = from.WindowWidth;
			this.WindowHeight = from.WindowHeight;
		}
	}
}
