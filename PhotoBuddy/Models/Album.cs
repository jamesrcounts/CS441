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
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    /// An album contains an album id and a list of photos.
    /// </summary>
    /// <remarks>
    ///   <para>Author(s): Miguel Gonzales, Andrea Tan, Jim Counts</para>
    ///   <para>Modified: 2011-10-28</para>
    /// </remarks>
    [DebuggerDisplay("{AlbumId}")]
    public class Album
    {
        /// <summary>
        /// A reference to the XML element this album is derived from.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-28</para>
        /// </remarks>
        private readonly XElement albumElement;

        /// <summary>
        /// Backing store for indexed photo list.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-28</para>
        /// </remarks>
        private readonly IDictionary<string, Photo> photos = new Dictionary<string, Photo>();

        /////// <summary>
        /////// Initializes a new instance of the <see cref="Album"/> class.
        /////// </summary>
        /////// <param name="repository">The album repository.</param>
        /////// <param name="name">The album name.</param>
        /////// <remarks>
        ///////   <para>Authors: Jim Counts</para>
        ///////   <para>Created: 2011-10-26</para>
        /////// </remarks>
        ////protected Album(AlbumRespository repository, string name)
        ////{
        ////    this.albumElement = new XElement("album");
        ////    PopulateAlbumElement(this.albumElement, name);
        ////    ////this.AlbumId = name;
        ////    this.Repository = repository;
        ////}

        /// <summary>
        /// Initializes a new instance of the <see cref="Album"/> class.
        /// </summary>
        /// <param name="repository">The album repository.</param>
        /// <param name="albumElement">The album element.</param>
        /// <remarks>
        ///   <para>Authors: Jim Counts</para>
        ///   <para>Created: 2011-10-28</para>
        /// </remarks>
        public Album(AlbumRespository repository, XElement albumElement)
        {
            this.Repository = repository;
            this.albumElement = albumElement;
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
        public string AlbumId
        {
            get
            {
                return this.albumElement.Attribute("id_tag").Value;
            }

            set
            {
                this.albumElement.Attribute("id_tag").Value = value;
            }
        }

        /// <summary>
        /// Gets the photos in the album.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-27</para>
        ///   <para>Modified: 2011-10-28</para>
        /// </remarks>
        public IEnumerable<Photo> Photos
        {
            get
            {
                if (this.photos == null)
                {
                    return Enumerable.Empty<Photo>();
                }

                if (this.photos.Count == 0)
                {
                    var photoElements = this.albumElement.Descendants("photo");
                    foreach (var photoElement in photoElements)
                    {
                        var photo = new Photo(this, photoElement);
                        this.photos.Add(photo.PhotoId, photo);
                    }
                }

                return this.photos.Values;
            }
        }

        /// <summary>
        /// Gets a reference to the repository this album belongs to.
        /// </summary>
        /// <remarks>
        /// <para>Authors(s): Jim Counts and Eric Wei</para>
        /// <para>Created: 2011-10-25</para></remarks>
        public AlbumRespository Repository { get; private set; }

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
                return this.Photos.Count();
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
                if (this.Photos.Count() <= 0)
                {
                    return PhotoBuddy.Properties.Resources.FolderIcon.ToBitmap();
                }

                return this.Photos.First().GetImage();
            }
        }

        /// <summary>
        /// Creates a new album element.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>A new XML element which describes an empty album.</returns>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-28</para>
        /// </remarks>
        public static XElement CreateAlbumElement(string name)
        {
            var albumElement = new XElement("album");
            albumElement.Add(new XElement("photos"));
            albumElement.Add(new XAttribute("id_tag", name));
            return albumElement;
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
            if (!this.photos.ContainsKey(photoId))
            {
                // Update XML
                XElement photoElement = Photo.CreatePhotoElement(photoId, displayName, fileName);
                this.albumElement.Add(photoElement);

                // Update Index
                Photo photo = new Photo(this, photoElement);
                this.photos.Add(photo.PhotoId, photo);
            }
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
        public Photo GetPhoto(string photoId)
        {
            Photo photo = null;
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
            this.albumElement.Remove();
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
            this.photos.Remove(photoId);
            XElement photoElment = this.albumElement.Descendants("photo").Where(p => p.Attribute("id_tag").Value == photoId).Single();
            photoElment.Remove();
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
