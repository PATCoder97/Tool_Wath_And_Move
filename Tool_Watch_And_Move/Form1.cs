using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tool_Watch_And_Move
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string FromFolder;
        string ToFolder;

        private void Form1_Load(object sender, EventArgs e)
        {
            string filePath = Application.StartupPath + "\\Temp.txt";

            if (File.Exists(filePath))
            {
                string[] lines;
                lines = File.ReadAllLines(filePath);
                FromFolder = lines[0];
                ToFolder = lines[1];
            }
            else
            {
                MessageBox.Show("Thieu file Temp.txt");
            }
            fileSystemWatcher1.Path = FromFolder;
            fileSystemWatcher1.Filter = "*.sce*";
        }

        private void fileSystemWatcher1_Created_1(object sender, FileSystemEventArgs e)
        {
            listBox1.Items.Add(e.FullPath);
            File.Copy(e.FullPath, ToFolder + e.Name,true);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to finish ?", "Comfirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
