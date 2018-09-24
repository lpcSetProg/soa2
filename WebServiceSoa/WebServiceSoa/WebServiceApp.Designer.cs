namespace WebServiceSoa
{
    partial class WebServiceApp
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
            this.comboBox_webServiceSelector = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboBox_webServiceSelector
            // 
            this.comboBox_webServiceSelector.FormattingEnabled = true;
            this.comboBox_webServiceSelector.Location = new System.Drawing.Point(39, 55);
            this.comboBox_webServiceSelector.Name = "comboBox_webServiceSelector";
            this.comboBox_webServiceSelector.Size = new System.Drawing.Size(196, 21);
            this.comboBox_webServiceSelector.TabIndex = 0;
            this.comboBox_webServiceSelector.SelectedIndexChanged += new System.EventHandler(this.comboBox_webServiceSelector_SelectedIndexChanged);
            // 
            // WebServiceApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.comboBox_webServiceSelector);
            this.Name = "WebServiceApp";
            this.Text = "Web Services ";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_webServiceSelector;
    }
}

