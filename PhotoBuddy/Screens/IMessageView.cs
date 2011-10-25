// -----------------------------------------------------------------------
// <copyright file="IMessageView.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace PhotoBuddy.Screens
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// Describes methods to manipulate modal message boxes.
    /// </summary>
    /// <remarks>
    ///   <para>Author: Jim Counts</para>
    ///   <para>Created: 2011-10-24</para>
    /// </remarks>
    public interface IMessageView
    {
        /// <summary>
        /// Shows the specified text, caption, buttons, and icon.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <returns>A <see cref="DialogResult"/> value </returns>
        /// <remarks>
        /// <para>Author: Jim Counts</para>
        /// <para>Created: 2011-10-24</para>
        /// </remarks>
        DialogResult Show(string text, string caption, MessageBoxButtons buttons);
     
        /// <summary>
        /// Shows the specified text, caption, buttons, and icon.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <returns>A <see cref="DialogResult"/> value </returns>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-24</para>
        /// </remarks>
        DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);
    }

    /// <summary>
    /// Describes a service which displays canned messages.
    /// </summary>
    /// <remarks>
    ///   <para>Author: Jim Counts</para>
    ///   <para>Created: 2011-10-24</para>
    /// </remarks>
    public interface IMessageService
    {
        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A <see cref="DialogResult"/>.</returns>
        DialogResult ShowMessage(IMessage type);
    }

    /// <summary>
    /// Describes a message.
    /// </summary>
    /// <remarks>
    ///   <para>Author: Jim Counts</para>
    ///   <para>Created: 2011-10-24</para>
    /// </remarks>
    public interface IMessage
    {
        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-24</para>
        /// </remarks>
        string Text { get; }
        
        /// <summary>
        /// Gets the caption.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-24</para>
        /// </remarks>
        string Caption { get; }
        
        /// <summary>
        /// Gets the buttons.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-24</para>
        /// </remarks>
        MessageBoxButtons Buttons { get; }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-24</para>
        /// </remarks>
        MessageBoxIcon Icon { get; }
    }

    /// <summary>
    /// Provides data for an invalid album name message.
    /// </summary>
    /// <remarks>
    ///   <para>Author: Jim Counts</para>
    ///   <para>Created: 2011-10-24</para>
    /// </remarks>
    public sealed class InvalidAlbumNameMessage : IMessage
    {
        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-24</para>
        /// </remarks>
        public string Text
        {
            get { return "Invalid album name! Please enter a new album name."; }
        }

        /// <summary>
        /// Gets the caption.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-24</para>
        /// </remarks>
        public string Caption
        {
            get { return "Album Name Invalid"; }
        }

        /// <summary>
        /// Gets the buttons.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
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
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-24</para>
        /// </remarks>
        public MessageBoxIcon Icon
        {
            get { return MessageBoxIcon.Warning; }
        }
    }
}