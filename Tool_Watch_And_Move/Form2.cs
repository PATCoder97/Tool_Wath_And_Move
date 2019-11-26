using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tool_Watch_And_Move
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        _ini iniTemp = new _ini(Application.StartupPath + "\\Temp.ini");

        private void Form2_Load(object sender, EventArgs e)
        {
            richTextBox1.Text=iniTemp.Read("LINK", "filter");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            iniTemp.Write("LINK", "filter", richTextBox1.Text);
            MessageBox.Show("Update complete!", "Notification");
            Close();
        }
    }
}
