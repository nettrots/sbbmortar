using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using MortarFEM;
using MortarFEM.Dialogs;
using MortarPresentation.Dialogs;
using SbBMortar.SbB;

namespace MortarPresentation
{
    public partial class MortarPresentation : Form
    {
        GlMortar glMortar=new GlMortar();
        glGraphic glGraphic=new glGraphic();
        glGrid grid = new glGrid();
        private List<Domain> domains;
        private Domain domain;
        private string state="Domain not loaded";

        public MortarPresentation()
        {
            InitializeComponent();
            panel1.Controls.Add(glMortar);
            glMortar.Dock = DockStyle.Fill;
            glMortar.InitializeContexts();
            glMortar.Drawer.drawScene();
            domains=new List<Domain>();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void addEvents()
        {
            domain.onDomainLoaded += delegate
                                         {
                                             progStatus.Text = "Domain Loaded.";
                                         };
            domain.onSolveComplete += delegate
                                          {
                                              progStatus.Text = "Solve Completed.";
                                          };
            domain.onTriangComplete += delegate
                                           {
                                               progStatus.Text = "Triangulation Completed.";
                                           };
            domain.onCreateGlobalMatrix_Complete += delegate
                                                        {
                                                            progStatus.Text = "Create Global Matrix Completed,Creating Vector Matrix...";
                                                        };
            domain.onCreateGlobalVector_Complete += delegate
                                                        {
                                                            progStatus.Text = "Create Global Vector Completed,Solving system...";
                                                        };
            domain.onCreateK_Complete += delegate
                                             {
                                                 progStatus.Text = "Create Matrix K Completed,Creating D... ";
                                             };
            domain.onCreateD_Complete += delegate
                                             {
                                                 progStatus.Text = "Create Matrix D Completed,Creating Global Matrix... ";
                                             };
            return;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                domain = new Domain(ofd.FileName);
                domains.Add(domain);
                addEvents();
                progStatus.Text = "Domain Loaded.";

                glMortar.Drawer.Elemenst.Clear();
                glDomain glDomain = new glDomain(domain);
                glMortar.Drawer.Elemenst.Add(glDomain);

                //grid = new glGrid();
                //glMortar.Drawer.Elemenst.Add(grid);

                glMortar.Invalidate();
            }
        }

        private void trianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (domain.Polygon == null) return;
            if(domain.Elements==null)
            domain.mesh();

            if (domain.Elements.Count == 0) return;

            glMortar.Drawer.Elemenst.Clear();
            glTriangulation glTrig=new glTriangulation(domain.Elements);
            glMortar.Drawer.Elemenst.Add(glTrig);
            grid = new glGrid();
            //glMortar.Drawer.Elemenst.Add(grid);
            glMortar.Invalidate();

        }

        private void subdomainsSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(domain==null)return;
            SubDomainDialog dialog=new SubDomainDialog(domain);
            dialog.ShowDialog();
            glMortar.Invalidate();
        }

        private void boundarySettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (domain == null) return;
            BoundaryDialog dialog=new BoundaryDialog(domain);
            dialog.ShowDialog();
            glMortar.Invalidate();
        }

        private void functionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (domain.Result == null) return;
         
            glMortar.Drawer.Elemenst.Clear();
            if (glGraphic == null) glGraphic=new glGraphic();

            
            grid=new glGrid();
            glMortar.Drawer.Elemenst.Add(grid);
            glMortar.Drawer.Elemenst.Add(glGraphic);
            glMortar.Drawer.parseProperties();
            glMortar.Drawer.drawScene();
            glMortar.Invalidate();
            DrawGraphics dialog=new DrawGraphics(domain,glGraphic,glMortar);

            dialog.ShowDialog();
            glMortar.Drawer.parseProperties();
            glMortar.Drawer.drawScene();
            
        }

        void doit()
        {
            domain.mesh();
            domain.solve();
        }
        private void solveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread tr = new Thread(doit);
            tr.Start();
        }

        private void drawingSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (domain == null) return;
            DrawingElementsDialog dialog=new DrawingElementsDialog(glMortar.Drawer);
            dialog.ShowDialog();

        }

        private void mortarSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (domain == null) return;
            InsideBoundaryDialog dialog=new InsideBoundaryDialog(domain);
            dialog.ShowDialog();
        }

        private void tableOfResulstToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (domain == null||domain.Elements==null||domain.Result==null) return;
            Results results=new Results(domain);
            results.Show();
        }


    }
}
