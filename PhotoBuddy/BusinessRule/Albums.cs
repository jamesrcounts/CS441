/***********************************************************************************************
 * Author(s): Miguel Gonzales & Andrea Tan
 * Date: Sept 28 2011
 * Modified date: Oct 9 2011
 * High Level Description: this class is responsible for numerous collections 
 *                         of Album objects. In addition, providing the mean to remove and add 
 *                         specific album that its looking for. 
 *                         as well as to search a specific album that is being requested from 
 *                         the presenter.
 *           
 *
 **********************************************************************************************/

using System;
using System.Collections;

namespace TheNewPhotoBuddy.BussinessRule
{
    public class Albums 
    {
        //private hashtable of album objects declared
        private readonly Hashtable AlbumObjectHashtable;

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// Albums constructor which initialized the hash table to keep
        /// album objects.
        /// </summary>
        public Albums()
        {
            AlbumObjectHashtable = new Hashtable();
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// get method which return the hashtable when it is being requested
        /// precondition: hash table is initialized and not empty.
        /// postcondition: return the collections of album objects from hashtable
        /// </summary>
        public Hashtable albumsList
        {
            get { return AlbumObjectHashtable; }
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// a search function which to return a specific album that is being requested
        /// from a presenter.
        /// 
        /// preCondition : hashtable is not empty, and the key that is being passed is a valid key
        /// postCondition: return the album object from the hashtable.
        /// </summary>
        /// <param name="key">The album id (name).</param>
        /// <returns>the album object.</returns>
        public Album getAlbum(String key)
        {
            return (Album) AlbumObjectHashtable[key];
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// add album method which adding an album objects into a hashtable
        /// preCondition : the album object that is being passed must contain the necessary data without any null variables
        ///                the album object must be a unique key and if the object already existed in the hash table, it will get rejected.
        ///               
        /// postCondition: when album object is sucessfully added to hash table, the function will return a boolean true otherwise
        ///                it will return boolean false.
        /// </summary>
        /// <param name="newAlbum"></param>
        /// <returns></returns>
        public bool addAlbum(Album newAlbum)
        {
            if (!AlbumObjectHashtable.ContainsKey(newAlbum.albumID))
            {
                AlbumObjectHashtable.Add(newAlbum.albumID, newAlbum);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// a delete album method which adding an album objects into a hashtable
        /// preCondition : the album object that is being passed must contain the necessary data without any null variables
        ///                the album object must be a unique key and if the object already existed in the hash table, it will not be processed.
        ///               
        /// postCondition: when album object is sucessfully deletedd from hash table, the function will return a boolean true otherwise
        ///                it will return boolean false.
        /// </summary>
        /// <param name="existingAlbum"></param>
        /// <returns></returns>
        public bool deleteAlbum(Album existingAlbum)
        {
            if (AlbumObjectHashtable.ContainsKey(existingAlbum.albumID))
            {
                AlbumObjectHashtable.Remove(existingAlbum.albumID);
                return true;
            }
            else
            {
                //cannot find this album id probably has been deleted?
                return false;
            }
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// this method is for checking wether the album alraedy exist
        /// in the hash table
        /// preCondition : The input parameter string albumName
        /// postCondition: it returns the true/false boolean wether it exists or not
        /// </summary>
        /// <param name="albumName"></param>
        /// <returns></returns>
        public bool IsExistingAlbumName(string albumName)
        {
            return AlbumObjectHashtable.ContainsKey(albumName);
        }
    }
}
