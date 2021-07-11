
namespace MuhStok
{
    partial class Licence
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Licence));
            this.radTextBox1 = new Telerik.WinControls.UI.RadTextBox();
            this.ıconButton7 = new FontAwesome.Sharp.IconButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.crystalDarkTheme1 = new Telerik.WinControls.Themes.CrystalDarkTheme();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            this.SuspendLayout();
            // 
            // radTextBox1
            // 
            this.radTextBox1.Location = new System.Drawing.Point(283, 115);
            this.radTextBox1.Name = "radTextBox1";
            this.radTextBox1.Size = new System.Drawing.Size(439, 24);
            this.radTextBox1.TabIndex = 7;
            this.radTextBox1.ThemeName = "CrystalDark";
            // 
            // ıconButton7
            // 
            this.ıconButton7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ıconButton7.FlatAppearance.BorderSize = 0;
            this.ıconButton7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ıconButton7.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.ıconButton7.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.ıconButton7.IconColor = System.Drawing.SystemColors.Control;
            this.ıconButton7.IconSize = 20;
            this.ıconButton7.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ıconButton7.Location = new System.Drawing.Point(812, 0);
            this.ıconButton7.Name = "ıconButton7";
            this.ıconButton7.Rotation = 0D;
            this.ıconButton7.Size = new System.Drawing.Size(34, 28);
            this.ıconButton7.TabIndex = 6;
            this.ıconButton7.UseVisualStyleBackColor = true;
            this.ıconButton7.Click += new System.EventHandler(this.ıconButton7_Click);
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(129, 119);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(148, 20);
            this.radLabel1.TabIndex = 8;
            this.radLabel1.Text = "Lisans Anahtarını Girin:";
            this.radLabel1.ThemeName = "CrystalDark";
            // 
            // radButton1
            // 
            this.radButton1.Location = new System.Drawing.Point(387, 172);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(159, 44);
            this.radButton1.TabIndex = 9;
            this.radButton1.Text = "Kontrol Et";
            this.radButton1.ThemeName = "CrystalDark";
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // Licence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(23)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(844, 305);
            this.Controls.Add(this.radButton1);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.radTextBox1);
            this.Controls.Add(this.ıconButton7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Licence";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Licence";
            this.Load += new System.EventHandler(this.Licence_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Licence_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.radTextBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontAwesome.Sharp.IconButton ıconButton7;
        private Telerik.WinControls.UI.RadTextBox radTextBox1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.Themes.CrystalDarkTheme crystalDarkTheme1;
        private Telerik.WinControls.UI.RadButton radButton1;
    }
}