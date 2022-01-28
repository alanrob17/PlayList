// ------------------------------------ -------------------------------------------------------------------------------
// <copyright file="PlayList.cs" company="Software Inc.">
//   A.Robson
// </copyright>
// <summary>
//   Create a playlist of video training materials.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PlayList
{
    using Microsoft.WindowsAPICodePack.Shell;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Text.RegularExpressions;
    using System.Text;
    using System.IO;

    internal class Video
    {
        private static void Main(string[] args)
        {
            var argList = GetArguments(args);

            var fileDirectory = Environment.CurrentDirectory + @"\";

            var dirs = Directory.GetDirectories(fileDirectory, "*", SearchOption.TopDirectoryOnly);

            var directories = GetDirectories(dirs);

            foreach (var dir in directories)
            {
                // we have to create a video list for each directory
                IEnumerable<string> files = GetFileList(dir);

                // Create the playlist here
                if (files.Count() > 0)
                {
                    CreatePlayList(files);
                }
            }

            Console.ReadLine();

        }

        /// <summary>
        /// Get a list of directories that start with a numeric character.
        /// </summary>
        /// <param name="dirs">The complete directory list.</param>
        /// <returns>The <see cref="IEnumerable"/>ammended list of directories.</returns>
        private static IEnumerable<string> GetDirectories(string[] dirs)
        {
            var newDirs = new List<string>();
            var dirPattern = "^[0-9].*";

            foreach (string dir in dirs)
            {
                var iposn = dir.LastIndexOf("\\");
                var directory = dir.Substring(iposn + 1);

                if (Regex.IsMatch(directory, dirPattern))
                {
                    newDirs.Add(dir);
                }
            }

            return newDirs;
        }

        /// <summary>
        /// Get a list of all files in a folder structure.
        /// </summary>
        /// <param name="folder">The folder name.</param>
        /// <returns>A list of video files.</returns>
        private static IEnumerable<string> GetFileList(string folder)
        {
            var fileList = GetFiles(folder);

            return fileList;
        }

        /// <summary>
        /// Get list of files.
        /// </summary>
        /// <param name="fileList">The image List.</param>
        private static IEnumerable<string> GetFiles(string folder)
        {
            var fileList = Directory.EnumerateFiles(folder, "*.*", SearchOption.AllDirectories)
                                .Where(s => s.EndsWith(".mp4") || s.EndsWith(".mkv"));

            return fileList;
        }

        /// <summary>
        /// Get command line arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The <see cref="bool"/>subfolder status.</returns>
        private static ArgList GetArguments(IList<string> args)
        {
            var subFolders = false;

            if (args.Count == 1)
            {
                if (args[0].ToLowerInvariant().Contains("s"))
                {
                    subFolders = true;
                }
            }

            if (args.Count == 1)
            {
                var argList = new ArgList(subFolders);

                return argList;
            }
            else
            {
                var arglist = new ArgList(false);

                return arglist;
            }
        }
    }
}
