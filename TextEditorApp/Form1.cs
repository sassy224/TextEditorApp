using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextEditorApp.Mediator;

namespace TextEditorApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //Init form
            InitializeComponent();

            //Init mediator
            IEditorMediator mediator = new EditorMediator(textEditor1);

            //Delegate event handlers to mediator
            textEditor1.CustomDragEnter += mediator.HandleCustomDragEnter;
            textEditor1.CustomDragDrop += mediator.HandleCustomDragDrop;
            textEditor1.OpenButtonClick += mediator.HandleOpenButtonClick;
            textEditor1.SaveButtonClick += mediator.HandleSaveButtonClick;

            //Uncomment to disable drag n drop
            //textEditor1.AllowDragDropTextFile = false;
        }
    }
}