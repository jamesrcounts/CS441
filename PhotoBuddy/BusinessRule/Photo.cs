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
namespace PhotoBuddy.BusinessRule
{
    using System;
    using System.Drawing;
    using System.IO;
    using PhotoBuddy.Common.CommonClass;

    /// <summary>
    /// Provides methods for working with Album photos.
    /// </summary>
    public class Photo
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
        /// Initializes a new instance of the <see cref="Photo"/> class.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-27</para>
        /// </remarks>
        public Photo(string photoId, string displayName, string fileName)
        {
            this.PhotoId = photoId;
            this.DisplayName = displayName;
            this.FileName = fileName;
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
        public string PhotoId { get; private set; }

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
        public string FileName { get; private set; }

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
        public Image GetImage()
        {
            if (!File.Exists(this.FullPath))
            {
                return Photo.DefaultImage;
            }

            // Load the file
            try
            {
                return Image.FromFile(this.FullPath);
            }
            catch (OutOfMemoryException)
            {
                return Photo.DefaultImage;
            }
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