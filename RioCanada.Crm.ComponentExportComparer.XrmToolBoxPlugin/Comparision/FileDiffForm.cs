namespace RioCanada.Crm.ComponentExportComparer.Diff.Net
{
    #region Using Directives

    using Menees;
    using Menees.Diffs;
    using Menees.Windows.Forms;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    #endregion

    sealed partial class FileDiffForm : ExtendedForm
    {
        #region Private Data Members

        private ShowDiffArgs currentDiffArgs;

        #endregion

        #region Constructors

        public FileDiffForm()
        {
            this.InitializeComponent();

            Options.OptionsChanged += this.OptionsChanged;

            XrmToolBoxPlugin.Comparision.HotKeyManager.AddHotKey(this, () => this.Close(), Keys.Escape);
            XrmToolBoxPlugin.Comparision.HotKeyManager.AddHotKey(this, () => this.DiffCtrl.Find(), Keys.F, ctrl: true);
            XrmToolBoxPlugin.Comparision.HotKeyManager.AddHotKey(this, () => this.DiffCtrl.FindNext(), Keys.F3);
            XrmToolBoxPlugin.Comparision.HotKeyManager.AddHotKey(this, () => this.DiffCtrl.FindPrevious(), Keys.F3, shift: true);
        }

        #endregion

        #region Public Properties

        public string ToolTipText
        {
            get
            {
                string result = null;

                if (this.currentDiffArgs != null)
                {
                    result = this.currentDiffArgs.A + Environment.NewLine + this.currentDiffArgs.B;
                }

                return result;
            }
        }

        #endregion

        #region Public Methods

        public void ShowDifferences(ShowDiffArgs e)
        {
            string textA = e.A;
            string textB = e.B;
            DiffType diffType = e.DiffType;

            IList<string> a, b;
            int leadingCharactersToIgnore = 0;
            bool fileNames = diffType == DiffType.File;
            if (fileNames)
            {
                GetFileLines(textA, textB, out a, out b, out leadingCharactersToIgnore);
            }
            else
            {
                GetTextLines(textA, textB, out a, out b);
            }

            bool isBinaryCompare = leadingCharactersToIgnore > 0;
            bool ignoreCase = isBinaryCompare ? false : Options.IgnoreCase;
            bool ignoreTextWhitespace = isBinaryCompare ? false : Options.IgnoreTextWhitespace;
            TextDiff diff = new TextDiff(Options.HashType, ignoreCase, ignoreTextWhitespace, leadingCharactersToIgnore, !Options.ShowChangeAsDeleteInsert);
            EditScript script = diff.Execute(a, b);

            string captionA = string.Empty;
            string captionB = string.Empty;
            if (fileNames)
            {
                captionA = textA;
                captionB = textB;
                this.Text = string.Format("{0} : {1}", Path.GetFileName(textA), Path.GetFileName(textB));
            }
            else
            {
                this.Text = "Text Comparison";
            }

            // Apply options first since SetData needs to know things
            // like SpacesPerTab and ShowWhitespace up front, so it
            // can build display lines, determine scroll bounds, etc.
            this.ApplyOptions();

            this.DiffCtrl.SetData(a, b, script, captionA, captionB, ignoreCase, ignoreTextWhitespace, isBinaryCompare);

            if (Options.LineDiffHeight != 0)
            {
                this.DiffCtrl.LineDiffHeight = Options.LineDiffHeight;
            }

            // this.Show();

            this.currentDiffArgs = e;
        }

        #endregion

        #region Private Methods

        private static void GetFileLines(string fileNameA, string fileNameB, out IList<string> a, out IList<string> b, out int leadingCharactersToIgnore)
        {
            a = null;
            b = null;
            leadingCharactersToIgnore = 0;
            CompareType compareType = Options.CompareType;
            bool isAuto = compareType == CompareType.Auto;

            if (compareType == CompareType.Binary ||
                (isAuto && (DiffUtility.IsBinaryFile(fileNameA) || DiffUtility.IsBinaryFile(fileNameB))))
            {
                using (FileStream fileA = File.OpenRead(fileNameA))
                using (FileStream fileB = File.OpenRead(fileNameB))
                {
                    BinaryDiff diff = new BinaryDiff
                    {
                        FootprintLength = Options.BinaryFootprintLength,
                    };
                    AddCopyCollection addCopy = diff.Execute(fileA, fileB);

                    BinaryDiffLines lines = new BinaryDiffLines(fileA, addCopy, Options.BinaryFootprintLength);
                    a = lines.BaseLines;
                    b = lines.VersionLines;
                    leadingCharactersToIgnore = BinaryDiffLines.PrefixLength;
                }
            }

            if (compareType == CompareType.Xml || (isAuto && (a == null || b == null)))
            {
                a = TryGetXmlLines(DiffUtility.GetXmlTextLines, fileNameA, fileNameA, !isAuto);

                // If A failed to parse with Auto, then there's no reason to try B.
                if (a != null)
                {
                    b = TryGetXmlLines(DiffUtility.GetXmlTextLines, fileNameB, fileNameB, !isAuto);
                }

                // If we get here and the compare type was XML, then both
                // inputs parsed correctly, and both lists should be non-null.
                // If we get here and the compare type was Auto, then one
                // or both lists may be null, so we'll fallthrough to the text
                // handling logic.
            }

            if (a == null || b == null)
            {
                a = DiffUtility.GetFileTextLines(fileNameA);
                b = DiffUtility.GetFileTextLines(fileNameB);
            }
        }

        private static void GetTextLines(string textA, string textB, out IList<string> a, out IList<string> b)
        {
            a = null;
            b = null;
            CompareType compareType = Options.CompareType;
            bool isAuto = compareType == CompareType.Auto;

            if (compareType == CompareType.Xml || isAuto)
            {
                a = TryGetXmlLines(DiffUtility.GetXmlTextLinesFromXml, "the left side text", textA, !isAuto);

                // If A failed to parse with Auto, then there's no reason to try B.
                if (a != null)
                {
                    b = TryGetXmlLines(DiffUtility.GetXmlTextLinesFromXml, "the right side text", textB, !isAuto);
                }

                // If we get here and the compare type was XML, then both
                // inputs parsed correctly, and both lists should be non-null.
                // If we get here and the compare type was Auto, then one
                // or both lists may be null, so we'll fallthrough to the text
                // handling logic.
            }

            if (a == null || b == null)
            {
                a = DiffUtility.GetStringTextLines(textA);
                b = DiffUtility.GetStringTextLines(textB);
            }
        }

        private static IList<string> TryGetXmlLines(
            Func<string, bool, IList<string>> converter,
            string name,
            string input,
            bool throwOnError)
        {
            IList<string> result = null;
            try
            {
                result = converter(input, Options.IgnoreXmlWhitespace);
            }
            catch (XmlException ex)
            {
                if (throwOnError)
                {
                    StringBuilder sb = new StringBuilder("An XML comparison was attempted, but an XML exception occurred while parsing ");
                    sb.Append(name).AppendLine(".").AppendLine();
                    sb.AppendLine("Exception Message:").Append(ex.Message);
                    throw new XmlException(sb.ToString(), ex);
                }
            }

            return result;
        }

        private void ApplyOptions()
        {
            this.DiffCtrl.ShowWhiteSpaceInMainDiff = Options.ShowWSInMainDiff;
            this.DiffCtrl.ShowWhiteSpaceInLineDiff = Options.ShowWSInLineDiff;
            this.DiffCtrl.ViewFont = Options.ViewFont;
        }

        private void DiffCtrl_LineDiffSizeChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                Options.LineDiffHeight = this.DiffCtrl.LineDiffHeight;
            }
        }

        private void DiffCtrl_RecompareNeeded(object sender, EventArgs e)
        {
            if (this.currentDiffArgs != null)
            {
                using (WaitCursor wc = new WaitCursor(this))
                {
                    this.ShowDifferences(this.currentDiffArgs);
                    this.GoToFirstDiff();
                }
            }
        }

        private void FileDiffForm_Closed(object sender, EventArgs e)
        {
            Options.OptionsChanged -= this.OptionsChanged;
        }

        private void FileDiffForm_Load(object sender, EventArgs e)
        {
            // http://stackoverflow.com/questions/888865/problem-with-icon-on-creating-new-maximized-mdi-child-form-in-net
            this.Icon = (Icon)this.Icon.Clone();
        }

        private void FileDiffForm_Shown(object sender, EventArgs e)
        {
            this.GoToFirstDiff();
        }

        private void GoToFirstDiff()
        {
            if (Options.GoToFirstDiff)
            {
                this.DiffCtrl.GoToFirstDiff();
            }
        }

        private void OptionsChanged(object sender, EventArgs e)
        {
            this.ApplyOptions();
        }

        #endregion
    }

    sealed class ShowDiffArgs
    {
        #region Constructors

        public ShowDiffArgs(string itemA, string itemB, DiffType diffType)
        {
            this.A = itemA;
            this.B = itemB;
            this.DiffType = diffType;
        }

        #endregion

        #region Public Properties

        public string A { get; }

        public string B { get; }

        public DiffType DiffType { get; }

        #endregion
    }

    enum DiffType
    {
        File,
        Directory,
        Text,
    }

    enum CompareType
    {
        Auto,
        Text,
        Xml,
        Binary,
    }

    static class Options
    {
        #region Private Data Members

        private const float DefaultFontSize = 9.75F;

        private static readonly IList<string> CustomFilters = new List<string>();
        private static bool changed;
        private static bool checkDirExists = true;
        private static bool checkFileExists = true;
        private static bool showWSInLineDiff = true;
        private static bool showWSInMainDiff;
        private static bool showMdiTabs = true;
        private static HashType hashType = HashType.HashCode;
        private static int updateLevel;
        private static Font viewFont = new Font("Courier New", DefaultFontSize, GraphicsUnit.Point);

        #endregion

        #region Public Events

        public static event EventHandler OptionsChanged;

        #endregion

        #region Non-Event-Firing Public Properties

        public static int BinaryFootprintLength { get; set; } = 8;

        public static bool CheckDirExists => checkDirExists;

        public static bool CheckFileExists => checkFileExists;

        public static CompareType CompareType { get; set; }

        public static DirectoryDiffFileFilter FileFilter { get; set; }

        public static bool GoToFirstDiff { get; set; } = true;

        public static HashType HashType => hashType;

        public static bool IgnoreCase { get; set; }

        public static bool IgnoreDirectoryComparison { get; set; } = true;

        public static bool IgnoreTextWhitespace { get; set; }

        public static bool IgnoreXmlWhitespace { get; set; }

        public static string LastDirA { get; set; } = string.Empty;

        public static string LastDirB { get; set; } = string.Empty;

        public static string LastFileA { get; set; } = string.Empty;

        public static string LastFileB { get; set; } = string.Empty;

        public static string LastTextA { get; set; } = string.Empty;

        public static string LastTextB { get; set; } = string.Empty;

        public static int LineDiffHeight { get; set; }

        public static bool OnlyShowDirDialogIfShiftPressed { get; set; }

        public static bool OnlyShowFileDialogIfShiftPressed { get; set; }

        public static bool Recursive { get; set; } = true;

        public static bool ShowChangeAsDeleteInsert { get; set; }

        public static bool ShowDifferent { get; set; } = true;

        public static bool ShowOnlyInA { get; set; } = true;

        public static bool ShowOnlyInB { get; set; } = true;

        public static bool ShowSame { get; set; } = true;

        #endregion

        #region Event-Firing Public Properties

        public static bool ShowWSInLineDiff
        {
            get
            {
                return showWSInLineDiff;
            }

            set
            {
                SetValue(ref showWSInLineDiff, value);
            }
        }

        public static bool ShowWSInMainDiff
        {
            get
            {
                return showWSInMainDiff;
            }

            set
            {
                SetValue(ref showWSInMainDiff, value);
            }
        }

        public static Font ViewFont
        {
            get
            {
                return viewFont;
            }

            set
            {
                SetValue(ref viewFont, value);
            }
        }

        public static bool ShowMdiTabs
        {
            get
            {
                return showMdiTabs;
            }

            set
            {
                SetValue(ref showMdiTabs, value);
            }
        }

        #endregion

        #region Public Methods

        public static bool IsShiftPressed
            /* We have to use Keys.Shift instead of Keys.ShiftKey because "Shift"
			is "The SHIFT modifier key", and it's what ModifierKeys returns. */
            => (Control.ModifierKeys & Keys.Shift) == Keys.Shift;

        public static void AddCustomFilter(string filter)
        {
            // Case-insensitively look for the current filter
            int index = -1;
            for (int i = 0; i < CustomFilters.Count; i++)
            {
                if (string.Compare(CustomFilters[i], filter, true) == 0)
                {
                    index = i;
                    break;
                }
            }

            // Remove the old filter if necessary and add it
            // back at the beginning of the list.
            if (index >= 0)
            {
                CustomFilters.RemoveAt(index);
            }

            CustomFilters.Insert(0, filter);

            // Limit the history count to 20.
            const int MaxFilters = 20;
            while (CustomFilters.Count > MaxFilters)
            {
                CustomFilters.RemoveAt(CustomFilters.Count - 1);
            }
        }

        public static void BeginUpdate()
        {
            updateLevel++;
        }

        public static bool DirExists(string directoryName)
            => CheckDirExists ? Directory.Exists(directoryName) : true;

        public static void EndUpdate()
        {
            updateLevel--;

            if (updateLevel == 0 && changed)
            {
                changed = false;
                OptionsChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        public static bool FileExists(string fileName)
            => CheckFileExists ? File.Exists(fileName) : true;

        public static string[] GetCustomFilters()
        {
            int numFilters = CustomFilters.Count;
            string[] filters = new string[numFilters];
            for (int i = 0; i < numFilters; i++)
            {
                filters[i] = CustomFilters[i];
            }

            return filters;
        }

        public static void Load(ISettingsNode node)
        {
            showWSInMainDiff = node.GetValue(nameof(ShowWSInMainDiff), false);
            showWSInLineDiff = node.GetValue(nameof(ShowWSInLineDiff), true);
            IgnoreCase = node.GetValue(nameof(IgnoreCase), IgnoreCase);
            IgnoreTextWhitespace = node.GetValue(nameof(IgnoreTextWhitespace), IgnoreTextWhitespace);
            CompareType = node.GetValue(nameof(CompareType), CompareType);
            ShowOnlyInA = node.GetValue(nameof(ShowOnlyInA), ShowOnlyInA);
            ShowOnlyInB = node.GetValue(nameof(ShowOnlyInB), ShowOnlyInB);
            ShowDifferent = node.GetValue(nameof(ShowDifferent), ShowDifferent);
            ShowSame = node.GetValue(nameof(ShowSame), ShowSame);
            Recursive = node.GetValue(nameof(Recursive), Recursive);
            IgnoreDirectoryComparison = node.GetValue(nameof(IgnoreDirectoryComparison), IgnoreDirectoryComparison);
            OnlyShowFileDialogIfShiftPressed = node.GetValue(nameof(OnlyShowFileDialogIfShiftPressed), OnlyShowFileDialogIfShiftPressed);
            OnlyShowDirDialogIfShiftPressed = node.GetValue(nameof(OnlyShowDirDialogIfShiftPressed), OnlyShowDirDialogIfShiftPressed);
            GoToFirstDiff = node.GetValue(nameof(GoToFirstDiff), GoToFirstDiff);
            checkFileExists = node.GetValue(nameof(CheckFileExists), true);
            checkDirExists = node.GetValue(nameof(CheckDirExists), true);
            ShowChangeAsDeleteInsert = node.GetValue(nameof(ShowChangeAsDeleteInsert), ShowChangeAsDeleteInsert);
            showMdiTabs = node.GetValue(nameof(ShowMdiTabs), true);

            hashType = node.GetValue<HashType>(nameof(HashType), HashType.HashCode);
            IgnoreXmlWhitespace = node.GetValue(nameof(IgnoreXmlWhitespace), IgnoreXmlWhitespace);
            LineDiffHeight = node.GetValue(nameof(LineDiffHeight), LineDiffHeight);
            BinaryFootprintLength = node.GetValue(nameof(BinaryFootprintLength), BinaryFootprintLength);

            LastFileA = node.GetValue(nameof(LastFileA), LastFileA);
            LastFileB = node.GetValue(nameof(LastFileB), LastFileB);
            LastDirA = node.GetValue(nameof(LastDirA), LastDirA);
            LastDirB = node.GetValue(nameof(LastDirB), LastDirB);
            /* Note: We don't save or load the last text. */

            // Consolas has been around for 5+ years now, and it renders without misaligned hatch brushes when scrolling.
            string fontName = GetInstalledFontName(node.GetValue("FontName", "Consolas"), "Consolas", "Courier New", FontFamily.GenericMonospace.Name);
            FontStyle fontStyle = node.GetValue<FontStyle>("FontStyle", FontStyle.Regular);
            float fontSize = float.Parse(node.GetValue("FontSize", "9.75"));
            viewFont = new Font(fontName, fontSize, fontStyle, GraphicsUnit.Point);

            // Load custom filters
            CustomFilters.Clear();
            node = node.TryGetSubNode("Custom Filters");
            if (node == null)
            {
                // It appears to be the first time the program has run,
                // so add in some default filters.
                CustomFilters.Add("*.cs");
                CustomFilters.Add("*.cpp;*.h;*.idl;*.rc;*.c;*.inl");
                CustomFilters.Add("*.vb");
                CustomFilters.Add("*.xml");
                CustomFilters.Add("*.htm;*.html");
                CustomFilters.Add("*.txt");
                CustomFilters.Add("*.sql");
                CustomFilters.Add("*.obj;*.pdb;*.exe;*.dll;*.cache;*.tlog;*.trx;*.FileListAbsolute.txt");
            }
            else
            {
                IList<string> names = node.GetSettingNames();
                for (int i = 0; i < names.Count; i++)
                {
                    CustomFilters.Add(node.GetValue(names[i], string.Empty));
                }
            }
        }

        public static void Save(ISettingsNode node)
        {
            node.SetValue(nameof(CompareType), CompareType);

            node.SetValue(nameof(ShowWSInMainDiff), showWSInMainDiff);
            node.SetValue(nameof(ShowWSInLineDiff), showWSInLineDiff);
            node.SetValue(nameof(IgnoreCase), IgnoreCase);
            node.SetValue(nameof(IgnoreTextWhitespace), IgnoreTextWhitespace);
            node.SetValue(nameof(ShowOnlyInA), ShowOnlyInA);
            node.SetValue(nameof(ShowOnlyInB), ShowOnlyInB);
            node.SetValue(nameof(ShowDifferent), ShowDifferent);
            node.SetValue(nameof(ShowSame), ShowSame);
            node.SetValue(nameof(Recursive), Recursive);
            node.SetValue(nameof(IgnoreDirectoryComparison), IgnoreDirectoryComparison);
            node.SetValue(nameof(OnlyShowFileDialogIfShiftPressed), OnlyShowFileDialogIfShiftPressed);
            node.SetValue(nameof(OnlyShowDirDialogIfShiftPressed), OnlyShowDirDialogIfShiftPressed);
            node.SetValue(nameof(GoToFirstDiff), GoToFirstDiff);
            node.SetValue(nameof(CheckFileExists), checkFileExists);
            node.SetValue(nameof(CheckDirExists), checkDirExists);
            node.SetValue(nameof(ShowChangeAsDeleteInsert), ShowChangeAsDeleteInsert);
            node.SetValue(nameof(IgnoreXmlWhitespace), IgnoreXmlWhitespace);
            node.SetValue(nameof(ShowMdiTabs), showMdiTabs);

            node.SetValue(nameof(HashType), hashType);
            node.SetValue(nameof(LineDiffHeight), LineDiffHeight);
            node.SetValue(nameof(BinaryFootprintLength), BinaryFootprintLength);

            node.SetValue(nameof(LastFileA), LastFileA);
            node.SetValue(nameof(LastFileB), LastFileB);
            node.SetValue(nameof(LastDirA), LastDirA);
            node.SetValue(nameof(LastDirB), LastDirB);
            /* Note: We don't save or load the last text. */

            node.SetValue("FontName", viewFont.Name);
            node.SetValue("FontStyle", viewFont.Style);
            node.SetValue("FontSize", Convert.ToString(viewFont.SizeInPoints));

            // Save custom filters
            if (node.TryGetSubNode("Custom Filters") != null)
            {
                node.DeleteSubNode("Custom Filters");
            }

            if (CustomFilters.Count > 0)
            {
                node = node.GetSubNode("Custom Filters");
                for (int i = 0; i < CustomFilters.Count; i++)
                {
                    node.SetValue(i.ToString(), CustomFilters[i]);
                }
            }
        }

        #endregion

        #region Private Methods

        private static string GetInstalledFontName(params string[] fontNames)
        {
            string result = null;

            foreach (string fontName in fontNames)
            {
                // Set result here, so if none of the fonts are installed,
                // then we'll at least return the last name passed in.
                result = fontName;

                // http://stackoverflow.com/questions/113989/test-if-a-font-is-installed
                using (Font font = new Font(fontName, DefaultFontSize))
                {
                    if (font.Name == fontName)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        private static void SetValue<T>(ref T member, T value)
        {
            if (!object.Equals(member, value))
            {
                BeginUpdate();
                member = value;
                changed = true;
                EndUpdate();
            }
        }

        #endregion
    }
}
