//-----------------------------------------------------------------------
// <copyright file="HomeScreenUserControl.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales and Andrea Tan
// Date: Sept 28 2011
// Modified date: Oct 9 2011
// Description: this class is responsible for the use control in home default mainForm
//              view which is called during the state changes and the 
//              startup of the program
//-----------------------------------------------------------------------
namespace PhotoBuddy.Screens
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows.Forms;
    using PhotoBuddy.BussinessRule;
    using PhotoBuddy.Controls;
    using PhotoBuddy.EventObjects;

    /// <summary>
    /// The Opening View
    /// </summary>
    [DebuggerDisplay("{DisplayName}")]
    public partial class HomeScreenUserControl : UserControl, IScreen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeScreenUserControl"/> class.
        /// </summary>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public HomeScreenUserControl()
        {
            this.InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.DisplayName = "Albums";
        }

        /// <summary>
        /// Defines a delegate to handle "Create Album" events.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>Create Album events are typically fired by the "Create Album" button, and indicate that the user wants to 
        /// create an album.</remarks>
        public delegate void CreateButtonClicked(object sender, EventArgs e);

        /// <summary>
        /// Defines a delegate to handle the "Album Selected" event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="TheNewPhotoBuddy.EventObjects.AlbumEventArgs"/> instance containing the event data.</param>
        /// <remarks>Album selected events are typically fired when the user clicks on an album, and indicate that the user wants to 
        /// view an album.</remarks>
        public delegate void AlbumSelectedEventHandler(object sender, AlbumEventArgs e);

        /// <summary>
        /// Occurs when the user clicks the create button.
        /// </summary>
        public event CreateButtonClicked CreateButtonEvent;

        /// <summary>
        /// Occurs when an album is clicked.
        /// </summary>
        public event AlbumSelectedEventHandler AlbumSelectedEvent;

        /// <summary>
        /// Gets or sets the albums.
        /// </summary>
        /// <value>
        /// The albums.
        /// </value>
        /// <remarks>Author: Jim Counts</remarks>
        public Albums Albums { get; set; }

        /// <summary>
        /// Gets the control managed by this view.
        /// </summary>
        /// <remarks>Author: Jim Counts</remarks>
        public UserControl Control
        {
            get
            {
                return this;
            }
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Refreshes the album view list.
        /// </summary>
        /// <param name="albums">The list of all the albums.</param>
        /// <remarks>
        /// <para>Author(s): Miguel Gonzales and Andrea Tan</para>
        /// </remarks>
        public void RefreshAlbumViewList(Albums albums)
        {
            this.albumsFlowPanel.Controls.Clear();
            if (albums.AlbumList.Count == 0)
            {
                return;
            }

            foreach (Album album in albums.AlbumList.Values)
            {
                ClickLabel label = new ClickLabel() { Text = album.AlbumID.Replace("&", "&&") };

                label.Click += this.HandleAlbumClick;
                this.albumsFlowPanel.Controls.Add(label);
            }
        }

        /// <summary>
        /// Shows the view.
        /// </summary>
        /// <param name="history">The caller's history of previous views.</param>
        /// <remarks>Author: Jim Counts</remarks>
        public void ShowView(Stack<UserControl> history)
        {
            // Refresh the list of Albums.
            this.RefreshAlbumViewList(this.Albums);

            // Push myself onto the history.
            history.Push(this);

            // Show myself.
            this.Visible = true;

            // Important for focusing text boxes.
            this.Focus();
        }

        /// <summary>
        /// Handles the Click event of the album control.
        /// </summary>
        /// <param name="sender">The label displaying the name of the album.</param>
        /// <param name="e">The event args.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void HandleAlbumClick(object sender, EventArgs e)
        {
            ClickLabel albumLabel = sender as ClickLabel;
            AlbumEventArgs args = new AlbumEventArgs(albumLabel.Text);
            this.AlbumSelectedEvent(this, args);
        }

        /// <summary>
        /// Exits the program.
        /// </summary>
        /// <param name="sender">The exit button.</param>
        /// <param name="e">The event args.</param>
        /// <remarks>
        /// <para>Author(s): Miguel Gonzales and Andrea Tan</para>
        /// </remarks>
        private void HandleExitButtonClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Handles the Click event of the CreateButton control.
        /// </summary>
        /// <param name="sender">The create button.</param>
        /// <param name="e">The event args.</param>
        /// <remarks>
        /// <para>Author(s): Miguel Gonzales and Andrea Tan</para>
        /// </remarks>
        private void HandleCreateButtonClick(object sender, EventArgs e)
        {
            // raise the create event
            EventArgs args = new EventArgs();
            this.CreateButtonEvent(this, args);
        }
    }
}