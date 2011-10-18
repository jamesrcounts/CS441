/***********************************************************************************
 * Author(s): Miguel Gonzales and Andrea Tan
 * Date: Sept 28 2011
 * Modified date: Oct 18 2011
 * Description: program start up forms, which gets inherited from multiple usercontrols
 * 
 ************************************************************************************/

using System;
using System.Drawing;
using System.Windows.Forms;
using PhotoBuddy.Resources;
using TheNewPhotoBuddy.BussinessRule;
using TheNewPhotoBuddy.EventObjects;
using TheNewPhotoBuddy.Screens;

namespace TheNewPhotoBuddy
{
  public partial class MainForm : Form
  {
    public static Collectors collectors = new Collectors();

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
    /// Author(s): Miguel Gonzales and Andrea Tan
    /// 
    /// The MainForm constructor.
    /// </summary>
    public MainForm()
    {
      InitializeComponent();
      InitializeUIScreens();
      CurrentView = HomeView;
      ShowScreen(CurrentView);
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
    public UserControl CurrentView { get; protected set; }

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
    public UserControl PreviousView { get; protected set; }

    /// <summary>
    /// Author(s): Miguel Gonzales and Andrea Tan
    /// 
    /// setting the user control screen to whichever it focues on
    /// preCondition: usercontrol screen must be passed int
    /// postCondition: shows the current usercontrol that is being passed in.
    /// </summary>
    /// <param name="screen">The screen to make visible.</param>
    protected void ShowScreen(UserControl screen)
    {
      // Helps prevent flickering by suspending layout during changes.
      panelScreenHolder.SuspendLayout();

      // Refresh the albums list if we are showing the home screen.
      if (screen == HomeView)
      {
        HomeView.RefreshingAlbumViewList(collectors.getAlbums);
      }

      // Can't go back to create album screen so don't allow previous screen to be set to it.
      if (CurrentView != CreateAlbumView)
      {
        PreviousView = CurrentView;
      }

      CurrentView = screen;
      HideAllScreens();
      screen.Visible = true;

      // Set the main form focus to the screen. This is important for focusing textboxes.
      CurrentView.Focus();

      panelScreenHolder.ResumeLayout();
    }

    /// <summary>
    /// Author(s): Miguel Gonzales and Andrea Tan
    /// 
    /// this function is created to hide all the screen that is currently opened
    /// preCondition: None
    /// postCondition: none of the controls will be shown.
    /// </summary>
    private void HideAllScreens()
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
      AttachEventsToScreens();
      this.panelScreenHolder.Controls.Add(HomeView);
      this.panelScreenHolder.Controls.Add(AlbumView);
      this.panelScreenHolder.Controls.Add(CreateAlbumView);
      HideAllScreens();
    }

    /// <summary>
    /// Author(s): Miguel Gonzales and Andrea Tan
    /// 
    /// Attaches all of the event handlers in this form to the various events on 
    /// the apps screens.
    /// preCondition: program is started
    /// postCondition: added the events to all the buttons that are avaialble in the UI
    /// </summary>
    private void AttachEventsToScreens()
    {
      HomeView.CreateButtonEvent +=
          new HomeScreenUserControl.CreateButtonClicked(CreateButton_Click);
      HomeView.AlbumSelectedEvent +=
          new HomeScreenUserControl.AlbumSelectedEventHandler(showSelectedAlbum);
      CreateAlbumView.CancelEvent +=
          new CreateAlbumUserControl.CancelEventHandler(back);
      CreateAlbumView.ContinueEvent +=
          new CreateAlbumUserControl.ContinueEventHandler(FinishCreateOrEditAlbum);
      AlbumView.BackEvent +=
          new AlbumViewUserControl.BackEventHandler(backToHomeScreen);
      AlbumView.AddPhotosEvent +=
          new AlbumViewUserControl.AddPhotosEventHandler(addPhotos);
      AlbumView.RenameAlbumEvent +=
          new AlbumViewUserControl.RenameAlbumHandler(renameAlbum);
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
      AlbumView.CurrentAlbum = (Album)collectors.getAlbums.albumsList[e.TheAlbum.albumID];
      ShowScreen(AlbumView);
    }

