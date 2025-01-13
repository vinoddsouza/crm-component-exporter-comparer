using System;
using System.IO;

namespace RioCanada.Crm.ComponentExportComparer.Core
{
    public class Logger
    {
        public string LogFolder { get; }
        private string LogFile { get; }
        private bool LogFolderEnsured { get; set; }
        private object lck = new object();

        public Logger(string logFolder)
        {
            this.LogFolder = logFolder;
            this.LogFile = Path.Combine(logFolder, "log.txt");
        }

        private void EnsureDirectory()
        {
            if (LogFolderEnsured) return;
            ExportService.EnsureDirectory(LogFolder);
            LogFolderEnsured = true;
        }

        private void WriteLog(string message)
        {
            lock (lck)
            {
                this.EnsureDirectory();
                File.AppendAllText(LogFile, DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss.fff]") + " " + message + Environment.NewLine);
            }
        }

        public void Clean()
        {
            LogFolderEnsured = false;
            if (Directory.Exists(LogFolder))
            {
                Directory.Delete(LogFolder, true);
            }
        }

        public void WriteFile(string fileName, string content)
        {
            lock (lck)
            {
                this.EnsureDirectory();
                File.WriteAllText(Path.Combine(LogFolder, fileName), content);
            }
        }

        public void WriteFile(string fileName, byte[] data)
        {
            lock (lck)
            {
                this.EnsureDirectory();
                File.WriteAllBytes(Path.Combine(LogFolder, fileName), data);
            }
        }

        public void Log(string message)
        {
            this.WriteLog("[LOG] " + message);
        }

        public void Error(string message)
        {
            this.WriteLog("[ERROR] " + message);
        }

        public void Error(Exception ex)
        {
            this.WriteLog("[ERROR] " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine);
        }
    }
}
