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
    using PhotoBuddy.Controls;
    using PhotoBuddy.EventObjects;
    using PhotoBuddy.Models;
    using System.Drawing;

    /// <summary>
    /// The Opening View
    /// </summary>
    [DebuggerDisplay("{DisplayName}")]
    public partial class HomeScreenUserControl : UserControl, IScreen
    {
        /// <summary>
        /// The album manager.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-28</para>
        /// </remarks>
        private readonly AlbumRespository albums;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeScreenUserControl"/> class.
        /// </summary>
        /// <param name="albumRespository">The collection of albums.</param>
        /// <remarks>
        ///   <para>Author(s): Miguel Gonzales, Andrea Tan, Jim Counts</para>
        ///   <para>Modified: 2011-28-10</para>
        /// </remarks>
        public HomeScreenUserControl(AlbumRespository albumRespository)
        {
            if (albumRespository == null)
            {
                throw new ArgumentNullException("albumRespository", "albumRespository is null.");
            }

            this.InitializeComponent();
            this.albums = albumRespository;
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
        /// Occurs when an album is selected event.
        /// </summary>
        public event EventHandler<AlbumEventArgs> AlbumSelectedEvent;

        /// <summary>
        /// Occurs when the delete event is selected for an album.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-27</para>
        /// </remarks>
        public event EventHandler<AlbumEventArgs> DeleteAlbumEvent;

        /// <summary>
        /// Gets the a reference to the album repository.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-28</para>
        /// </remarks>
        public AlbumRespository Repository
        {
            get
            {
                return this.albums;
            }
        }

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
        /// <remarks>
        ///   <para>Author(s): Miguel Gonzales, Andrea Tan, Jim Counts</para>
        ///   <para>Modified: 2011-10-28</para>
        /// </remarks>
        public void RefreshAlbumViewList()
        {
            this.albumsFlowPanel.Controls.Clear();
            if (this.Repository.Count == 0)
            {
                return;
            }

            foreach (var album in this.Repository.Albums)
            {
                var albumControl = new AlbumThumnailUserControl()
                                {
                                    AlbumName = album.AlbumId,
                                    Count = album.Count,
                                    Image = album.CoverPhoto
                                };

                albumControl.AlbumSelectedEvent += this.OnAlbumSelectedEvent;
                albumControl.DeleteAlbumEvent += this.OnDeleteAlbumEvent;

                this.albumsFlowPanel.Controls.Add(albumControl);
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
            this.RefreshAlbumViewList();

            // Push myself onto the history.
            history.Push(this);

            // Show myself.
            this.Visible = true;

            // Important for focusing text boxes.
            this.Focus();
        }

        /// <summary>
        /// Called when album selected event fires on a album thumbnail control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PhotoBuddy.EventObjects.AlbumEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-26</para>
        /// </remarks>
        protected virtual void OnAlbumSelectedEvent(object sender, AlbumEventArgs e)
        {
            EventHandler<AlbumEventArgs> handler = this.AlbumSelectedEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        /// <summary>
        /// Called when delete album event is triggered.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PhotoBuddy.EventObjects.AlbumEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-26</para>
        /// </remarks>
        protected virtual void OnDeleteAlbumEvent(object sender, AlbumEventArgs e)
        {
            EventHandler<AlbumEventArgs> handler = this.DeleteAlbumEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
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

        /// <summary>
        /// Change the color of button controls when the mouse enters.
        /// </summary>
        /// <param name="sender">Any Button on this form.</param>
        /// <param name="e">The event args,</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void HandleButtonMouseEnter(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.ForeColor = Color.Black;
        }

        /// <summary>
        /// Change the color of button controls when the mouse leaves.
        /// </summary>
        /// <param name="sender">Any Button on this form.</param>
        /// <param name="e">The event args.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void HandleButtonMouseLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.ForeColor = Color.White;
        }
    }
}