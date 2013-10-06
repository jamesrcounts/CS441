//-----------------------------------------------------------------------
// <copyright file="AlbumNameEventArgs.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales and Andrea Tan
// Date: Sept 28 2011
// Modified date: Oct 9 2011
// Description: An event args object that enables the passing of an album object after 
//              an event occurs.
//-----------------------------------------------------------------------
namespace PhotoBuddy.Models
{
    using System;

    /// <summary>
    /// Pass an album using events.
    /// </summary>
    /// <remarks>
    /// Author(s): Miguel Gonzales and Andrea Tan
    /// Date: Sept 28 2011
    /// Modified date: Oct 23 2011
    /// </remarks>
    public class AlbumNameEventArgs : EventArgs
    {
        /// <summary>
        /// The album to pass.
        /// </summary>
        private readonly string albumId;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumNameEventArgs"/> class.
        /// </summary>
        /// <param name="albumId">The name of the album to pass.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales, Andrea Tan, Jim Counts and Eric Wei
        /// Date: Sept 28 2011
        /// Modified date: Oct 26 2011
        /// </remarks>
        public AlbumNameEventArgs(string albumId)
        {
            this.albumId = albumId;
        }

        /// <summary>
        /// Gets the album name.
        /// </summary>
        /// <value>
        /// The name of the album.
        /// </value>
        /// <remarks>
        /// Author(s): Miguel Gonzales, Andrea Tan, Jim Counts and Eric Wei
        /// Date: Sept 28 2011
        /// Modified date: Oct 23 2011
        /// </remarks>
        public string AlbumName
        {
            get
            {
                return this.albumId;
            }
        }
    }
}
