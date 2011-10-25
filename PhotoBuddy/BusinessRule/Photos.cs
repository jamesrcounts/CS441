//-----------------------------------------------------------------------
// <copyright file="Photos.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales and Andrea Tan
// Date: Sept 28 2011
// Modified date: Oct 9 2011
// Description: this class is responsible for collections of photo object. 
//             Moreover it provides a functionality of adding a photo into hash,
//             removing the photo from hash,  and last but not least is to return the
//             collections of the photos when requested.
//-----------------------------------------------------------------------
namespace PhotoBuddy.BusinessRule
{
    using System;
    using System.Collections;

    /// <summary>
    /// A collection of photos.
    /// </summary>
    public class Photos
    {
        /// <summary>
        /// Photo collection backing store.
        /// </summary>
        private readonly Hashtable photoTable = new Hashtable();

        /// <summary>
        /// Gets the photo table.
        /// </summary>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public Hashtable PhotoTable
        {
            get { return this.photoTable; }
        }

        /// <summary>
        /// Adds the specified photo.
        /// </summary>
        /// <param name="photoToAdd">The photo to add.</param>
        /// <returns>true if the photo was not present in the collection and added; otherwise false.</returns>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public bool Add(Photo photoToAdd)
        {
            if (!this.photoTable.ContainsKey(photoToAdd.PhotoId))
            {
                this.photoTable.Add(photoToAdd.PhotoId, photoToAdd);
                return true;
            }
            
            return false;
        }
    }
}