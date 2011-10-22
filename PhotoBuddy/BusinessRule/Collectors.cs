/****************************************************************************************************
 * Author(s): Miguel Gonzales and Andrea Tan
 * Date: Sept 28 2011
 * Modified date: Oct 9 2011
 * High level Description: this class is responsible for populating the objects from the xmls,
 *                         holding the of overall album and photos objects.
 *                         collectors are the one that's responsible as a global object holders of
 *                         everything in the bussienss rule.
 *              
 *
 *****************************************************************************************************/


using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using TheNewPhotoBuddy.Common.CommonClass;
using TheNewPhotoBuddy.DataAccessLayer;

namespace TheNewPhotoBuddy.BussinessRule
{

    public class Collectors
    {
        readonly XDocument document;

        //private Photos globalPhotos;

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// collectors constructor calls the create secret directory if it has not been made.
        /// collectors also calls the load xml file from the dataLayer to grab the entire node which
        /// are exist in the info xml file.
        /// 
        /// preCondition: none
        /// postCondition: folder and xml will be created if it it has never been created before.
        ///                prepopulate the objects from the xml if the data is already there.
        ///                
        /// </summary>
        public Collectors()
        {
            //make directory
            CheckAndCreatePhotoBuddyDir();

            //initialize xdocument & dataaccessXML
            document = new XDocument();
            DataAccessBaseXML dataAccessXML = new DataAccessBaseXML();
            document = dataAccessXML.loadORinitializeInfoXML(Constants.XMLDataFilePath);

            //initialize all objects
            Albums = new Albums();
            LoadAlbums();
        }


        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// EditAlumName
        /// this function takes in the old name of the album and as well as a new name
        /// then update the current album object that is being looked up and added back
        /// to the collection hashtable album object. finally it also writes it back to a file
        /// to update the data file in the xml as well.
        /// 
        /// preCondition: oldName, and newName are distinct
        /// postCondition: update the album object and added back to hashtable
        ///                write the new object into XMl file.
        /// </summary>
        /// <param name="oldName"></param>
        /// <param name="newName"></param>
        public void EditAlbumName(string oldName, string newName)
        {
            Album tempAlbum = Albums.getAlbum(oldName);
            Albums.albumsList.Remove(oldName);
            tempAlbum.albumID = newName;
            Albums.addAlbum(tempAlbum);
            populateObjectsIntoXML();
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// checkAndCreatePhotoBuddyDirectory is responsible to see check weather
        /// the current startup path of the program directory has our secret folder
        /// created. if it isnt then the folder will be created otherwise do nothing
        /// 
        /// preCondition  : None
        /// postCondition : secret directory will be created if it has not been there
        ///                 otherwise do nothing.
        /// </summary>
        public static void CheckAndCreatePhotoBuddyDir()
        {
            if (!Directory.Exists(Constants.PhotosFolderPath))
            {
                Directory.CreateDirectory(Constants.PhotosFolderPath);
            }
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// getAlbums object provides a method to easily get and update the Albums object.
        /// </summary>
        public Albums Albums { get; set; }


        /// <summary>
        /// Adds the specified album to the repository.
        /// </summary>
        /// <param name="albumToAdd">The album to add.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public void Add(Album albumToAdd)
        {
            if (albumToAdd == null)
            {
                return;
            }

            this.Albums.addAlbum(albumToAdd);
        }

        /// <summary>
        /// Adds or replaces a collection of photos to an album.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="photosToAdd">The photos to add.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public void ReplaceAlbumPhotos(string albumId, Photos photosToAdd)
        {
            if (string.IsNullOrWhiteSpace(albumId) || photosToAdd == null)
            {
                return;
            }

            var album = Albums.getAlbum(albumId);
            album.photoObjects = photosToAdd;
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// this function is resposible to write back all the objects that have been created (albums,photos)
        /// into an xmls.
        /// preCondition: objects are not empty
        /// postCondition: write all the objects into xml file.
        /// </summary>
        public void populateObjectsIntoXML()
        {
            //Create the XmlDocument.
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);
            XmlNode productsNode = doc.CreateElement("photo_buddy");
            doc.AppendChild(productsNode);
            XmlNode albumsNode = doc.CreateElement("albums");
            productsNode.AppendChild(albumsNode);

            foreach (Album albumObj in Albums.albumsList.Values)
            {
                XmlNode albumNode = doc.CreateElement("album");
                XmlAttribute albumIDAttr = albumNode.OwnerDocument.CreateAttribute("id_tag");
                string id = albumObj.albumID;
               //// var tempXElement = new System.Xml.Linq.XElement("Temp", id);
                albumIDAttr.Value = id;
                albumNode.Attributes.Append(albumIDAttr);

                XmlNode photosNode = doc.CreateElement("photos");

                if (albumObj.photoObjects != null)
                {
                    foreach (Photo photoObj in albumObj.photoObjects.photoList.Values)
                    {
                        XmlNode photoNode = doc.CreateElement("photo");

                        XmlAttribute photoIDAttr = photoNode.OwnerDocument.CreateAttribute("id_tag");
                        photoIDAttr.Value = photoObj.PhotoId;
                        photoNode.Attributes.Append(photoIDAttr);

                        XmlElement photoCopiedPathNode = doc.CreateElement("copied_path");
                        photoCopiedPathNode.InnerText = photoObj.copiedPath;

                        XmlElement photoDisplayNameELem = doc.CreateElement("display_name");
                        photoDisplayNameELem.InnerText = photoObj.display_name;

                        photosNode.AppendChild(photoNode);
                        photoNode.AppendChild(photoCopiedPathNode);
                        photoNode.AppendChild(photoDisplayNameELem);
                    }
                }
                albumNode.AppendChild(photosNode);
                albumsNode.AppendChild(albumNode);
            }
            doc.Save(Constants.XMLDataFilePath);
        }

        /// <summary>
        /// Loads the albums.
        /// </summary>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public void LoadAlbums()
        {
            // look through album node.
            var albumNodes = from node in document.Descendants("album")
                             select new
                             {
                                 id_tag = node.Attribute("id_tag").Value,
                                 photos = node.Element("photos")
                             };

            foreach (var albumInfo in albumNodes)
            {
                var album = new Album() { albumID = albumInfo.id_tag };

                //traverse through all the photos in particular album
                var photoList = new Photos();
                foreach (var photoInfo in albumInfo.photos.Descendants("photo"))
                {
                    //photo
                    var tempPhoto = new Photo()
                                        {
                                            PhotoId = photoInfo.Attribute("id_tag").Value,
                                            display_name = photoInfo.Element("display_name").Value,
                                            copiedPath = photoInfo.Element("copied_path").Value
                                        };

                    photoList.addPhotosAt(tempPhoto);
                }

                album.photoObjects = photoList;
                Albums.addAlbum(album);
            }
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// addPhototoAlbum functions does the following:
        /// 1. generate unique hashkey to avoid adding the same picture over and over again
        /// 2. copy over the the picture and use the hashkey as its filename.
        /// 3. populate the photos objects and add it into particular album.
        /// 4. update album into the hashtable.
        /// 
        /// preCondition: key,photoname,photofile name must not be empty.
        /// 
        /// postCondiction: info xml file gets updated and the objects as well.
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="photoName"></param>
        /// <param name="photoFilename"></param>
        public void AddPhotoToAlbum(string key, string photoName, string photoFilename)
        {
            Photo tempPhoto = new Photo()
            {
                PhotoId = Photo.GenerateUniqueHashPhotoKey(photoFilename),
                display_name = photoName,
                copiedPath = photoFilename
            };

            // Combines two paths without having to worry about whether path1 ends with a '\' character
            string path = Path.Combine(Constants.PhotosFolderPath, tempPhoto.PhotoId);

            // Changes or adds the original file extension to the new path
            string fileExtension = Path.GetExtension(photoFilename);
            path = Path.ChangeExtension(path, fileExtension);

            // Copies the file to the secret location.
            Photo.CopyOverThefileToSecretDir(@tempPhoto.copiedPath, path);

            tempPhoto.copiedPath = tempPhoto.PhotoId + fileExtension;
            //query for album from album list
            Album currentAlbum = new Album();
            currentAlbum = Albums.getAlbum(key);

            currentAlbum.photoObjects.addPhotosAt(tempPhoto);
            populateObjectsIntoXML();
        }
    }
}
