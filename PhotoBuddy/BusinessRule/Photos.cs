/***********************************************************************************
 * Author(s): Miguel Gonzales and Andrea Tan
 * Date: Sept 28 2011
 * Modified date: Oct 9 2011
 * Description: this class is responsible for collections of photo object. 
 *              Moreover it provides a functionality of adding a photo into hash,
 *              removing the photo from hash,  and last but not least is to return the
 *              collections of the photos when requested.
 * 
 *
 ************************************************************************************/

using System;
using System.Collections;
using System.IO;

namespace TheNewPhotoBuddy.BussinessRule
{
    public class Photos
    {
        // private hash of photos
        private readonly Hashtable PhotosObjectHashtable;

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// photos constructor which initialized the hash table
        /// </summary>
        public Photos()
        {
            PhotosObjectHashtable = new Hashtable();
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// return the hash when is requested.
        /// </summary>
        public Hashtable photoList
        {
            get { return PhotosObjectHashtable; }
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// add picture functionality, which takes a photo object
        /// and add it into the collection (hashtable)
        /// 
        /// preCondition: photo object as parameter and not empty
        /// postCondition: return a boolean true/false when photo gets added successfully/fail
        /// into hashtable.
        /// </summary>
        /// <param name="tempPhoto"></param>
        /// <returns></returns>
        public bool addPhotosAt(Photo tempPhoto)
        {
            if (!PhotosObjectHashtable.ContainsKey(tempPhoto.PhotoId))
            {
                PhotosObjectHashtable.Add(tempPhoto.PhotoId, tempPhoto);
                return true;
            }
            else
            {
                return false;
            }
        }

        /////// <remarks>
        /////// Author(s): Miguel Gonzales and Andrea Tan
        /////// 
        /////// removePhoto from hashtable
        /////// preCondition: photo object and string of the real path of where the picture resides.
        /////// postCondition: photo object gets deleted off hashtable and also deleted from the
        ///////                secret directory.
        /////// </remarks>
        /////// <param name="tempPhoto"></param>
        /////// <param name="removeFileatDir"></param>
        /////// <returns></returns>
        ////public bool removePhotosAt(Photo tempPhoto, String removeFileatDir)
        ////{
        ////    if (!PhotosObjectHashtable.ContainsKey(tempPhoto.PhotoId))
        ////    {
        ////        // cannot find this album id probably has been deleted?
        ////        return false;
        ////    }

        ////    // Delete a file by using File class static method...
        ////    if (File.Exists(@removeFileatDir))
        ////    {
        ////        // Use a try block to catch IOExceptions, to
        ////        // handle the case of the file already being
        ////        // opened by another process.
        ////        File.Delete(@removeFileatDir);
        ////    }

        ////    PhotosObjectHashtable.Remove(tempPhoto.PhotoId);
        ////    return true;
        ////}
    }
}
