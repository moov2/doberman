using System;
using Doberman.Model;
using System.IO;

namespace Doberman.Checks
{
    public class FileExistsCheck : ICheck
    {
        private const string CheckName = "File Exists";

        /// <summary>
        /// Path to file that is being checked that it exists.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Original path entered into the constructor.
        /// </summary>
        private string _originalPath;

        /// <summary>
        /// Returns the base directory of this assembly.
        /// </summary>
        public string BaseDirectory
        {
            get { return AppDomain.CurrentDomain.BaseDirectory + "\\"; }
        }

        public FileExistsCheck(string path)
        {
            _originalPath = path;
            Path = NeedsBaseDirectory(_originalPath) ? BaseDirectory + _originalPath : _originalPath;
        }

        public DobermanResult Execute()
        {
            var result = new DobermanResult { Check = CheckName };

            if (File.Exists(Path))
            {
                result.Success = true;
                result.Detail = _originalPath + " file does exist.";
            }
            else if (Directory.Exists(Path))
            {
                result.Success = true;
                result.Detail = _originalPath + " directory does exist.";
            }
            else
            {
                result.Success = false;
                result.Detail = "Unable to locate file or directory matching " + _originalPath + ".";
            }

            return result;
        }

        /// <summary>
        /// Flag indicating whether the path given requires the
        /// base directory.
        /// </summary>
        /// <param name="path">Path to be verified.</param>
        /// <returns>True if base directory needed, otherwise false.</returns>
        private static bool NeedsBaseDirectory(string path)
        {
            return !System.IO.Path.IsPathRooted(path);
        }
    }
}
