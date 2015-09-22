using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextEditorApp.CustomControl;

namespace TextEditorApp.Mediator
{
    public interface IEditorMediator
    {
        /// <summary>
        /// Interface method to handle the CustomDragEnter event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void HandleCustomDragEnter(object sender, DragEventArgs e);

        /// <summary>
        /// Interface method to handle the CustomDragDrop event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void HandleCustomDragDrop(object sender, DragEventArgs e);

        /// <summary>
        /// Interface method to handle the OpenButtonClick event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void HandleOpenButtonClick(object sender, EventArgs e);

        /// <summary>
        /// Interface method to handle the SaveButtonClick event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void HandleSaveButtonClick(object sender, EventArgs e);
    }
}