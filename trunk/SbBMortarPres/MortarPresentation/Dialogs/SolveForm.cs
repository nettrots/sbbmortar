
using System.Windows.Forms;
using SbBMortar.SbB;

namespace MortarPresentation.Dialogs
{
    public partial class SolveForm : Form
    {
        private Domain domain;
        public SolveForm(Domain domain)
        {
            InitializeComponent();
            this.domain = domain;
        }

        private void SolveForm_Shown(object sender, System.EventArgs e)
        {
           
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void SolveForm_Load(object sender, System.EventArgs e)
        {
            domain.mesh();
            domain.solve();
        }
    }
}
