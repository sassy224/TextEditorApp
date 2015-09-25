using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextEditorApp.Model;

namespace TextEditorApp.Utilities
{
    public static class FileUtils
    {
        private const int BUFFER_SIZE = 10 * 1024; //10Kb

        /// <summary>
        /// Read text content from a file asynchronously and report pieces of text back for appending
        /// </summary>
        /// <param name="filePath">Full path to file</param>
        /// <param name="worker">BackgroundWorker object</param>
        public static void ReadTextContentAsync(string filePath, BackgroundWorker worker)
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
                FileOperationArgument foa;

                // Open the text file with open filemode access.
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    using (StreamReader stream = new StreamReader(fs))
                    {
                        // This buffer is only 10*1024 characters long so we process the file in 10*1024 char chunks.
                        // We could have boosted this up, but we want a slow process to show the slow progress.
                        char[] buff = new char[10 * 1024];
                        int len = buff.Length;

                        // Read through the file until end of file
                        while (!stream.EndOfStream)
                        {
                            // Add to the current position in the file
                            currentSize += stream.Read(buff, 0, buff.Length);

                            //Remove "\0"
                            sb.Append(RemoveSpecialChars(buff));
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

                                foa = new FileOperationArgument(String.Empty, false, sb.ToString());
                                worker.ReportProgress(percentComplete, foa);

                                //Reset string builder
                                sb = new StringBuilder();
                                //Sleep 100ms to let UI update
                                Thread.Sleep(200);
                            }
                        }
                    }
                }

                foa = new FileOperationArgument(String.Empty, false, sb.ToString());
                worker.ReportProgress(100, foa);
                Thread.Sleep(200);

                //Free memory
                sb = null;
                GC.Collect();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Write text string to a file asynchronously
        /// </summary>
        /// <param name="textContent">Text string to be written to file</param>
        /// <param name="filePath">Full path to file</param>
        /// <param name="worker">BackgroundWorker object</param>
        /// <returns>True if success, Exception otherwise</returns>
        public static void WriteTextToFileAsync(string textContent, string filePath, BackgroundWorker worker)
        {
            try
            {
                //Split into smaller arrays of length 10k
                var chunks = textContent.SplitByLength(10 * 1024);
                long totalSize = textContent.Length;
                long incrementSize = (totalSize / 100);
                long currentChunksCount = 0;
                long currentTotalChunksCount = 0;

                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        foreach (var chunk in chunks)
                        {
                            sw.Write(chunk);
                            currentChunksCount += chunk.Count();

                            if (worker != null && currentChunksCount >= incrementSize)
                            {
                                currentTotalChunksCount += currentChunksCount;
                                currentChunksCount -= incrementSize;

                                int percentage = (int)((float)currentTotalChunksCount / (float)totalSize * 100);
                                if (percentage > 100)
                                    percentage = 100;

                                worker.ReportProgress(percentage);
                                //Sleep 100ms to let UI update
                                Thread.Sleep(200);
                            }
                        }
                        sw.Flush();
                    }
                    //fs.Flush();
                }
                worker.ReportProgress(100);
                Thread.Sleep(200);

                //Free memory
                chunks = null;
                GC.Collect();
            }
            catch (Exception)
            {
                throw;
            }
        }

        static string[] tobeRemoved = { "\0" };

        public static char[] RemoveSpecialChars(char[] chars)
        {
            string tmp = new string(chars);
            foreach (string item in tobeRemoved)
            {
                tmp = tmp.Replace(item, String.Empty);
            }
            return tmp.ToCharArray();
        }
    }
}