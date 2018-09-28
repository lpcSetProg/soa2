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
            this.comboBox_methodSelector = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox_services = new System.Windows.Forms.GroupBox();
            this.groupBox_request = new System.Windows.Forms.GroupBox();
            this.groupBox_response = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.groupBox_services.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_webServiceSelector
            // 
            this.comboBox_webServiceSelector.FormattingEnabled = true;
            this.comboBox_webServiceSelector.Location = new System.Drawing.Point(9, 43);
            this.comboBox_webServiceSelector.Name = "comboBox_webServiceSelector";
            this.comboBox_webServiceSelector.Size = new System.Drawing.Size(196, 21);
            this.comboBox_webServiceSelector.TabIndex = 0;
            this.comboBox_webServiceSelector.SelectedIndexChanged += new System.EventHandler(this.comboBox_webServiceSelector_SelectedIndexChanged);
            // 
            // comboBox_methodSelector
            // 
            this.comboBox_methodSelector.FormattingEnabled = true;
            this.comboBox_methodSelector.Location = new System.Drawing.Point(9, 101);
            this.comboBox_methodSelector.Name = "comboBox_methodSelector";
            this.comboBox_methodSelector.Size = new System.Drawing.Size(196, 21);
            this.comboBox_methodSelector.TabIndex = 1;
            this.comboBox_methodSelector.SelectedIndexChanged += new System.EventHandler(this.comboBox_methodSelector_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Web Service";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Methods";
            // 
            // groupBox_services
            // 
            this.groupBox_services.Controls.Add(this.comboBox_webServiceSelector);
            this.groupBox_services.Controls.Add(this.label2);
            this.groupBox_services.Controls.Add(this.comboBox_methodSelector);
            this.groupBox_services.Controls.Add(this.label1);
            this.groupBox_services.Location = new System.Drawing.Point(12, 12);
            this.groupBox_services.Name = "groupBox_services";
            this.groupBox_services.Size = new System.Drawing.Size(227, 489);
            this.groupBox_services.TabIndex = 4;
            this.groupBox_services.TabStop = false;
            this.groupBox_services.Text = "Services";
            // 
            // groupBox_request
            // 
            this.groupBox_request.Location = new System.Drawing.Point(276, 12);
            this.groupBox_request.Name = "groupBox_request";
            this.groupBox_request.Size = new System.Drawing.Size(232, 489);
            this.groupBox_request.TabIndex = 5;
            this.groupBox_request.TabStop = false;
            this.groupBox_request.Text = "Request";
            // 
            // groupBox_response
            // 
            this.groupBox_response.Location = new System.Drawing.Point(549, 12);
            this.groupBox_response.Name = "groupBox_response";
            this.groupBox_response.Size = new System.Drawing.Size(227, 489);
            this.groupBox_response.TabIndex = 6;
            this.groupBox_response.TabStop = false;
            this.groupBox_response.Text = "Response";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(807, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(537, 152);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(807, 180);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(537, 146);
            this.richTextBox2.TabIndex = 1;
            this.richTextBox2.Text = "";
            // 
            // richTextBox3
            // 
            this.richTextBox3.Location = new System.Drawing.Point(807, 355);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(537, 146);
            this.richTextBox3.TabIndex = 7;
            this.richTextBox3.Text = "";
            // 
            // WebServiceApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1382, 513);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.groupBox_response);
            this.Controls.Add(this.groupBox_request);
            this.Controls.Add(this.groupBox_services);
            this.Name = "WebServiceApp";
            this.Text = "Web Services ";
            this.groupBox_services.ResumeLayout(false);
            this.groupBox_services.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_webServiceSelector;
        private System.Windows.Forms.ComboBox comboBox_methodSelector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox_services;
        private System.Windows.Forms.GroupBox groupBox_request;
        private System.Windows.Forms.GroupBox groupBox_response;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox3;
    }
}

