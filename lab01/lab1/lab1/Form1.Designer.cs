namespace BakeryApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblBakeryStyle = new System.Windows.Forms.Label();
            this.cmbBakeryStyle = new System.Windows.Forms.ComboBox();
            this.lblBreadType = new System.Windows.Forms.Label();
            this.cmbBreadType = new System.Windows.Forms.ComboBox();
            this.btnOrder = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.pnlLog = new System.Windows.Forms.Panel();
            this.pnlControls.SuspendLayout();
            this.pnlLog.SuspendLayout();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(280, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🥐 Пекарня Factory Method";

            // lblBakeryStyle
            this.lblBakeryStyle.AutoSize = true;
            this.lblBakeryStyle.Location = new System.Drawing.Point(20, 70);
            this.lblBakeryStyle.Name = "lblBakeryStyle";
            this.lblBakeryStyle.Size = new System.Drawing.Size(120, 15);
            this.lblBakeryStyle.TabIndex = 1;
            this.lblBakeryStyle.Text = "Стиль пекарни:";

            // cmbBakeryStyle
            this.cmbBakeryStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBakeryStyle.FormattingEnabled = true;
            this.cmbBakeryStyle.Items.AddRange(new object[] { "French", "American" });
            this.cmbBakeryStyle.Location = new System.Drawing.Point(150, 67);
            this.cmbBakeryStyle.Name = "cmbBakeryStyle";
            this.cmbBakeryStyle.Size = new System.Drawing.Size(150, 23);
            this.cmbBakeryStyle.TabIndex = 2;

            // lblBreadType
            this.lblBreadType.AutoSize = true;
            this.lblBreadType.Location = new System.Drawing.Point(20, 110);
            this.lblBreadType.Name = "lblBreadType";
            this.lblBreadType.Size = new System.Drawing.Size(120, 15);
            this.lblBreadType.TabIndex = 3;
            this.lblBreadType.Text = "Тип выпечки:";

            // cmbBreadType
            this.cmbBreadType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBreadType.FormattingEnabled = true;
            this.cmbBreadType.Items.AddRange(new object[] { "Baguette", "Croissant", "Brioche", "Sourdough" });
            this.cmbBreadType.Location = new System.Drawing.Point(150, 107);
            this.cmbBreadType.Name = "cmbBreadType";
            this.cmbBreadType.Size = new System.Drawing.Size(150, 23);
            this.cmbBreadType.TabIndex = 4;

            // btnOrder
            this.btnOrder.BackColor = System.Drawing.Color.FromArgb(255, 193, 7);
            this.btnOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrder.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnOrder.Location = new System.Drawing.Point(23, 150);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(277, 40);
            this.btnOrder.TabIndex = 5;
            this.btnOrder.Text = "🛒 Заказать";
            this.btnOrder.UseVisualStyleBackColor = false;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);

            // txtLog
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtLog.Location = new System.Drawing.Point(0, 0);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.TabIndex = 6;

            // lblResult
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblResult.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
            this.lblResult.Location = new System.Drawing.Point(20, 210);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(0, 19);
            this.lblResult.TabIndex = 7;

            // pnlControls
            this.pnlControls.Controls.Add(this.lblTitle);
            this.pnlControls.Controls.Add(this.lblBakeryStyle);
            this.pnlControls.Controls.Add(this.cmbBakeryStyle);
            this.pnlControls.Controls.Add(this.lblBreadType);
            this.pnlControls.Controls.Add(this.cmbBreadType);
            this.pnlControls.Controls.Add(this.btnOrder);
            this.pnlControls.Controls.Add(this.lblResult);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControls.Location = new System.Drawing.Point(0, 0);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Padding = new System.Windows.Forms.Padding(20);
            this.pnlControls.Size = new System.Drawing.Size(500, 250);
            this.pnlControls.TabIndex = 8;

            // pnlLog
            this.pnlLog.Controls.Add(this.txtLog);
            this.pnlLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLog.Location = new System.Drawing.Point(0, 250);
            this.pnlLog.Name = "pnlLog";
            this.pnlLog.Padding = new System.Windows.Forms.Padding(20, 10, 20, 20);
            this.pnlLog.Size = new System.Drawing.Size(500, 200);
            this.pnlLog.TabIndex = 9;

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 450);
            this.Controls.Add(this.pnlLog);
            this.Controls.Add(this.pnlControls);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "🥐 Пекарня - Factory Method Demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.pnlLog.ResumeLayout(false);
            this.pnlLog.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblBakeryStyle;
        private System.Windows.Forms.ComboBox cmbBakeryStyle;
        private System.Windows.Forms.Label lblBreadType;
        private System.Windows.Forms.ComboBox cmbBreadType;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.Panel pnlLog;
    }
}