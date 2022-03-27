using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDMExporterForRhino
{
    public partial class Form1 : Form
    {

        public bool waitClosing = true;

        public Form1()
        {
            InitializeComponent();
        }

        public void InsertText(string str)
        {

            textBox1.Text = str;

            textBox1.SelectionStart = textBox1.TextLength;
            textBox1.ScrollToCaret();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            waitClosing = false;
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
