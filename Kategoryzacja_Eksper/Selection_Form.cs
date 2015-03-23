using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kategoryzacja_Eksper
{
    public partial class Selection_Form : Form
    {
        public Selection_Form()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cat_Video_Form f2 = new Cat_Video_Form();
            this.Visible = false;
            f2.ShowDialog();
            this.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cat_Audio_Form f2 = new Cat_Audio_Form();
            this.Visible = false;
            f2.ShowDialog();
            this.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cat_Both_Form f2 = new Cat_Both_Form();
            this.Visible = false;
            f2.ShowDialog();
            this.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Ass_Audio_Form f2 = new Ass_Audio_Form();
            this.Visible = false;
            f2.ShowDialog();
            this.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Ass_Video_Form f2 = new Ass_Video_Form();
            this.Visible = false;
            f2.ShowDialog();
            this.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Ass_Both_Form f2 = new Ass_Both_Form();
            this.Visible = false;
            f2.ShowDialog();
            this.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Ord_Audio_Form f2 = new Ord_Audio_Form();
            this.Visible = false;
            //f2.ShowDialog();
            this.Visible = true;
        }

    }
}
