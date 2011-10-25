//-----------------------------------------------------------------------
// <copyright file="IMessageService.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace PhotoBuddy.Screens
{
    using System.Windows.Forms;

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
        /// Gets the about photo buddy message.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-25</para>
        /// </remarks>
        IMessage AboutPhotoBuddy { get; }

        /// <summary>
        /// Gets the invalid album name message.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-25</para>
        /// </remarks>
        IMessage InvalidAlbumName { get; }

        /// <summary>
        /// Gets the name too long message.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-25</para>
        /// </remarks>
        IMessage NameTooLong { get; }

        /// <summary>
        /// Gets the not a picture file message.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-25</para>
        /// </remarks>
        IMessage NotPictureFile { get; }

        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="message">The type.</param>
        /// <returns>A <see cref="DialogResult"/>.</returns>
        DialogResult ShowMessage(IMessage message);
    }
}