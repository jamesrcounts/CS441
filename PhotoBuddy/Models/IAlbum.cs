//-----------------------------------------------------------------------
// <copyright file="IAlbum.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace PhotoBuddy.Models
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// Describes an album.
    /// </summary>
    /// <remarks>
    ///   <para>Author: Jim Counts and Eric Wei</para>
    ///   <para>Created On: 2011-11-03</para>
    /// </remarks>
    public interface IAlbum
    {
        /// <summary>
        /// Occurs when a photo is added to the album.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created On: 2011-11-06</para>
        /// </remarks>
        event EventHandler<EventArgs<IPhoto>> PhotoAddedEvent;

        /// <summary>
        /// Gets or sets the album id.
        /// </summary>
        /// <value>
        /// The album id.
        /// </value>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created On: 2011-11-03</para>
        /// </remarks>
        string AlbumId { get; set; }

        /// <summary>
        /// Gets the photos.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created On: 2011-11-03</para>
        /// </remarks>
        IEnumerable<IPhoto> Photos { get; }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created On: 2011-11-03</para>
        /// </remarks>
        AlbumRepository Repository { get; }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created On: 2011-11-03</para>
        /// </remarks>
        int Count { get; }

        /// <summary>
        /// Adds the photo.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <returns>The attached photo.</returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created On: 2011-11-03</para>
        /// </remarks>
        IPhoto AddPhoto(IPhoto photo);

        /// <summary>
        /// Adds the photo.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>The attached photo.</returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created On: 2011-11-06</para>
        /// </remarks>
        IPhoto AddPhoto(string filePath);

        /// <summary>
        /// Adds the photo.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The attached photo.</returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created On: 2011-11-03</para>
        /// </remarks>
        IPhoto AddPhoto(string photoId, string displayName, string fileName);

        /// <summary>
        /// Adds a new photo.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="filePath">The file path.</param>
        /// <returns>The attached photo.</returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created On: 2011-11-03</para>
        /// </remarks>
        IPhoto AddPhoto(string displayName, string filePath);

        /// <summary>
        /// Gets the photo.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns>The photo with the specified id.</returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created On: 2011-11-03</para>
        /// </remarks>
        IPhoto GetPhoto(string photoId);

        /// <summary>
        /// Gets the number of photos that have display names matching the prefix of the specified display name.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="prefixLength">Length of the prefix.</param>
        /// <returns>The number of matches</returns>
        int GetPrefixMatchCount(string displayName, int prefixLength);

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created On: 2011-11-03</para>
        /// </remarks>
        void Delete();

        /// <summary>
        /// Removes the photo.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created On: 2011-11-03</para>
        /// </remarks>
        void RemovePhoto(string photoId);

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created On: 2011-11-03</para>
        /// </remarks>
        string ToString();

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
        Image CreateThumbnail(int maxWidth, int maxHeight);

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
        bool ContainsPhoto(string photoId);

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
        bool ContainsName(string displayName);
    }
}
