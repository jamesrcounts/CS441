//-----------------------------------------------------------------------
// <copyright file="SearchResultAlbum.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
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
        /// Gets the cover photo.
        /// </summary>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei.</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public Image CoverPhoto
        {
            get { return this.photoBucket.First().Image; }
        }

        /// <summary>
        /// Adds the photo.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei.</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public void AddPhoto(IPhoto photo)
        {
            this.photoBucket.Add(photo);
        }

        /// <summary>
        /// Adds a new photo.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="filePath">The file path.</param>
        /// <remarks>
        ///   <para>Authors: Jim Counts.</para>
        ///   <para>Created: 2011-11-04</para>
        /// </remarks>
        public void AddPhoto(string displayName, string filePath)
        {
            throw new NotSupportedException();
        }
        
        /// <summary>
        /// Adds the photo.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei.</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public void AddPhoto(string photoId, string displayName, string fileName)
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
        /// Determines whether the album contains the specified display name.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <returns>
        ///   <c>true</c> if the album contains the specified display name; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei.</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public bool ContainsName(string displayName)
        {
            return this.photoBucket.Any(photo => photo.DisplayName == displayName);
        }
    }
}
