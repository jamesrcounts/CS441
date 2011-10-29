//-----------------------------------------------------------------------
// <copyright file="AboutPhotoBuddyMessage.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace PhotoBuddy.Models
{
    using System.Text;
    using System.Windows.Forms;
    using PhotoBuddy.Resources;
    using PhotoBuddy.Screens;

    /// <summary>
    /// Provides data for a message about PhotoBuddy.
    /// </summary>
    /// <remarks>
    ///   <para>Author: Jim Counts</para>
    ///   <para>Created: 2011-10-25</para>
    /// </remarks>
    public sealed class AboutPhotoBuddyMessage : IMessage
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
            get
            {
                StringBuilder aboutPhotoBuddy = new StringBuilder();
                aboutPhotoBuddy.AppendLine("Photo Buddy by GOLD RUSH.");
                aboutPhotoBuddy.AppendFormat("Version: {0}", Application.ProductVersion).AppendLine();
                return aboutPhotoBuddy.ToString();
            }
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
            get { return MessageBoxIcon.None; }
        }
    }
}