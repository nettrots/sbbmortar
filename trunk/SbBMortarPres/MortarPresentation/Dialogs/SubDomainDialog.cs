using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SbBMortar.SbB;

namespace MortarFEM.Dialogs
{
    public partial class SubDomainDialog : Form
    {
        private Domain domain;
        private double[] maxAreas;
        private double[] minAngles;
        private double[] youngModuluses;
        private double[] poissonRatios;
        private string[] order;
       // private ApproximationOrder[] orders;

        public SubDomainDialog(Domain domain)
        {
            this.domain = domain;

            InitializeComponent();

            if (domain!=null)
            {
                maxAreas = new double[domain.SubDomainCount];
                minAngles = new double[domain.SubDomainCount];
                youngModuluses = new double[domain.SubDomainCount];
                poissonRatios = new double[domain.SubDomainCount];
                order = new string[domain.SubDomainCount];
                //orders = new ApproximationOrder[domain.SubDomainCount];
                for (int i = 0; i < domain.SubDomainCount; i++)
                {
                    listBox1.Items.Add(i + 1);
                    maxAreas[i] = domain[i].MaxArea;
                    minAngles[i] = domain[i].MinAngle;
                    youngModuluses[i] = domain[i].YoungModulus;
                    poissonRatios[i] = domain[i].PoissonRatio;
                    order[i] = domain[i].Triangulation.ToString();
                   // orders[i] = domain[i].;
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;
            if (i >= 0)
            {
                textBox1.Enabled = textBox2.Enabled = textBox3.Enabled = textBox4.Enabled = true;
                label1.Enabled = label2.Enabled = label3.Enabled = label4.Enabled = label5.Enabled = true;
                comboBox1.Enabled = true;

                textBox1.Text = maxAreas[i].ToString();
                textBox2.Text = minAngles[i].ToString();
                textBox3.Text = youngModuluses[i].ToString();
                textBox4.Text = poissonRatios[i].ToString();

                //comboBox1.SelectedIndex = (int)orders[i];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool t = false;
            if (domain!=null)
            {
                for (int i = 0; i < domain.SubDomainCount; i++)
                {
                    if(domain[i].MaxArea != maxAreas[i]||domain[i].MinAngle != minAngles[i]) t = true;
                    domain[i].MaxArea = maxAreas[i];
                    domain[i].MinAngle = minAngles[i];
                    domain[i].YoungModulus = youngModuluses[i];
                    domain[i].PoissonRatio = poissonRatios[i];
                  //  domain[i].Order = orders[i];
                }
                if (t) domain.Elements = null;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex>=0)
            {
                double.TryParse(textBox1.Text, out maxAreas[listBox1.SelectedIndex]);
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                double.TryParse(textBox2.Text, out minAngles[listBox1.SelectedIndex]);
            }
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                double.TryParse(textBox3.Text, out youngModuluses[listBox1.SelectedIndex]);
            }
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                double.TryParse(textBox4.Text, out poissonRatios[listBox1.SelectedIndex]);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;
            if (i >= 0)
            {
                int k = comboBox1.SelectedIndex;
                order[i] = comboBox1.SelectedText;
            }
        }


    }
}