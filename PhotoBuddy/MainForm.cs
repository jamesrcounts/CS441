//-----------------------------------------------------------------------
// <copyright file="MainForm.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales and Andrea Tan
// Date: Sept 28 2011
// Modified date: Oct 19 2011
// Description: program start up forms, which gets inherited from multiple user controls
//-----------------------------------------------------------------------
namespace PhotoBuddy
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using PhotoBuddy.EventObjects;
    using PhotoBuddy.Models;
    using PhotoBuddy.Resources;
    using PhotoBuddy.Screens;

    /// <summary>
    /// The application shell.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Stores the history of previous views.
        /// </summary>
        /// <remarks>
        /// <para>Author: Jim Counts</para>
        /// <para>Created: 2011-10-24</para>
        /// </remarks>
        private readonly Stack<UserControl> previousViews = new Stack<UserControl>();

        /// <summary>
        /// The album model
        /// </summary>
        private static readonly AlbumRepository Model = new AlbumRepository();

        /// <summary>
        /// The Opening View shows a list of albums; allows user to create new albums.
        /// </summary>
        private readonly HomeScreenUserControl homeScreen;

        /// <summary>
        /// The Album View shows list of photos in album; allows new photos to be added.
        /// </summary>
        private readonly AlbumViewUserControl albumScreen = new AlbumViewUserControl();

        /// <summary>
        /// The Create Album View allows user to name new albums.
        /// </summary>
        private readonly CreateAlbumUserControl createAlbumScreen = new CreateAlbumUserControl();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        /// <remarks>
        /// Author(s): Miguel Gonzales, Andrea Tan, Jim Counts
        /// </remarks>
        public MainForm()
        {
            this.InitializeComponent();
            this.homeScreen = new HomeScreenUserControl(Model);
            this.InitializeUIScreens();
            this.CurrentView = this.HomeView;
            this.ShowView(this.HomeView);
            this.Text = Strings.AppName;
        }

        /// <summary>
        /// Gets a reference to the Album View.
        /// </summary>    
        /// <remarks>
        /// <para>Author: Jim Counts</para>
        /// </remarks>
        public AlbumViewUserControl AlbumView
        {
            get
            {
                return this.albumScreen;
            }
        }

        /// <summary>
        /// Gets a reference to the create album view.
        /// </summary>
        /// <remarks>
        /// <para>Author: Jim Counts</para>
        /// </remarks>
        public CreateAlbumUserControl CreateAlbumView
        {
            get
            {
                return this.createAlbumScreen;
            }
        }

        /// <summary>
        /// Gets or sets a reference to the current screen.
        /// </summary>
        /// <value>
        /// The current screen.
        /// </value>
        /// <remarks>
        /// <para>Author: Jim Counts</para>
        /// </remarks>
        public UserControl CurrentView { get; set; }

        /// <summary>
        /// Gets a reference to the Opening View.
        /// </summary>
        /// <remarks>
        /// <para>Author: Jim Counts</para>
        /// </remarks>
        public HomeScreenUserControl HomeView
        {
            get
            {
                return this.homeScreen;
            }
        }

        /// <summary>
        /// Gets the previous view history.
        /// </summary>
        /// <remarks>
        /// <para>Author: Jim Counts</para>
        /// </remarks>
        public Stack<UserControl> PreviousViews
        {
            get
            {
                return this.previousViews;
            }
        }

        /// <summary>
        /// Shows the view.
        /// </summary>
        /// <param name="viewToShow">The view to show.</param>
        /// <remarks>
        /// Author: Jim Counts
        /// </remarks>
        protected void ShowView(IScreen viewToShow)
        {
            // Helps prevent flickering by suspending layout during changes.
            this.panelScreenHolder.SuspendLayout();

            this.CurrentView = viewToShow.Control;
            this.HideAllViews();

            viewToShow.ShowView(this.PreviousViews);

            this.panelScreenHolder.ResumeLayout();
        }

        /// <summary>
        /// Hides all views.
        /// </summary>
        /// <remarks><para>Author(s): Miguel Gonzales and Andrea Tan</para></remarks>
        private void HideAllViews()
        {
            foreach (Control screen in this.panelScreenHolder.Controls)
            {
                screen.Visible = false;
            }
        }

        /// <summary>
        /// Initializes the UI screens.
        /// </summary>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void InitializeUIScreens()
        {
            //// this.HomeView.AlbumCollection = MainForm.Model.AlbumCollection;
            this.AttachEventsToScreens();
            this.panelScreenHolder.Controls.Add(this.HomeView);
            this.panelScreenHolder.Controls.Add(this.AlbumView);
            this.panelScreenHolder.Controls.Add(this.CreateAlbumView);
            this.HideAllViews();
        }

        /// <summary>
        /// Attaches the events to screens.
        /// </summary>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void AttachEventsToScreens()
        {
            this.HomeView.CreateButtonEvent += this.HandleCreateButtonClick;
            this.HomeView.AlbumSelectedEvent += this.ShowSelectedAlbum;
            this.HomeView.DeleteAlbumEvent += this.DeleteAlbum;
            this.CreateAlbumView.CancelEvent += this.GoBack;
            this.CreateAlbumView.ContinueEvent += this.CreateOrEditAlbum;
            this.AlbumView.BackEvent += this.ReturnToHomeView;
            ////this.AlbumView.AddPhotosEvent += this.AddPhotos;
            this.AlbumView.RenameAlbumEvent += this.RenameAlbum;
        }

        /// <summary>
        /// Shows the album selected from the opening view.
        /// </summary>
        /// <param name="sender">The album name label.</param>
        /// <param name="e">The event args.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void ShowSelectedAlbum(object sender, AlbumEventArgs e)
        {
            this.AlbumView.CurrentAlbum = Model.GetAlbum(e.AlbumName);
            //// this.AlbumView.CurrentAlbum = (Album)Model.Albums.AlbumList[e.TheAlbum.AlbumID.Replace("&&", "&")];
            this.ShowView(this.AlbumView);
        }

        /// <summary>
        /// Deletes the album specified in the event data.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PhotoBuddy.EventObjects.AlbumEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-27</para>
        ///   <para>Modified: 2011-10-28</para>
        /// </remarks>
        private void DeleteAlbum(object sender, AlbumEventArgs e)
        {
            var result = CultureAwareMessageBox.Show(
                this,
                "Are you sure you want to delete this album?",
                "Delete Album?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }

            var album = Model.GetAlbum(e.AlbumName);
            album.Delete();
            this.ShowView(this.HomeView);
        }

        /// <summary>
        /// Create or Edit an album
        /// </summary>
        /// <param name="sender">The continue button from the createScreen.</param>
        /// <param name="e">The event args.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales, Andrea Tan, Jim Counts
        /// </remarks>
        private void CreateOrEditAlbum(object sender, EventArgs e)
        {
            string rawAlbumName = this.CreateAlbumView.UserEnteredText;
            if (Model.IsExistingAlbumName(rawAlbumName))
            {
                var invalidAlbumNameMessage = new InvalidAlbumNameMessage();
                CultureAwareMessageBox.Show(
                    this,
                    invalidAlbumNameMessage.Text,
                    invalidAlbumNameMessage.Caption,
                    invalidAlbumNameMessage.Buttons,
                    invalidAlbumNameMessage.Icon);
                return;
            }

            if (this.CreateAlbumView.AlbumCreateMode)
            {
                // Creating a new album
                Model.AddAlbum(rawAlbumName);
                Model.SaveAlbums();
            }
            else
            {
                // Editing an album
                Model.RenameAlbum(this.CreateAlbumView.AlbumName, rawAlbumName);
            }

            // Return to the album view screen, showing the current album.
            this.AlbumView.CurrentAlbum = Model.GetAlbum(rawAlbumName);
            this.ShowView(this.AlbumView);
        }

        /// <summary>
        /// Renames the album.
        /// </summary>
        /// <param name="sender">The rename album button.</param>
        /// <param name="e">The event args.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void RenameAlbum(object sender, EventArgs e)
        {
            if (this.AlbumView.CurrentAlbum != null)
            {
                this.CreateAlbumView.ResetForm(false, this.AlbumView.CurrentAlbum.AlbumId);
                this.ShowView(this.CreateAlbumView);
            }
        }

        /////// <summary>
        /////// Adds photos to an album.
        /////// </summary>
        /////// <param name="sender">The finish button.</param>
        /////// <param name="e">The event args.</param>
        /////// <remarks>
        /////// Author(s): Miguel Gonzales, Andrea Tan, Jim Counts
        /////// </remarks>
        ////private void AddPhotos(object sender, EventArgs e)
        ////{
        ////    using (OpenFileDialog fileBrowser = new OpenFileDialog())
        ////    {
        ////        fileBrowser.InitialDirectory = this.importFolderPath;
        ////        fileBrowser.Filter = "jpg files (*.jpg)|*.jpg|png files (*.png)|*.png|bmp files (*.bmp)|*.bmp|gif files (*.gif)|*.gif";
        ////        fileBrowser.FilterIndex = 1;
        ////        fileBrowser.RestoreDirectory = true;
        ////        fileBrowser.Multiselect = true;
        ////        fileBrowser.Title = Format.Culture("Add to {0} - Photo Buddy", this.AlbumView.CurrentAlbum.AlbumId);
        ////        var result = fileBrowser.ShowDialog();
        ////        if (result == DialogResult.OK)
        ////        {
        ////            ////var multiPhotoImportForm = new MultiPhotoImportForm();
        ////            int maxLen = PhotoBuddy.Common.Constants.MaxNameLength - 4;
        ////            this.importFolderPath = Path.GetDirectoryName(fileBrowser.FileName);
        ////            foreach (string fileName in fileBrowser.FileNames)
        ////            {
        ////                string displayName = fileName.Substring(0, maxLen);

        ////                multiPhotoImportForm.AddImageFromFile(fileName);
        ////                ////     this.VerifyIncomingPhoto(fileName);
        ////            }

        ////            var importResult = multiPhotoImportForm.ShowDialog();
        ////            if (importResult == DialogResult.OK)
        ////            {
        ////                foreach (var importFile in multiPhotoImportForm.SelectedFiles)
        ////                {
        ////                    // User approved so upload the photo to the album.
        ////                    string name = importFile.Key;
        ////                    string file = importFile.Value;
        ////                    Model.AddPhotoToAlbum(this.AlbumView.CurrentAlbum.AlbumId, name, file);
        ////                }

        ////                this.AlbumView.RefreshPhotoList();
        ////            }
        ////        }
        ////    }
        ////}

        /////// <summary>
        /////// Verifies the incoming photo.
        /////// </summary>
        /////// <param name="photoFileName">The name of the photo the user is uploading.</param>
        /////// <remarks>
        ///////   <para>Author(s): Miguel Gonzales and Andrea Tan</para>
        ///////   <para>When the user selects a photo to import, we want to show them the photo so they can confirm
        /////// that they selected the correct photo.</para>
        /////// </remarks>
        ////private void VerifyIncomingPhoto(string photoFileName)
        ////{
        ////    using (UploadViewForm uploadPhoto = new UploadViewForm(this.MessageService, photoFileName))
        ////    {
        ////        // Need this check to see if the user attempted an invalid file type.
        ////        if (uploadPhoto.IsDisposed)
        ////        {
        ////            return;
        ////        }

        ////        DialogResult result = uploadPhoto.ShowDialog();
        ////        if (result == DialogResult.OK)
        ////        {
        ////            // User approved so upload the photo to the album.
        ////            string name = uploadPhoto.DisplayName;
        ////            string file = photoFileName;
        ////            Model.AddPhotoToAlbum(this.AlbumView.CurrentAlbum.AlbumId, name, file);
        ////        }
        ////    }
        ////}

        /// <summary>
        /// Goes back to the most recent screen before the current screen.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void GoBack(object sender, EventArgs e)
        {
            if (this.PreviousViews == null || this.PreviousViews.Count < 1)
            {
                this.ShowView(this.HomeView);
            }

            this.ShowView((IScreen)this.PreviousViews.Pop());
        }

        /// <summary>
        /// Returns to the Opening View.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void ReturnToHomeView(object sender, EventArgs e)
        {
            this.ShowView(this.HomeView);
        }

        /// <summary>
        /// Handles the create button click.
        /// </summary>
        /// <param name="sender">Create button click on Home screen.</param>
        /// <param name="e">The event args.</param>
        /// <remarks>
        /// <para>Author(s): Miguel Gonzales and Andrea Tan</para>
        /// <para>Initiates the creation of a new album.</para>
        /// </remarks>
        private void HandleCreateButtonClick(object sender, EventArgs e)
        {
            this.CreateAlbumView.ResetForm(true, string.Empty);
            this.ShowView(this.CreateAlbumView);
        }

        /// <summary>
        /// Changes the color of the app name label when the mouse passes over it.
        /// </summary>
        /// <param name="sender">AppName label.</param>
        /// <param name="e">The event args.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void HandleAppNameLabelMouseEnter(object sender, EventArgs e)
        {
            this.AppNameLabel.ForeColor = Color.Azure;
        }

        /// <summary>
        /// Changes the color of the app name label when the mouse passes out of it.
        /// </summary>
        /// <param name="sender">AppName label.</param>
        /// <param name="e">The event args.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void HandleAppNameLabelMouseLeave(object sender, EventArgs e)
        {
            this.AppNameLabel.ForeColor = Color.Black;
        }

        /// <summary>
        /// Handles the Click event of the AppNameLabel control.
        /// </summary>
        /// <param name="sender">App Name label.</param>
        /// <param name="e">The event args.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// this method is showing the message box when photobuddy is clicked
        /// at the home screen (mainUserControl) if the user is on the homescreen.
        /// Otherwise it returns to the homescreen.
        /// preCondition: must be already in homeScreeenUserControl
        /// postCondition: display the ownership of the program.
        /// </remarks>
        private void HandleAppNameLabelClick(object sender, EventArgs e)
        {
            if (this.CurrentView == this.HomeView)
            {
                var aboutPhotoBuddyMessage = new AboutPhotoBuddyMessage();
                CultureAwareMessageBox.Show(
                    this,
                    aboutPhotoBuddyMessage.Text,
                    aboutPhotoBuddyMessage.Caption,
                    aboutPhotoBuddyMessage.Buttons,
                    aboutPhotoBuddyMessage.Icon);
                return;
            }

            this.ShowView(this.HomeView);
        }
    }
}