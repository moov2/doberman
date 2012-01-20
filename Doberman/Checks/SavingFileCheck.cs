using System;
using Doberman.Model;
using System.Reflection;
using System.IO;

namespace Doberman.Checks
{
    public class SavingFileCheck : ICheck
    {
        private const string CheckName = "Saving File";

        /// <summary>
        /// Name of the dummy file that is attempted to be saved.
        /// </summary>
        public const string DummyFileName = "doberman-test.png";

        /// <summary>
        /// Directory that is being checked that a file can be saved too.
        /// </summary>
        public string Directory { get; private set; }

        /// <summary>
        /// Full path of the dummy file being saved.
        /// </summary>
        private string DummyFilePath
        {
            get 
            {
                var path = "";
                
                if (Directory.StartsWith(AppDomain.CurrentDomain.BaseDirectory))
                    path += Directory;
                else
                {
                    path += AppDomain.CurrentDomain.BaseDirectory;

                    path = (Directory.StartsWith(@"\")) ? path + Directory : path + "\\" + Directory;
                }

                if (!Directory.EndsWith(@"\"))
                    path += "\\";

                return path + DummyFileName;
            }
        }

        public SavingFileCheck(string directoryToCheck)
        {
            Directory = directoryToCheck;
        }

        /// <summary>
        /// Deletes the dummy file if it exists.
        /// </summary>
        private void DeleteDummyFile()
        {
            if (File.Exists(DummyFilePath))
                File.Delete(DummyFilePath);
        }

        public DobermanResult Execute()
        {
            var result = new DobermanResult { Check = CheckName };

            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var imageStream = assembly.GetManifestResourceStream("Doberman.Resources.doberman.png");

                SaveDummyFileToDisk(imageStream);
                imageStream.Close();

                result.Success = File.Exists(DummyFilePath);

                if (result.Success)
                    result.Detail = "Saved dummy file to " + Directory + " successfully.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Detail = ex.Message;
            }

            DeleteDummyFile();

            return result;
        }

        /// <summary>
        /// Saves the dummy file to the directory specified.
        /// </summary>
        /// <param name="stream">Stream of the dummy file.</param>
        private void SaveDummyFileToDisk(Stream stream)
        {
            var fileStream = File.Create(DummyFilePath);
            stream.CopyTo(fileStream);
            fileStream.Close();
        }
    }
}
