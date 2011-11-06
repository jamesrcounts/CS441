//-----------------------------------------------------------------------
// <copyright file="Album.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales, Andrea Tan, Jim Counts
// Date: Sept 28 2011
// Modified date: Oct 26 2011
// Description: this class is responsible in instantiation of the album objects.
//              this class also provides the mean of accessing the album contents
//              as well as updating its contents as well
//-----------------------------------------------------------------------
namespace PhotoBuddy.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// An album contains an album id and a list of photos.
    /// </summary>
    /// <remarks>
    ///   <para>Author(s): Miguel Gonzales, Andrea Tan, Jim Counts</para>
    ///   <para>Modified: 2011-10-28</para>
    /// </remarks>
    [DebuggerDisplay("{AlbumId}")]
    public sealed class Album : IAlbum
    {
        /// <summary>
        /// Backing store for indexed photo list.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-28</para>
        /// </remarks>
        private readonly IDictionary<string, IPhoto> photos = new Dictionary<string, IPhoto>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Album"/> class.
        /// </summary>
        /// <param name="repository">The album repository.</param>
        /// <param name="albumId">The album id.</param>
        /// <param name="photos">The photos.</param>
        /// <remarks>
        ///   <para>Authors: Jim Counts</para>
        ///   <para>Created: 2011-10-28</para>
        /// </remarks>
        public Album(AlbumRepository repository, string albumId, IEnumerable<IPhoto> photos)
        {
            this.Repository = repository;
            this.AlbumId = albumId;
            foreach (var photo in photos)
            {
                this.photos.Add(photo.PhotoId, photo);
            }
        }

        /// <summary>
        /// Gets or sets the album ID.
        /// </summary>
        /// <value>
        /// The album ID.
        /// </value>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan.
        /// </remarks>
        public string AlbumId { get; set; }

        /// <summary>
        /// Gets the photos in the album.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-27</para>
        ///   <para>Modified: 2011-10-28</para>
        /// </remarks>
        public IEnumerable<IPhoto> Photos
        {
            get
            {
                return this.photos.Values;
            }
        }

        /// <summary>
        /// Gets a reference to the repository this album belongs to.
        /// </summary>
        /// <remarks>
        /// <para>Authors(s): Jim Counts and Eric Wei</para>
        /// <para>Created: 2011-10-25</para></remarks>
        public AlbumRepository Repository { get; private set; }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-26</para>
        /// </remarks>
        public int Count
        {
            get
            {
                return this.photos.Count;
            }
        }

        /// <summary>
        /// Gets the cover photo.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-26</para>
        /// </remarks>
        public Image CoverPhoto
        {
            get
            {
                if (this.Count <= 0)
                {
                    return PhotoBuddy.Properties.Resources.PhotoBuddy.ToBitmap();
                }

                return this.Photos.First().Image;
            }
        }

        /// <summary>
        /// Adds the specified photo.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <remarks>
        ///   <para>Author(s): Miguel Gonzales, Andrea Tan, Jim Counts</para>
        ///   <para>Created: 2011-10-28</para>
        /// </remarks>
        public void AddPhoto(string photoId, string displayName, string fileName)
        {
            this.AddPhoto(new Photo(this, photoId, displayName, fileName));
        }

        /// <summary>
        /// Adds the photo.
        /// </summary>
        /// <param name="photo">The photo to add.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public void AddPhoto(IPhoto photo)
        {
            if (this.ContainsPhoto(photo.PhotoId))
            {
                throw new ArgumentException("Photo with the same id already exists in the album.", "photo");
            }

            if (this.ContainsName(photo.DisplayName))
            {
                throw new ArgumentException("Photo with the same display name already exists in the album.", "photo");
            }

            this.photos.Add(photo.PhotoId, photo);
        }

        /// <summary>
        /// Adds the photo to an album.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="filePath">The file path.</param>
        /// <remarks>
        ///   <para>Author(s): Miguel Gonzales, Andrea Tan, Jim Counts</para>
        ///   <para>Modified: 2011-11-04</para>
        /// </remarks>
        public void AddPhoto(string displayName, string filePath)
        {
            // Copies the file to the secret location.
            string photoId = Photo.GeneratePhotoKey(filePath);
            string storagePath = AlbumRepository.StoreFile(filePath, photoId);
            string storageName = Path.GetFileName(storagePath);

            // Put the photo in the album data structure.
            this.AddPhoto(photoId, displayName, storageName);           
        }
        
        /// <summary>
        /// Determines whether the album contains the specified photo id.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns>
        ///   <c>true</c> if the album contains the specified photo id; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public bool ContainsPhoto(string photoId)
        {
            return this.photos.ContainsKey(photoId);
        }

        /// <summary>
        /// Determines whether the album contains the specified display name.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <returns>
        ///   <c>true</c> if the album contains the specified display name; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public bool ContainsName(string displayName)
        {
            return this.photos.Values.Any(photo => photo.DisplayName == displayName);
        }

        /// <summary>
        /// Gets the specified photo.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns>The specified photo if found; otherwise null.</returns>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-27</para>
        /// </remarks>
        public IPhoto GetPhoto(string photoId)
        {
            IPhoto photo = null;
            return this.photos.TryGetValue(photoId, out photo) ? photo : null;
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-28</para>
        ///   <para>Modified: 2011-10-28</para>
        /// </remarks>
        public void Delete()
        {
            foreach (var photo in this.photos.Values)
            {
                photo.Delete();
                photo.Dispose();
            }

            this.Repository.DeleteAlbum(this.AlbumId);
        }

        /// <summary>
        /// Removes the specified photo.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-27</para>
        ///   <para>Modified: 2011-10-28</para>
        /// </remarks>
        public void RemovePhoto(string photoId)
        {
            var photo = this.GetPhoto(photoId);
            photo.Delete();
            this.photos.Remove(photoId);
            photo.Dispose();
        }

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
            return this.AlbumId;
        }
    }
}
