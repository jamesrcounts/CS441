/***********************************************************************************
 * Author(s): Miguel Gonzales & Andrea Tan
 * Date: Sept 28 2011
 * Modified date: Oct 9 2011
 * Description: this class is responsible for the use control in album view which 
 *              is called from mainForm to do the state changes.
 *             
 * 
 ************************************************************************************/

using System;
using System.Windows.Forms;
using TheNewPhotoBuddy.BussinessRule;
using TheNewPhotoBuddy.Controls;
using System.Drawing;
using TheNewPhotoBuddy.Common.CommonClass;

namespace TheNewPhotoBuddy.Screens
{
    public partial class AlbumViewUserControl : UserControl, IScreen
    {
        string displayName;

        private Album currentAlbum;
        public Album CurrentAlbum
        {
            get { return currentAlbum; }
            set
            {
                currentAlbum = value;
                if (currentAlbum != null)
                {
                    labelAlbumName.Text = currentAlbum.albumID;
                    refreshingAlbumPhotosViewList();
                    //photosFlowPanel.Focus();
                }
            }
        }

        public virtual string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        // Author(s): Miguel Gonzales & Andrea Tan
        // Date: Sept 28 2011
        // Modified date: Oct 9 2011

        // Create the events for this user control.

        //public event CancelEventHandler CancelEvent;
        public delegate void BackEventHandler(object sender, EventArgs e);
        // add an event of the delegate type
        public event BackEventHandler BackEvent;
        //public event CancelEventHandler CancelEvent;
        public delegate void RenameAlbumHandler(object sender, EventArgs e);
        // add an event of the delegate type
        public event RenameAlbumHandler RenameAlbumEvent;
        //public event CancelEventHandler CancelEvent;
        public delegate void AddPhotosEventHandler(object sender, EventArgs e);
        // add an event of the delegate type
        public event AddPhotosEventHandler AddPhotosEvent;

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// Album View user control constructor.
        /// </summary>
        public AlbumViewUserControl()
        {
            InitializeComponent();
            // Set the DockStyle of the UserControl to Fill.
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayName = "Album";
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// Handles the click of the back button.
        /// </summary>
        /// <param name="sender">Back button</param>
        /// <param name="e">event args</param>
        private void backButton_Click(object sender, EventArgs e)
        {
            // raise the create event
            BackEvent(this, e);
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// Handles the click of the add photos button.
        /// </summary>
        /// <param name="sender">Add photos button,</param>
        /// <param name="e">the event args.</param>
        private void AddPhotosButton_Click(object sender, EventArgs e)
        {
            AddPhotosEvent(this, e);
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// Handles the click of the rename album button.
        /// </summary>
        /// <param name="sender">Rename album button</param>
        /// <param name="e">the event args.</param>
        private void renameAlbumButton_Click(object sender, EventArgs e)
        {
            RenameAlbumEvent(this, e);
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// Handles the click of the name of a photo.
        /// </summary>
        /// <param name="sender">The photo name label.</param>
        /// <param name="e">the event args.</param>
        private void photo_Click(object sender, EventArgs e)
        {
            //Label photoLabel = sender as Label;
            PictureBox uc = sender as PictureBox;
            // Show the photo that the user clicked on.
            ViewPhotoForm photoForm = new ViewPhotoForm(currentAlbum, (Photo)uc.Tag);
            photoForm.ShowDialog();
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// Refreshes the list of photos in the current album.
        /// </summary>
        public void refreshingAlbumPhotosViewList()
        {
            if (currentAlbum != null)
            {
                // Clear out the photos is the panel.
                photosFlowPanel.Controls.Clear();
                if (currentAlbum.photoObjects.photoList.Count != 0)
                {
                    foreach (Photo d in currentAlbum.photoObjects.photoList.Values)
                    {
                        Photo tempPhoto = (Photo)d;

                        // OLD PHOTO NAME LABEL CODE WHEN PHOTOS WERE DISPLAYED AS TEXT.
                        // Create a new text label for each photo in the album
                        //PB_ClickLabel label = new PB_ClickLabel();  
                        //label.Text = tempAlbum.display_name;
                        //label.Tag = tempAlbum;
                        ////wire up the click event handler
                        //label.Click += new EventHandler(photo_Click);

                        // Create a thumbnail control for the current photo
                        PB_ThumbNailUserControl thumb = new PB_ThumbNailUserControl();
                        thumb.DiaplayName = tempPhoto.display_name;
                        // Store the photo object in the thumbnail tag.
                        // thumbnail is a public property to set the picturebox on the thumbnailUserControl.
                        thumb.thumbnail.Tag = tempPhoto;
                        // Get the photo from file.
                        thumb.thumbnail.Image = Image.FromFile(Constants.PhotosFolderPath + tempPhoto.copiedPath);
                        // Wire the click event to the picturebox
                        thumb.thumbnail.Click += new EventHandler(photo_Click);
                        // Add the thumb control to the flow panel.
                        photosFlowPanel.Controls.Add(thumb);
                    }
                }
            }
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// Flow panel needs to be focused in order to scroll using the mouse wheel and that 
        /// is what this function does.
        /// </summary>
        private void photosFlowPanel_MouseEnter(object sender, EventArgs e)
        {
            photosFlowPanel.Focus();
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// Flow panel needs to be focused in order to scroll using the mouse wheel and that 
        /// is what this function does.
        /// </summary>
        private void photosFlowPanel_MouseClick(object sender, MouseEventArgs e)
        {
            photosFlowPanel.Focus();
        }
    }
}
