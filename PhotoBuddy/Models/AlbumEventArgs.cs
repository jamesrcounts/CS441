//-----------------------------------------------------------------------
// <copyright file="AlbumEventArgs.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Jim Counts and Eric Wei
// Date: Nov 5 2011
// Modified date: Nov 25 2011
// Description: this class is responsible in album event arguments
//-----------------------------------------------------------------------
namespace PhotoBuddy.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Passes an <see cref="IAlbum"/> instance as event data.
    /// </summary>
    /// <remarks>
    ///   <para>Author: Jim Counts and Eric Wei</para>
    ///   <para>Created: 2011-11-03</para>
    /// </remarks>  
    public sealed class AlbumEventArgs : EventArgs
    {
        /// <summary>
        /// The event data.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>  
        private readonly IAlbum data;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumEventArgs"/> class.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>  
        public AlbumEventArgs(IAlbum album)
        {
            this.data = album;
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
                return this.data;
            }
        }
    }
}