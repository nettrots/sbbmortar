
using Tao.Platform.Windows;


namespace MortarPresentation
{
    public partial class GlMortar : SimpleOpenGlControl
    {
        private SbBglDrawer drawer;

        public GlMortar()
        {
            InitializeComponent();
            Drawer = new SbBglDrawer(Size);
        }


        public SbBglDrawer Drawer
        {
            get { return drawer; }
            set { drawer = value; }
        }

        private void GlMortar_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            drawer.drawScene();
        }

        private void GlMortar_SizeChanged(object sender, System.EventArgs e)
        {
            drawer.WindowSize = Size;
        }
    }
}
