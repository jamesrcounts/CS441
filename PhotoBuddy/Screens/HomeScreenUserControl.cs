/***********************************************************************************
 * Author(s): Miguel Gonzales & Andrea Tan
 * Date: Sept 28 2011
 * Modified date: Oct 9 2011
 * Description: this class is responsible for the use control in home default mainForm
 *              view which is called during the state changes and the 
 *              startup of the program.
 *             
 * 
 ************************************************************************************/
using System;
using System.Windows.Forms;
using TheNewPhotoBuddy.BussinessRule;
using TheNewPhotoBuddy.EventObjects;
using TheNewPhotoBuddy.Controls;

namespace TheNewPhotoBuddy.Screens
{
    public partial class HomeScreenUserControl : UserControl, IScreen
    {
        string displayName;

        public virtual string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        // Create the events for this user control.

        // add a delegate
        public delegate void CreateButtonClicked(object sender, EventArgs e);
        // add an event of the delegate type
        public event CreateButtonClicked CreateButtonEvent;
        // add a delegate
        public delegate void AlbumSelectedEventHandler(object sender, AlbumEventArgs e);
        // add an event of the delegate type
        public event AlbumSelectedEventHandler AlbumSelectedEvent;

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// Constructor.
        /// </summary>
        public HomeScreenUserControl()
        {
            InitializeComponent();
            // Set the DockStyle of the UserControl to Fill.
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayName = "Albums";
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// Handles the user clicking on an ablum name in the list of albums.
        /// </summary>
        /// <param name="sender">The label displaying the name of the album.</param>
        /// <param name="e">The event args.</param>
        private void album_Click(object sender, EventArgs e)
        {
            PB_ClickLabel albumLabel = sender as PB_ClickLabel;
            AlbumEventArgs args = new AlbumEventArgs(albumLabel.Text);
            AlbumSelectedEvent(this, args);
        }
       
        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// exist method which simply exit the program.
        /// preCondition: program has started.
        /// postCOndition: exit when exit event is triggered.
        /// </summary>
        /// <param name="sender">The exit button.</param>
        /// <param name="e">The event args.</param>
        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// when click button is triggered from the UI
        /// this method is simply passing itself and its event args.
        /// preCondition: already in homescreenusercontrol, and events are triggered.
        /// postCondition: called the createButtonEvent
        /// </summary>
        /// <param name="sender">The create button.</param>
        /// <param name="e">The event args.</param>
        private void CreateButton_Click(object sender, EventArgs e)
        {
            // raise the create event
            EventArgs args = new EventArgs();
            CreateButtonEvent(this, args);
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// refreshingAlbumViewList
        /// this main function is to refresh the list in which the list has been changed
        /// (album gets added or renamed)
        /// preCondition: changed on the albums
        /// postCOndition: display the list of the albums thata re currently have.
        /// </summary>
        /// <param name="albums">The list of all the albums.</param>
        public void refreshingAlbumViewList(Albums albums)
        {
            //this.listBoxAlbums.Items.Clear();
            albumsFlowPanel.Controls.Clear();
            if (albums.albumsList.Count != 0)
            {
                foreach (Album d in albums.albumsList.Values)
                {  
                    Album tempAlbum = (Album)d;
                    PB_ClickLabel label = new PB_ClickLabel();
                    label.Text = tempAlbum.albumID;

                    label.Click += new EventHandler(album_Click);
                    albumsFlowPanel.Controls.Add(label);
                }
            }
        }
    }
}
