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
    using System.Linq;
    using System.Windows.Forms;
    using PhotoBuddy.Controls;
    using PhotoBuddy.Models;

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
        private readonly AlbumRepository albums;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeScreenUserControl"/> class.
        /// </summary>
        /// <param name="albumRepository">The collection of albums.</param>
        /// <remarks>
        ///   <para>Author(s): Miguel Gonzales, Andrea Tan, Jim Counts</para>
        ///   <para>Modified: 2011-28-10</para>
        /// </remarks>
        public HomeScreenUserControl(AlbumRepository albumRepository)
        {
            if (albumRepository == null)
            {
                throw new ArgumentNullException("albumRepository", "albumRespository is null.");
            }

            this.InitializeComponent();
            this.albums = albumRepository;
            this.Dock = DockStyle.Fill;
            this.DisplayName = "Albums";
        }

        /// <summary>
        /// Occurs when the user clicks the create button.
        /// </summary>
        public event EventHandler CreateButtonEvent;

        /// <summary>
        /// Occurs when an album is selected event.
        /// </summary>
        public event EventHandler<AlbumNameEventArgs> AlbumSelectedEvent;

        /// <summary>
        /// Occurs when the delete event is selected for an album.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-27</para>
        /// </remarks>
        public event EventHandler<AlbumNameEventArgs> DeleteAlbumEvent;

        /// <summary>
        /// Occurs when a search completes.
        /// </summary>
        public event EventHandler<AlbumEventArgs> SearchCompleteEvent;

        /// <summary>
        /// Occurs when a request is made to rename an album.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-11-04</para>
        /// </remarks>
        public event EventHandler<AlbumEventArgs> RenameAlbumEvent;

        /// <summary>
        /// Gets the a reference to the album repository.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-28</para>
        /// </remarks>
        public AlbumRepository Repository
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
        /// Gets the album thumbnails.
        /// </summary>
        /// <remarks>
        /// Author: Jim Counts
        /// Created: 2011-11-04
        /// </remarks>
        public IEnumerable<AlbumThumbnailUserControl> Thumbnails
        {
            get
            {
                return this.albumsFlowPanel.Controls.OfType<AlbumThumbnailUserControl>();
            }
        }

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
                var albumControl = new AlbumThumbnailUserControl()
                                {
                                    Album = album,
                                    AlbumName = album.AlbumId,
                                    Count = album.Count,
                                    Image = album.CoverPhoto
                                };

                albumControl.AlbumSelectedEvent += this.OnAlbumSelectedEvent;
                albumControl.DeleteAlbumEvent += this.OnDeleteAlbumEvent;
                albumControl.RenameAlbumEvent += this.OnRenameAlbumEvent;

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
        /// Called when [search complete event].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PhotoBuddy.Models.AlbumEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>  
        public virtual void OnSearchCompleteEvent(object sender, AlbumEventArgs e)
        {
            EventHandler<AlbumEventArgs> handler = this.SearchCompleteEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
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
        protected virtual void OnAlbumSelectedEvent(object sender, AlbumNameEventArgs e)
        {
            EventHandler<AlbumNameEventArgs> handler = this.AlbumSelectedEvent;
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
        protected virtual void OnDeleteAlbumEvent(object sender, AlbumNameEventArgs e)
        {
            EventHandler<AlbumNameEventArgs> handler = this.DeleteAlbumEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        /// <summary>
        /// Called when an album rename is requested.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PhotoBuddy.Models.AlbumEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-11-04</para>
        /// </remarks>
        protected virtual void OnRenameAlbumEvent(object sender, AlbumEventArgs e)
        {
            EventHandler<AlbumEventArgs> handler = this.RenameAlbumEvent;
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
        /// Handles the search button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        private void HandleSearchButtonClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.searchTextBox.Text))
            {
                return;
            }

            var terms = this.searchTextBox.Text.Split(' ');
            var searchReults = this.Repository.Search(terms);
            this.OnSearchCompleteEvent(this, new AlbumEventArgs(searchReults));
        }
    }
}