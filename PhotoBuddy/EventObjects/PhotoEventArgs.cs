//-----------------------------------------------------------------------
// <copyright file="PhotoEventArgs.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace PhotoBuddy.EventObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Passes data about an event related to a photo.
    /// </summary>
    /// <remarks>
    ///   <para>Author: Jim Counts and Eric Wei</para>
    ///   <para>Created: 2011-10-27</para>
    /// </remarks>
    public class PhotoEventArgs : EventArgs
    {
        /// <summary>
        /// The photo display name.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-27</para>
        /// </remarks>
        private readonly string displayName;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoEventArgs"/> class.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-27</para>
        /// </remarks>
        public PhotoEventArgs(string displayName)
        {
            this.displayName = displayName;
        }

        /// <summary>
        /// Gets the display name of the photo.
        /// </summary>
        /// <value>
        /// The display name of the photo.
        /// </value>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-27</para>
        /// </remarks>
        public string PhotoDisplayName
        {
            get
            {
                return this.displayName;
            }
        }
    }
}
