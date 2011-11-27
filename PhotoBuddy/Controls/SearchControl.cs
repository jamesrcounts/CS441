//-----------------------------------------------------------------------
// <copyright file="SearchControl.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
//
// Author(s): Jim Counts and Eric wei
// Date: Nov 5, 2011
// Modified date: Nov 25, 2011
// High level Description: this class is for searching photos 
//-----------------------------------------------------------------------
namespace PhotoBuddy.Controls
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Provides a text box and a search button.
    /// </summary>
    public partial class SearchControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchControl"/> class.
        /// </summary>
        public SearchControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Occurs when the user initiates a search.
        /// </summary>
        public event EventHandler<EventArgs<string[]>> SearchInitiatedEvent;

        /// <summary>
        /// Raises the search initiated event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PhotoBuddy.EventArgs&lt;System.String[]&gt;"/> instance containing the event data.</param>
        protected virtual void OnSearchInitiatedEvent(object sender, EventArgs<string[]> e)
        {
            EventHandler<EventArgs<string[]>> handler = this.SearchInitiatedEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        /// <summary>
        /// Check for the enter key press and execute the search button if it was pressed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales, Andrea Tan, Jim Counts
        /// Created:  2011-11-07
        /// </remarks>
        private void HandleSearchTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            // See if the user pressed the enter key and if so execute the continue button.
            if (e.KeyCode == Keys.Enter)
            {
                this.HandleSearchButtonClick(sender, e);
            }
        }

        /// <summary>
        /// Handles the search button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        private void HandleSearchButtonClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.searchTextBox.Text))
            {
                return;
            }

            var terms = this.searchTextBox.Text.ToUpperInvariant().Trim().Split(' ');
            this.OnSearchInitiatedEvent(this, new EventArgs<string[]>(terms));
        }
    }
}
