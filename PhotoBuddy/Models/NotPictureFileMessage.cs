//-----------------------------------------------------------------------
// <copyright file="NotPictureFileMessage.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace PhotoBuddy.Models
{
    using System.Windows.Forms;
    using PhotoBuddy.Resources;
    using PhotoBuddy.Screens;

    /// <summary>
    /// Provides a message to use when the user selects a file that is not an image.
    /// </summary>
    /// <remarks>
    ///   <para>Author: Jim Counts</para>
    ///   <para>Created: 2011-10-25</para>
    /// </remarks>
    public sealed class NotPictureFileMessage : IMessage
    {
        /// <summary>
        /// Gets the caption.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-25</para>
        /// </remarks>
        public string Caption
        {
            get { return Strings.AppName; }
        }

        /// <summary>
        /// Gets the buttons.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-25</para>
        /// </remarks>
        public MessageBoxButtons Buttons
        {
            get { return MessageBoxButtons.OK; }
        }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-25</para>
        /// </remarks>
        public MessageBoxIcon Icon
        {
            get { return MessageBoxIcon.Warning; }
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-25</para>
        /// </remarks>
        public string Text
        {
            get { return Strings.ErrorNotPictureFile; }
        }
    }
}