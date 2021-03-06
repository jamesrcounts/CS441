﻿//-----------------------------------------------------------------------
// <copyright file="SearchResultAlbum.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Jim Counts and Eric Wei
// Date: Nov 6 2011
// Modified date: Nov 26 2011
// Description: this class is responsible in instantiation of a search album object.
//              The search album does not follow normal album rules.
//-----------------------------------------------------------------------
namespace PhotoBuddy.Models
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    /// <summary>
    /// An album to host search results, does not obey the normal album rules
    /// that restrict duplicates.
    /// </summary>
    /// <remarks>
    ///   <para>Authors: Jim Counts and Eric Wei.</para>
    ///   <para>Created: 2011-11-03</para>
    /// </remarks>
    public class SearchResultAlbum : IAlbum
    {
        /// <summary>
        /// The album repository.
        /// </summary>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei.</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        private readonly AlbumRepository albumRepository;

        /// <summary>
        /// A simple list of photos.
        /// </summary>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei.</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        private readonly IList<IPhoto> photoBucket = new List<IPhoto>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResultAlbum"/> class.
        /// </summary>
        /// <param name="albumRepository">The album repository.</param>
        /// <param name="albumId">The album id.</param>
        /// <param name="photos">The photos.</param>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei.</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public SearchResultAlbum(AlbumRepository albumRepository, string albumId, IEnumerable<IPhoto> photos)
        {
            this.AlbumId = albumId;
            this.albumRepository = albumRepository;

            foreach (var photo in photos)
            {
                this.photoBucket.Add(photo);
            }
        }

        /// <summary>
        /// Occurs when a photo is added to the album.
        /// </summary>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-06</para>
        /// </remarks>
        public event EventHandler<EventArgs<IPhoto>> PhotoAddedEvent;

        /// <summary>
        /// Gets or sets the album id.
        /// </summary>
        /// <value>
        /// The album id.
        /// </value>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei.</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public string AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the cover photo.
        /// </summary>
        /// <value>
        /// The cover photo.
        /// </value>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created On: 2011-11-07</para>
        /// </remarks>
        public IPhoto CoverPhoto { get; set; }

        /// <summary>
        /// Gets the photos.
        /// </summary>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei.</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public IEnumerable<IPhoto> Photos
        {
            get
            {
                return this.photoBucket;
            }
        }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei.</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public AlbumRepository Repository
        {
            get
            {
                return this.albumRepository;
            }
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei.</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public int Count
        {
            get { return this.photoBucket.Count; }
        }

        /// <summary>
        /// Adds the photo.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <returns>The attached photo.</returns>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei.</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public IPhoto AddPhoto(IPhoto photo)
        {
            this.photoBucket.Add(photo);
            return photo;
        }

        /// <summary>
        /// Adds a new photo.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="filePath">The file path.</param>
        /// <returns>The attached photo.</returns>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei.</para>
        ///   <para>Created: 2011-11-04</para>
        /// </remarks>
        public IPhoto AddPhoto(string displayName, string filePath)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Adds the photo.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The attached photo.</returns>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei.</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public IPhoto AddPhoto(string photoId, string displayName, string fileName)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets the photo.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns>
        /// The photo with the specified id.
        /// </returns>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei.</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public IPhoto GetPhoto(string photoId)
        {
            throw new System.NotSupportedException();
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei.</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public void Delete()
        {
            throw new System.NotSupportedException();
        }

        /// <summary>
        /// Removes the photo.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei.</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public void RemovePhoto(string photoId)
        {
            throw new System.NotSupportedException();
        }

        /// <summary>
        /// Determines whether the album contains the specified photo id.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns>
        ///   <c>true</c> if the album contains the specified photo id; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei.</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public bool ContainsPhoto(string photoId)
        {
            return this.photoBucket.Any(photo => photo.PhotoId == photoId);
        }

        /// <summary>
        /// Creates the thumbnail.
        /// </summary>
        /// <param name="maxWidth">Maximum Width.</param>
        /// <param name="maxHeight">Maximum Height.</param>
        /// <returns>
        /// A small version of the image.
        /// </returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-06</para>
        /// </remarks>
        public Image CreateThumbnail(int maxWidth, int maxHeight)
        {
            throw new System.NotSupportedException();
        }

        /// <summary>
        /// Determines whether the album contains the specified display name.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <returns>
        ///   <c>true</c> if the album contains the specified display name; otherwise, <c>false</c>.
        /// </returns>
        /// <returns>The attached photo.</returns>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei.</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public bool ContainsName(string displayName)
        {
            return this.photoBucket.Any(photo => photo.DisplayName == displayName);
        }

        /// <summary>
        /// Gets the number of photos that have display names matching the prefix of the specified display name.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="prefixLength">Length of the prefix.</param>
        /// <returns>
        /// The number of matches
        /// </returns>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-06</para>
        /// </remarks>
        public int GetPrefixMatchCount(string displayName, int prefixLength)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Adds the photo.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>The attached photo.</returns>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-06</para>
        /// </remarks>
        public IPhoto AddPhoto(string filePath)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Raises photo added event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PhotoBuddy.EventArgs&lt;PhotoBuddy.Models.IPhoto&gt;"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-06</para>
        /// </remarks>
        public virtual void OnPhotoAddedEvent(object sender, EventArgs<IPhoto> e)
        {
            EventHandler<EventArgs<IPhoto>> handler = this.PhotoAddedEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }
}
