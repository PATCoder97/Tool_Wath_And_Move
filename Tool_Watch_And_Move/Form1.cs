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
//Test từ máy FHS
//Test 2
//Test 3
namespace Tool_Watch_And_Move
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        _ini iniTemp = new _ini(Application.StartupPath + "\\Temp.ini");
        string[] Filter;

        private void Form1_Load(object sender, EventArgs e)
        {
            txtFrom.Text = iniTemp.Read("LINK", "from");
            txtTo.Text = iniTemp.Read("LINK", "to");
            this.KeyPreview = true;
            
        }

        private void fileSystemWatcher1_Created_1(object sender, FileSystemEventArgs e)
        {
            if (btStart.Text=="STOP")
            {
                string DateTimeNow = DateTime.Now.Year.ToString() + "/" +
                        string.Format("{0:00}", DateTime.Now.Month).ToString() + "/" +
                        string.Format("{0:00}", DateTime.Now.Day).ToString() + " " +
                        string.Format("{0:00}", DateTime.Now.Hour).ToString() + ":" +
                        string.Format("{0:00}", DateTime.Now.Minute).ToString() + ":" +
                        string.Format("{0:00}", DateTime.Now.Second).ToString();
                for (int i = 0; i < Filter.Length; i++)
                {
                    if (e.Name.EndsWith(Filter[i]))
                    {
                        listBox1.Items.Add(DateTimeNow + "____" + e.Name);
                        File.Copy(e.FullPath, txtTo.Text + "\\" + e.Name, true);
                        listBox1.SelectedIndex = listBox1.Items.Count - 1;
                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to finish ?", "Comfirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btFrom_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtFrom.Text = folderDlg.SelectedPath;
                Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }

        private void btTo_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtTo.Text = folderDlg.SelectedPath;
                Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }

        private void txtFrom_TextChanged(object sender, EventArgs e)
        {
            iniTemp.Write("LINK", "from", txtFrom.Text);
        }

        private void txtTo_TextChanged(object sender, EventArgs e)
        {
            iniTemp.Write("LINK", "to", txtTo.Text);
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            Filter = iniTemp.Read("LINK", "filter").Split('/');
            btStart.Text = btStart.Text ==  "START" ? "STOP":"START";
            if (btStart.Text=="START")
            {
                btStart.BackColor = Color.Lime;
                txtFrom.Enabled = true;
                txtTo.Enabled = true;
                btFrom.Enabled = true;
                btTo.Enabled = true;
            }
            else
            {
                btStart.BackColor = Color.Red;
                txtFrom.Enabled = false;
                txtTo.Enabled = false;
                btFrom.Enabled = false;
                btTo.Enabled = false;

                fileSystemWatcher1.Path = txtFrom.Text;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtFrom.Enabled != false)
            {
                if (e.KeyCode == Keys.T)
                {
                    Form2 f = new Form2();
                    f.ShowDialog();
                }
            }
        }
    }
}
