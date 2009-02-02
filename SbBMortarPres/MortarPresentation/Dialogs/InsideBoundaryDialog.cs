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
    public partial class InsideBoundaryDialog : Form
    {
        private Domain domain;
        private bool[] changed;
        private string[] mortars;
        private string[] nonmortars;

        public InsideBoundaryDialog(Domain domain)
        {
            InitializeComponent();
            changed =new bool[domain.MortarSides.Length]; 
            for (int i = 0; i < domain.MortarSides.Length; i++)
            {
                string s = "";
                foreach (SubDomain mortar in domain.MortarSides[i].Mortars)
                {
                    s += mortar.N+",";
                }
                s = s.Remove(s.Length - 1);
                s += "-";
                foreach (SubDomain mortar in domain.MortarSides[i].Nonmortars)
                {
                    s += mortar.N + ",";
                }
                s = s.Remove(s.Length - 1);
                listBox1.Items.Add(s);
                changed[i] = false;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            int i = listBox1.SelectedIndex;
            if(i>=0)
            {
                if(!textBox1.Enabled)
                    foreach (Control c in this.groupBox2.Controls)
                    {
                        c.Enabled = true;
                    }
                string[] s = ((string) listBox1.SelectedItem).Split('-');
                textBox1.Text = s[0];
                textBox2.Text = s[1];
                //SubDomain[] temp = domain.MortarSides[i].Nonmortars;
                //domain.MortarSides[i].Nonmortars = domain.MortarSides[i].Mortars;
                //domain.MortarSides[i].Mortars = temp;
                //listBox1.SelectedItem=s[1]+"-"+s[0];
                
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;
            if (i>=0)
            {
                changed[i] = !changed[i];

                string[] s = ((string)listBox1.SelectedItem).Split('-');

                listBox1.Items[i] = s[1] + "-" + s[0];
                textBox1.Text = s[1];
                textBox2.Text = s[0];
                
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < changed.Length; i++) 
                if (changed[i])
                {
                    SubDomain[] temp = domain.MortarSides[i].Nonmortars;
                    domain.MortarSides[i].Nonmortars = domain.MortarSides[i].Mortars;
                    domain.MortarSides[i].Mortars = temp;
                }
            
        }
    }
}