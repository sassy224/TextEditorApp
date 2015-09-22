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
            try
            {
                StringBuilder sb = new StringBuilder();
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
                using (StreamReader stream = new StreamReader(new FileStream(filePath, FileMode.Open)))
                {
                    // This buffer is only 100 characters long so we process the file in 100 char chunks.
                    // We could have boosted this up, but we want a slow process to show the slow progress.
                    char[] buff = new char[100];
                    int len = buff.Length;

                    // Read through the file until end of file
                    while (!stream.EndOfStream)
                    {
                        // Add to the current position in the file
                        currentSize += stream.Read(buff, 0, buff.Length);
                        sb.Append(buff);
                        Array.Clear(buff, 0, buff.Length);

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
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool WriteTextToFileAsync(string textContent, string filePath, BackgroundWorker worker, DoWorkEventArgs e)
        {
            try
            {
                //Convert string to bytes
                byte[] bytesArray = GetBytes(textContent);
                //Split into 100 smaller arrays
                var splitArrays = bytesArray.Split(bytesArray.Length / 100);
                int percentage = 0;

                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    foreach (var arr in splitArrays)
                    {
                        fs.Write(arr.ToArray(), 0, arr.Count());
                        percentage += 1;
                        if (percentage > 100)
                            percentage = 100;
                        worker.ReportProgress(percentage);
                    }

                    fs.Flush();
                    fs.Close();
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