namespace TextEditorApp
{
    partial class Form1
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
            this.textEditor1 = new TextEditorApp.CustomControl.TextEditor();
            this.SuspendLayout();
            // 
            // textEditor1
            // 
            this.textEditor1.AllowDragDropTextFile = true;
            this.textEditor1.BodyContentText = "Default text";
            this.textEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEditor1.LabelResultText = "";
            this.textEditor1.Location = new System.Drawing.Point(0, 0);
            this.textEditor1.Name = "textEditor1";
            this.textEditor1.OpenButtonText = "Custom Open";
            this.textEditor1.SaveButtonText = "Custom Save";
            this.textEditor1.ShowOpenButton = true;
            this.textEditor1.ShowSaveButton = true;
            this.textEditor1.Size = new System.Drawing.Size(647, 533);
            this.textEditor1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 533);
            this.Controls.Add(this.textEditor1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControl.TextEditor textEditor1;
    }
}

