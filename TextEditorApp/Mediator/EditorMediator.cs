using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextEditorApp.CustomControl;
using TextEditorApp.Model;
using TextEditorApp.Utilities;

namespace TextEditorApp.Mediator
{
    public class EditorMediator : IEditorMediator
    {
        private IEditorControl editorControl = null;
        private OpenFileDialog openTextFileDialog;
        private SaveFileDialog saveTextFileDialog;
        private BackgroundWorker bgWorker;
        private bool isWrite = false;

        public EditorMediator(IEditorControl control)
        {
            //editorControl
            editorControl = control;

            //openTextFileDialog
            this.openTextFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.openTextFileDialog.DefaultExt = "txt";
            this.openTextFileDialog.Filter = "Text files (*.txt)|*.txt";
            this.openTextFileDialog.RestoreDirectory = true;

            //saveTextFileDialog
            this.saveTextFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveTextFileDialog.DefaultExt = "txt";
            this.saveTextFileDialog.Filter = "Text files (*.txt)|*.txt";
            this.saveTextFileDialog.RestoreDirectory = true;

            //bgWorker
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.bgWorker.WorkerReportsProgress = true;
            bgWorker.DoWork += bgWorker_DoWork;
            bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
            bgWorker.ProgressChanged += bgWorker_ProgressChanged;
        }

        /// <summary>
        /// Implement method to handle the CustomDragEnter event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleCustomDragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// Implement method to handle the CustomDragDrop event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleCustomDragDrop(object sender, System.Windows.Forms.DragEventArgs e)
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
                    editorControl.SetTextForEditor(String.Empty);
                    //rtbContent.LoadFile(list[0], RichTextBoxStreamType.PlainText);
                    StartReadFile(fileNames[0]);
                }
            }
        }

        /// <summary>
        /// Implement method to handle the OpenButtonClick event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleOpenButtonClick(object sender, EventArgs e)
        {
            if (openTextFileDialog.ShowDialog() == DialogResult.OK)
            {
                StartReadFile(openTextFileDialog.FileName);
            }
        }

        /// <summary>
        /// Implement method to handle the SaveButtonClick event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleSaveButtonClick(object sender, EventArgs e)
        {
            if (saveTextFileDialog.ShowDialog() == DialogResult.OK)
            {
                StartSaveFile(saveTextFileDialog.FileName);
            }
        }

        /// <summary>
        /// Handle event when BackgroundWorker completes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Set progress bar to maximum
            editorControl.UpdateProgress(100, null);

            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                editorControl.SetLabelResultText(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled
                // the operation.
                // Note that due to a race condition in
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                editorControl.SetLabelResultText("Canceled");
            }
            else
            {
                // Finally, handle the case where the operation
                // succeeded.

                if (isWrite)
                {
                    editorControl.SetLabelResultText("Write file successfully!");
                }
                else
                {
                    //Set content for rtb
                    editorControl.SetTextForEditor(e.Result.ToString().Replace("\0", ""));
                    editorControl.SetLabelResultText("Read file successfully!");
                }
            }

            // Enable controls
            editorControl.EnableControls();
        }

        /// <summary>
        /// Handle event when BackgroundWorker starts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            FileOperationArgument foa = (FileOperationArgument)e.Argument;
            if (foa.IsWrite)
            {
                e.Result = FileUtils.WriteTextToFileAsync(foa.FileContent, foa.FilePath, bgWorker);
            }
            else
            {
                e.Result = FileUtils.ReadTextContentAsync(foa.FilePath, bgWorker);
            }
        }

        /// <summary>
        /// Handle event when BackgroundWorker reports progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Work around to fix bug animation of progress bar
            if (e.ProgressPercentage == 100)
            {
                editorControl.UpdateProgress(e.ProgressPercentage - 1);
                editorControl.UpdateProgress(e.ProgressPercentage);
            }
            else
            {
                editorControl.UpdateProgress(e.ProgressPercentage + 1);
            }
            //Set corrent value
            editorControl.UpdateProgress(e.ProgressPercentage);
        }

        /// <summary>
        /// Start reading file
        /// </summary>
        /// <param name="fileName"></param>
        private void StartReadFile(string fileName)
        {
            //Disable controls
            editorControl.DisableControls();
            //Reset progress
            editorControl.UpdateProgress(0, null);
            //Create params
            isWrite = false;
            FileOperationArgument foa = new FileOperationArgument(fileName, isWrite, String.Empty);

            bgWorker.RunWorkerAsync(foa);
        }

        /// <summary>
        /// Start saving file
        /// </summary>
        /// <param name="fileName"></param>
        private void StartSaveFile(string fileName)
        {
            //Disable controls
            editorControl.DisableControls();
            //Reset progress
            editorControl.UpdateProgress(0, null);

            isWrite = true;
            FileOperationArgument foa = new FileOperationArgument(fileName, isWrite, editorControl.GetTextOfEditor());
            bgWorker.RunWorkerAsync(foa);
        }
    }
}