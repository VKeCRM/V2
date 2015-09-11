using System;
using System.IO;
using SystemDirectory = System.IO.Directory;
using SystemPath = System.IO.Path;
using SystemFile = System.IO.File;

namespace VKeCRM.Common.IO
{
    /// <summary>
    /// Exposes instance methods for parsing, creating, and checking existence of paths. 
    /// </summary>
    public sealed class Path
    {
        private readonly string DirectorySeparator = new string(SystemPath.DirectorySeparatorChar, 1);
        private readonly string UncPrefix = new string(SystemPath.DirectorySeparatorChar, 2);

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the specified path string contains UNC path information.
        /// </summary>
        public bool IsPathUnc
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether the specified path string contains absolute or relative path information.
        /// </summary>
        public bool IsPathRooted
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the root directory information of the original path.
        /// </summary>
        public string PathRoot
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the original path.
        /// </summary>
        public string FullPath
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the original path without the filename if one was specified.
        /// </summary>
        public string FullPathWithoutFilename
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets an array of subfolders in the original path.
        /// </summary>
        public string[] Subfolders
        {
            get;
            private set;
        }

        /// <summary>
        /// Determines whether the original path includes a file name.
        /// </summary>
        public bool HasFileName
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the file name and extension of the original path string.
        /// </summary>
        public string Filename
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the file name without extension of the original path string.
        /// </summary>
        public string FilenameWithoutExtension
        {
            get;
            private set;
        }

        /// <summary>
        /// Determines whether the original path includes a file name extension.
        /// </summary>
        public bool HasExtension
        {
            get; 
            private set;
        }

        /// <summary>
        /// Returns the extension of the original path string.
        /// </summary>
        public string Extension
        {
            get; 
            private set;
        }

        #endregion Properties

        #region constructors

        /// <summary>
        /// Initializes a new instance of the Path class
        /// </summary>
        private Path()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Path class with the specified path.
        /// </summary>
        /// <param name="path">The file or directory for which to obtain path information.</param>
        public Path(string path)
        {
            FullPath = path;
            IsPathRooted = SystemPath.IsPathRooted(FullPath);
            IsPathUnc = ((IsPathRooted) && (FullPath.StartsWith(UncPrefix)));

            string pathRoot = string.Empty;
            if (IsPathRooted)
            {
                pathRoot = SystemPath.GetPathRoot(FullPath);
                if (IsPathUnc)
                    pathRoot = string.Concat(pathRoot, DirectorySeparator);
            }
            PathRoot = pathRoot;

            Subfolders = SystemPath.GetDirectoryName((IsPathRooted)
                                                           ? FullPath.Substring(PathRoot.Length)
                                                           : FullPath)
                                   .Split(DirectorySeparator.ToCharArray());

            Filename = SystemPath.GetFileName(FullPath);
            FilenameWithoutExtension = SystemPath.GetFileNameWithoutExtension(FullPath);

            HasFileName = (Filename.Length != 0);

            Extension = SystemPath.GetExtension(FullPath);
            HasExtension = (Extension.Length != 0);

            FullPathWithoutFilename = (HasFileName) ? FullPath.Substring(0, FullPath.Length - Filename.Length) : FullPath;

        }

        #endregion constructors

        #region Methods

        /// <summary>
        /// Creates the directory specified in the original path.
        /// </summary>
        /// <returns>true if the directory was created; otherwise, false.</returns>
        /// <exception cref="IOException">The directory cannot be created.</exception>
        public bool Create()
        {
            return (SystemDirectory.CreateDirectory(FullPathWithoutFilename) != null);
        }

        /// <summary>
        /// Determines whether the original path refers to an existing directory on disk.
        /// If the original path contains a file name, only the existence of the path is checked.
        /// Use Exists(false) to check to see if the file exists.
        /// </summary>
        /// <returns>true if path refers to an existing directory; otherwise, false.</returns>
        public bool Exists()
        {
            return Exists(true);
        }

        /// <summary>
        /// Determines whether the original path refers to an existing directory or file on disk.
        /// </summary>
        /// <param name="pathOnly">Indicates whether the existence of the folder or file is checked. If the
        /// original path contains a file name, pass a false to check for the existence of the file; 
        /// otherwise true to check for the folder.</param>
        /// <returns>true if path refers to an existing directory or file; otherwise, false.</returns>
        public bool Exists(bool pathOnly)
        {
            bool result = false;

            if ((pathOnly) || (!HasFileName))
            {
                result = SystemDirectory.Exists(FullPathWithoutFilename);
            }
            else
            {
                result = SystemFile.Exists(FullPath);
            }

            return result;
        }

        #endregion Methods
    }
}
