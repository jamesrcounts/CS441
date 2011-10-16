/***********************************************************************************
 * Author(s): Miguel Gonzales & Andrea Tan
 * Date: Sept 28 2011
 * Modified date: Oct 9 2011
 * Description: this class is responsible for the use control in create album which 
 *              is called from mainForm to do the state changes.
 *             
 * 
 ************************************************************************************/

using System;
using System.Windows.Forms;

namespace TheNewPhotoBuddy.Screens
{
    public partial class CreateAlbumUserControl : UserControl, IScreen
    {
        string displayName;
        string albumName;
        private string userEnteredText;
        // Indicates whether this control is used to created an ablum (true) or edit an album.
        bool isCreate = true;

        public string AlbumName { get { return albumName; } }
        public string UserEnteredText { get { return userEnteredText; } }
        public bool IsCreate { get { return isCreate; } }

        public virtual string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        // Create the events for this user control.
        public delegate void CancelEventHandler(object sender, EventArgs e);
        // add an event of the delegate type
        public event CancelEventHandler CancelEvent;

        public delegate void ContinueEventHandler(object sender, EventArgs e);
        // add an event of the delegate type
        public event ContinueEventHandler ContinueEvent;

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// Contructor for Create album user control.
        /// </summary>
        public CreateAlbumUserControl()
        {
            InitializeComponent();
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayName = "Create New Album";
            this.createHeaderLabel.Text = this.displayName;
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// Handles the cancel button click.
        /// </summary>
        /// <param name="sender">The cancel button.</param>
        /// <param name="e">the event args.</param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            // raise the create event
            CancelEvent(this, e);
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// this function is made to check when event create album is invoked
        /// the input name must not be empty otherwise there will be a message warning
        /// showing that the label is empty and must be change.
        /// preCondition: input label must not be empty string
        /// postCondition: if the album name has not been created then simply continue and
        ///                eventually get added to album list. otherwise it will do nothing.
        /// </summary>
        /// <param name="sender">Continue button</param>
        /// <param name="e">the event args.</param>
        private void continueButton_Click(object sender, EventArgs e)
        {
            if (albumNameTextBox.Text == "")
            {
                MessageBox.Show("Album name must not be empty!", "Empty Album ID Issue", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!isCreate) 
            {
                // Renaming album - user entered the existing name so cancel the rename
                if (albumName == albumNameTextBox.Text)
                {
                    CancelEvent(this, e);
                    return;
                }
            }
            // Store the text that the user entered.
            userEnteredText = albumNameTextBox.Text;
            // raise the create event
            ContinueEvent(this, e);
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// reset Create form method which takes a boolean and a string album name.
        /// this function is mainly used to set focus on object label of what user control
        /// is currently at and set the label name for it.
        /// depending if it's on create it will assigned a different kind of text message,
        /// and if it's on the renaming it will use the same usercontrol but change the prompt message for it.
        /// 
        /// preCondition: 2 parameters string and bool
        /// postCondition: display a necessary label depending of what state it is currently in.
        /// </summary>
        /// <param name="isCreateAlbum">Bool value indicates creating a new object or editing existing.</param>
        /// <param name="theAlbumName">Name of the album if editing.</param>
        public void ResetCreateForm(bool isCreateAlbum, string theAlbumName)
        {
            albumName = theAlbumName;
            this.isCreate = isCreateAlbum;
            if (isCreate)
            {// Creating a new album.
                createAlbumLabel.Text = "Please enter the name of the new album:";
                albumNameTextBox.Text = "";
                this.createHeaderLabel.Text = this.displayName;
            }
            else
            {// Editing an existing album.
                albumNameTextBox.Text = theAlbumName;
                createAlbumLabel.Text = "Please enter the new album name for: " + albumName;
                this.createHeaderLabel.Text = "Edit Album: " + theAlbumName;
            }
            albumNameTextBox.Focus();
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// Checks to see if the user pressed the return key and if so it executes the continue button
        /// click event.
        /// </summary>
        /// <param name="sender">The album name textbox.</param>
        /// <param name="e">the event args.</param>
        private void albumNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // See if the user pressed the enter key.
            if (e.KeyCode == Keys.Enter)
            {
                continueButton_Click(sender, e);
            }
        }
    }
}
