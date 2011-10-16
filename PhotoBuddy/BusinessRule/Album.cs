/***********************************************************************************
 * Author(s): Miguel Gonzales & Andrea Tan
 * Date: Sept 28 2011
 * Modified date: Oct 9 2011
 * Description: this class is responsible in instation of the album objects.
 *              this class also provides the mean of accessing the album contents
 *              as well as updating its contents as well
 * 
 *
 ************************************************************************************/
using System;

namespace TheNewPhotoBuddy.BussinessRule
{
    /// <summary>
    /// Author(s): Miguel Gonzales & Andrea Tan
    /// 
    /// Album Object Class this constructor initialize an album object.
    /// this class also contains public functions to set and get the following below:
    /// 1. AlbumID.
    /// 2. CoverPhoto.
    /// 3. a collections of photolists.
    /// </summary>
    public class Album 
    {
        private string _albumID;
        private Photos _photoList;  

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// constructor which initialized the variables albumid, coverphoto, and photolist
        /// precondition: none
        /// postcondition: initialzed objects and variables such as albumid and cover photo
        /// </summary>
        public Album()
        {
            _albumID = "";
            _photoList = new Photos();
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// overloading constructor which initialized the variables albumid, coverphoto, and photolist
        /// precondition: parameter albumName must be passed into this constructor.
        /// postcondition: initialzed objects and variables such as albumid and cover photo
        /// </summary>C:\Documents and Settings\andrea.tan\My Documents\Downloads\TheNewPhotoBuddy-1-0-0-5-Source\TheNewPhotoBuddy\TheNewPhotoBuddy\Controls\
        /// <param name="albumName"></param>
        public Album(string albumName)
        {
            this._albumID = albumName;
            _photoList = new Photos();
        }
       
        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// get and set albumID from instantiated album object.
        /// </summary>
        public String albumID
        {
            get { return _albumID; }
            set { _albumID = value; }
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// get and set photoObjects from instantiated album object.
        /// </summary>
        public Photos photoObjects
        {
            get { return _photoList; }
            set { _photoList = value; }
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
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
