namespace MortarPresentation
{
    partial class GlMortar
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GlMortar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.Name = "GlMortar";
            this.Size = new System.Drawing.Size(332, 281);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GlMortar_Paint);
            this.SizeChanged += new System.EventHandler(this.GlMortar_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
