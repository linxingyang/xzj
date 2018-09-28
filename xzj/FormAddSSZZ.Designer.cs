namespace xzj
{
    partial class FormAddSSZZ
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
            this.panel126 = new System.Windows.Forms.Panel();
            this.label41 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpSSZZ_createDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.labSSZZ_Creator = new System.Windows.Forms.Label();
            this.panel126.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel126
            // 
            this.panel126.Controls.Add(this.labSSZZ_Creator);
            this.panel126.Controls.Add(this.label41);
            this.panel126.Location = new System.Drawing.Point(94, 43);
            this.panel126.Margin = new System.Windows.Forms.Padding(4);
            this.panel126.Name = "panel126";
            this.panel126.Size = new System.Drawing.Size(300, 43);
            this.panel126.TabIndex = 37;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(23, 9);
            this.label41.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(72, 16);
            this.label41.TabIndex = 1;
            this.label41.Text = "创建人：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtpSSZZ_createDate);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(94, 93);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 43);
            this.panel1.TabIndex = 38;
            // 
            // dtpSSZZ_createDate
            // 
            this.dtpSSZZ_createDate.CustomFormat = "yyyy年MM月dd月";
            this.dtpSSZZ_createDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSSZZ_createDate.Location = new System.Drawing.Point(98, 4);
            this.dtpSSZZ_createDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpSSZZ_createDate.Name = "dtpSSZZ_createDate";
            this.dtpSSZZ_createDate.ShowUpDown = true;
            this.dtpSSZZ_createDate.Size = new System.Drawing.Size(180, 26);
            this.dtpSSZZ_createDate.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "创建日期：";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 12F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(267, 144);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(91, 35);
            this.btnCancel.TabIndex = 42;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.DodgerBlue;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("宋体", 12F);
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(137, 144);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(91, 35);
            this.button4.TabIndex = 41;
            this.button4.Text = "添加";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // labSSZZ_Creator
            // 
            this.labSSZZ_Creator.AutoSize = true;
            this.labSSZZ_Creator.Location = new System.Drawing.Point(98, 9);
            this.labSSZZ_Creator.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labSSZZ_Creator.Name = "labSSZZ_Creator";
            this.labSSZZ_Creator.Size = new System.Drawing.Size(16, 16);
            this.labSSZZ_Creator.TabIndex = 2;
            this.labSSZZ_Creator.Text = "_";
            // 
            // FormAddSSZZ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(507, 221);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel126);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormAddSSZZ";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加手术追踪";
            this.Load += new System.EventHandler(this.FormAddSSZZ_Load);
            this.panel126.ResumeLayout(false);
            this.panel126.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel126;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DateTimePicker dtpSSZZ_createDate;
        private System.Windows.Forms.Label labSSZZ_Creator;

    }
}