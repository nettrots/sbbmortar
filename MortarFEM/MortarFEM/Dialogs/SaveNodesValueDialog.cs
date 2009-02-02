using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SbB;
using SbB.FEM;
using SbB.Geometry;

namespace MortarFEM.Dialogs
{
    public partial class SaveNodesValueDialog : Form
    {
        private GlobalSystem gs;
        public SaveNodesValueDialog(GlobalSystem gs)
        {
            this.gs = gs;
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                textBox3.Text = saveFileDialog1.FileName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("The name of files is not set!", "MortarFEM",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double x = double.Parse(textBox1.Text);
            double y = double.Parse(textBox2.Text);
            string file = textBox3.Text;
            string[] names = {"_U", "_V"};
            StreamWriter[][] sws = new StreamWriter[3][] {new StreamWriter[names.Length],
                                                          new StreamWriter[names.Length],
                                                          new StreamWriter[names.Length]};
            for (int i=0; i<names.Length; i++)
            {
                if (checkBox1.Checked)
                    sws[0][i] = File.CreateText(file + names[i] + "(x=" + x+").txt");
                if (checkBox2.Checked)
                    sws[1][i] = File.CreateText(file + names[i] + "(y=" + y + ").txt");
                if (checkBox3.Checked)
                    sws[2][i] = File.CreateText(file + names[i] + ".txt");
            }

            foreach (Vertex v in gs.Vertexes)
            {
                if (checkBox1.Checked && Math.Abs(v.X-x)<Constants.EPS)
                {
                    sws[0][0].WriteLine("x = {0}\ty = {1}\t\t{2}", v.X, v.Y, gs.Result[2*v.Number]);
                    sws[0][1].WriteLine("x = {0}\ty = {1}\t\t{2}", v.X, v.Y, gs.Result[2*v.Number + 1]);
                }
                if (checkBox2.Checked && Math.Abs(v.Y - y) < Constants.EPS)
                {
                    sws[1][0].WriteLine("x = {0}\ty = {1}\t\t{2}", v.X, v.Y, gs.Result[2 * v.Number]);
                    sws[1][1].WriteLine("x = {0}\ty = {1}\t\t{2}", v.X, v.Y, gs.Result[2 * v.Number + 1]);
                }
                if (checkBox3.Checked)
                {
                    sws[2][0].WriteLine("x = {0}\ty = {1}\t\t{2}", v.X, v.Y, gs.Result[2 * v.Number]);
                    sws[2][1].WriteLine("x = {0}\ty = {1}\t\t{2}", v.X, v.Y, gs.Result[2 * v.Number + 1]);
                }
            }

            for (int i = 0; i < names.Length; i++)
            {
                if (checkBox1.Checked)
                    sws[0][i].Close();
                if (checkBox2.Checked)
                    sws[1][i].Close();
                if (checkBox3.Checked)
                    sws[2][i].Close();
            }
        }
    }
}