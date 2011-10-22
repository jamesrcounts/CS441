/***********************************************************************************
 * Author(s): Miguel Gonzales and Andrea Tan
 * Date: Sept 28 2011
 * Modified date: Oct 9 2011
 * Description: this class is responsible to show photo or possibly multiple photos
 *              that are exist in the current album that the user added.
 * 
 ************************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TheNewPhotoBuddy.BussinessRule;
using System.IO;
using TheNewPhotoBuddy.Common.CommonClass;

namespace TheNewPhotoBuddy
{
    public partial class ViewPhotoForm : Form
    {
        readonly Album album;
        readonly string photoID;
        readonly Photo picture;
        List<Photo> allPhotosInAlbum;
        // The index of the photo in the allPhotosInAlbum list.
        int photoIndex;

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// constructor which initialized all the necessary hooks to show the objects forms.
        /// </summary>
        public ViewPhotoForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// overloading constructor in which it takes an album object and a photo
        /// to assign to its GUI textboxs and labels. and also it calles the display
        /// of the index it is currently looked at.
        /// preCondition: album and photo objects
        /// postCondition: assigned the guis from the object parameters it gets from.
        ///                and also display it.
        /// </summary>
        /// <param name="currentAlbum">The album whose photos the user is viewing.</param>
        /// <param name="thePhoto">the specific photo to view.</param>
        public ViewPhotoForm(Album currentAlbum, Photo thePhoto)
        {
            InitializeComponent();
            this.Text = "Photo Display - Photo Buddy";
            this.album = currentAlbum;
            this.photoID = thePhoto.PhotoId;
            this.picture = thePhoto;
            currentAlbumLabel.Text = album.albumID;
            // Convert the hashtable to a list so we can enumerate through them.
            fillPhotoList();
            photoIndex = allPhotosInAlbum.IndexOf(picture);
            displayPhoto(photoIndex);
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// Creates a list of photos in the current album from the hashtable object.
        /// preCondition: collections of album photos are not empty
        /// postCondition: assigned all photos into allPhotoinAlbum variable
        /// </summary>
        private void fillPhotoList()
        {
            allPhotosInAlbum = album.photoObjects.photoList.Values.Cast<Photo>().ToList();
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// display Photo it actually takes a peek of where exactly the photo album is located
        /// and set the image to the pictureBox for image display.
        /// preCondition: index exist.
        /// postCondition: UI labels and textbox assignments and also picture display.
        /// </summary>
        /// <param name="index">The photo index in the list of photos.</param>
        private void displayPhoto(int index)
        {
            string filename = Path.GetFileName(allPhotosInAlbum[index].copiedPath);
            string path = Path.Combine(Constants.PhotosFolderPath, filename);
            pictureBox1.Image = Image.FromFile(path);
            photoNameLabel.Text = allPhotosInAlbum[index].display_name;
            this.Text = allPhotosInAlbum[index].display_name + " - Photo Buddy";
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// back button click method is used to listen to an event which back button is pressed.
        /// when this happened, the view form will be closed.
        /// preCondition: back button is pressed.
        /// postCondition: exit from viewphoto forms.
        /// </summary>
        /// <param name="sender">The back button.</param>
        /// <param name="e">The event args.</param>
        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// next button photos is used to traverse to the photos that are currently in particular viewed album
        /// preCondition: event right arrow is clicked
        /// postCOndition: focus and display the next photo objects.
        /// </summary>
        /// <param name="sender">The next photo button.</param>
        /// <param name="e">The event args.</param>
        private void nextPhotoButton_Click(object sender, EventArgs e)
        {
            if (canGoForward())
            {
                photoIndex++;
                displayPhoto(photoIndex);
            }
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// back button photos is used to traverse to the photos that are currently in particular viewed album
        /// preCondition: event left arrow is clicked
        /// postCOndition: focus and display the previous photo objects.
        /// </summary>
        /// <param name="sender">Previous photo button.</param>
        /// <param name="e">The event args.</param>
        private void previousPhotoButton_Click(object sender, EventArgs e)
        {
            if (canGoBack())
            {
                photoIndex--;
                displayPhoto(photoIndex);
            }
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// cangoForward method is to check wether the the photos are at the end
        /// preCondition: counts must not be 0 and not at the end.
        /// postCondition: return true/false based on if index is less than its count-1.
        /// </summary>
        /// <returns>true if we are not at the end of the list.</returns>
        private bool canGoForward()
        {
            return (photoIndex < allPhotosInAlbum.Count - 1);
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// cangoBack method is to check wether the the photos are at the beginning
        /// preCondition: counts must not be 0 and not at the end.
        /// postCondition: return true/false based on if index is bigger than 0.
        /// </summary>
        /// <returns>true if we are not at the first photo in the list.</returns>
        private bool canGoBack()
        {
            return (photoIndex > 0);
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// event change color when mouse arrow is hoover on top of the arrow object.
        /// All of the buttons on this form use this function to change color on mouse over.
        /// </summary>
        /// <param name="sender">Any Button on this form.</param>
        /// <param name="e">The event args,</param>
        private void previousPhotoButton_MouseEnter(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.ForeColor = Color.Azure;
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// event change color when mouse arrow is away from of the arrow object.
        /// All of the buttons on this form use this function to change color on mouse leave.
        /// </summary>
        /// <param name="sender">Any Button on this form.</param>
        /// <param name="e">The event args.</param>
        private void previousPhotoButton_MouseLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.ForeColor = Color.DarkGray;
        }
    }
}
