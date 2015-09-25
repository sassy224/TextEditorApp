using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditorApp.Model
{
    public class FileOperationArgument : IDisposable
    {
        public string FilePath;
        public bool IsWrite;
        public string FileContent;

        public FileOperationArgument()
        {
            FilePath = null;
            IsWrite = false;
            FileContent = null;
        }

        public FileOperationArgument(string filePath, bool isWrite, string fileContent)
        {
            this.FilePath = filePath;
            this.IsWrite = isWrite;
            this.FileContent = fileContent;
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (FilePath != null) FilePath = null;
                    if (FileContent != null) FileContent = null;
                }

                // Now disposed of any unmanaged objects
                // ...

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Destructor
        ~FileOperationArgument()
        {
            Dispose(false);
        }
    }
}