//-----------------------------------------------------------------------
// <copyright file="XmlPhoto.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace PhotoBuddy.Models
{
    using System;
    using System.Drawing;
    using System.Xml.Linq;
    using PhotoBuddy.Common;

    /// <summary>
    /// Decorates an <see cref="IPhoto"/> implementation with an <see cref="XmlElement"/> (attached to a document) that describes
    /// the photo.
    /// </summary>
    /// <remarks>
    ///   <para>Author: Jim Counts and Eric Wei</para>
    ///   <para>Created: 2011-11-03</para>
    /// </remarks>
    public sealed class XmlPhoto : IPhoto, IDisposable
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
        /// string literal: photo
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        private const string PhotoTag = "photo";

        /// <summary>
        /// string literal: copied_path
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        private const string CopiedPathTag = "copied_path";

        /// <summary>
        /// string literal: display_name;
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        private const string DisplayNameTag = "display_name";

        /// <summary>
        /// The XML element which describes this photo.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        private readonly XElement photoElement;

        /// <summary>
        /// Backing store for parsed photo data.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        private IPhoto decoratedPhoto;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlPhoto"/> class.
        /// </summary>
        /// <param name="parentAlbum">The parent album.</param>
        /// <param name="photoElement">The photo element.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public XmlPhoto(IAlbum parentAlbum, XElement photoElement)
        {
            this.photoElement = photoElement;
            string photoId = this.photoElement.Attribute(IdTag).Value;
            string displayName = this.photoElement.Element(DisplayNameTag).Value;
            string fileName = this.photoElement.Element(CopiedPathTag).Value;
            this.decoratedPhoto = new Photo(parentAlbum, photoId, displayName, fileName);
        }

        /// <summary>
        /// Gets the album.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public IAlbum Album
        {
            get
            {
                return this.decoratedPhoto.Album;
            }
        }

        /// <summary>
        /// Gets the photo id.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public string PhotoId
        {
            get
            {
                return this.decoratedPhoto.PhotoId;
            }
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public string DisplayName
        {
            get
            {
                return this.decoratedPhoto.DisplayName;
            }

            set
            {
                this.photoElement.Element(DisplayNameTag).Value = value;
                this.decoratedPhoto.DisplayName = value;
            }
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public string FileName
        {
            get
            {
                return this.decoratedPhoto.FileName;
            }
        }

        /// <summary>
        /// Gets the full path.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public string FullPath
        {
            get
            {
                return this.decoratedPhoto.FullPath;
            }
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public Image Image
        {
            get
            {
                return this.decoratedPhoto.Image;
            }
        }

        /// <summary>
        /// Creates a new photo element.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>An XML element which describes the photo.</returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public static XElement CreatePhotoElement(string photoId, string displayName, string fileName)
        {
            if (Constants.MaxNameLength < displayName.Length)
            {
                var nameTooLongMessage = Format.Culture("Photo name is too long.  Please enter a name up to {0} characters.", Constants.MaxNameLength);
                throw new ArgumentException(nameTooLongMessage, "displayName");
            }

            XElement photoElement = new XElement(PhotoTag);
            photoElement.Add(new XAttribute(IdTag, photoId));
            photoElement.Add(new XElement(CopiedPathTag, fileName));
            photoElement.Add(new XElement(DisplayNameTag, displayName));
            return photoElement;
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-06</para>
        /// </remarks>
        public void Close()
        {
            this.decoratedPhoto.Close();
        }

        /// <summary>
        /// Deletes this photo.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public void Delete()
        {
            this.photoElement.Remove();
            this.decoratedPhoto.Delete();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public override string ToString()
        {
            return this.decoratedPhoto.ToString();
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
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public Image CreateThumbnail(int maxWidth, int maxHeight)
        {
            if (this.decoratedPhoto == null)
            {
                return null;
            }

            return this.decoratedPhoto.CreateThumbnail(maxWidth, maxHeight);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-06</para>
        /// </remarks>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-06</para>
        /// </remarks>
        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (this.decoratedPhoto != null)
            {
                this.decoratedPhoto.Dispose();
                this.decoratedPhoto = null;
            }
        }
    }
}