//-----------------------------------------------------------------------
// <copyright file="Album.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales, Andrea Tan, Jim Counts and Eric Wei
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
    ///   <para>Author(s): Miguel Gonzales, Andrea Tan, Jim Counts and Eric Wei</para>
    ///   <para>Modified: 2011-10-28</para>
    /// </remarks>
    [DebuggerDisplay("{AlbumId}")]
    public sealed class Album : IAlbum
    {
        /// <summary>
        /// Backing store for indexed photo list.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-28</para>
        /// </remarks>
        private readonly IDictionary<string, IPhoto> photos = new Dictionary<string, IPhoto>();

        /// <summary>
        /// Backing store for the cover photo.
        /// </summary>
        private IPhoto coverPhoto;

        /// <summary>
        /// Initializes a new instance of the <see cref="Album"/> class.
        /// </summary>
        /// <param name="repository">The album repository.</param>
        /// <param name="albumId">The album id.</param>
        /// <param name="photos">The photos.</param>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei</para>
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
        /// Occurs when a photo is added.
        /// </summary>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-06</para>
        /// </remarks>
        public event EventHandler<EventArgs<IPhoto>> PhotoAddedEvent;

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
        /// Gets or sets the cover photo.
        /// </summary>
        /// <value>
        /// The cover photo.
        /// </value>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created On: 2011-11-07</para>
        /// </remarks>
        public IPhoto CoverPhoto
        {
            get
            {
                return this.coverPhoto;
            }

            set
            {
                // Cover photo must exist in this album.
                if (value != null && !this.ContainsPhoto(value.PhotoId))
                {
                    return;
                }

                this.coverPhoto = value;
            }
        }

        /// <summary>
        /// Gets the photos in the album.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
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
        /// Gets the count of photos in the album.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
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
        /// Adds the photo.
        /// </summary>
        /// <param name="photo">The photo to add.</param>
        /// <returns>The attached photo.</returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public IPhoto AddPhoto(IPhoto photo)
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
            var attachedPhoto = this.GetPhoto(photo.PhotoId);
            this.OnPhotoAddedEvent(this, new EventArgs<IPhoto>(attachedPhoto));
            return attachedPhoto;
        }

        /// <summary>
        /// Adds the photo.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>The attached photo.</returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created On: 2011-11-06</para>
        /// </remarks>
        public IPhoto AddPhoto(string filePath)
        {
            string dn = Path.GetFileNameWithoutExtension(filePath);
            int maxLen = PhotoBuddy.Properties.Settings.Default.MaxNameLength;
            int prefixLen = maxLen - 4;
            int matchCount = this.GetPrefixMatchCount(dn, prefixLen);
            int subKeyLen = Math.Min(prefixLen, dn.Length);
            int keyLen = Math.Min(maxLen, dn.Length);

            string displayName = dn.Substring(0, keyLen);
            if (matchCount != 0)
            {
                displayName = displayName.Substring(0, subKeyLen) + matchCount;
            }

            return this.AddPhoto(displayName, filePath);
        }

        /// <summary>
        /// Adds the photo to an album.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="filePath">The file path.</param>
        /// <returns>The attached photo.</returns>
        /// <remarks>
        ///   <para>Author(s): Miguel Gonzales, Andrea Tan, Jim Counts and Eric Wei</para>
        ///   <para>Modified: 2011-11-04</para>
        /// </remarks>
        public IPhoto AddPhoto(string displayName, string filePath)
        {
            // Copies the file to the secret location.
            string photoId = Photo.GeneratePhotoKey(filePath);
            if (this.ContainsPhoto(photoId))
            {
                return null;
            }
            
            string storagePath = AlbumRepository.StoreFile(filePath, photoId);
            string storageName = Path.GetFileName(storagePath);

            // Put the photo in the album data structure.
            this.AddPhoto(photoId, displayName, storageName);
            IPhoto attachedPhoto = this.GetPhoto(photoId);
            this.OnPhotoAddedEvent(this, new EventArgs<IPhoto>(attachedPhoto));
            return attachedPhoto;
        }

        /// <summary>
        /// Adds the specified photo.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The attached photo.</returns>
        /// <remarks>
        ///   <para>Author(s): Miguel Gonzales, Andrea Tan, Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-28</para>
        /// </remarks>
        public IPhoto AddPhoto(string photoId, string displayName, string fileName)
        {
            this.AddPhoto(new Photo(this, photoId, displayName, fileName));
            IPhoto attachedPhoto = this.GetPhoto(photoId);
            this.OnPhotoAddedEvent(this, new EventArgs<IPhoto>(attachedPhoto));
            return attachedPhoto;
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
            if (this.Count <= 0)
            {
                return PhotoBuddy.Properties.Resources.PhotoBuddy.ToBitmap();
            }

            if (this.coverPhoto == null)
            {
                return this.Photos.First().CreateThumbnail(maxWidth, maxHeight);
            }

            var thumbnail = this.coverPhoto.CreateThumbnail(maxWidth, maxHeight);

            if (thumbnail == null)
            {
                return this.Photos.First().CreateThumbnail(maxWidth, maxHeight);
            }

            return thumbnail;
        }

        /// <summary>
        /// Determines whether the album contains the specified photo id.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns>
        ///   <c>true</c> if the album contains the specified photo id; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
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
        ///   <para>Author: Jim Counts and Eric Wei</para>
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
        ///   <para>Author: Jim Counts and Eric Wei</para>
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
        ///   <para>Author: Jim Counts and Eric Wei</para>
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
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-27</para>
        ///   <para>Modified: 2011-10-28</para>
        /// </remarks>
        public void RemovePhoto(string photoId)
        {
            var photo = this.GetPhoto(photoId);
            if (this.coverPhoto == photo)
            {
                this.coverPhoto = null;
            }

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

        /// <summary>
        /// Gets the number of photos that have display names matching the prefix of the specified display name.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="prefixLength">Length of the prefix.</param>
        /// <returns>
        /// The number of matches
        /// </returns>
        /// <remarks>
        /// Author: Jim Counts and Eric Wei
        /// Created: 2011-11-06
        /// </remarks>
        public int GetPrefixMatchCount(string displayName, int prefixLength)
        {
            int subKeyLen = Math.Min(prefixLength, displayName.Length);
            string subKey = displayName.Substring(0, subKeyLen);
            var prefixMatches = from key in this.photos.Values.Select(photo => photo.DisplayName)
                                where key.StartsWith(subKey, StringComparison.OrdinalIgnoreCase)
                                select key;
            return prefixMatches.Count();
        }

        /// <summary>
        /// Raises the photo added event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PhotoBuddy.EventArgs&lt;PhotoBuddy.Models.IPhoto&gt;"/> instance containing the event data.</param>
        /// <remarks>
        /// Author: Jim Counts and Eric Wei
        /// Created: 2011-11-06
        /// </remarks>
        public void OnPhotoAddedEvent(object sender, EventArgs<IPhoto> e)
        {
            EventHandler<EventArgs<IPhoto>> handler = this.PhotoAddedEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }
}
