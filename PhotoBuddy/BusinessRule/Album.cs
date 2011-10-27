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
namespace PhotoBuddy.BusinessRule
{
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using PhotoBuddy.Common.CommonClass;

    /// <summary>
    /// An album contains an album id and a list of photos.
    /// </summary>
    /// <remarks>
    /// Author(s): Miguel Gonzales and Andrea Tan
    /// </remarks>
    [DebuggerDisplay("{AlbumID}")]
    public class Album
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Album"/> class.
        /// </summary>
        /// <param name="repository">The album repository.</param>
        /// <param name="name">The album name.</param>
        /// <remarks>
        ///   <para>Authors: Jim Counts</para>
        ///   <para>Created: 2011-10-26</para>
        /// </remarks>
        public Album(AlbumRespository repository, string name)
        {
            this.AlbumID = name;
            this.Repository = repository;
            this.PhotoList = new Photos();
        }

        /// <summary>
        /// Gets or sets the album ID.
        /// </summary>
        /// <value>
        /// The album ID.
        /// </value>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public string AlbumID { get; set; }

        /// <summary>
        /// Gets or sets the photo objects.
        /// </summary>
        /// <value>
        /// The photo objects.
        /// </value>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public Photos PhotoList { get; set; }

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
                return this.PhotoList.PhotoTable.Count;
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
                if (this.PhotoList.PhotoTable.Count <= 0)
                {
                    return PhotoBuddy.Properties.Resources.FolderIcon.ToBitmap();
                }

                var firstPhoto = this.PhotoList.PhotoTable.Values.First();
                string path = Path.Combine(Constants.PhotosFolderPath, firstPhoto.CopiedPath);
                if (!File.Exists(path))
                {
                    return PhotoBuddy.Properties.Resources.FolderIcon.ToBitmap();
                }

                return Image.FromFile(path);                    
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
            return this.AlbumID;
        }
    }
}
