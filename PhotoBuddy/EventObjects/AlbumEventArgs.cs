//-----------------------------------------------------------------------
// <copyright file="AlbumEventArgs.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales and Andrea Tan
// Date: Sept 28 2011
// Modified date: Oct 9 2011
// Description: An event args object that enables the passing of an album object after 
//              an event occurs.
//-----------------------------------------------------------------------
namespace PhotoBuddy.EventObjects
{
    using System;
    using PhotoBuddy.BusinessRule;

    /// <summary>
    /// Pass an album using events.
    /// </summary>
    /// <remarks>
    /// Author(s): Miguel Gonzales and Andrea Tan
    /// Date: Sept 28 2011
    /// Modified date: Oct 23 2011
    /// </remarks>
    public class AlbumEventArgs : EventArgs
    {
        /// <summary>
        /// The album to pass.
        /// </summary>
        private readonly Album album;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumEventArgs"/> class.
        /// </summary>
        /// <param name="albumName">The name of the album to pass.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// Date: Sept 28 2011
        /// Modified date: Oct 23 2011
        /// </remarks>
        public AlbumEventArgs(string albumName)
        {
            this.album = new Album(albumName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumEventArgs"/> class.
        /// </summary>
        /// <param name="theAlbum">The album object to pass</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// Date: Sept 28 2011
        /// Modified date: Oct 23 2011
        /// </remarks>
        public AlbumEventArgs(Album theAlbum)
        {
            this.album = theAlbum;
        }

        /// <summary>
        /// Gets the album.
        /// </summary>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// Date: Sept 28 2011
        /// Modified date: Oct 23 2011
        /// </remarks>
        public Album TheAlbum
        {
            get
            {
                return this.album;
            }
        }
    }
}
