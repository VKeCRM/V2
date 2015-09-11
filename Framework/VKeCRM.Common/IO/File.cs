using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace VKeCRM.Common.IO
{
    public class File
    {
        public static int WriteFileToDisk(string fullyQualifiedPathAndFileName, byte[] contentsToWrite)
        {
            int bytesRead = 0;
            // Escape clause
            if (System.IO.File.Exists(fullyQualifiedPathAndFileName))
                throw new IOException(String.Format("Write file failure.\r\nThe file {0} already exists.", fullyQualifiedPathAndFileName));
            try
            {
                FileStream fileToBeWritten = new FileStream(fullyQualifiedPathAndFileName, FileMode.Create, FileAccess.Write, FileShare.None);

                try
                {
                    fileToBeWritten.Seek(0, SeekOrigin.Begin);
                    fileToBeWritten.Write(contentsToWrite, 0, contentsToWrite.Length);
                    fileToBeWritten.Flush();
                    bytesRead = contentsToWrite.Length;
                }
                catch (Exception ex)
                {
                    throw new IOException(String.Format("Failed to write {0} to disk,", fullyQualifiedPathAndFileName), ex);
                }
                finally
                {
                    fileToBeWritten.Close();
                    fileToBeWritten.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new IOException(String.Format("Failed to open {0} to write,", fullyQualifiedPathAndFileName), ex);
            }

            return bytesRead;
        }
        public static byte[] ReadFileFromDisk(string fullyQualifiedPathAndFileName)
        {
            
            if (!System.IO.File.Exists(fullyQualifiedPathAndFileName))
                throw new IOException(String.Format("Read file failure.\r\nThe file {0} does not exist.", fullyQualifiedPathAndFileName));

            Byte[] readBytes = new Byte[]{};

            try
            {
                FileStream fileToBeRead = new FileStream(fullyQualifiedPathAndFileName, FileMode.Open, FileAccess.Read, FileShare.Read);

                try
                {
                    readBytes = new Byte[fileToBeRead.Length];
                    fileToBeRead.Read(readBytes, 0, Convert.ToInt32(fileToBeRead.Length));

                }
                catch (Exception ex)
                {
                    throw new IOException(String.Format("Failed to write {0} to disk,", fullyQualifiedPathAndFileName), ex);
                }
                finally
                {
                    fileToBeRead.Close();
                    fileToBeRead.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new IOException(String.Format("Failed to write {0} to disk,", fullyQualifiedPathAndFileName), ex);
            }
            

            return readBytes;
        }
    }
}
