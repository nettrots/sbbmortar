using System;

using System.Windows.Forms;
using SbBMortar.SbB;
using System.Collections.Generic;

namespace MortarPresentation.Dialogs
{
    public delegate double Fxy(double x, double y);

    public partial class DrawGraphics : Form
    {
        private Domain domain;
        private glGraphic glGraphic;
        private GlMortar glMortar;
        public DrawGraphics(Domain domain, glGraphic glGraphic, GlMortar glMortar)
        {
            InitializeComponent();
            this.domain = domain;
            this.glMortar = glMortar;
            this.glGraphic = glGraphic;
            for (int i = 0; i < glGraphic.FuncTables.Count; i++)
            {
                listBox1.Items.Add(glGraphic.Names[i]);
            }
            funcList.Items.AddRange(new string[] { "U", "V" });
            funcList.SelectedIndex = 0;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled =!comboBox1.Enabled;
        }

        private void radioButtons_CheckChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton) sender;
            funcList.Items.Clear();
            switch(rb.Text)
            {
                case "Переміщення":
                    funcList.Items.AddRange(new string[]{"U","V"});
                    break;
                case "Деформації":
                    funcList.Items.AddRange(new string[] { "Exx", "2Exy","Eyy" });
                    break;
                case "Напруження":
                    funcList.Items.AddRange(new string[] { "Sxx", "Sxy", "Syy", });
                    break;
                
            }
            funcList.SelectedIndex= 0;
           
        }

      

        private void AddToList_Click(object sender, EventArgs e)
        {
          
            int number =(int) dotsNum.Value;
            double arg = 0;
            try
            {
                arg = double.Parse(textBox1.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error with input number");
                return;
            }

         
            Functions fxy=Functions.V;
            switch ((string)funcList.SelectedItem)
            {
                case "U":
                    fxy = Functions.U;
                    break;
                case "V":
                    fxy = Functions.V;
                    break;
                case "Exx":
                    fxy = Functions.Exx;
                    break;
                case "2Exy":
                    fxy = Functions.Exy;
                    
                    break;
                case "Eyy":
                    fxy = Functions.Eyy;
                    
                    break;
                case "Sxx":
                    fxy = Functions.Sxx;
                    
                    break;
                case "Sxy":
                    fxy = Functions.Sxy;
                    
                    break;
                case "Syy":
                    fxy = Functions.Syy;
                    
                    break;
                default:    
                    break;
            }

            List<Vertex>[] rez=new List<Vertex>[0];
            string s1="";
            if (radioButtonX.Checked)
            {
                rez = domain.getFuncArrX(fxy, arg, number);
                s1 = "x";
            }
            if (radioButtonY.Checked)
            {
                rez = domain.getFuncArrY(fxy, arg, number);
                s1 = "y";
            }


            int n, n1 = glGraphic.FuncTables.Count; ;
            
            glGraphic.FuncTables.Add(rez);
            glGraphic.DoFs.Add(domain.Result.Length);

            if (glGraphic.FuncTables == null)throw new Exception("Some problems(no vertexex added to draw)");

            if (checkBox1.Checked)
                glGraphic.Names.Add(textBox2.Text);
            else
            {
                string sss="|";
                for (int i = 0; i < domain.SubDomainCount; i++)
                {
                    sss+=domain[i].MaxArea;
                    sss += "'";
                }
               // sss = domain[0].MaxArea.ToString();
                glGraphic.Names.Add((string) funcList.SelectedItem + "|" + s1 + "=" + arg + "|" + domain.ToString()+sss);
            }
            listBox1.Items.Add(glGraphic.Names[glGraphic.Names.Count-1]);
            
            glMortar.Drawer.parseProperties();
            glMortar.Drawer.drawScene();
            glMortar.Invalidate();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                glGraphic.FuncTables.RemoveAt(listBox1.SelectedIndex);
                glGraphic.Names.RemoveAt(listBox1.SelectedIndex);
                glGraphic.DoFs.RemoveAt(listBox1.SelectedIndex);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);

                

                glMortar.Drawer.parseProperties();
                glMortar.Drawer.drawScene();
                glMortar.Invalidate();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = checkBox1.Checked;
        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) this.deleteToolStripMenuItem_Click(this, null);
        }

      
      
    }
}
