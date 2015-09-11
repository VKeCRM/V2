using System;

namespace VKeCRM.Common.IO
{
    public static class DirectoryManager
    {
        public static String CombineDirectories(params String[] dirs)
        {
            if (dirs == null || dirs.Length <= 0)
                return String.Empty;

            String completeDir = String.Empty;
            foreach (String dir in dirs)
            {
                if (dir.Trim() == String.Empty) continue;

                String tmpDir = dir.Trim().RemoveFirstBackStroke().RemoveLastBackStroke();
                completeDir += tmpDir + @"\";
            }

            if (completeDir != String.Empty)
                completeDir = completeDir.RemoveLastBackStroke();

            return completeDir;
        }
    }
}
