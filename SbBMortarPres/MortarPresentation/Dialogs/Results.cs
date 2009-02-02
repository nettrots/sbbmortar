using System;
using System.Windows.Forms;
using SbBMortar.SbB;

namespace MortarFEM
{
    public partial class Results : Form
    {
        private Domain domain;
        public Results(Domain domain)
        {
            xx = double.NaN;
            yy = double.NaN;
            this.domain = domain;
            InitializeComponent();
            ReInit();
            
        }

        private double avr(double []a)
        {
            int c = 0;
            double s = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if(!double.IsNaN(a[i]))
                {
                    s += a[i];
                    c++;
                }
            }
            s /= c;
            return s;
        }
        private void ReInit()
        {
            dg.Rows.Clear();
            dg.Columns.Clear();

            dg.Columns.Add("N", "N");
            dg.Columns[0].DividerWidth = 5;
            dg.Columns.Add("X", "X");
            dg.Columns.Add("Y", "Y");
            dg.Columns[2].DividerWidth = 5;
            dg.Columns.Add("U", "U");
            dg.Columns.Add("V", "V");
            dg.Columns[4].DividerWidth = 5;
            dg.Columns.Add("Exx", "Exx");
            dg.Columns.Add("Eyy", "Eyy");
            dg.Columns.Add("2Exy", "2Exy");
            dg.Columns[7].DividerWidth = 5;
            dg.Columns.Add("Sxx", "Sxx");
            dg.Columns.Add("Syy", "Syy");
            dg.Columns.Add("Sxy", "Sxy");



            foreach (Vertex v in domain.Vertexes)
            {
                if (!double.IsNaN(xx))
                    if (!(v.X == xx))
                        continue;
                if (!double.IsNaN(yy))
                    if (!(v.Y == yy))
                        continue;
                #region AddData

                double x = v.X, y = v.Y, exx = 0.0, eyy = 0.0, exy = 0.0, sxx = 0.0, syy = 0.0, sxy = 0.0;
                double[] t;
                int n1 = 0, n2 = 0, n3 = 0, n11 = 0, n12 = 0, n13 = 0;

                exx = domain.Exx(v);
                eyy = domain.Eyy(v);
                exy = domain.Exy(v);
                sxx = domain.Sxx(v);
                syy = domain.Syy(v);
                sxy = domain.Sxy(v);

                dg.Rows.Add(v.Number, v.X, v.Y, domain.U(v).ToString("e4"), domain.V(v).ToString("e4"),
                            exx.ToString("e4"), eyy.ToString("e4"), exy.ToString("e4"), sxx.ToString("e4"),
                            syy.ToString("e4"), sxy.ToString("e4"));
                #endregion
            }
        }
        private double xx = double.NaN, yy = double.NaN;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            xx = double.Parse(toolStripTextBox1.Text);
            yy = double.NaN;
            ReInit();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            yy = double.Parse(toolStripTextBox2.Text);
            xx = double.NaN;
            ReInit();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            xx = double.NaN;
            yy = double.NaN;
            ReInit();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (dg.SelectionMode == DataGridViewSelectionMode.CellSelect)
                dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            else dg.SelectionMode = DataGridViewSelectionMode.CellSelect;
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStripButton1_Click(this, null);
            }
            
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStripButton2_Click(this, null);
            }
        }

     
    }
}