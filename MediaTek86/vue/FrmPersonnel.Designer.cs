namespace MediaTek86.vue
{
    partial class FrmPersonnel
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.Label lblPrenom;
        private System.Windows.Forms.Label lblTel;
        private System.Windows.Forms.Label lblMail;
        private System.Windows.Forms.Label lblService;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.TextBox txtPrenom;
        private System.Windows.Forms.TextBox txtTel;
        private System.Windows.Forms.TextBox txtMail;
        private System.Windows.Forms.ComboBox cboService;
        private System.Windows.Forms.Button btnEnregistrer;
        private System.Windows.Forms.Button btnAnnuler;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblNom = new System.Windows.Forms.Label();
            this.lblPrenom = new System.Windows.Forms.Label();
            this.lblTel = new System.Windows.Forms.Label();
            this.lblMail = new System.Windows.Forms.Label();
            this.lblService = new System.Windows.Forms.Label();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.txtPrenom = new System.Windows.Forms.TextBox();
            this.txtTel = new System.Windows.Forms.TextBox();
            this.txtMail = new System.Windows.Forms.TextBox();
            this.cboService = new System.Windows.Forms.ComboBox();
            this.btnEnregistrer = new System.Windows.Forms.Button();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // labels
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(30, 30);
            this.lblNom.Text = "Nom :";
            this.lblPrenom.AutoSize = true;
            this.lblPrenom.Location = new System.Drawing.Point(30, 70);
            this.lblPrenom.Text = "Prénom :";
            this.lblTel.AutoSize = true;
            this.lblTel.Location = new System.Drawing.Point(30, 110);
            this.lblTel.Text = "Téléphone :";
            this.lblMail.AutoSize = true;
            this.lblMail.Location = new System.Drawing.Point(30, 150);
            this.lblMail.Text = "Mail :";
            this.lblService.AutoSize = true;
            this.lblService.Location = new System.Drawing.Point(30, 190);
            this.lblService.Text = "Service :";
            // textboxes
            this.txtNom.Location = new System.Drawing.Point(140, 27);
            this.txtNom.Size = new System.Drawing.Size(200, 20);
            this.txtPrenom.Location = new System.Drawing.Point(140, 67);
            this.txtPrenom.Size = new System.Drawing.Size(200, 20);
            this.txtTel.Location = new System.Drawing.Point(140, 107);
            this.txtTel.Size = new System.Drawing.Size(200, 20);
            this.txtMail.Location = new System.Drawing.Point(140, 147);
            this.txtMail.Size = new System.Drawing.Size(200, 20);
            // combobox
            this.cboService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboService.Location = new System.Drawing.Point(140, 187);
            this.cboService.Size = new System.Drawing.Size(200, 21);
            // boutons
            this.btnEnregistrer.Location = new System.Drawing.Point(140, 240);
            this.btnEnregistrer.Size = new System.Drawing.Size(100, 30);
            this.btnEnregistrer.Text = "Enregistrer";
            this.btnEnregistrer.UseVisualStyleBackColor = true;
            this.btnEnregistrer.Click += new System.EventHandler(this.BtnEnregistrer_Click);
            this.btnAnnuler.Location = new System.Drawing.Point(250, 240);
            this.btnAnnuler.Size = new System.Drawing.Size(90, 30);
            this.btnAnnuler.Text = "Annuler";
            this.btnAnnuler.UseVisualStyleBackColor = true;
            this.btnAnnuler.Click += new System.EventHandler(this.BtnAnnuler_Click);
            // 
            // FrmPersonnel
            // 
            this.AcceptButton = this.btnEnregistrer;
            this.ClientSize = new System.Drawing.Size(384, 301);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnEnregistrer);
            this.Controls.Add(this.cboService);
            this.Controls.Add(this.txtMail);
            this.Controls.Add(this.txtTel);
            this.Controls.Add(this.txtPrenom);
            this.Controls.Add(this.txtNom);
            this.Controls.Add(this.lblService);
            this.Controls.Add(this.lblMail);
            this.Controls.Add(this.lblTel);
            this.Controls.Add(this.lblPrenom);
            this.Controls.Add(this.lblNom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPersonnel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Personnel";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}