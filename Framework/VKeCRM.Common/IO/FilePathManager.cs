using System;
using System.IO;

namespace VKeCRM.Common.IO
{
    public static class FilePathManager
    {
        public static String ComputeFilePath(String directory, String fileName)
        {
            directory = directory.Trim();
            if (directory == String.Empty)
                throw new Exception("Directory cannot be empty.");

            fileName = fileName.Trim();
            if (fileName == String.Empty)
                throw new Exception("File name cannot be empty.");

            //--- Check if directory end with '\' ---//
            if (directory[directory.Length - 1] != '\\')
                directory = directory + "\\";

            return directory + fileName;
        }

        public static String CombineFilePath(params String[] directories)
        {
            if (directories == null || directories.Length <= 0)
                throw new Exception("Directories cannot be null or empty.");

            if (directories.Length == 1)
            {
                String dir = directories[0];
                if (dir.LastIndexOf('\\') == dir.Length - 1)
                    return dir.Substring(0, dir.Length - 1);
                else
                    return dir;
            }
            else
            {
                String baseDir = directories[0];
                if (baseDir.LastIndexOf('\\') == baseDir.Length - 1)
                    baseDir = baseDir.Substring(0, baseDir.Length - 1);

                for (Int32 i = 1; i < directories.Length; i++)
                {
                    String dir = directories[i].Trim();
                    if (dir == String.Empty) continue;

                    if (dir.LastIndexOf('\\') == dir.Length - 1)
                        dir = dir.Substring(0, dir.Length - 1);

                    if (dir.IndexOf('\\') == 0)
                        dir = dir.Substring(1, dir.Length - 1);

                    baseDir = baseDir + "\\" + dir;
                }

                return baseDir;
            }
        }
    }
}
