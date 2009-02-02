using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SbB.Geometry;

namespace MortarFEM.Dialogs
{
    public partial class BoundaryDialog : Form
    {
        private Domain domain;
        private BoundaryClass[] bc;

        public BoundaryDialog(Domain domain)
        {
            this.domain = domain;
            InitializeComponent();

            if (domain!=null)
            {
                bc = new BoundaryClass[domain.P.N];
                for (int i = 0; i < domain.P.N; i++)
                {
                    listBox1.Items.Add(i + 1);
                    bc[i] = domain.boundary(i);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;
            if (i>=0)
            {
                radioButton1.Enabled = radioButton2.Enabled = true;
                //textBox1.Text = textBox2.Text = "0";
                switch (bc[i].type())
                {
                    case BoundaryType.CINEMATIC:
                        radioButton1.Checked = true;
                        break;
                    case BoundaryType.STATIC:
                        textBox1.Text = ((StaticBoundary) bc[i]).P[0].ToString();
                        textBox2.Text = ((StaticBoundary) bc[i]).P[1].ToString();
                        radioButton2.Checked = true;
                        break;
                    default:
                        break;
                }
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;
            if (i >= 0)
            {
                if (radioButton2.Checked)
                {
                    label1.Enabled = label2.Enabled = true;
                    textBox1.Enabled = textBox2.Enabled = true;
                    //if (bc[i].type() == BoundaryType.CINEMATIC)
                       // textBox1.Text = textBox2.Text = "0";
                    double x = 0, y = 0;
                    double.TryParse(textBox1.Text, out x);
                    double.TryParse(textBox2.Text, out y);
                    bc[i] = new StaticBoundary(x, y);
                }
                else
                {
                    label1.Enabled = label2.Enabled = false;
                    textBox1.Enabled = textBox2.Enabled = false;
                    bc[i] = new CinematicBoundary();
                }
            }
        }
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;
            if (i >= 0)
            {
                double x = 0, y = 0;
                double.TryParse(textBox1.Text, out x);
                double.TryParse(textBox2.Text, out y);
                bc[i] = new StaticBoundary(x, y);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (domain != null)
                for (int i = 0; i < domain.P.N; i++)
                    domain.setBoundary(i, bc[i]);
        }
    }
}