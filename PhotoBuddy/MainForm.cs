//-----------------------------------------------------------------------
// <copyright file="MainForm.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales, Andrea Tan, Jim Counts and Eric Wei
// Date: Sept 28 2011
// Modified date: 2011-11-05
// Description: program start up forms, which gets inherited from multiple user controls
//-----------------------------------------------------------------------
namespace PhotoBuddy
{
    using System;
    using System.Text;
    using System.Windows.Forms;
    using PhotoBuddy.Models;
    using PhotoBuddy.Screens;

    /// <summary>
    /// The application shell.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// The album model
        /// </summary>
        private static readonly AlbumRepository Model = new AlbumRepository();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        /// <remarks>
        /// Author(s): Miguel Gonzales, Andrea Tan, Jim Counts and Eric Wei
        /// Modified: 2011-11-05
        /// </remarks>
        public MainForm()
        {
            this.InitializeComponent();
            this.Text = PhotoBuddy.Properties.Resources.AppName;
            this.ShowScreen("Home");
            this.searchControl.SearchInitiatedEvent += this.ShowSearchResults;
        }

        /// <summary>
        /// Gets the current view.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-05</para>
        /// </remarks>
        public IScreen CurrentView { get; private set; }

        #region View Updaters

        /// <summary>
        /// Shows the album view for the requested album.
        /// </summary>
        /// <param name="albumName">Name of the album.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-05</para>
        /// </remarks>
        public void ShowAlbum(string albumName)
        {
            this.ClearScreen();
            var view = this.GetView("Album") as AlbumViewUserControl;
            view.CurrentAlbum = Model.GetAlbum(albumName);
            this.CurrentView = view;
            this.LoadView();
        }

        /// <summary>
        /// Shows the rename album view.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-05</para>
        /// </remarks>
        public void ShowRenameAlbum(IAlbum album)
        {
            this.ClearScreen();
            var view = this.GetView("RenameAlbum") as CreateAlbumUserControl;
            view.Album = album;
            this.CurrentView = view;
            this.LoadView();
        }

        /// <summary>
        /// Shows the requested screen.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-26</para>
        /// </remarks>
        public void ShowScreen(string viewName)
        {
            this.ClearScreen();
            this.CurrentView = this.GetView(viewName);
            this.LoadView();
        }

        /// <summary>
        /// Shows the search results
        /// </summary>
        /// <param name="searchResults">The search results.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-05</para>
        /// </remarks>
        public void ShowSearchResults(IAlbum searchResults)
        {
            this.ClearScreen();
            var view = this.GetView("Album") as AlbumViewUserControl;
            view.CurrentAlbum = searchResults;
            view.AddPhotosEnabled = false;
            this.CurrentView = view;
            this.LoadView();
        }

        #endregion

        #region Update View Request Handlers

        /// <summary>
        /// Reacts to new album creation.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PhotoBuddy.EventArgs{System.String}"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-05</para>
        /// </remarks>
        private void ShowAlbum(object sender, EventArgs<string> e)
        {
            this.ShowAlbum(e.Data);
        }

        /// <summary>
        /// Returns to the Opening View.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales, Andrea Tan, Jim Counts and Eric Wei
        /// Modified: 2011-11-05
        /// </remarks>
        private void ShowHomeView(object sender, EventArgs e)
        {
            if (this.CurrentView is HomeScreenUserControl)
            {
                StringBuilder aboutPhotoBuddy = new StringBuilder();
                aboutPhotoBuddy.AppendLine("Photo Buddy by GOLD RUSH.");
                aboutPhotoBuddy.AppendFormat("Version: {0}", Application.ProductVersion).AppendLine();
                CultureAwareMessageBox.Show(
                    this,
                    aboutPhotoBuddy.ToString(),
                    PhotoBuddy.Properties.Resources.AppName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None);
                return;
            }

            this.ShowScreen("Home");
        }

