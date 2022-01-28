// --------------------------------------------------------------------------------------------------------------------
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
