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
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
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
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-28</para>
        /// </remarks>
        private readonly AlbumRepository albums;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeScreenUserControl"/> class.
        /// </summary>
        /// <param name="albumRepository">The collection of albums.</param>
        /// <remarks>
        ///   <para>Author(s): Miguel Gonzales, Andrea Tan, Jim Counts and Eric Wei</para>
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
        public event EventHandler CreateAlbumEvent;

        /// <summary>
        /// Occurs when an album is selected event.
        /// </summary>
        public event EventHandler<EventArgs<string>> AlbumSelectedEvent;

        /// <summary>
        /// Occurs when a search completes.
        /// </summary>
        public event EventHandler<AlbumEventArgs> SearchCompleteEvent;

        /// <summary>
        /// Occurs when a request is made to rename an album.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-04</para>
        /// </remarks>
        public event EventHandler<EventArgs<IAlbum>> RenameAlbumEvent;

        /// <summary>
        /// Gets the a reference to the album repository.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
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
        /// <remarks>Author: Jim Counts and Eric Wei</remarks>
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
        /// Author: Jim Counts and Eric Wei
        /// Created: 2011-11-04
        /// </remarks>
        public IEnumerable<AlbumIconUserControl> Thumbnails
        {
            get
            {
                return this.albumsFlowPanel.Controls.OfType<AlbumIconUserControl>();
            }
        }

        /// <summary>
        /// Called when the user wants to create an album.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-05</para>
        /// </remarks>
        public virtual void OnCreateButtonEvent(object sender, EventArgs e)
        {
            EventHandler handler = this.CreateAlbumEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        /// <summary>
        /// Refreshes the album view list.
        /// </summary>
        /// <remarks>
        ///   <para>Author(s): Miguel Gonzales, Andrea Tan, Jim Counts and Eric Wei</para>
        ///   <para>Modified: 2011-10-28</para>
        /// </remarks>
        public void RefreshAlbumViewList()
        {
            this.albumsFlowPanel.Controls.Clear();
            if (this.Repository.Count == 0)
            {
                // No albums to show.
                return;
            }

            var albumBuffer = new BlockingCollection<IAlbum>();
            var controlBuffer = new BlockingCollection<AlbumIconUserControl>(32);
            var thumbnailBuffer = new BlockingCollection<AlbumIconUserControl>(32);
            this.StartPipelineStages(albumBuffer, controlBuffer, thumbnailBuffer);

            try
            {
                foreach (var album in this.Repository.Albums)
                {
                    albumBuffer.Add(album);
                }
            }
            finally
            {
                albumBuffer.CompleteAdding();
            }
        }

        /// <summary>
        /// Shows the view.
        /// </summary>
        /// <param name="history">The caller's history of previous views.</param>
        /// <remarks>Author: Jim Counts and Eric Wei</remarks>
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
        ///   <para>Author: Jim Counts and Eric Wei</para>
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
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-26</para>
        /// </remarks>
        protected virtual void OnAlbumSelectedEvent(object sender, EventArgs<string> e)
        {
            EventHandler<EventArgs<string>> handler = this.AlbumSelectedEvent;
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
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-26</para>
        /// </remarks>
        protected virtual void OnDeleteAlbumEvent(object sender, EventArgs<string> e)
        {
            var thumb = sender as Control;
            if (thumb == null)
            {
                return;
            }

            this.albumsFlowPanel.Controls.Remove(thumb);
            thumb.Dispose();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.UserControl.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-05</para>
        /// </remarks>
        protected override void OnLoad(EventArgs e)
        {
            this.RefreshAlbumViewList();
            base.OnLoad(e);
        }

        /// <summary>
        /// Called when an album rename is requested.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PhotoBuddy.Models.AlbumEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-04</para>
        /// </remarks>
        protected virtual void OnRenameAlbumEvent(object sender, EventArgs<IAlbum> e)
        {
            EventHandler<EventArgs<IAlbum>> handler = this.RenameAlbumEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        /// <summary>
        /// Generates the thumbnail for the AlbumIconControls.
        /// </summary>
        /// <param name="albums">The albums.</param>
        /// <param name="thumbnailAlbums">The thumbnail albums.</param>
        private static void GenerateThumbnails(BlockingCollection<AlbumIconUserControl> albums, BlockingCollection<AlbumIconUserControl> thumbnailAlbums)
        {
            try
            {
                foreach (var albumControl in albums.GetConsumingEnumerable())
                {
                    albumControl.Image = albumControl.Album.CreateThumbnail(
                        albumControl.ThumbnailWidth,
                        albumControl.ThumbnailHeight);
                    thumbnailAlbums.Add(albumControl);
                }
            }
            finally
            {
                thumbnailAlbums.CompleteAdding();
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
            this.OnCreateButtonEvent(this, e);
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

        /// <summary>
        /// Creates the album icon user control.
        /// </summary>
        /// <returns>A new <see cref="AlbumIconUserControl"/>, created on the UI thread.</returns>
        private AlbumIconUserControl CreateAlbumIconUserControl()
        {
            if (this.InvokeRequired)
            {
                Func<AlbumIconUserControl> invoker = this.CreateAlbumIconUserControl;
                return (AlbumIconUserControl)this.Invoke(invoker);
            }

            return new AlbumIconUserControl();
        }

        /// <summary>
        /// Generates the thumbnail controls.
        /// </summary>
        /// <param name="albums">The albums.</param>
        /// <param name="thumbnails">The thumbnails.</param>
        private void GenerateThumbnailControls(BlockingCollection<IAlbum> albums, BlockingCollection<AlbumIconUserControl> thumbnails)
        {
            try
            {
                foreach (var album in albums.GetConsumingEnumerable())
                {
                    AlbumIconUserControl albumControl = this.CreateAlbumIconUserControl();
                    albumControl.Album = album;
                    albumControl.AlbumName = album.AlbumId;
                    albumControl.Count = album.Count;
                    albumControl.AlbumSelectedEvent += this.OnAlbumSelectedEvent;
                    albumControl.DeleteAlbumEvent += this.OnDeleteAlbumEvent;
                    albumControl.RenameAlbumEvent += this.OnRenameAlbumEvent;
                    thumbnails.Add(albumControl);
                }
            }
            finally
            {
                thumbnails.CompleteAdding();
            }
        }

        /// <summary>
        /// Adds the thumbnail controls.
        /// </summary>
        /// <param name="albumControls">The album controls.</param>
        private void AddThumbnailControls(BlockingCollection<AlbumIconUserControl> albumControls)
        {
            foreach (var albumControl in albumControls.GetConsumingEnumerable())
            {
                this.AddAlbumControl(albumControl);
            }
        }

        /// <summary>
        /// Adds the album control to the albumsFlowPanel, marshalling the request to the UI thread if necessary.
        /// </summary>
        /// <param name="albumControl">The album control.</param>
        private void AddAlbumControl(AlbumIconUserControl albumControl)
        {
            if (this.InvokeRequired)
            {
                Action<AlbumIconUserControl> invoker = this.AddAlbumControl;
                this.BeginInvoke(invoker, albumControl);
                return;
            }

            this.albumsFlowPanel.Controls.Add(albumControl);
        }

        /// <summary>
        /// Starts the pipeline stages involved with adding album thumbnail icons to the hove view.
        /// </summary>
        /// <param name="albumBuffer">The album buffer.</param>
        /// <param name="controlBuffer">The control buffer.</param>
        /// <param name="thumbnailBuffer">The thumbnail buffer.</param>
        private void StartPipelineStages(BlockingCollection<IAlbum> albumBuffer, BlockingCollection<AlbumIconUserControl> controlBuffer, BlockingCollection<AlbumIconUserControl> thumbnailBuffer)
        {
            Task.Factory.StartNew(
                            () => this.GenerateThumbnailControls(albumBuffer, controlBuffer),
                            TaskCreationOptions.LongRunning);

            Task.Factory.StartNew(
                () => HomeScreenUserControl.GenerateThumbnails(controlBuffer, thumbnailBuffer),
                TaskCreationOptions.LongRunning);

            Task.Factory.StartNew(
                () => this.AddThumbnailControls(thumbnailBuffer),
                TaskCreationOptions.LongRunning);
        }
    }
}