        /// <summary>
        /// Handles the create button click.
        /// </summary>
        /// <param name="sender">Create button click on Home screen.</param>
        /// <param name="e">The event args.</param>
        /// <remarks>
        /// <para>Author(s): Miguel Gonzales, Andrea Tan, Jim Counts and Eric Wei</para>
        /// <para>Modified: 2011-11-05</para>
        /// <para>Initiates the creation of a new album.</para>
        /// </remarks>
        private void ShowCreateAlbumView(object sender, EventArgs e)
        {
            this.ShowScreen("CreateAlbum");
        }

        /// <summary>
        /// Handles the SearchInitiatedEvent event of the searchControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PhotoBuddy.EventArgs&lt;System.String[]&gt;"/> instance containing the event data.</param>
        private void ShowSearchResults(object sender, EventArgs<string[]> e)
        {
            var searchResults = Model.Search(e.Data);
            this.ShowSearchResults(searchResults);
        }

        /// <summary>
        /// Shows the rename album view.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PhotoBuddy.Models.AlbumEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-03</para>
        /// </remarks>
        private void ShowRenameAlbumView(object sender, EventArgs<IAlbum> e)
        {
            this.ShowRenameAlbum(e.Data);
        }

        #endregion

        #region View Factory Methods

        /// <summary>
        /// Gets the view.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The requested view if <paramref name="name"/> matches a known view; otherwise null.</returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-05</para>
        /// </remarks>
        private IScreen GetView(string name)
        {
            if (name == "Home")
            {
                return this.CreateHomeView();
            }

            if (name == "CreateAlbum")
            {
                return this.CreateCreateAlbumView();
            }

            if (name == "RenameAlbum")
            {
                return this.CreateRenameAlbumView();
            }

            if (name == "Album")
            {
                return this.CreateAlbumView();
            }

            return null;
        }

        /// <summary>
        /// Creates the home view.
        /// </summary>
        /// <returns>The home view.</returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-05</para>
        /// </remarks>
        private HomeScreenUserControl CreateHomeView()
        {
            HomeScreenUserControl view = new HomeScreenUserControl(Model);
            view.CreateAlbumEvent += this.ShowCreateAlbumView;
            view.AlbumSelectedEvent += this.ShowAlbum;
            view.RenameAlbumEvent += this.ShowRenameAlbumView;
            return view;
        }

        /// <summary>
        /// Creates the album view.
        /// </summary>
        /// <returns>A new album view.</returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-05</para>
        /// </remarks>
        private AlbumViewUserControl CreateAlbumView()
        {
            var view = new AlbumViewUserControl();
            view.BackEvent += this.ShowHomeView;
            return view;
        }

        /// <summary>
        /// Creates the create album view.
        /// </summary>
        /// <returns>The create album view.</returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-05</para>
        /// </remarks>
        private CreateAlbumUserControl CreateCreateAlbumView()
        {
            CreateAlbumUserControl view = new CreateAlbumUserControl(Model);
            view.CancelEvent += this.ShowHomeView;
            view.CreateAlbumEvent += this.ShowAlbum;
            return view;
        }

        /// <summary>
        /// Creates the rename album view.
        /// </summary>
        /// <returns>The rename album view.</returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-05</para>
        /// </remarks>
        private CreateAlbumUserControl CreateRenameAlbumView()
        {
            CreateAlbumUserControl view = new CreateAlbumUserControl(Model);
            view.CancelEvent += this.ShowHomeView;
            view.RenameAlbumEvent += this.ShowHomeView;
            return view;
        }

        #endregion

        /// <summary>
        /// Clears the screen.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-05</para>
        /// </remarks>
        private void ClearScreen()
        {
            while (0 < this.screenHolderPanel.Controls.Count)
            {
                this.screenHolderPanel.Controls[0].Dispose();
            }
        }

        /// <summary>
        /// Loads the current view.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-05</para>
        /// </remarks>
        private void LoadView()
        {
            this.screenHolderPanel.Controls.Add(this.CurrentView.Control);
            this.CurrentView.Control.Show();
            this.CurrentView.Control.Focus();
        }
    }
}