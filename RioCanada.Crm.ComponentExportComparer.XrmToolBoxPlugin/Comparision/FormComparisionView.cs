using RioCanada.Crm.ComponentExportComparer.Core.Models;
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
    partial class FormComparisionView : Form
    {
        public FormComparisionView(Settings settings, string sourceIndexFile, string targetIndexFile)
        {
            InitializeComponent();

            this.comparisionView1.Settings = settings;
            this.comparisionView1.SetIndexFiles(sourceIndexFile, targetIndexFile);

            // INIT HOTKEYS
            InitHotkeys();
        }

        public FormComparisionView(Settings settings, IndexLineItem sourceIndexItem, IndexLineItem targetIndexItem, string rootItemLabel, string title)
        {
            InitializeComponent();

            this.Text = title;
            this.comparisionView1.IsContentView = true;
            this.comparisionView1.RootItemLabel = rootItemLabel;
            this.comparisionView1.Settings = settings;
            this.comparisionView1.SetIndexItems(sourceIndexItem, targetIndexItem);

            // INIT HOTKEYS
            InitHotkeys();
        }

        private void InitHotkeys()
        {
            // register the hotkeys with the form
            //HotKeyManager.AddHotKey(this, this.Close, Keys.Escape);
        }
    }
}
