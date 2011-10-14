/***********************************************************************************
 * Author(s): Miguel Gonzales & Andrea Tan
 * Date: Sept 28 2011
 * Modified date: Oct 9 2011
 * Description: An event args object that enables the passing of an album object after 
 *              an event occurs.
 * 
 ************************************************************************************/

using TheNewPhotoBuddy.BussinessRule;

namespace TheNewPhotoBuddy.EventObjects
{
    /// <summary>
    /// Author(s): Miguel Gonzales & Andrea Tan
    /// Date: Sept 28 2011
    /// Modified date: Oct 9 2011
    /// 
    /// This is used to pass data after an album has been created.
    /// this function inherrited
    /// </summary>
    public class AlbumEventArgs : System.EventArgs
    {
        private Album album;

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// Date: Sept 28 2011
        /// Modified date: Oct 9 2011
        /// 
        /// Constructor for the event args.
        /// </summary>
        /// <param name="albumName">The name of the album to pass.</param>
        public AlbumEventArgs(string albumName)
        {
            album = new Album(albumName);
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// Date: Sept 28 2011
        /// Modified date: Oct 9 2011
        /// 
        /// Constructor takes an album object.
        /// </summary>
        /// <param name="theAlbum">The album object to pass</param>
        public AlbumEventArgs(Album theAlbum)
        {
            this.album = theAlbum;
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// Date: Sept 28 2011
        /// Modified date: Oct 9 2011
        /// 
        /// Returns the Album object in the event args.
        /// </summary>
        public Album TheAlbum
        {
            get
            {
                return album;
            }
        }
    }
}
