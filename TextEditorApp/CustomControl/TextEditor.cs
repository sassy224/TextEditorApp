using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextEditorApp.Utilities;

namespace TextEditorApp.CustomControl
{
    public partial class TextEditor : UserControl
    {
        private bool isWrite = false;
        private string textContent = String.Empty;

        public TextEditor()
        {
            InitializeComponent();
        }

        private void cmdBold_Click(object sender, EventArgs e)
        {
            if (rtbContent.SelectionFont == null)
            {
                // The selection includes multiple fonts. Sadly,
                // there's no way to get information about any
                // of them.
                return;
            }

            // Get the current style.
            FontStyle style = rtbContent.SelectionFont.Style;

            // Adjust as required.
            if (rtbContent.SelectionFont.Bold)
            {
                style &= ~FontStyle.Bold;
            }
            else
            {
                style |= FontStyle.Bold;
            }

            // Assign font with new style.
            rtbContent.SelectionFont = new Font(rtbContent.SelectionFont, style);
        }

        private void cmdItalic_Click(object sender, EventArgs e)
        {
            if (rtbContent.SelectionFont == null)
            {
                // The selection includes multiple fonts. Sadly,
                // there's no way to get information about any
                // of them.
                return;
            }

            // Get the current style.
            FontStyle style = rtbContent.SelectionFont.Style;

            // Adjust as required.
            if (rtbContent.SelectionFont.Italic)
            {
                style &= ~FontStyle.Italic;
            }
            else
            {
                style |= FontStyle.Italic;
            }

            // Assign font with new style.
            rtbContent.SelectionFont = new Font(rtbContent.SelectionFont, style);
        }

        private void cmdUnderline_Click(object sender, EventArgs e)
        {
            if (rtbContent.SelectionFont == null)
            {
                // The selection includes multiple fonts. Sadly,
                // there's no way to get information about any
                // of them.
                return;
            }

            // Get the current style.
            FontStyle style = rtbContent.SelectionFont.Style;

            // Adjust as required.
            if (rtbContent.SelectionFont.Underline)
            {
                style &= ~FontStyle.Underline;
            }
            else
            {
                style |= FontStyle.Underline;
            }

            // Assign font with new style.
            rtbContent.SelectionFont = new Font(rtbContent.SelectionFont, style);
        }

        private void lstColors_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            KnownColor selectedColor;
            selectedColor = (KnownColor)System.Enum.Parse(typeof(KnownColor), e.ClickedItem.Text);

            rtbContent.SelectionColor = Color.FromKnownColor(selectedColor);
        }

        private void lstFonts_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (rtbContent.SelectionFont == null)
            {
                // The selection includes multiple fonts. Sadly,
                // there's no way to get information about any
                // of them.
                rtbContent.SelectionFont = new Font(e.ClickedItem.Text, rtbContent.Font.Size);
            }
            rtbContent.SelectionFont = new Font(e.ClickedItem.Text, rtbContent.SelectionFont.Size);
        }

        private void lstSizes_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (rtbContent.SelectionFont == null)
            {
                // The selection includes multiple fonts. Sadly,
                // there's no way to get information about any
                // of them.
                return;
            }
            rtbContent.SelectionFont =
                new Font(rtbContent.SelectionFont.FontFamily,
                Convert.ToInt32(e.ClickedItem.Text),
                rtbContent.SelectionFont.Style);
        }

        private void TextEditor_Load(object sender, EventArgs e)
        {
            //Bind list colors
            foreach (string color in ListUtils.GetKnownColorNames())
            {
                lstColors.DropDownItems.Add(color);
            }

            //Bind list fonts
            foreach (FontFamily family in ListUtils.GetInstalledFontFamilies())
            {
                lstFonts.DropDownItems.Add(family.Name);
            }

            //Bind list font sizes
            foreach (int size in ListUtils.GetFontSizes())
            {
                lstSizes.DropDownItems.Add(size.ToString());
            }

            //Init background worker
            InitializeBackgroundWorker();
        }

        private void InitializeBackgroundWorker()
        {
            bgWorker.DoWork += bgWorker_DoWork;
            bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
            bgWorker.ProgressChanged += bgWorker_ProgressChanged;
        }

        void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Work around to fix bug animation of progress bar
            if (e.ProgressPercentage == this.prgBar.Maximum)
            {
                this.prgBar.Value = e.ProgressPercentage - 1;
                this.prgBar.Value = e.ProgressPercentage;
            }
            else
            {
                this.prgBar.Value = e.ProgressPercentage + 1;
            }
            //Set corrent value
            this.prgBar.Value = e.ProgressPercentage;

            this.lblResult.Text = e.ProgressPercentage.ToString();
        }

        void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                lblResult.Text = e.Error.Message;
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled
                // the operation.
                // Note that due to a race condition in
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                lblResult.Text = "Canceled";
            }
            else
            {
                // Finally, handle the case where the operation
                // succeeded.

                if (isWrite)
                {
                    lblResult.Text = "Write file successfully!";
                }
                else
                {
                    //Set content for rtb
                    rtbContent.Text = e.Result.ToString().Replace("\0", "");
                    lblResult.Text = "Read file successfully!";
                }
            }

            // Enable controls
            this.btnOpen.Enabled = true;
            this.btnSave.Enabled = true;
        }

        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (isWrite)
            {
                e.Result = FileUtils.WriteTextToFileAsync(textContent, saveTextFileDialog.FileName, bgWorker, e);
            }
            else
            {
                e.Result = FileUtils.ReadTextContentAsync(openTextFileDialog.FileName, bgWorker, e);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (openTextFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Disable controls
                this.btnOpen.Enabled = false;
                this.btnSave.Enabled = false;

                isWrite = false;
                prgBar.Value = 0;
                bgWorker.RunWorkerAsync();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveTextFileDialog.ShowDialog() == DialogResult.OK)
            {
                isWrite = true;
                prgBar.Value = 0;
                textContent = rtbContent.Text;
                bgWorker.RunWorkerAsync();
            }
        }
    }
}