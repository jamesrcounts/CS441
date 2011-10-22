/***********************************************************************************
 * Author(s): Miguel Gonzales and Andrea Tan
 * Date: Sept 28 2011
 * Modified date: Oct 9 2011
 * Description: this class is responsible in instantiation of the album objects.
 *              this class also provides the mean of accessing the album contents
 *              as well as updating its contents as well
 * 
 *
 ************************************************************************************/
using System;
using System.Diagnostics;

namespace TheNewPhotoBuddy.BussinessRule
{
    /// <summary>
    /// Author(s): Miguel Gonzales and Andrea Tan
    /// 
    /// Album Object Class this constructor initialize an album object.
    /// this class also contains public functions to set and get the following below:
    /// 1. AlbumID.
    /// 2. CoverPhoto.
    /// 3. a collections of photo lists.
    /// </summary>
    [DebuggerDisplay("{albumID}")]
    public class Album 
    {

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// constructor which initialized the variables album id, cover photo, and photo list
        /// precondition: none
        /// postcondition: initialized objects and variables such as album id and cover photo
        /// </summary>
        public Album()
        {
            albumID = "";
            photoObjects = new Photos();
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// overloading constructor which initialized the variables albumid, coverphoto, and photolist
        /// precondition: parameter albumName must be passed into this constructor.
        /// postcondition: initialized objects and variables such as albumid and cover photo
        /// </summary>C:\Documents and Settings\andrea.tan\My Documents\Downloads\TheNewPhotoBuddy-1-0-0-5-Source\TheNewPhotoBuddy\TheNewPhotoBuddy\Controls\
        /// <param name="albumName"></param>
        public Album(string albumName)
        {
            this.albumID = albumName;
            photoObjects = new Photos();
        }
       
        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// get and set albumID from instantiated album object.
        /// </summary>
        public String albumID { get; set; }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// get and set photoObjects from instantiated album object.
        /// </summary>
        public Photos photoObjects { get; set; }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// overidde method to return album id in string type 
        /// for a generic get of the album object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return albumID;
        }
    }
}
