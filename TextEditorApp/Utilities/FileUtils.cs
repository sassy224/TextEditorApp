using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditorApp.Utilities
{
    class FileUtils
    {
        public static string ReadTextContentAsync(string filePath, BackgroundWorker worker, DoWorkEventArgs e)
        {
            StreamReader stream = null;
            StringBuilder sb = new StringBuilder();

            try
            {
                // Lets get the length so that when we are reading we know
                // when we have hit a "milestone" and to update the progress bar.
                FileInfo fileSize = new FileInfo(filePath);
                long totalSize = fileSize.Length;

                // Next we need to know where we are at and what defines a milestone in the
                // progress. So take the size of the file and divide it into 100 milestones
                // (which will match our 100 marks on the progress bar).
                long currentSize = 0;
                long currentTotalSize = 0;
                long incrementSize = (totalSize / 100);

                // Open the text file with open filemode access.
                stream = new StreamReader(new FileStream(filePath, FileMode.Open));

                // This buffer is only 10 characters long so we process the file in 10 char chunks.
                // We could have boosted this up, but we want a slow process to show the slow progress.
                char[] buff = new char[10];

                // Read through the file until end of file
                while (!stream.EndOfStream)
                {
                    // Add to the current position in the file
                    currentSize += stream.Read(buff, 0, buff.Length);
                    sb.Append(buff);

                    // Once we hit a milestone, subtract the milestone value and
                    // call our delegate we defined above.
                    if (currentSize >= incrementSize)
                    {
                        currentTotalSize += currentSize;
                        currentSize -= incrementSize;

                        int percentComplete = (int)((float)currentTotalSize / (float)totalSize * 100);
                        if (percentComplete > 100)
                            percentComplete = 100;
                        worker.ReportProgress(percentComplete);
                    }
                }

                //Set to 100%
                worker.ReportProgress(100);
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
                //return ex.Message;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }

        public static bool WriteTextToFileAsync(string textContent, string filePath, BackgroundWorker worker, DoWorkEventArgs e)
        {
            try
            {
                byte[] bytes = GetBytes(textContent);
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (BufferedStream outfile = new BufferedStream(fs))
                    {
                        int index = 0;
                        int bufferSize = 1024;
                        int totalSize = bytes.Length;
                        long incrementSize = (totalSize / 100);

                        while (index < totalSize - bufferSize)
                        {
                            outfile.Write(bytes, index, bufferSize);
                            index += bufferSize;
                        }

                        outfile.Flush();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}