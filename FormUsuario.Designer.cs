namespace SistemaCalculoSueldos
{
    partial class FormUsuario
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
            this.txtRutUsuario = new System.Windows.Forms.TextBox();
            this.btnConsultarSueldo = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtRutUsuario
            // 
            this.txtRutUsuario.Location = new System.Drawing.Point(299, 126);
            this.txtRutUsuario.Name = "txtRutUsuario";
            this.txtRutUsuario.Size = new System.Drawing.Size(137, 20);
            this.txtRutUsuario.TabIndex = 0;
            // 
            // btnConsultarSueldo
            // 
            this.btnConsultarSueldo.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnConsultarSueldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsultarSueldo.Location = new System.Drawing.Point(171, 81);
            this.btnConsultarSueldo.Name = "btnConsultarSueldo";
            this.btnConsultarSueldo.Size = new System.Drawing.Size(102, 65);
            this.btnConsultarSueldo.TabIndex = 1;
            this.btnConsultarSueldo.Text = "Ingresar Rut Empleado";
            this.btnConsultarSueldo.UseVisualStyleBackColor = false;
            this.btnConsultarSueldo.Click += new System.EventHandler(this.btnConsultarSueldo_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnVolver.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnVolver.Location = new System.Drawing.Point(45, 34);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(75, 23);
            this.btnVolver.TabIndex = 2;
            this.btnVolver.Text = "Atrás";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // FormUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 261);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.btnConsultarSueldo);
            this.Controls.Add(this.txtRutUsuario);
            this.Name = "FormUsuario";
            this.Text = "Consulta para usuarios";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRutUsuario;
        private System.Windows.Forms.Button btnConsultarSueldo;
        private System.Windows.Forms.Button btnVolver;
    }
}