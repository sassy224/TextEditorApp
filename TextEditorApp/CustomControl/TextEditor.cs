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
    public partial class TextEditor : UserControl, IEditorControl
    {
        private bool isWrite = false;
        private string textContent = String.Empty;

        public TextEditor()
        {
            InitializeComponent();
        }

        private void TextEditor_Load(object sender, EventArgs e)
        {
            //Init events
            InitEvents();

            //Init background worker
            InitializeBackgroundWorker();

            //Init drag and drop for richtextbox
            InitializeDragDrop();
        }

        private void InitEvents()
        {
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
        }

        private void InitializeBackgroundWorker()
        {
            bgWorker.DoWork += bgWorker_DoWork;
            bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
            bgWorker.ProgressChanged += bgWorker_ProgressChanged;
        }

        private void InitializeDragDrop()
        {
            rtbContent.AllowDrop = true;
            rtbContent.DragEnter += rtbContent_DragEnter;
            rtbContent.DragDrop += rtbContent_DragDrop;
        }

        void rtbContent_DragDrop(object sender, DragEventArgs e)
        {
            if (CustomDragDrop != null)
            {
                //There's already a custom implementation, use it
                //Raise event to notify the subscribers of this event
                CustomDragDrop(sender, e);
            }
            else
            {
                //There's no custom implementation, use default implementation of this control
                //Get data of the files dropped in
                object fileData = e.Data.GetData("FileDrop");
                if (fileData != null)
                {
                    //Get list file paths
                    var fileNames = fileData as string[];

                    //Only read the first file
                    if (fileNames != null && !string.IsNullOrWhiteSpace(fileNames[0]))
                    {
                        rtbContent.Clear();
                        //rtbContent.LoadFile(list[0], RichTextBoxStreamType.PlainText);
                        StartReadFile(fileNames[0]);
                    }
                }
            }
        }

        void rtbContent_DragEnter(object sender, DragEventArgs e)
        {
            if (CustomDragEnter != null)
            {
                //There's already a custom implementation, use it
                //Raise event to notify the subscribers of this event
                CustomDragEnter(sender, e);
            }
            else
            {
                //There's no custom implementation, use default implementation
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
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
            //Set progress bar to maximum
            prgBar.Value = prgBar.Maximum;

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
            string fileName = (string)e.Argument;
            if (isWrite)
            {
                e.Result = FileUtils.WriteTextToFileAsync(textContent, fileName, bgWorker);
            }
            else
            {
                e.Result = FileUtils.ReadTextContentAsync(fileName, bgWorker);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (OpenButtonClick != null)
            {
                //There's already a custom implementation, use it
                //Raise event to notify the subscribers of this event
                OpenButtonClick(sender, e);
            }
            else
            {
                //There's no custom implementation, use default implementation
                if (openTextFileDialog.ShowDialog() == DialogResult.OK)
                {
                    StartReadFile(openTextFileDialog.FileName);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveButtonClick != null)
            {
                //There's already a custom implementation, use it
                //Raise event to notify the subscribers of this event
                SaveButtonClick(sender, e);
            }
            else
            {
                //There's no custom implementation, use default implementation
                if (saveTextFileDialog.ShowDialog() == DialogResult.OK)
                {
                    StartSaveFile(saveTextFileDialog.FileName);
                }
            }
        }

        private void StartReadFile(string fileName)
        {
            //Disable controls
            this.btnOpen.Enabled = false;
            this.btnSave.Enabled = false;

            isWrite = false;
            prgBar.Value = 0;
            bgWorker.RunWorkerAsync(fileName);
        }

        private void StartSaveFile(string fileName)
        {
            //Disable controls
            this.btnOpen.Enabled = false;
            this.btnSave.Enabled = false;

            isWrite = true;
            prgBar.Value = 0;
            textContent = rtbContent.Text;
            bgWorker.RunWorkerAsync(fileName);
        }

        /* Interface implementation */
        public event DragEventHandler CustomDragEnter;

        public event DragEventHandler CustomDragDrop;

        public event EventHandler OpenButtonClick;

        public event EventHandler SaveButtonClick;

        public void UpdateProgress(int percentage, object additionalData = null)
        {
            prgBar.Value = percentage;
            lblResult.Text = percentage + "%";
        }

        public void SetLabelResultText(string text)
        {
            lblResult.Text = text;
        }

        public void SetTextForEditor(string text)
        {
            if (String.IsNullOrEmpty(text))
                rtbContent.Clear();
            else
                rtbContent.Text = text;
        }

        public string GetTextOfEditor()
        {
            return rtbContent.Text;
        }

        public void DisableControls()
        {
            btnOpen.Enabled = false;
            btnSave.Enabled = false;
            rtbContent.Enabled = false;
        }

        public void EnableControls()
        {
            btnOpen.Enabled = true;
            btnSave.Enabled = true;
            rtbContent.Enabled = true;
        }
    }
}