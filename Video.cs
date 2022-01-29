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
    using System.IO;

    internal class Video
    {
        private static void Main(string[] args)
        {
            var argList = GetArguments(args);

            var fileDirectory = Environment.CurrentDirectory + @"\";

            IEnumerable<string> directories = new List<string>();

            if (argList.SubFolder)
            {
                var dirs = Directory.GetDirectories(fileDirectory, "*", SearchOption.TopDirectoryOnly);

                directories = GetDirectories(dirs);
            }
            else
            {
                // This isn't working - I need to add the current directory.
                directories = new string[] { fileDirectory };
            }

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
        }

        /// <summary>
        /// Create a playlist from the video files found.
        /// </summary>
        /// <param name="IEnumerable">The list of video files.</param>
        private static void CreatePlayList(IEnumerable<string> files)
        {
            var playListFolder = Path.GetDirectoryName(files.FirstOrDefault());
            var id = -1;
            var items = new List<Item>();

            foreach (var file in files)
            {
                var item = new Item();

                item.ItemId = ++id;
                item.Name = Path.GetFileName(file);
                var time = GetVideoDuration(file);
                item.Duration = time.TotalMilliseconds;
                items.Add(item);
            }

            BuildPlayList(items, playListFolder);
        }

        /// <summary>
        /// Build the playlist file.
        /// </summary>
        /// <param name="IEnnumerable">The List of video files.</param>
        /// <param name="playListFolder">The current folder.</param>
        private static void BuildPlayList(IEnumerable<Item> items, string playListFolder)
        {
            var playList = new StringBuilder();

            BuildHeader(playList);

            BuildPlayListItems(playList, items);

            BuildFooter(playList, items.Count());

            string[] delim = { Environment.NewLine, "\n" }; // "\n" added in case you manually appended a newline
            string[] lines = playList.ToString().Split(delim, StringSplitOptions.None);

            var outFile = playListFolder + "\\_video.xspf";
            var outStream = File.Create(outFile);
            var sw = new StreamWriter(outStream);

            foreach (var item in lines)
            {
                sw.WriteLine(item);
            }

            // flush and close
            sw.Flush();
            sw.Close();
        }

        /// <summary>
        /// Add items to the playlist.
        /// </summary>
        /// <param name="StringBuilder">The playlist items.</param>
        /// <param name="Count">The item count.</param>
        /// <returns>The <see cref="StringBuilder"/>partial playlist.</returns>
        private static StringBuilder BuildFooter(StringBuilder playList, int count)
        {
            playList.Append("\t</trackList>\n");
            playList.Append("\t<extension application=\"http://www.videolan.org/vlc/playlist/0\">\n");

            for (int i = 0; i < count; i++)
            {
                playList.Append("\t\t<vlc:item tid=\"" + i + "\"/>\n");
            }

            playList.Append("\t</extension>\n");
            playList.Append("</playlist>\n");

            return playList;
        }

        /// <summary>
        /// Add items to the playlist.
        /// </summary>
        /// <param name="StringBuilder">The playlist.</param>
        /// <param name="Item">The playlist items.</param>
        /// <returns>The <see cref="StringBuilder"/>partial playlist.</returns>
        private static StringBuilder BuildPlayListItems(StringBuilder playList, IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                playList.Append("\t\t<track>\n");
                playList.Append("\t\t<location>" + "file:///" + item.Name + "</location>\"\n");
                playList.Append("\t\t<duration>" + item.Duration + "</duration>\"\n");
                playList.Append("\t\t<extension application=\"http://www.videolan.org/vlc/playlist/0\">\n");
                playList.Append("\t\t\t<vlc:id>" + item.ItemId + "</vlc:id>\"\n");
                playList.Append("\t\t</extension>\n");
                playList.Append("\t\t</track>\n");
            }

            return playList;
        }

        /// <summary>
        /// Build the playlist header.
        /// </summary>
        /// <param name="IEnumerable">The List of video files.</param>
        /// <returns>The <see cref="IEnumerable"/>partial playlist.</returns>
        private static StringBuilder BuildHeader(StringBuilder playList)
        {
            playList.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n");
            playList.Append("<playlist xmlns=\"http://xspf.org/ns/0/\" xmlns:vlc=\"http://www.videolan.org/vlc/playlist/ns/0/\" version=\"1\">\n");
            playList.Append("\t<title>Playlist</title>\n");
            playList.Append("\t<trackList>\n");

            return playList;
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

        private static TimeSpan GetVideoDuration(string filePath)
        {
            using (var shell = ShellObject.FromParsingName(filePath))
            {
                Microsoft.WindowsAPICodePack.Shell.PropertySystem.IShellProperty prop = shell.Properties.System.Media.Duration;
                var t = (ulong)prop.ValueAsObject;
                return TimeSpan.FromTicks((long)t);
            }
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
