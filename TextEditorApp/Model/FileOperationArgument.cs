using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditorApp.Model
{
    public struct FileOperationArgument
    {
        public string FilePath;
        public bool IsWrite;
        public string FileContent;

        public FileOperationArgument(string filePath, bool isWrite, string fileContent)
        {
            this.FilePath = filePath;
            this.IsWrite = isWrite;
            this.FileContent = fileContent;
        }
    }
}