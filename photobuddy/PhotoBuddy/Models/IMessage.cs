//-----------------------------------------------------------------------
// <copyright file="IMessage.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Jim Counts and Eric Wei
// Date: Nov 5 2011
// Modified date: Nov 25 2011
// Description: this interface is responsible in messages used in message box.
//-----------------------------------------------------------------------
namespace PhotoBuddy.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// Describes a message.
    /// </summary>
    /// <remarks>
    ///   <para>Author: Jim Counts and Eric Wei</para>
    ///   <para>Created: 2011-10-24</para>
    /// </remarks>
    public interface IMessage
    {
        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-24</para>
        /// </remarks>
        string Text { get; }

        /// <summary>
        /// Gets the caption.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-24</para>
        /// </remarks>
        string Caption { get; }

        /// <summary>
        /// Gets the buttons.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-24</para>
        /// </remarks>
        MessageBoxButtons Buttons { get; }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-24</para>
        /// </remarks>
        MessageBoxIcon Icon { get; }
    }
}