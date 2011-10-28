//-----------------------------------------------------------------------
// <copyright file="MessageBoxProxy.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace PhotoBuddy.Screens
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// Provides an implementation of <see cref="IMessageView"/> which wraps <see cref="MessageBox"/>.
    /// </summary>
    /// <remarks>
    ///   <para>Author: Jim Counts</para>
    ///   <para>Created: 2011-10-24</para>
    /// </remarks>
    /// <seealso cref="http://msdn.microsoft.com/en-us/library/ms182191.aspx"/>
    public class MessageBoxProxy : IMessageView
    {
        /// <summary>
        /// Shows the specified text, caption, buttons, and icon.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <returns>
        /// A <see cref="DialogResult"/> value
        /// </returns>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-24</para>
        /// </remarks>
        public DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(text, caption, buttons, icon);
        }

        /// <summary>
        /// Shows the specified text, caption, buttons, and icon.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <returns>
        /// A <see cref="DialogResult"/> value
        /// </returns>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-24</para>
        /// </remarks>
        public DialogResult Show(string text, string caption, MessageBoxButtons buttons)
        {
            return MessageBox.Show(text, caption, buttons);
        }
    }
}