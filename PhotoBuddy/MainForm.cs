/***********************************************************************************
 * Author(s): Miguel Gonzales and Andrea Tan
 * Date: Sept 28 2011
 * Modified date: Oct 19 2011
 * Description: program start up forms, which gets inherited from multiple usercontrols
 ************************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using PhotoBuddy.Resources;
using TheNewPhotoBuddy.BussinessRule;
using TheNewPhotoBuddy.EventObjects;
using TheNewPhotoBuddy.Screens;

namespace TheNewPhotoBuddy
{
    /// <summary>
    /// The application shell.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// The album model
        /// </summary>
        public static readonly Collectors model = new Collectors();

        // The different screens(or views) of the application.
        /// <summary>
        /// The Opening View shows a list of albums; allows user to create new albums.
        /// </summary>
        private readonly HomeScreenUserControl homeScreen = new HomeScreenUserControl();

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
        /// <remarks><para>Author(s): Miguel Gonzales and Andrea Tan</para></remarks>
        public MainForm()
        {
            InitializeComponent();
            this.PreviousViews = new Stack<UserControl>();
            InitializeUIScreens();
            CurrentView = HomeView;
            ShowView((IScreen)HomeView);
            // Set the newAlbumName of the form
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
        /// Gets or sets the previous View.
        /// </summary>
        /// <value>
        /// The previous screen.
        /// </value>
        /// <remarks>
        /// <para>Author: Jim Counts</para>
        /// </remarks>
        public Stack<UserControl> PreviousViews { get; protected set; }

        ///// <summary>
        ///// Shows a view, hides all others.
        ///// </summary>
        ///// <param name="viewToShow">The view to show.</param>
        ///// <remarks>
        ///// Author(s): Miguel Gonzales and Andrea Tan
        ///// </remarks>
        //[System.Obsolete("Use ShowView(IScreen) instead")]
        //protected void ShowView(UserControl viewToShow)
        //{
        //    // Helps prevent flickering by suspending layout during changes.
        //    panelScreenHolder.SuspendLayout();

        //    // Refresh the albums list if we are showing the home screen.
        //    if (viewToShow == HomeView)
        //    {
        //        HomeView.RefreshAlbumViewList(model.Albums);
        //    }

        //    // Can't go back to create album screen so don't allow previous screen to be set to it.
        //    if (CurrentView != CreateAlbumView)
        //    {
        //        this.PreviousViews.Push(this.CurrentView);
        //    }

        //    CurrentView = viewToShow;
        //    HideAllViews();
        //    viewToShow.Visible = true;

        //    // Set the main form focus to the screen. This is important for focusing text boxes.
        //    CurrentView.Focus();

        //    panelScreenHolder.ResumeLayout();
        //}

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
            foreach (Control screen in panelScreenHolder.Controls)
            {
                screen.Visible = false;
            }
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// This adds all of the apps screens to the screen holder panel and attaches all the main 
        /// form event handlers for the screens' events.
        /// preCondition: program is started
        /// postCondition: initialized the control to all the screens that are available in this project.
        /// </summary>
        private void InitializeUIScreens()
        {
            this.HomeView.Albums = MainForm.model.Albums;
            AttachEventsToScreens();
            this.panelScreenHolder.Controls.Add(HomeView);
            this.panelScreenHolder.Controls.Add(AlbumView);
            this.panelScreenHolder.Controls.Add(CreateAlbumView);
            HideAllViews();
        }

        /// <summary>
        /// Attaches the events to screens.
        /// </summary>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void AttachEventsToScreens()
        {
            HomeView.CreateButtonEvent += this.HandleCreateButtonClick;
            HomeView.AlbumSelectedEvent += showSelectedAlbum;
            CreateAlbumView.CancelEvent += GoBack;
            CreateAlbumView.ContinueEvent += CreateOrEditAlbum;
            AlbumView.BackEvent += backToHomeScreen;
            AlbumView.AddPhotosEvent += AddPhotos;
            AlbumView.RenameAlbumEvent += renameAlbum;
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// Shows an album when the user selects an album name from the list of albums.
        /// preCondition: event button clicked for changing UI
        /// postCondition: assigned current Album object and switch to the new screen.
        /// </summary>
        /// <param name="sender">The album name label.</param>
        /// <param name="e">The event args.</param>
        private void showSelectedAlbum(object sender, AlbumEventArgs e)
        {
            AlbumView.CurrentAlbum = (Album)model.Albums.albumsList[e.TheAlbum.albumID.Replace("&&", "&")];
            ShowView((IScreen)AlbumView);
        }

        /// <summary>
        /// Create or Edit an album
        /// </summary>
        /// <param name="sender">The continue button from the createScreen.</param>
        /// <param name="e">The event args.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void CreateOrEditAlbum(object sender, EventArgs e)
        {
            string rawAlbumName = CreateAlbumView.UserEnteredText;
            if (model.Albums.IsExistingAlbumName(rawAlbumName))
            {
                MessageBox.Show(
                    "Invalid album name! Please enter a new album name.",
                    "Album Name Invalid",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (this.CreateAlbumView.InCreateMode)
            {
                // Creating a new album
                model.Add(new Album(rawAlbumName));
                model.populateObjectsIntoXML();
            }
            else
            {
                // Editing an album
                model.EditAlbumName(CreateAlbumView.AlbumName, rawAlbumName);
            }

            // Return to the album view screen, showing the current album.
            AlbumView.CurrentAlbum = (Album)model.Albums.albumsList[rawAlbumName];
            ShowView(AlbumView);
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// this function is used to rename album when an event to change the album name is clicked.
        /// preCondition : change album name is clicked
        /// postCondition: when album exist, it will call showscreen screateAlbumscreen userControl 
        /// </summary>
        /// <param name="sender">The rename album button.</param>
        /// <param name="e">The event args.</param>
        private void renameAlbum(object sender, EventArgs e)
        {
            if (AlbumView.CurrentAlbum != null)
            {
                CreateAlbumView.ResetForm(false, AlbumView.CurrentAlbum.albumID);
                ShowView(CreateAlbumView as IScreen);
            }
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// addPhotos event is invoked, it will call openDialog for the user to choose all the possible picture type photos.
        /// when OK button is clicked it will call haveUserVerifyAddPhoto function
        /// preCondition: event for addphoto is called
        /// postCOndition: called haveUserVerifyAddphoto event when OK is pressed otherwise do nothing.
        ///                also refresh the albumphotoviewList
        /// </summary>
        /// <param name="sender">The finish button.</param>
        /// <param name="e">The event args.</param>
        private void AddPhotos(object sender, EventArgs e)
        {
            using (OpenFileDialog fileBrowser = new OpenFileDialog())
            {
                fileBrowser.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                fileBrowser.Filter = "jpg files (*.jpg)|*.jpg|png files (*.png)|*.png|bmp files (*.bmp)|*.bmp|gif files (*.gif)|*.gif";
                fileBrowser.FilterIndex = 1;
                fileBrowser.RestoreDirectory = true;
                fileBrowser.Multiselect = false;
                fileBrowser.Title = string.Format("Add to {0} - Photo Buddy", AlbumView.CurrentAlbum.albumID);
                DialogResult result = fileBrowser.ShowDialog();
                if (result == DialogResult.OK)
                {
                    VerifyAddPhoto(fileBrowser.FileName);
                }
            }
            AlbumView.RefreshPhotoList();
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// have User verify add photo takes in photo file name string
        /// and instantiated the uploadPhoto object.
        /// when Ok result return value is the return variable then it will
        /// add the photo into the Album list table.
        /// 
        /// preCondition: photoFilename
        /// postCondition: dispose the picture after another event is clicked and if the result is OK
        ///                then it will add to the album list, otherwise it will do nothing.
        /// </summary>
        /// <param name="photoFilename">The name of the photo the user is uploading.</param>
        private void VerifyAddPhoto(string photoFilename)
        {
            using (UploadViewForm uploadPhoto = new UploadViewForm(photoFilename))
            {
                // Need this check to see if the user attempted an invalid file type.
                if (uploadPhoto.IsDisposed)
                {
                    return;
                }

                DialogResult result = uploadPhoto.ShowDialog();
                if (result == DialogResult.OK)
                {


                    // User approved so upload the photo to the album.
                    string name = uploadPhoto.PhotoName;
                    string file = photoFilename;
                    model.AddPhotoToAlbum(AlbumView.CurrentAlbum.albumID, name, file);
                }
            }
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// back button function takes in a event.
        /// when back button is clicked, it will try to return to the previous page
        /// that was previously looked at before the current page
        /// PreCondition: must not be in start page.
        /// postCondition: return to previous page (i-1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoBack(object sender, EventArgs e)
        {
            if (this.PreviousViews == null || this.PreviousViews.Count < 1)
            {
                this.ShowView((IScreen)this.HomeView);
            }

            this.ShowView((IScreen)this.PreviousViews.Pop());
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// Back to Home screen,
        /// when Photobuddy button is clicked, this method will get called
        /// to return to the main usercontrol of the program.
        /// preCondition: none
        /// postCondition: always return to the main Usercontrol
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backToHomeScreen(object sender, EventArgs e)
        {
            ShowView((IScreen)HomeView);
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// Handles the create new album event.
        /// </summary>
        /// <param name="sender">Create button click on Home screen.</param>
        /// <param name="e">The event args.</param>
        private void HandleCreateButtonClick(object sender, EventArgs e)
        {
            CreateAlbumView.ResetForm(true, "");
            ShowView((IScreen)CreateAlbumView);
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// this is change even color
        /// when mouse is hoovered on top of the Photo Buddy label.
        /// the color will change.
        /// </summary>
        /// <param name="sender">AppName label.</param>
        /// <param name="e">The event args.</param>
        private void AppNameLabel_MouseEnter(object sender, EventArgs e)
        {
            AppNameLabel.ForeColor = Color.Azure;
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// this is change even color
        /// when mouse is hoovered away from the object
        /// the color will change.
        /// </summary>
        /// <param name="sender">AppName label.</param>
        /// <param name="e">The event args.</param>
        private void AppNameLabel_MouseLeave(object sender, EventArgs e)
        {
            AppNameLabel.ForeColor = Color.Black;
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// this method is showing the message box when photobuddy is clicked
        /// at the home screen (mainUserControl) if the user is on the homescreen. 
        /// Otherwise it returns to the homescreen.
        /// preCondition: must be already in homeScreeenUserControl
        /// postCondition: display the ownership of the program.
        /// </summary>
        /// <param name="sender">App Name label.</param>
        /// <param name="e">The event args.</param>
        private void AppNameLabel_Click(object sender, EventArgs e)
        {
            if (CurrentView == HomeView)
            {
                MessageBox.Show("Photo Buddy by GOLD RUSH\n", Strings.AppName, MessageBoxButtons.OK);
                return;
            }
            
            ShowView((IScreen)HomeView);
        }
    }
}
