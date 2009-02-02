
using System;
using System.Windows.Forms;

namespace MortarPresentation.Dialogs
{
    public partial class DrawingElementsDialog : Form
    {
        private SbBglDrawer drawer;
        public DrawingElementsDialog(SbBglDrawer drawer)
        {
            InitializeComponent();
            this.drawer = drawer;
            foreach (DrawingElement element in drawer.Elemenst)
            {
                listBox1.Items.Add(element);
            }
           
        }

        private void listBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int ind = listBox1.SelectedIndex;
            object obj = listBox1.SelectedItem;
            TextBox tb=new TextBox();
            conteiner.Controls.Add(tb);
            tb.Dock = DockStyle.Top;
            
//            tb.Text = (2).ToString();
            if (obj.GetType() == typeof(glDomain))
            {
                glDomain glDomain = (glDomain) obj;
                CheckBox cb = new CheckBox();
                cb.Text = "Hide";
                cb.Checked = glDomain.Hide;
             
                conteiner.Controls.Add(cb);
                cb.Dock = DockStyle.Top;
            }
            if (obj.GetType() == typeof(glTriangulation))
            {
                glTriangulation glTriang = (glTriangulation)obj;
                CheckBox cb = new CheckBox();
                cb.Text = "Hide";
                cb.Checked = glTriang.Hide;

                CheckBox cb1 = new CheckBox();
                cb1.Text = "Show Numbers";
                cb.Checked =(bool) glTriang.Props["Numbers"];

                conteiner.Controls.Add(cb);
                conteiner.Controls.Add(cb1);

                cb.Dock = DockStyle.Top;
                cb1.Dock = DockStyle.Top;
                //System.Reflection.
            }
        }
    }
}
