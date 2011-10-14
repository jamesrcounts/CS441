/***********************************************************************************
 * Author(s): Miguel Gonzales & Andrea Tan
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

namespace TheNewPhotoBuddy.BussinessRule
{
    public class Photos
    {
        // private hash of photos
        private Hashtable PhotosObjectHashtable;

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// photos constructor which initialized the hash table
        /// </summary>
        public Photos()
        {
            PhotosObjectHashtable = new Hashtable();
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// return the hash when is requested.
        /// </summary>
        public Hashtable photoList
        {
            get { return PhotosObjectHashtable; }
        }
       
        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
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
            if (!PhotosObjectHashtable.ContainsKey(tempPhoto.ID))
            {
                PhotosObjectHashtable.Add(tempPhoto.ID, tempPhoto);
                return true;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// removePhoto from hashtable
        /// preCondition: photo object and string of the real path of where the picture resides.
        /// postCondition: photo object gets deleted off hashtable and also deleted from the
        ///                secret directory.
        /// </summary>
        /// <param name="tempPhoto"></param>
        /// <param name="removeFileatDir"></param>
        /// <returns></returns>
        public bool removePhotosAt(Photo tempPhoto, String removeFileatDir)
        {
            if (PhotosObjectHashtable.ContainsKey(tempPhoto.ID))
            {

                // Delete a file by using File class static method...
                if (System.IO.File.Exists(@removeFileatDir))
                {
                    // Use a try block to catch IOExceptions, to
                    // handle the case of the file already being
                    // opened by another process.
                    try
                    {
                        System.IO.File.Delete(@removeFileatDir);
                    }
                    catch            
                    {
                    }
                }

                PhotosObjectHashtable.Remove(tempPhoto.ID);
                return true;
            }
            else
            {
                //cannot find this album id probably has been deleted?
                return false;
            }
        }
    }
}
