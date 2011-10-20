//-----------------------------------------------------------------------
// <copyright file="IScreen.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
//  Author(s): Miguel Gonzales and Andrea Tan
//  Date: Sept 28 2011
//  Modified date: Oct 9 2011
//  Description: this defines an interface for the screens we use in the application.
//               A screen is a user control that represents a view in the application.
//               All screens inherit from this.
//-----------------------------------------------------------------------
namespace TheNewPhotoBuddy.Screens
{
    using System.Collections.Generic;
    using System.Windows.Forms;

    /// <summary>
    /// Describes a view
    /// </summary>
    public interface IScreen
    {
        /// <summary>
        /// Gets the control managed by this view.
        /// </summary>
        UserControl Control { get; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        string DisplayName { get; set; }

        /// <summary>
        /// Shows the view.
        /// </summary>
        /// <param name="caller">The caller.</param>
        void ShowView(Stack<UserControl> caller);
    }
}