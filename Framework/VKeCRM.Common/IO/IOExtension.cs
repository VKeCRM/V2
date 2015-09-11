using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace VKeCRM.Common.IO
{
    public static class IOExtension
    {
        public static List<String> ImageFileExtensions = new List<String>() { "jpg", "png", "bmp", "jpeg" };

        public static Byte[] ToByteArray(this String value)
        {
            return value.ToByteArray(Encoding.UTF8);
        }

        public static Byte[] ToByteArray(this String value, Encoding encoder)
        {
            if (value == null || value == String.Empty) return null;
            return encoder.GetBytes(value);
        }

        public static String GetString(this Byte[] value, Encoding encoder = null)
        {
            if (value == null || value.Length <= 0) return String.Empty;
            if (encoder == null)
                return Encoding.UTF8.GetString(value);
            else
                return encoder.GetString(value);
        }

        public static String ReplaceXMLReservedChar(this String content)
        {
            if (String.IsNullOrWhiteSpace(content)) return content;

            return content.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
        }

        public static void WriteToFile(this String content, String filePath, Boolean overwriteIfExist = false)
        {
            if (System.IO.File.Exists(filePath) && !overwriteIfExist)
                throw new Exception(String.Format("File '{0}' already exists.", filePath));

            CreateFolderWhileNotExists(filePath);

            using (Mutex mutex = new Mutex(false, filePath.GetMutexName()))
            {
                mutex.WaitOne();

                try
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        using (TextWriter tw = new StreamWriter(fs))
                        {
                            tw.Write(content);
                            tw.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
        }

        public static void WriteToFile(this Byte[] fileBytes, String filePath)
        {
            CreateFolderWhileNotExists(filePath);

            using (Mutex mutex = new Mutex(false, filePath.GetMutexName()))
            {
                mutex.WaitOne();

                try
                {
                    FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                    fs.Write(fileBytes, 0, fileBytes.Length);
                    fs.Close();
                    fs.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
        }

        public static void WriteToFile(this Byte[] fileBytes, String filePath, Boolean overwriteIfExist = false, bool throwExceptionIfExist = true)
        {
            if (System.IO.File.Exists(filePath) && !overwriteIfExist)
            {
                if (throwExceptionIfExist)
                    throw new Exception(String.Format("File '{0}' already exists.", filePath));
                
                return;
            }

            CreateFolderWhileNotExists(filePath);

            using (Mutex mutex = new Mutex(false, filePath.GetMutexName()))
            {
                mutex.WaitOne();

                try
                {
                    FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                    fs.Write(fileBytes, 0, fileBytes.Length);
                    fs.Close();
                    fs.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
        }

        public static Byte[] ReadFile(this String filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                using (Mutex mutex = new Mutex(false, filePath.GetMutexName()))
                {
                    mutex.WaitOne();

                    try
                    {
                        FileStream fs = System.IO.File.OpenRead(filePath);
                        Byte[] bytes = new Byte[fs.Length];
                        fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                        fs.Close();

                        return bytes;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }
                }
            }
            else
            {
                throw new Exception(String.Format("File ('{0}') does not exists.", filePath));
            }
        }

        public static String GetFileVersion(this String filePath)
        {
            if (!System.IO.File.Exists(filePath))
                throw new Exception(String.Format("File ({0}) does not exists.", filePath));

            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(filePath);
            return fvi.FileVersion;
        }

        public static String RemoveLastBackStroke(this String dir)
        {
            dir = dir.Trim();
            if (dir == String.Empty) return String.Empty;
            if (dir == @"\") return String.Empty;

            //--- Remove last '\' ---//
            if (dir.Substring(dir.Length - 1, 1) == @"\")
                dir = dir.Substring(0, dir.Length - 1);

            return dir;
        }

        public static String RemoveFirstBackStroke(this String dir)
        {
            dir = dir.Trim();
            if (dir == String.Empty) return String.Empty;
            if (dir == @"\") return String.Empty;

            //--- Remove first '\' ---//
            if (dir.Substring(0, 1) == @"\")
                dir = dir.Substring(1, dir.Length - 1);

            return dir;
        }

        public static Boolean Contains(this String value, String[] arrToSearch)
        {
            if (arrToSearch == null)
                return false;

            if (arrToSearch.Length <= 0)
                return false;

            if (value == null)
                return false;

            foreach (string str in arrToSearch)
            {
                if (!value.ToUpper().Contains(str.ToUpper()))
                    return false;
            }

            return true;
        }

        public static Boolean ExistsIn(this String value, List<string> listToSearch)
        {
            if (listToSearch == null)
                return false;

            if (listToSearch.Count <= 0)
                return false;

            if (value == null)
                return false;

            foreach (string str in listToSearch)
            {
                if (value.ToUpper().Equals(str.ToUpper()))
                    return true;
            }

            return false;
        }

        // Create all necessary folders before saving the file
        public static void CreateFolderWhileNotExists(this string filePath)
        {
            string finalDirectoryPath = string.Empty;

            if (!string.IsNullOrEmpty(filePath))
                finalDirectoryPath = filePath.Substring(0, filePath.LastIndexOf(@"\"));

            if (!string.IsNullOrEmpty(finalDirectoryPath) && !Directory.Exists(finalDirectoryPath))
            {
                using (Mutex mutex = new Mutex(false, finalDirectoryPath.GetMutexName()))
                {
                    mutex.WaitOne();

                    try
                    {
                        System.IO.Directory.CreateDirectory(finalDirectoryPath);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }
                }
            }
        }

        public static String GetFileExtension(this string fileFullName)
        {
            if (String.IsNullOrWhiteSpace(fileFullName))
                return "";
            else if (fileFullName.LastIndexOf(".") < 0)
                return "";
            else
                return fileFullName.Substring(fileFullName.LastIndexOf(".") + 1);
        }

        public static Boolean IsImageFile(this String fileExtension)
        {
            foreach (String imgExtension in ImageFileExtensions)
            {
                if (imgExtension.ToUpper() == fileExtension.ToUpper())
                    return true;
            }

            return false;
        }

        public static void SaveImage(this System.Drawing.Bitmap bmp, Stream stream, String fileExtension)
        {
            fileExtension = fileExtension.ToUpper();
            if (fileExtension == "JPEG" || fileExtension == "JPG")
                bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            else if (fileExtension == "BMP")
                bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            else if (fileExtension == "PNG")
                bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            else
            {
                throw new Exception("Unhandled image file extension");
            }
        }

        public static String GetMutexName(this string filePath)
        {
            if (String.IsNullOrWhiteSpace(filePath))
                throw new Exception("File path is empty or null.");

            filePath = filePath.ToUpper().Replace(@"\", "_");
            if (filePath.Length <= 260)
                return filePath;
            else
                return filePath.Substring(0, 260);
        }

        public static void DeleteDirectory(this string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                using (Mutex mutex = new Mutex(false, directoryPath.GetMutexName()))
                {
                    mutex.WaitOne();

                    try
                    {
                        Directory.Delete(directoryPath, true);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }
                }
            }
        }

        public static void DeleteFile(this string filePath)
        {
            FileInfo fileInfo = null;

            fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists)
            {
                using (Mutex mutex = new Mutex(false, fileInfo.FullName.GetMutexName()))
                {
                    mutex.WaitOne();

                    try
                    {
                        fileInfo.Delete();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }
                }
                
            }

            fileInfo = null;
        }
    }
}