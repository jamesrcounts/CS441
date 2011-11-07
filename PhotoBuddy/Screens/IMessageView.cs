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
    ///   <para>Author: Jim Counts and Eric Wei</para>
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
        /// <para>Author: Jim Counts and Eric Wei</para>
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
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-24</para>
        /// </remarks>
        DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);
    }
}