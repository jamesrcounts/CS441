//-----------------------------------------------------------------------
// <copyright file="XmlAlbum.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace PhotoBuddy.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    /// Decorates an <see cref="IAlbum"/> instance with an <see cref="XElement"/> and synchronizes the data in each.
    /// </summary>
    /// <remarks>
    ///   <para>Author: Jim Counts and Eric Wei</para>
    ///   <para>Created: 2011-11-02</para>
    /// </remarks>
    [DebuggerDisplay("{AlbumId}")]
    public class XmlAlbum : IAlbum
    {
        /// <summary>
        /// string literal: id_tag
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        private const string IdTag = "id_tag";

        /// <summary>
        /// string literal: album
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        private const string AlbumTag = "album";

        /// <summary>
        /// string literal: photo
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        private const string PhotoTag = "photo";

        /// <summary>
        /// The decorated album.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        private readonly IAlbum decoratedAlbum;

        /// <summary>
        /// A reference to the XML element this album is derived from.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-28</para>
        /// </remarks>
        private readonly XElement albumElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlAlbum"/> class.
        /// </summary>
        /// <param name="albumRepository">The album repository.</param>
        /// <param name="albumElement">The album element.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public XmlAlbum(AlbumRepository albumRepository, XElement albumElement)
        {
            this.albumElement = albumElement;
            string albumId = this.albumElement.Attribute(IdTag).Value;
            IEnumerable<IPhoto> photos = from photoElement in this.albumElement.Descendants(PhotoTag)
                                         select new XmlPhoto(this, photoElement);
            this.decoratedAlbum = new Album(albumRepository, albumId, photos);
            this.decoratedAlbum.PhotoAddedEvent += this.OnPhotoAddedEvent;
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
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public string AlbumId
        {
            get
            {
                return this.decoratedAlbum.AlbumId;
            }

            set
            {
                this.albumElement.Attribute(IdTag).Value = value;
                this.decoratedAlbum.AlbumId = value;
            }
        }

        /// <summary>
        /// Gets the photos.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public IEnumerable<IPhoto> Photos
        {
            get
            {
                return this.decoratedAlbum.Photos;
            }
        }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public AlbumRepository Repository
        {
            get
            {
                return this.decoratedAlbum.Repository;
            }
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public int Count
        {
            get
            {
                return this.decoratedAlbum.Count;
            }
        }

        /// <summary>
        /// Creates a new album element.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>A new XML element which describes an empty album.</returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-28</para>
        /// </remarks>
        public static XElement CreateAlbumElement(string name)
        {
            var albumElement = new XElement(AlbumTag);
            //// albumElement.Add(new XElement(PhotoTag));
            albumElement.Add(new XAttribute(IdTag, name));
            return albumElement;
        }

        /// <summary>
        /// Adds the photo.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <returns>The attached photo.</returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public IPhoto AddPhoto(IPhoto photo)
        {
            return this.AddPhoto(photo.PhotoId, photo.DisplayName, photo.FullPath);
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
            // Copies the file to the secret location.
            string photoId = Photo.GeneratePhotoKey(filePath);
            string storagePath = AlbumRepository.StoreFile(filePath, photoId);
            string storageName = Path.GetFileName(storagePath);

            return this.AddPhoto(photoId, displayName, storageName);
        }

        /// <summary>
        /// Adds the photo.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The attached photo.</returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public IPhoto AddPhoto(string photoId, string displayName, string fileName)
        {
            if (this.ContainsPhoto(photoId))
            {
                throw new ArgumentException("Photo with the same id already exists in the album.", "photoId");
            }

            if (this.ContainsName(displayName))
            {
                throw new ArgumentException("Photo with the same display name already exists in the album.", "displayName");
            }

            // Update XML
            XElement photoElement = XmlPhoto.CreatePhotoElement(photoId, displayName, fileName);
            this.albumElement.Add(photoElement);
            this.Repository.SaveAlbums();

            // Update Indexes
            return this.decoratedAlbum.AddPhoto(new XmlPhoto(this, photoElement));
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
            return this.decoratedAlbum.ContainsPhoto(photoId);
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
            return this.decoratedAlbum.ContainsName(displayName);
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
            return this.decoratedAlbum.CreateThumbnail(maxWidth, maxHeight);
        }

        /// <summary>
        /// Gets the photo.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns>The photo with the specified id.</returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public IPhoto GetPhoto(string photoId)
        {
            return this.decoratedAlbum.GetPhoto(photoId);
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public void Delete()
        {
            this.albumElement.Remove();
            this.decoratedAlbum.Delete();
        }

        /// <summary>
        /// Removes the photo.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public void RemovePhoto(string photoId)
        {
            this.decoratedAlbum.RemovePhoto(photoId);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-06</para>
        /// </remarks>
        public override string ToString()
        {
            return this.decoratedAlbum.ToString();
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
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public int GetPrefixMatchCount(string displayName, int prefixLength)
        {
            return this.decoratedAlbum.GetPrefixMatchCount(displayName, prefixLength);
        }

        /// <summary>
        /// Adds the photo.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>The attached photo.</returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-06</para>
        /// </remarks>
        public IPhoto AddPhoto(string filePath)
        {
            var photo = this.decoratedAlbum.AddPhoto(filePath);

            // Update XML
            XElement photoElement = XmlPhoto.CreatePhotoElement(photo.PhotoId, photo.DisplayName, photo.FileName);
            this.albumElement.Add(photoElement);
            this.Repository.SaveAlbums();

            return photo;
        }

        /// <summary>
        /// Raises when photo added event.
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
