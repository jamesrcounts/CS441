//-----------------------------------------------------------------------
// <copyright file="MessageService.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace PhotoBuddy.Screens
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using PhotoBuddy.Models;

    /// <summary>
    /// Provides a presenter for message boxes.
    /// </summary>
    /// <remarks>
    ///   <para>Author: Jim Counts</para>
    ///   <para>Created: 2011-10-25</para>
    /// </remarks>
    public class MessageService : IMessageService
    {
        /// <summary>
        /// Gets the about photo buddy message.
        /// </summary>
        public IMessage AboutPhotoBuddy
        {
            get { return new AboutPhotoBuddyMessage(); }
        }

        /// <summary>
        /// Gets the invalid album name message.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-25</para>
        /// </remarks>
        public IMessage InvalidAlbumName
        {
            get
            {
                return new InvalidAlbumNameMessage();
            }
        }

        /// <summary>
        /// Gets the name too long message.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-25</para>
        /// </remarks>
        public IMessage NameTooLong
        {
            get
            {
                return new NameTooLongMessage();
            }
        }

        /// <summary>
        /// Gets the not a picture file message.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-25</para>
        /// </remarks>
        public IMessage NotPictureFile
        {
            get { return new NotPictureFileMessage(); }
        }

        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="message">The type.</param>
        /// <returns>
        /// A <see cref="DialogResult"/>.
        /// </returns>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-25</para>
        /// </remarks>
        public DialogResult ShowMessage(IMessage message)
        {
            return new MessageBoxProxy().Show(message.Text, message.Caption, message.Buttons, message.Icon);
        }
    }
}