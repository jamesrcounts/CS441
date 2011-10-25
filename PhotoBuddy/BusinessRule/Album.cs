//-----------------------------------------------------------------------
// <copyright file="Album.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales and Andrea Tan
// Date: Sept 28 2011
// Modified date: Oct 23 2011
// Description: this class is responsible in instantiation of the album objects.
//              this class also provides the mean of accessing the album contents
//              as well as updating its contents as well
//-----------------------------------------------------------------------
namespace PhotoBuddy.BusinessRule
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// An album contains an album id and a list of photos.
    /// </summary>
    /// <remarks>
    /// Author(s): Miguel Gonzales and Andrea Tan
    /// </remarks>
    [DebuggerDisplay("{albumID}")]
    public class Album
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Album"/> class.
        /// </summary>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public Album()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Album"/> class.
        /// </summary>
        /// <param name="albumName">Name of the album.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public Album(string albumName)
        {
            this.AlbumID = albumName;
            this.PhotoList = new Photos();
        }

        /// <summary>
        /// Gets or sets the album ID.
        /// </summary>
        /// <value>
        /// The album ID.
        /// </value>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public string AlbumID { get; set; }

        /// <summary>
        /// Gets or sets the photo objects.
        /// </summary>
        /// <value>
        /// The photo objects.
        /// </value>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public Photos PhotoList { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public override string ToString()
        {
            return this.AlbumID;
        }
    }
}
