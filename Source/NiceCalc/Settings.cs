using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ExtendedNumerics;
using Newtonsoft.Json;
using static System.Windows.Forms.LinkLabel;

namespace NiceCalc
{
    public class Settings : INotifyPropertyChanged
    {
        public static string DefaultFilename = "Settings.json";
        public static string ExceptionLogPath = Program.ExceptionLogPath;
        public event PropertyChangedEventHandler PropertyChanged;

        [JsonIgnore]
        public static TextBox OutputTextBox { get; set; }

        [JsonIgnore]
        public static Action<string> LogOutputFunction
        {
            get
            {
                return new Action<string>((message) =>
                {
                    if (!string.IsNullOrEmpty(ExceptionLogPath))
                    {
                        File.AppendAllText(ExceptionLogPath, Environment.NewLine + message);
                    }

                    if (OutputTextBox != null)
                    {
                        OutputTextBox.AppendText(Environment.NewLine + message);
                    }
                });
            }
        }

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

        public bool PreferFractionsResult
        {
            get { return _preferFractionsResult; }
            set
            {
                if (_preferFractionsResult != value)
                {
                    _preferFractionsResult = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _preferFractionsResult;

        [JsonProperty("BigDecimal.Precision")]
        public int BigDecimal_Precision
        {
            get { return _bigDecimal_Precision; }
            set
            {
                if (_bigDecimal_Precision != value)
                {
                    _bigDecimal_Precision = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _bigDecimal_Precision;

        [JsonProperty("BigDecimal.AlwaysNormalize")]
        public bool BigDecimal_AlwaysNormalize
        {
            get { return _bigDecimal_AlwaysNormalize; }
            set
            {
                if (_bigDecimal_AlwaysNormalize != value)
                {
                    _bigDecimal_AlwaysNormalize = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _bigDecimal_AlwaysNormalize;

        [JsonProperty("BigDecimal.AlwaysTruncate")]
        public bool BigDecimal_AlwaysTruncate
        {
            get { return _bigDecimal_AlwaysTruncate; }
            set
            {
                if (_bigDecimal_AlwaysTruncate != value)
                {
                    _bigDecimal_AlwaysTruncate = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _bigDecimal_AlwaysTruncate;

        public string FontName
        {
            get { return _fontName; }
            set
            {
                if (_fontName != value)
                {
                    _fontName = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _fontName;

        public float FontSize
        {
            get { return _fontSize; }
            set
            {
                if (_fontSize != value)
                {
                    _fontSize = value;
                    OnPropertyChanged();
                }
            }
        }
        private float _fontSize;

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

        public int WindowLocationX
        {
            get { return _windowLocationX; }
            set
            {
                if (_windowLocationX != value)
                {
                    _windowLocationX = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _windowLocationX;

        public int WindowLocationY
        {
            get { return _windowLocationY; }
            set
            {
                if (_windowLocationY != value)
                {
                    _windowLocationY = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _windowLocationY;

        protected bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                if (_isDirty != value)
                {
                    _isDirty = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _isDirty;

        protected string SettingsFilename { get; set; }

        public Settings()
        { }

        public virtual void Save()
        {
            if (IsDirty == true)
            {
                //JsonSerializerSettings settings = new JsonSerializerSettings();
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
        }

        protected virtual void SetProperties(Settings from)
        {
            this.CopyInputToOutput = from.CopyInputToOutput;
            this.CtrlEnterForTotal = from.CtrlEnterForTotal;
            this.BigDecimal_Precision = from.BigDecimal_Precision;
            this.BigDecimal_AlwaysNormalize = from.BigDecimal_AlwaysNormalize;
            this.BigDecimal_AlwaysTruncate = from.BigDecimal_AlwaysTruncate;
            this.FontName = from.FontName;
            this.FontSize = from.FontSize;
            this.RightPanelWidth = from.RightPanelWidth;
            this.WindowWidth = from.WindowWidth;
            this.WindowHeight = from.WindowHeight;
            this.WindowLocationX = from.WindowLocationX;
            this.WindowLocationY = from.WindowLocationY;

            IsDirty = false;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.IsDirty = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
