using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditorApp.CustomControl
{
    public interface IEditorControl
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

        //Set text for label result of this control
        void SetLabelResultText(string text);

        //Set text for the editor's body
        void SetTextForEditor(string text);

        string GetTextOfEditor();

        void DisableControls();

        void EnableControls();
    }
}