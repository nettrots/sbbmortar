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
    public partial class InsideBoundaryDialog : Form
    {
        private Domain domain;
        private bool[] changed;
        private string[] mortars;
        private string[] nonmortars;

        public InsideBoundaryDialog(Domain domain)
        {
            this.domain = domain;
            InitializeComponent();

            if (domain != null)
            {
                changed = new bool[domain.IBN];
                mortars = new string[domain.IBN];
                nonmortars = new string[domain.IBN];
                for (int i = 0; i < domain.IBN; i++)
                {
                    listBox1.Items.Add(i + 1);
                    for (int j = 0; j < domain.N; j++)
                    {
                        if (domain.insideBoundary(i).Mortar == domain[j])
                            mortars[i] = "Subdomain " + (j + 1);
                        if (domain.insideBoundary(i).NonMortar == domain[j])
                            nonmortars[i] = "Subdomain " + (j + 1);
                    }
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;
            if(i>=0)
            {
                textBox1.Enabled = textBox2.Enabled = label1.Enabled = label2.Enabled = button3.Enabled = true;
                textBox1.Text = mortars[i];
                textBox2.Text = nonmortars[i];
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;
            if (i>=0)
            {
                changed[i] = !changed[i];
                string s = mortars[i];
                mortars[i] = nonmortars[i];
                nonmortars[i] = s;
                textBox1.Text = mortars[i];
                textBox2.Text = nonmortars[i];
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i=0;i<changed.Length;i++)
                if (changed[i]) domain.insideBoundary(i).changeMortars();
        }
    }
}