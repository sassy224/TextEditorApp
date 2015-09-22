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
        /// <summary>
        /// Read text content from a file asynchronously
        /// </summary>
        /// <param name="filePath">Full path to file</param>
        /// <param name="worker">BackgroundWorker object</param>
        /// <returns>The string content of the file</returns>
        public static string ReadTextContentAsync(string filePath, BackgroundWorker worker)
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

        /// <summary>
        /// Write text string to a file asynchronously
        /// </summary>
        /// <param name="textContent">Text string to be written to file</param>
        /// <param name="filePath">Full path to file</param>
        /// <param name="worker">BackgroundWorker object</param>
        /// <returns>True if success, Exception otherwise</returns>
        public static bool WriteTextToFileAsync(string textContent, string filePath, BackgroundWorker worker)
        {
            try
            {
                //Convert string to bytes
                byte[] bytesArray = GetBytes(textContent);

                if (bytesArray.Length < 1024 * 1024 * 10) //10Mb
                {
                    WriteSmallFile(bytesArray, filePath);
                }
                else
                {
                    WriteBigFileAsync(bytesArray, filePath, worker);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Write small file, use FileStream to write all bytes to file right away.
        /// Since this will be finished in an instance, no need to use BackgroundWorker to report progress
        /// </summary>
        /// <param name="bytesArray">Array of bytes to be written to file</param>
        /// <param name="filePath">Full path to file</param>
        private static void WriteSmallFile(byte[] bytesArray, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                fs.Write(bytesArray, 0, bytesArray.Length);
                fs.Flush();
                fs.Close();
            }
        }

        /// <summary>
        /// Write big file, split the byte array into smaller arrays (chunks), then write them in sequence to the file.
        /// Since this might take some times, use a BackgroundWorker to report progress
        /// </summary>
        /// <param name="bytesArray">Array of bytes to be written to file</param>
        /// <param name="filePath">Full path to file</param>
        /// <param name="worker">BackgroundWorker object</param>
        private static void WriteBigFileAsync(byte[] bytesArray, string filePath, BackgroundWorker worker)
        {
            //Split into 100 smaller arrays
            var chunks = bytesArray.Split(bytesArray.Length / 100);
            int percentage = 0;

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                foreach (var chunk in chunks)
                {
                    fs.Write(chunk.ToArray(), 0, chunk.Count());

                    if (worker != null)
                    {
                        percentage += 1;
                        if (percentage > 100)
                            percentage = 100;
                        worker.ReportProgress(percentage);
                    }
                }

                fs.Flush();
                fs.Close();
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