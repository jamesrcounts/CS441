//-----------------------------------------------------------------------
// <copyright file="Photo.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales, Andrea Tan, Jim Counts
// Date: Sept 28 2011
// Modified date: Oct 27 2011
// Description: this Photo class is responsible in instantiation of the photo object.
//              this class also provides the mean of accessing the photo contents
//              as well as updating its contents as well. 
//-----------------------------------------------------------------------
namespace PhotoBuddy.Models
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using PhotoBuddy.Common;

    /// <summary>
    /// Provides methods for working with Album photos.
    /// </summary>
    public sealed class Photo : IPhoto
    {
        /// <summary>
        /// Image to display any time there is a problem fetching the intended photo.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-27</para>
        /// </remarks>
        private static readonly Image DefaultImage = PhotoBuddy.Properties.Resources.MissingImageIcon.ToBitmap();

        /// <summary>
        /// The album which this photo belongs to.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-28</para>
        /// </remarks>
        private readonly IAlbum parentAlbum;

        /// <summary>
        /// Identifies the photo.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        private readonly string photoId;

        /// <summary>
        /// Name of the photo.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        private readonly string fileName;

        /// <summary>
        /// Cached instance of the photo.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-11-02</para>
        /// </remarks>
        private Image photoImage;

        /// <summary>
        /// Initializes a new instance of the <see cref="Photo"/> class.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="photoId">The photo id.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-28</para>
        /// </remarks>
        public Photo(IAlbum album, string photoId, string displayName, string fileName)
        {
            this.parentAlbum = album;
            this.photoId = photoId;
            this.DisplayName = displayName;
            this.fileName = fileName;
        }

        /// <summary>
        /// Gets the album.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        public IAlbum Album
        {
            get
            {
                return this.parentAlbum;
            }
        }

        /// <summary>
        /// Gets the photo id.
        /// </summary>
        /// <value>
        /// The photo id.
        /// </value>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public string PhotoId
        {
            get
            {
                return this.photoId;
            }
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets the file name.
        /// </summary>
        /// <value>
        /// The file name used by PhotoBuddy's internal storage mechanism.
        /// </value>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public string FileName
        {
            get
            {
                return this.fileName;
            }
        }

        /// <summary>
        /// Gets the full path to the image file in storage.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-27</para>
        /// </remarks>
        public string FullPath
        {
            get
            {
                return Path.Combine(Constants.PhotosFolderPath, this.FileName);
            }
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <returns>The photo image if there are no errors; otherwise the default image.</returns>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-27</para>
        /// </remarks>
        public Image Image
        {
            get
            {
                if (this.photoImage == null)
                {
                    if (!File.Exists(this.FullPath))
                    {
                        this.photoImage = Photo.DefaultImage;
                        return this.photoImage;
                    }

                    // Load the file
                    try
                    {
                        using (MemoryStream stream = new MemoryStream(File.ReadAllBytes(this.FullPath)))
                        {
                            this.photoImage = Image.FromStream(stream);
                        }
                    }
                    catch (OutOfMemoryException)
                    {
                        this.photoImage = Photo.DefaultImage;
                    }
                }

                return this.photoImage;
            }
        }

        /// <summary>
        /// Generates the photo key for the specified file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>A unique string which identifies the file by its contents.</returns>
        /// <remarks>
        /// Author(s): Miguel Gonzales, Andrea Tan, Jim Counts, Eric Wei
        /// </remarks>
        public static string GeneratePhotoKey(string filePath)
        {
            // Reading the bytes of the actual file contents
            byte[] source = File.ReadAllBytes(filePath);

            // Computing the SHA 256 Hash value
            using (SHA256Managed hashAlgorithm = new SHA256Managed())
            {
                byte[] hash = hashAlgorithm.ComputeHash(source);

                // Convert to HEX encoded string
                StringBuilder hashString = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    hashString.Append(hash[i].ToString("X2", CultureInfo.InvariantCulture));
                }

                return hashString.ToString();
            }
        }

        /// <summary>
        /// Deletes this photo from the album.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-11-02</para>
        ///   <para>After removing itself from the repository data, the photo triggers the repository's
        /// garbage collector.</para>
        /// </remarks>
        public void Delete()
        {
            this.parentAlbum.Repository.CollectGarbage(this.PhotoId, this.FullPath);
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
            return this.DisplayName;
        }
    }
}