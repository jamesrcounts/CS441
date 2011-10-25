//-----------------------------------------------------------------------
// <copyright file="NameTooLongMessage.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace PhotoBuddy
{
    using System.Windows.Forms;
    using PhotoBuddy.Common.CommonClass;
    using PhotoBuddy.Screens;

    /// <summary>
    /// Provides a message when the name entered is too long.
    /// </summary>
    /// <remarks>
    ///   <para>Author: Jim Counts</para>
    ///   <para>Created: 2011-10-25</para>
    /// </remarks>
    public sealed class NameTooLongMessage : IMessage
    {
        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-25</para>
        /// </remarks>
        public string Text
        {
            get { return "Name is too long.  Please enter a name less than " + Constants.MaxNameLength; }
        }

        /// <summary>
        /// Gets the caption.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-25</para>
        /// </remarks>
        public string Caption
        {
            get { return "Name Length Issue"; }
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
    }
}