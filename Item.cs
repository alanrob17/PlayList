﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Item.cs" company="Software Inc">
//   A.Robson
// </copyright>
// <summary>
//   Defines the Item type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PlayList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class Item
    {
        /// <summary>
        /// Gets or sets the item id.
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Duration in Milliseconds.
        /// </summary>
        public double Duration { get; set; }
    }
}