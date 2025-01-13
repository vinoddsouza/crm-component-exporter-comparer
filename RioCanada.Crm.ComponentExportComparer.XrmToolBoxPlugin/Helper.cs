using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin
{
    static class Helper
    {
        public static IDE DetectDefaultIDE()
        {
            if (DetectVSCode() != null)
            {
                return new IDE
                {
                    DisplayName = "VS Code",
                    Type = IDEType.VSCode,
                };
            }
            else if (DetectNotepadPlus() != null)
            {
                return new IDE
                {
                    DisplayName = "VS Code",
                    Type = IDEType.VSCode,
                };
            }

            return new IDE
            {
                DisplayName = "Notepad",
                Type = IDEType.Notepad,
            };
        }

        public static string DetectNotepad()
        {
            var p = new System.Diagnostics.Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "where";
            p.StartInfo.Arguments = "notepad";
            p.Start();

            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            if(string.IsNullOrWhiteSpace(output) && !output.Contains("Could not find"))
            {
                return "notepad";
            }
            else
            {
                return null;
            }
        }

        public static string DetectVSCode()
        {
            var p = new System.Diagnostics.Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "where";
            p.StartInfo.Arguments = "code";
            p.Start();

            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            if (string.IsNullOrWhiteSpace(output) && !output.Contains("Could not find"))
            {
                return "code";
            }
            else
            {
                return null;
            }
        }

        public static string DetectNotepadPlus()
        {
            if (System.IO.File.Exists(@"C:\Program Files (x86)\Notepad++\\notepad++.exe") || System.IO.File.Exists(@"C:\Program Files\Notepad++\\notepad++.exe"))
            {
                return "notepad++";
            }
            else
            {
                return null;
            }
        }

        public static int GetProgressValue(int min, int max, int value)
        {
            return (((max - min) * Math.Min(100, Math.Max(0, value))) / 100) + min;
        }
    }

    public class IDE
    {
        public IDEType Type { get; set; }
        public string DisplayName { get; set; }
        public string ExecutablePath { get; set; }
    }

    public enum IDEType
    {
        Notepad,
        NotepadPlus,
        VSCode,
        Other,
    }
}
