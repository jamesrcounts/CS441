//-----------------------------------------------------------------------
// <copyright file="InvalidAlbumNameMessage.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Jim Counts and Eric Wei
// Date: Nov 5 2011
// Modified date: Nov 25 2011
// Description: this class is responsible for invalid album name message.
//-----------------------------------------------------------------------
namespace PhotoBuddy.Models
{
    using System.Windows.Forms;

    /// <summary>
    /// Provides data for an invalid album name message.
    /// </summary>
    /// <remarks>
    ///   <para>Author: Jim Counts and Eric Wei</para>
    ///   <para>Created: 2011-10-24</para>
    /// </remarks>
    public sealed class InvalidAlbumNameMessage : IMessage
    {
        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-24</para>
        /// </remarks>
        public string Text
        {
            get { return "Album name is already used. Please enter a different album name."; }
        }

        /// <summary>
        /// Gets the caption.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-24</para>
        /// </remarks>
        public string Caption
        {
            get { return "Photo Buddy - " + Application.ProductVersion; }
        }

        /// <summary>
        /// Gets the buttons.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-24</para>
        /// </remarks>
        public MessageBoxButtons Buttons
        {
            get { return MessageBoxButtons.OK; }
        }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-24</para>
        /// </remarks>
        public MessageBoxIcon Icon
        {
            get { return MessageBoxIcon.Warning; }
        }
    }
}