    /// <summary>
    /// Author(s): Miguel Gonzales and Andrea Tan
    /// 
    /// finished create or edit album is used to update the object that is in the album list (hashtable)
    /// and also write the data into xml
    /// preCondition: button finished or commit is invoked
    /// postCondition: create the album based on user entered inputs and if the name of the album is the same
    ///                a warning message will be shown to change the name.
    /// 
    /// </summary>
    /// <param name="sender">The continue button from the createScreen.</param>
    /// <param name="e">The event args.</param>
    private void FinishCreateOrEditAlbum(object sender, EventArgs e)
    {
      string newAlbumName = CreateAlbumView.UserEnteredText;
      //Creating a new album
      if (CreateAlbumView.IsCreate)
      {
        if (collectors.getAlbums.IsExistingAlbumName(newAlbumName))
        {
          MessageBox.Show(
          "Invalid album name! Please enter a new album name.",
          "Album Name Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }
        collectors.addAlbumtoAlbumList(new Album(newAlbumName));
        collectors.populateObjectsIntoXML();
      }
      // Editing an album
      else
      {
        if (collectors.getAlbums.IsExistingAlbumName(newAlbumName))
        {
          MessageBox.Show(
          "Invalid album name! please enter a new album name",
          "Album name Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }
        collectors.EditAlbumName(CreateAlbumView.AlbumName, newAlbumName);
      }
      // Return to the album view screen, showing the current album.
      AlbumView.CurrentAlbum = (Album)collectors.getAlbums.albumsList[newAlbumName];
      ShowScreen(AlbumView);
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
        CreateAlbumView.ResetCreateForm(false, AlbumView.CurrentAlbum.albumID);
        ShowScreen(CreateAlbumView);
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
    private void addPhotos(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog1 = new OpenFileDialog();
      openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
      openFileDialog1.Filter = "jpg files (*.jpg)|*.jpg|png files (*.png)|*.png|bmp files (*.bmp)|*.bmp|gif files (*.gif)|*.gif";
      openFileDialog1.FilterIndex = 1;
      openFileDialog1.RestoreDirectory = true;
      openFileDialog1.Multiselect = false;
      openFileDialog1.Title = "Add to " + AlbumView.CurrentAlbum.albumID + " - Photo Buddy";

      DialogResult result = openFileDialog1.ShowDialog();
      if (result == System.Windows.Forms.DialogResult.OK)
      {
        haveUserVerifyAddPhoto(openFileDialog1.FileName);
      }
      else
      {
        // Do nothing as the user canceled
      }
      AlbumView.refreshingAlbumPhotosViewList();
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
    private void haveUserVerifyAddPhoto(string photoFilename)
    {
      UploadViewForm uploadPhoto = new UploadViewForm(photoFilename);
      // Need this check to see if the user attempted an invalid file type.
      if (uploadPhoto.IsDisposed)
      {
        uploadPhoto.Close();
        return;
      }
      DialogResult result = uploadPhoto.ShowDialog();
      if (result == System.Windows.Forms.DialogResult.OK)
      {
        // User approved so upload the photo to the album.
        string name = uploadPhoto.PhotoName;
        string file = photoFilename;
        collectors.AddPhotoToAlbum(AlbumView.CurrentAlbum.albumID, name, file);
      }
      else
      {
        uploadPhoto.Close();
        //do nothing as dialog was not ok.
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
    private void back(object sender, EventArgs e)
    {
      if (PreviousView != null)
      {
        ShowScreen(PreviousView);
      }
      else
      {
        ShowScreen(HomeView);
      }
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
      ShowScreen(HomeView);
    }

    /// <summary>
    /// Author(s): Miguel Gonzales and Andrea Tan
    /// 
    /// Handles the create new album event.
    /// </summary>
    /// <param name="sender">Create button click on Home screen.</param>
    /// <param name="e">The event args.</param>
    private void CreateButton_Click(object sender, EventArgs e)
    {
      CreateAlbumView.ResetCreateForm(true, "");
      ShowScreen(CreateAlbumView);
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
      PreviousView = null;
      ShowScreen(HomeView);
    }
  }
}
