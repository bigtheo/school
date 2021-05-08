namespace Scool_cash_manager
{
    partial class ListeItem
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListeItem));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel_color = new System.Windows.Forms.Panel();
            this.lbl_noms = new System.Windows.Forms.Label();
            this.lbl_section = new System.Windows.Forms.Label();
            this.lbl_classe = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel_color.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(10, 18);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(51, 43);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel_color
            // 
            this.panel_color.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.panel_color.Controls.Add(this.pictureBox1);
            this.panel_color.Location = new System.Drawing.Point(3, 4);
            this.panel_color.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel_color.Name = "panel_color";
            this.panel_color.Size = new System.Drawing.Size(72, 76);
            this.panel_color.TabIndex = 1;
            // 
            // lbl_noms
            // 
            this.lbl_noms.AutoSize = true;
            this.lbl_noms.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_noms.Location = new System.Drawing.Point(94, 4);
            this.lbl_noms.Name = "lbl_noms";
            this.lbl_noms.Size = new System.Drawing.Size(227, 25);
            this.lbl_noms.TabIndex = 2;
            this.lbl_noms.Text = "Get nom from datasource";
            // 
            // lbl_section
            // 
            this.lbl_section.AutoSize = true;
            this.lbl_section.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lbl_section.Location = new System.Drawing.Point(96, 29);
            this.lbl_section.Name = "lbl_section";
            this.lbl_section.Size = new System.Drawing.Size(50, 17);
            this.lbl_section.TabIndex = 3;
            this.lbl_section.Text = "Section";
            // 
            // lbl_classe
            // 
            this.lbl_classe.AutoSize = true;
            this.lbl_classe.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lbl_classe.Location = new System.Drawing.Point(96, 48);
            this.lbl_classe.Name = "lbl_classe";
            this.lbl_classe.Size = new System.Drawing.Size(43, 17);
            this.lbl_classe.TabIndex = 4;
            this.lbl_classe.Text = "classe";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Location = new System.Drawing.Point(85, 75);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(395, 1);
            this.panel2.TabIndex = 5;
            // 
            // ListeItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lbl_classe);
            this.Controls.Add(this.lbl_section);
            this.Controls.Add(this.lbl_noms);
            this.Controls.Add(this.panel_color);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ListeItem";
            this.Size = new System.Drawing.Size(491, 83);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel_color.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel_color;
        private System.Windows.Forms.Label lbl_noms;
        private System.Windows.Forms.Label lbl_section;
        private System.Windows.Forms.Label lbl_classe;
        private System.Windows.Forms.Panel panel2;
    }
}
