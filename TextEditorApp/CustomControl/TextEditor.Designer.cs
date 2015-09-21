namespace TextEditorApp.CustomControl
{
    partial class TextEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextEditor));
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdBold = new System.Windows.Forms.ToolStripButton();
            this.cmdItalic = new System.Windows.Forms.ToolStripButton();
            this.cmdUnderline = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lstColors = new System.Windows.Forms.ToolStripDropDownButton();
            this.lstFonts = new System.Windows.Forms.ToolStripDropDownButton();
            this.lstSizes = new System.Windows.Forms.ToolStripDropDownButton();
            this.lblResult = new System.Windows.Forms.Label();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.rtbContent = new System.Windows.Forms.RichTextBox();
            this.openTextFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveTextFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Controls.Add(this.lblResult);
            this.panel1.Controls.Add(this.prgBar);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnOpen);
            this.panel1.Controls.Add(this.rtbContent);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(612, 504);
            this.panel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdBold,
            this.cmdItalic,
            this.cmdUnderline,
            this.toolStripSeparator1,
            this.lstColors,
            this.lstFonts,
            this.lstSizes});
            this.toolStrip1.Location = new System.Drawing.Point(5, 11);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(284, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cmdBold
            // 
            this.cmdBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cmdBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdBold.Name = "cmdBold";
            this.cmdBold.Size = new System.Drawing.Size(35, 22);
            this.cmdBold.Text = "Bold";
            this.cmdBold.Click += new System.EventHandler(this.cmdBold_Click);
            // 
            // cmdItalic
            // 
            this.cmdItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cmdItalic.Image = ((System.Drawing.Image)(resources.GetObject("cmdItalic.Image")));
            this.cmdItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdItalic.Name = "cmdItalic";
            this.cmdItalic.Size = new System.Drawing.Size(36, 22);
            this.cmdItalic.Text = "Italic";
            this.cmdItalic.Click += new System.EventHandler(this.cmdItalic_Click);
            // 
            // cmdUnderline
            // 
            this.cmdUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cmdUnderline.Image = ((System.Drawing.Image)(resources.GetObject("cmdUnderline.Image")));
            this.cmdUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdUnderline.Name = "cmdUnderline";
            this.cmdUnderline.Size = new System.Drawing.Size(62, 22);
            this.cmdUnderline.Text = "Underline";
            this.cmdUnderline.Click += new System.EventHandler(this.cmdUnderline_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // lstColors
            // 
            this.lstColors.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lstColors.Image = ((System.Drawing.Image)(resources.GetObject("lstColors.Image")));
            this.lstColors.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lstColors.Name = "lstColors";
            this.lstColors.Size = new System.Drawing.Size(49, 22);
            this.lstColors.Text = "Color";
            this.lstColors.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.lstColors_DropDownItemClicked);
            // 
            // lstFonts
            // 
            this.lstFonts.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lstFonts.Image = ((System.Drawing.Image)(resources.GetObject("lstFonts.Image")));
            this.lstFonts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lstFonts.Name = "lstFonts";
            this.lstFonts.Size = new System.Drawing.Size(44, 22);
            this.lstFonts.Text = "Font";
            this.lstFonts.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.lstFonts_DropDownItemClicked);
            // 
            // lstSizes
            // 
            this.lstSizes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lstSizes.Image = ((System.Drawing.Image)(resources.GetObject("lstSizes.Image")));
            this.lstSizes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lstSizes.Name = "lstSizes";
            this.lstSizes.Size = new System.Drawing.Size(40, 22);
            this.lstSizes.Text = "Size";
            this.lstSizes.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.lstSizes_DropDownItemClicked);
            // 
            // lblResult
            // 
            this.lblResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(270, 431);
            this.lblResult.Name = "lblResult";
            this.lblResult.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblResult.Size = new System.Drawing.Size(35, 13);
            this.lblResult.TabIndex = 4;
            this.lblResult.Text = "label1";
            // 
            // prgBar
            // 
            this.prgBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgBar.Location = new System.Drawing.Point(13, 474);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(585, 23);
            this.prgBar.Step = 1;
            this.prgBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prgBar.TabIndex = 3;
            this.prgBar.UseWaitCursor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(136, 426);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(117, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save text file";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpen.Location = new System.Drawing.Point(13, 426);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(117, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Open text file";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // rtbContent
            // 
            this.rtbContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbContent.Location = new System.Drawing.Point(4, 47);
            this.rtbContent.Name = "rtbContent";
            this.rtbContent.Size = new System.Drawing.Size(605, 364);
            this.rtbContent.TabIndex = 0;
            this.rtbContent.Text = "";
            // 
            // openTextFileDialog
            // 
            this.openTextFileDialog.DefaultExt = "txt";
            this.openTextFileDialog.Filter = "Text files (*.txt)|*.txt";
            this.openTextFileDialog.RestoreDirectory = true;
            // 
            // saveTextFileDialog
            // 
            this.saveTextFileDialog.DefaultExt = "txt";
            this.saveTextFileDialog.Filter = "Text files (*.txt)|*.txt";
            this.saveTextFileDialog.RestoreDirectory = true;
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.WorkerSupportsCancellation = true;
            // 
            // TextEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "TextEditor";
            this.Size = new System.Drawing.Size(612, 504);
            this.Load += new System.EventHandler(this.TextEditor_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox rtbContent;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton cmdBold;
        private System.Windows.Forms.ToolStripButton cmdItalic;
        private System.Windows.Forms.ToolStripButton cmdUnderline;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton lstColors;
        private System.Windows.Forms.ToolStripDropDownButton lstFonts;
        private System.Windows.Forms.ToolStripDropDownButton lstSizes;
        private System.Windows.Forms.OpenFileDialog openTextFileDialog;
        private System.Windows.Forms.SaveFileDialog saveTextFileDialog;
        private System.ComponentModel.BackgroundWorker bgWorker;
    }
}
