using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.Comparision
{
    partial class FormFileViewer : Form
    {
        readonly Scintilla scintilla;

        private readonly Color BACK_COLOR = IntToColor(0xFFFFFF);
        private readonly Color FORE_COLOR = IntToColor(0x333333);
        private readonly Color SELECTION_COLOR = IntToColor(0xFFFFBB);
        private readonly Color LINE_BACK_COLOR = IntToColor(0xF8F8F8);

        private readonly Color SYNTAX_COMMENT_COLOR = Color.Green;
        private readonly Color SYNTAX_NUMBER_COLOR = Color.Black;
        private readonly Color SYNTAX_WORD_COLOR = Color.Blue;
        private readonly Color SYNTAX_STRING_COLOR = Color.FromArgb(163, 21, 21);
        private readonly Color SYNTAX_STRINGEOL_COLOR = Color.Pink;
        private readonly Color SYNTAX_OPERATOR_COLOR = Color.Black;
        private readonly Color SYNTAX_PREPROCESSOR_COLOR = Color.Black;

        private readonly string FileExtension;
        private readonly Settings Settings;
        private readonly string Path;
        private readonly bool IsContentView;

        public FormFileViewer(Settings settings, string path, string content = null, string contentTitle = null)
        {
            InitializeComponent();

            this.Settings = settings;
            this.Path = path;
            this.IsContentView = path == null;

            // CREATE CONTROL
            scintilla = new Scintilla();
            TextPanel.Controls.Add(scintilla);

            // BASIC CONFIG
            scintilla.Dock = DockStyle.Fill;

            // SET CONTENT
            if (!this.IsContentView)
            {
                content = System.IO.File.ReadAllText(path);
            }
            scintilla.Text = content;
            scintilla.ReadOnly = true;

            if (this.IsContentView)
            {
                this.Text = $"Content View - {contentTitle}";
            }
            else
            {
                this.Text = $"File Viewer - {System.IO.Path.GetFileName(path)}";
            }

            this.FileExtension = string.IsNullOrEmpty(path) ? ".json" : System.IO.Path.GetExtension(path);

            // INITIAL VIEW CONFIG
            scintilla.WrapMode = WrapMode.None;
            scintilla.IndentationGuides = IndentView.None;

            // NUMBER MARGIN
            scintilla.Margins[0].Width = scintilla.Lines.Count.ToString().Length * 12;
            scintilla.Styles[Style.LineNumber].BackColor = LINE_BACK_COLOR;
            scintilla.Styles[Style.LineNumber].ForeColor = FORE_COLOR;

            // INIT HOTKEYS
            InitHotkeys();

            // STYLING
            scintilla.SetSelectionBackColor(true, SELECTION_COLOR);
            InitSyntaxColoring();

        }

        private void InitHotkeys()
        {
            // register the hotkeys with the form
            HotKeyManager.AddHotKey(this, OpenSearch, Keys.F, true);
            HotKeyManager.AddHotKey(this, CloseSearch, Keys.Escape);

            // remove conflicting hotkeys from scintilla
            scintilla.ClearCmdKey(Keys.Control | Keys.F);
        }

        private void InitSyntaxColoring()
        {
            // Configure the default style
            scintilla.StyleResetDefault();
            scintilla.Styles[Style.Default].Font = "Consolas";
            scintilla.Styles[Style.Default].Size = 10;
            scintilla.Styles[Style.Default].BackColor = BACK_COLOR;
            scintilla.Styles[Style.Default].ForeColor = FORE_COLOR;
            scintilla.StyleClearAll();

            switch (this.FileExtension)
            {
                case ".cs":
                    this.ConfigureCppStyling();
                    scintilla.Lexer = Lexer.Cpp;
                    scintilla.SetKeywords(0, "class extends implements import interface new case do while else if for in switch throw get set function var try catch finally while with default break continue delete return each const namespace package include use is as instanceof typeof author copy default deprecated eventType example exampleText exception haxe inheritDoc internal link mtasc mxmlc param private return see serial serialData serialField since throws usage version langversion playerversion productversion dynamic private public partial static intrinsic internal native override protected AS3 final super this arguments null Infinity NaN undefined true false abstract as base bool break by byte case catch char checked class const continue decimal default delegate do double descending explicit event extern else enum false finally fixed float for foreach from goto group if implicit in int interface internal into is lock long new null namespace object operator out override orderby params private protected public readonly ref return switch struct sbyte sealed short sizeof stackalloc static string select this throw true try typeof uint ulong unchecked unsafe ushort using var virtual volatile void while where yield");
                    scintilla.SetKeywords(1, "void Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String SyntaxError TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void Path File System Windows Forms ScintillaNET");
                    break;
                case ".js":
                    this.ConfigureCppStyling();
                    scintilla.Lexer = Lexer.Cpp;
                    scintilla.SetKeywords(0, "debugger abstract as base break case catch checked continue default delegate do else event explicit extern false finally fixed for foreach function goto if implicit in interface internal is let lock namespace new null object operator out override params private protected public readonly ref return sealed sizeof stackalloc switch this throw true try typeof unchecked unsafe using var virtual while");
                    scintilla.SetKeywords(1, "bool byte char class const decimal double enum float int long sbyte short static string struct uint ulong ushort void");
                    break;
                case ".css":
                    this.ConfigureCssStyling();
                    scintilla.Lexer = Lexer.Css;
                    break;
                case ".html":
                    this.ConfigureHtmlStyling();
                    scintilla.Lexer = Lexer.Html;
                    break;
                case ".xml":
                    this.ConfigureXmlStyling();
                    scintilla.Lexer = Lexer.Xml;
                    break;
                case ".json":
                    this.ConfigureJsonStyling();
                    scintilla.Lexer = Lexer.Json;
                    break;
            }
        }

        private void ConfigureCppStyling()
        {
            scintilla.Styles[Style.Cpp.Default].ForeColor = FORE_COLOR;
            scintilla.Styles[Style.Cpp.Comment].ForeColor = SYNTAX_COMMENT_COLOR;
            scintilla.Styles[Style.Cpp.CommentLine].ForeColor = SYNTAX_COMMENT_COLOR;
            scintilla.Styles[Style.Cpp.CommentLineDoc].ForeColor = SYNTAX_COMMENT_COLOR;
            scintilla.Styles[Style.Cpp.Number].ForeColor = SYNTAX_NUMBER_COLOR;
            scintilla.Styles[Style.Cpp.Word].ForeColor = SYNTAX_WORD_COLOR;
            scintilla.Styles[Style.Cpp.Word2].ForeColor = SYNTAX_WORD_COLOR;
            scintilla.Styles[Style.Cpp.String].ForeColor = SYNTAX_STRING_COLOR;
            scintilla.Styles[Style.Cpp.Character].ForeColor = SYNTAX_STRING_COLOR;
            scintilla.Styles[Style.Cpp.Verbatim].ForeColor = SYNTAX_STRING_COLOR;
            scintilla.Styles[Style.Cpp.StringEol].BackColor = SYNTAX_STRINGEOL_COLOR;
            scintilla.Styles[Style.Cpp.Operator].ForeColor = SYNTAX_OPERATOR_COLOR;
            scintilla.Styles[Style.Cpp.Preprocessor].ForeColor = SYNTAX_PREPROCESSOR_COLOR;
        }

        private void ConfigureCssStyling()
        {
            scintilla.Styles[Style.Css.Directive].ForeColor = Color.Red;
            scintilla.Styles[Style.Css.Variable].ForeColor = Color.Red;
            scintilla.Styles[Style.Css.Comment].ForeColor = SYNTAX_COMMENT_COLOR;
            scintilla.Styles[Style.Css.Attribute].ForeColor = Color.Red;
            scintilla.Styles[Style.Css.Class].ForeColor = Color.FromArgb(128, 0, 0);
            scintilla.Styles[Style.Css.Id].ForeColor = Color.FromArgb(128, 0, 0);
            scintilla.Styles[Style.Css.DoubleString].ForeColor = Color.Blue;
            scintilla.Styles[Style.Css.Important].ForeColor = Color.Blue;
            scintilla.Styles[Style.Css.SingleString].ForeColor = Color.Blue;
            scintilla.Styles[Style.Css.Value].ForeColor = Color.Blue;
            scintilla.Styles[Style.Css.Media].ForeColor = Color.Blue;
            scintilla.Styles[Style.Css.Tag].ForeColor = Color.FromArgb(128, 0, 0);
            scintilla.Styles[Style.Css.Operator].ForeColor = Color.FromArgb(128, 0, 0);
            scintilla.Styles[Style.Css.PseudoClass].ForeColor = Color.FromArgb(128, 0, 0);
            scintilla.Styles[Style.Css.PseudoElement].ForeColor = Color.FromArgb(128, 0, 0);
            scintilla.Styles[Style.Css.UnknownPseudoClass].ForeColor = Color.FromArgb(128, 0, 0);
            scintilla.Styles[Style.Css.ExtendedIdentifier].ForeColor = Color.FromArgb(128, 0, 0);
            scintilla.Styles[Style.Css.ExtendedPseudoClass].ForeColor = Color.FromArgb(128, 0, 0);
            scintilla.Styles[Style.Css.ExtendedPseudoElement].ForeColor = Color.FromArgb(128, 0, 0);
            scintilla.Styles[Style.Css.UnknownIdentifier].ForeColor = Color.Red;
            scintilla.Styles[Style.Css.Identifier].ForeColor = Color.Red;
            scintilla.Styles[Style.Css.Identifier2].ForeColor = Color.Red;
            scintilla.Styles[Style.Css.Identifier3].ForeColor = Color.Red;
        }

        private void ConfigureHtmlStyling()
        {
            scintilla.Styles[Style.Html.Asp].ForeColor = Color.Black;
            scintilla.Styles[Style.Html.Asp].BackColor = Color.Yellow;
            scintilla.Styles[Style.Html.AspAt].ForeColor = Color.Black;
            scintilla.Styles[Style.Html.AspAt].BackColor = Color.Yellow;
            scintilla.Styles[Style.Html.AttributeUnknown].ForeColor = Color.Red;
            scintilla.Styles[Style.Html.Attribute].ForeColor = Color.Red;
            scintilla.Styles[Style.Html.CData].ForeColor = Color.Blue;
            scintilla.Styles[Style.Html.Comment].ForeColor = Color.Green;
            scintilla.Styles[Style.Html.Default].ForeColor = Color.Black;
            scintilla.Styles[Style.Html.DoubleString].ForeColor = Color.Blue;
            scintilla.Styles[Style.Html.Other].ForeColor = Color.FromArgb(128, 0, 0);
            scintilla.Styles[Style.Html.Script].ForeColor = Color.FromArgb(128, 0, 0);
            scintilla.Styles[Style.Html.SingleString].ForeColor = Color.Blue;
            scintilla.Styles[Style.Html.Tag].ForeColor = Color.FromArgb(128, 0, 0);
            scintilla.Styles[Style.Html.TagEnd].ForeColor = Color.FromArgb(128, 0, 0);
            scintilla.Styles[Style.Html.XcComment].ForeColor = Color.Green;
            scintilla.Styles[Style.Html.XmlStart].ForeColor = Color.Blue;
            scintilla.Styles[Style.Html.XmlEnd].ForeColor = Color.Blue;
        }

        private void ConfigureXmlStyling()
        {
            scintilla.Styles[Style.Xml.Asp].BackColor = Color.Yellow;
            scintilla.Styles[Style.Xml.AspAt].ForeColor = Color.Black;
            scintilla.Styles[Style.Xml.AspAt].BackColor = Color.Yellow;
            scintilla.Styles[Style.Xml.AttributeUnknown].ForeColor = Color.Red;
            scintilla.Styles[Style.Xml.Attribute].ForeColor = Color.Red;
            scintilla.Styles[Style.Xml.CData].ForeColor = Color.Blue;
            scintilla.Styles[Style.Xml.Comment].ForeColor = Color.Green;
            scintilla.Styles[Style.Xml.Default].ForeColor = Color.Black;
            scintilla.Styles[Style.Xml.DoubleString].ForeColor = Color.Blue;
            scintilla.Styles[Style.Xml.Entity].ForeColor = Color.Blue;
            scintilla.Styles[Style.Xml.Number].ForeColor = Color.Blue;

            scintilla.Styles[Style.Html.Other].ForeColor = Color.FromArgb(128, 0, 0);
            scintilla.Styles[Style.Html.Script].ForeColor = Color.FromArgb(128, 0, 0);
            scintilla.Styles[Style.Html.SingleString].ForeColor = Color.Blue;
            scintilla.Styles[Style.Html.Tag].ForeColor = Color.FromArgb(128, 0, 0);
            scintilla.Styles[Style.Html.TagUnknown].ForeColor = Color.FromArgb(128, 0, 0);
            scintilla.Styles[Style.Html.TagEnd].ForeColor = Color.FromArgb(128, 0, 0);
            scintilla.Styles[Style.Html.XcComment].ForeColor = Color.Green;
            scintilla.Styles[Style.Html.XmlStart].ForeColor = Color.Blue;
            scintilla.Styles[Style.Html.XmlEnd].ForeColor = Color.Blue;
        }

        private void ConfigureJsonStyling()
        {
            scintilla.Styles[Style.Json.Default].ForeColor = FORE_COLOR;
            scintilla.Styles[Style.Json.Number].ForeColor = SYNTAX_NUMBER_COLOR;
            scintilla.Styles[Style.Json.String].ForeColor = SYNTAX_STRING_COLOR;
            scintilla.Styles[Style.Json.StringEol].ForeColor = SYNTAX_STRINGEOL_COLOR;
            scintilla.Styles[Style.Json.PropertyName].ForeColor = Color.Blue;
            scintilla.Styles[Style.Json.EscapeSequence].ForeColor = SYNTAX_STRING_COLOR;
            scintilla.Styles[Style.Json.LineComment].ForeColor = SYNTAX_COMMENT_COLOR;
            scintilla.Styles[Style.Json.BlockComment].ForeColor = SYNTAX_COMMENT_COLOR;
            scintilla.Styles[Style.Json.Operator].ForeColor = SYNTAX_OPERATOR_COLOR;
            scintilla.Styles[Style.Json.Uri].ForeColor = SYNTAX_STRING_COLOR;
            scintilla.Styles[Style.Json.CompactIRI].ForeColor = SYNTAX_STRING_COLOR;
            scintilla.Styles[Style.Json.Keyword].ForeColor = Color.Blue;
            scintilla.Styles[Style.Json.LdKeyword].ForeColor = Color.Blue;
            scintilla.Styles[Style.Json.Error].ForeColor = Color.Red;
        }

        #region Main Menu Commands

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSearch();
        }

        private void wordWrapToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // toggle word wrap
            wordWrapItem.Checked = !wordWrapItem.Checked;
            scintilla.WrapMode = wordWrapItem.Checked ? WrapMode.Word : WrapMode.None;
        }

        private void indentGuidesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // toggle indent guides
            indentGuidesItem.Checked = !indentGuidesItem.Checked;
            scintilla.IndentationGuides = indentGuidesItem.Checked ? IndentView.LookBoth : IndentView.None;
        }

        #endregion

        #region Quick Search Bar

        bool SearchIsOpen = false;

        private void OpenSearch()
        {

            SearchManager.SearchBox = TxtSearch;
            SearchManager.TextArea = scintilla;

            if (!SearchIsOpen)
            {
                SearchIsOpen = true;
                InvokeIfNeeded(delegate ()
                {
                    PanelSearch.Visible = true;
                    TxtSearch.Text = SearchManager.LastSearch;
                    TxtSearch.Focus();
                    TxtSearch.SelectAll();
                });
            }
            else
            {
                InvokeIfNeeded(delegate ()
                {
                    TxtSearch.Focus();
                    TxtSearch.SelectAll();
                });
            }
        }
        private void CloseSearch()
        {
            if (SearchIsOpen)
            {
                SearchIsOpen = false;
                InvokeIfNeeded(delegate ()
                {
                    PanelSearch.Visible = false;
                });
            }
            else
            {
                this.Close();
            }
        }

        private void BtnClearSearch_Click(object sender, EventArgs e)
        {
            CloseSearch();
        }

        private void BtnPrevSearch_Click(object sender, EventArgs e)
        {
            SearchManager.Find(false, false);
        }
        private void BtnNextSearch_Click(object sender, EventArgs e)
        {
            SearchManager.Find(true, false);
        }
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchManager.Find(true, true);
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (HotKeyManager.IsHotkey(e, Keys.Enter))
            {
                SearchManager.Find(true, false);
            }
            if (HotKeyManager.IsHotkey(e, Keys.Enter, true) || HotKeyManager.IsHotkey(e, Keys.Enter, false, true))
            {
                SearchManager.Find(false, false);
            }
        }

        #endregion

        #region Utils

        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        public void InvokeIfNeeded(Action action)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }
        }

        #endregion

        private void openInIDEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.IsContentView) return;
            OpenFileInIDE(this.Settings, this.Path);
        }

        public static void OpenFileInIDE(Settings settings, string path)
        {
            var defaultIDE = settings.Configuration.DefaultIDE;
            if (defaultIDE == null) return;

            switch (defaultIDE.Type)
            {
                case IDEType.Notepad:
                    {
                        var exePath = defaultIDE.ExecutablePath ?? "notepad";
                        System.Diagnostics.Process.Start(exePath, $"\"{path}\"");
                    }
                    break;
                case IDEType.NotepadPlus:
                    {
                        if (defaultIDE.ExecutablePath != null)
                        {
                            System.Diagnostics.Process.Start(defaultIDE.ExecutablePath, $"\"{path}\"");
                        }
                        else
                        {
                            System.Diagnostics.Process.Start("start", $"notepad++ \"{path}\"");
                        }
                    }
                    break;
                case IDEType.VSCode:
                    {
                        try
                        {
                            var exePath = defaultIDE.ExecutablePath ?? "code";
                            using (var process = new System.Diagnostics.Process())
                            {
                                process.StartInfo.UseShellExecute = false;
                                process.StartInfo.FileName = "cmd.exe"; // exePath;
                                //process.StartInfo.Arguments = $"\"{path}\"";
                                process.StartInfo.RedirectStandardInput = true;   //Accept input information from the calling program
                                process.StartInfo.RedirectStandardOutput = true;  //Get output information from the calling program
                                process.StartInfo.RedirectStandardError = true;   //Redirect standard error output
                                process.StartInfo.CreateNoWindow = true;
                                process.Start();

                                if (exePath.IndexOf(" ") != -1) exePath = "\"" + exePath + "\"";

                                process.StandardInput.WriteLine($"{exePath} \"{path}\"");
                                process.StandardInput.WriteLine("exit");
                                //process.CloseMainWindow();
                                System.Threading.Thread.Sleep(1000);
                                if (!process.HasExited)
                                {
                                    process.Kill();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            //
                        }
                    }
                    break;
                case IDEType.Other:
                    System.Diagnostics.Process.Start(defaultIDE.ExecutablePath, $"\"{path}\"");
                    break;
            }
        }

        public static DialogResult Open(Settings settings, string path)
        {
            return (new FormFileViewer(settings, path)).ShowDialog();
        }

        public static DialogResult ShowContent(Settings settings, string content, string title)
        {
            return (new FormFileViewer(settings, null, content, title)).ShowDialog();
        }
    }

    class HotKeyManager
    {

        public static bool Enable = true;

        public static void AddHotKey(Form form, Action function, Keys key, bool ctrl = false, bool shift = false, bool alt = false)
        {
            form.KeyPreview = true;

            form.KeyDown += delegate (object sender, KeyEventArgs e)
            {
                if (IsHotkey(e, key, ctrl, shift, alt))
                {
                    function();
                }
            };
        }

        public static bool IsHotkey(KeyEventArgs eventData, Keys key, bool ctrl = false, bool shift = false, bool alt = false)
        {
            return eventData.KeyCode == key && eventData.Control == ctrl && eventData.Shift == shift && eventData.Alt == alt;
        }


    }

    class SearchManager
    {

        public static ScintillaNET.Scintilla TextArea;
        public static TextBox SearchBox;

        public static string LastSearch = "";

        public static int LastSearchIndex;

        public static void Find(bool next, bool incremental)
        {
            bool first = LastSearch != SearchBox.Text;

            LastSearch = SearchBox.Text;
            if (LastSearch.Length > 0)
            {

                if (next)
                {

                    // SEARCH FOR THE NEXT OCCURANCE

                    // Search the document at the last search index
                    TextArea.TargetStart = LastSearchIndex - 1;
                    TextArea.TargetEnd = LastSearchIndex + (LastSearch.Length + 1);
                    TextArea.SearchFlags = SearchFlags.None;

                    // Search, and if not found..
                    if (!incremental || TextArea.SearchInTarget(LastSearch) == -1)
                    {

                        // Search the document from the caret onwards
                        TextArea.TargetStart = TextArea.CurrentPosition;
                        TextArea.TargetEnd = TextArea.TextLength;
                        TextArea.SearchFlags = SearchFlags.None;

                        // Search, and if not found..
                        if (TextArea.SearchInTarget(LastSearch) == -1)
                        {

                            // Search again from top
                            TextArea.TargetStart = 0;
                            TextArea.TargetEnd = TextArea.TextLength;

                            // Search, and if not found..
                            if (TextArea.SearchInTarget(LastSearch) == -1)
                            {

                                // clear selection and exit
                                TextArea.ClearSelections();
                                return;
                            }
                        }

                    }

                }
                else
                {

                    // SEARCH FOR THE PREVIOUS OCCURANCE

                    // Search the document from the beginning to the caret
                    TextArea.TargetStart = 0;
                    TextArea.TargetEnd = TextArea.CurrentPosition;
                    TextArea.SearchFlags = SearchFlags.None;

                    // Search, and if not found..
                    if (TextArea.SearchInTarget(LastSearch) == -1)
                    {

                        // Search again from the caret onwards
                        TextArea.TargetStart = TextArea.CurrentPosition;
                        TextArea.TargetEnd = TextArea.TextLength;

                        // Search, and if not found..
                        if (TextArea.SearchInTarget(LastSearch) == -1)
                        {

                            // clear selection and exit
                            TextArea.ClearSelections();
                            return;
                        }
                    }

                }

                // Select the occurance
                LastSearchIndex = TextArea.TargetStart;
                TextArea.SetSelection(TextArea.TargetEnd, TextArea.TargetStart);
                TextArea.ScrollCaret();

            }

            SearchBox.Focus();
        }
    }
}
