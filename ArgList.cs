// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgList.cs" company="Software Inc.">
//   A.Robson
// </copyright>
// <summary>
//   The argument list.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PlayList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class ArgList
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgList"/> class.
        /// </summary>
        /// <param name="subFolder">The sub folder.</param>
        /// <param name="changeFileName">Change file name.</param>
        /// <param name="removeBrackets">Remove brackets and content from music files.</param>
        public ArgList(bool subFolder)
        {
            this.SubFolder = subFolder;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to search sub folders.
        /// </summary>
        public bool SubFolder { get; set; }
    }
}
