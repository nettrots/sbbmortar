using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SbB.FEM;
using SbB.Geometry;

namespace MortarFEM
{
    public partial class Results : Form
    {
        private Manager manager;
        public Results(Manager manager)
        {
            xx = double.NaN;
            yy = double.NaN;
            this.manager = manager;
            InitializeComponent();
            ReInit();
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



            foreach (Vertex v in manager.Gs.Vertexes)
            {
                if (!double.IsNaN(xx))
                    if (!(v.X == xx))
                        continue;
                if (!double.IsNaN(yy))
                    if (!(v.Y == yy))
                        continue;
                #region AddData

                double x = v.X, y = v.Y, exx = 0.0, eyy = 0.0, exy = 0.0, sxx = 0.0, syy = 0.0, sxy = 0.0, t;
                int n1 = 0, n2 = 0, n3 = 0, n11 = 0, n12 = 0, n13 = 0;
                for (int i = 0; i < manager.Gs.Femdatas.Length; i++)
                {
                    t = manager.Exx(x, y)[i];
                    if (!double.IsNaN(t))
                    {
                        exx += t;
                        n1++;
                    }
                    t = manager.Eyy(x, y)[i];
                    if (!double.IsNaN(t))
                    {
                        eyy += t;
                        n2++;
                    }

                    t = manager.Exy(x, y)[i];
                    if (!double.IsNaN(t))
                    {
                        exy += t;
                        n3++;
                    }

                    t = manager.Sxx(x, y)[i];
                    if (!double.IsNaN(t))
                    {
                        sxx += t;
                        n11++;
                    }
                    t = manager.Syy(x, y)[i];
                    if (!double.IsNaN(t))
                    {
                        syy += t;
                        n12++;
                    }
                    t = manager.Sxy(x, y)[i];
                    if (!double.IsNaN(t))
                    {
                        sxy += t;
                        n13++;
                    }
                }
                exx /= n1;
                eyy /= n2;
                exy /= n3;
                sxx /= n11;
                syy /= n12;
                sxy /= n13;
                dg.Rows.Add(v.Number, v.X, v.Y, manager.u(v).ToString("e4"), manager.v(v).ToString("e4"),
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
    }
}