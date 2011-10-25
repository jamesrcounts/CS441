//-----------------------------------------------------------------------
// <copyright file="Albums.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales and Andrea Tan
// Date: Sept 28 2011
// Modified date: Oct 9 2011
// High Level Description: this class is responsible for numerous collections 
//                         of Album objects. In addition, providing the mean to remove and add 
//                         specific album that its looking for. 
//                         as well as to search a specific album that is being requested from 
//                         the presenter.
//-----------------------------------------------------------------------
namespace PhotoBuddy.BusinessRule
{
    using System;
    using System.Collections;

    /// <summary>
    /// A collection of <see cref="Album"/> objects.
    /// </summary>
    public class Albums 
    {
        /// <summary>
        /// Backing store for member items.
        /// </summary>
        private readonly Hashtable AlbumTable = new Hashtable();

        /// <summary>
        /// Gets the albums list.
        /// </summary>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public Hashtable AlbumList
        {
            get { return this.AlbumTable; }
        }

        /// <summary>
        /// Gets the album.
        /// </summary>
        /// <param name="key">The album id (name).</param>
        /// <returns>
        /// the album object.
        /// </returns>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public Album GetAlbum(string key)
        {
            return (Album)this.AlbumTable[key];
        }

        /// <summary>
        /// Adds the album to the collection.
        /// </summary>
        /// <param name="albumToAdd">The new album.</param>
        /// <returns>true if a new album was added to the collection; otherwise false.</returns>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public bool AddAlbum(Album albumToAdd)
        {
            if (!this.AlbumTable.ContainsKey(albumToAdd.AlbumID))
            {
                this.AlbumTable.Add(albumToAdd.AlbumID, albumToAdd);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the specified album name is already used.
        /// </summary>
        /// <param name="albumName">Name of the album.</param>
        /// <returns>
        ///   <c>true</c> if the specified album name is already used; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public bool IsExistingAlbumName(string albumName)
        {
            return this.AlbumTable.ContainsKey(albumName);
        }
    }
}