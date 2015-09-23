using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditorApp.CustomControl
{
    public interface ITextEditorControl
    {
        //Occurs when an object is dragged into the control's bounds.
        event DragEventHandler CustomDragEnter;
        //Occurs when the user completes a drag-and-drop
        event DragEventHandler CustomDragDrop;
        //Occurs when Open button is clicked
        event EventHandler OpenButtonClick;
        //Occurs when Save button is clicked
        event EventHandler SaveButtonClick;

        //Update progress bar of this control
        void UpdateProgress(int percentage, object additionalData = null);

        //Disable all chilren controls
        void DisableControls();

        //Enable all children controls
        void EnableControls();

        //Allow drag & drop text file
        bool AllowDragDropTextFile { get; set; }

        //Show or hide Open button
        bool ShowOpenButton { get; set; }

        //Show or hide Save button
        bool ShowSaveButton { get; set; }

        //Text on Open button
        string OpenButtonText { get; set; }

        //Text on Save button
        string SaveButtonText { get; set; }

        //Text on label result
        string LabelResultText { get; set; }

        //Text on editor's text control
        string BodyContentText { get; set; }
    }
}