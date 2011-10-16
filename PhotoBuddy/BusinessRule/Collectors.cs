/****************************************************************************************************
 * Author(s): Miguel Gonzales & Andrea Tan
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
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using TheNewPhotoBuddy.DataAccessLayer;
using System.IO;
using TheNewPhotoBuddy.Common.CommonClass;

namespace TheNewPhotoBuddy.BussinessRule
{

    public class Collectors
    {
        XDocument document;
        
        //private Photos globalPhotos;
        private Albums globalAlbums;


        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
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
            checkAndCreatePhotoBuddyDir();

            //initialize xdocument & dataaccessXML
            document = new XDocument();
            DataAccessBaseXML dataAccessXML = new DataAccessBaseXML();
            document = dataAccessXML.loadORinitializeInfoXML(Constants.XMLDataFilePath);

            //initialize all objects
            globalAlbums = new Albums();
            populateAlbumsAnditsPhotos();
        }


        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
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
            Album tempAlbum = globalAlbums.getAlbum(oldName);
            globalAlbums.albumsList.Remove(oldName);
            tempAlbum.albumID = newName;
            globalAlbums.addAlbum(tempAlbum);
            populateObjectsIntoXML();
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// checkAndCreatePhotoBuddyDirectory is responsible to see check weather
        /// the current startup path of the program directory has our secret folder
        /// created. if it isnt then the folder will be created otherwise do nothing
        /// 
        /// preCondition  : None
        /// postCondition : secret directory will be created if it has not been there
        ///                 otherwise do nothing.
        /// </summary>
        public void checkAndCreatePhotoBuddyDir()
        {
            if (!Directory.Exists(Constants.PhotosFolderPath))
            {
                Directory.CreateDirectory(Constants.PhotosFolderPath);
            }
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// getAlbums object provides a method to easily get and update the Albums object.
        /// </summary>
        public Albums getAlbums
        {
            get { return globalAlbums; }
            set { globalAlbums = value; }
        }


        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// this funcion provides the mean to add desired album object into
        /// hash table
        /// 
        /// Percondition: album object is not empty
        /// PostCondition: album object will be added into hashtable.
        /// </summary>
        /// <param name="tempAlbum"></param>
        public void addAlbumtoAlbumList(Album tempAlbum)
        {
            globalAlbums.addAlbum(tempAlbum);
        }


        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// add or edit photos in a certain album 
        /// gives a functionality to update a photos in particular album
        /// 
        /// preCondition : photos object and desired key that are being passed in are not empty
        /// postCondition: update the photos which was fetched from the hashtable of albums 
        /// </summary>
        /// <param name="tempPhotos"></param>
        /// <param name="key"></param>
        public void editOrAddPhotosToAlbum(Photos tempPhotos, String key)
        {
            //query for album from albumlist
            Album currentAlbum = new Album();
            currentAlbum = globalAlbums.getAlbum(key);

            currentAlbum.photoObjects = tempPhotos;
        }


        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
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

            foreach (Album albumObj in globalAlbums.albumsList.Values)
            {
                XmlNode albumNode = doc.CreateElement("album");
                XmlAttribute albumIDAttr = albumNode.OwnerDocument.CreateAttribute("id_tag");
                albumIDAttr.Value = albumObj.albumID;
                albumNode.Attributes.Append(albumIDAttr);
                
                XmlNode photosNode = doc.CreateElement("photos");

                if (albumObj.photoObjects != null)
                {
                    foreach (Photo photoObj in albumObj.photoObjects.photoList.Values)
                    {
                        XmlNode photoNode = doc.CreateElement("photo");

                        XmlAttribute photoIDAttr = photoNode.OwnerDocument.CreateAttribute("id_tag");
                        photoIDAttr.Value = photoObj.ID;
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
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// populate album and photos object when xml file is loaded.
        /// this is done by using a linq xml statements.
        /// 
        /// preCondition: None
        /// postCondition: hashtable will be full of objects that are being read from the xml file.
        /// </summary>
        public void populateAlbumsAnditsPhotos()
        {
            // look through album node.
            var albumNodes = from node in document.Descendants("album")
                                  select new 
                                  {
                                      id_tag = node.Attribute("id_tag").Value,
                                      photos = node.Element("photos")
                                  };

            // check if the albumNode is not empty.
            if (albumNodes.Count() == 0)
                return;

            foreach (var albumInfo in albumNodes)
            {
                Album tempAlbum = new Album();
                Photos localPhotoList = new Photos();

                tempAlbum.albumID = albumInfo.id_tag;

                // try and catch exception is made for album who has been created but does not have photos nodes in it.
                try
                {
                    //traverse through all the photos in particular album
                    foreach (XElement photoInfo in albumInfo.photos.Descendants("photo"))
                    {
                        //photo
                        Photo tempPhoto = new Photo();
                        tempPhoto.ID = photoInfo.Attribute("id_tag").Value;
                        tempPhoto.display_name = photoInfo.Element("display_name").Value;
                        tempPhoto.copiedPath = photoInfo.Element("copied_path").Value;

                        localPhotoList.addPhotosAt(tempPhoto);
                    }
                    tempAlbum.photoObjects = localPhotoList;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                    Photo tempPhoto = new Photo();
                    localPhotoList.addPhotosAt(tempPhoto);
                }
                globalAlbums.addAlbum(tempAlbum);
            }

        }//end function

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
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
            Photo tempPhoto = new Photo();
            tempPhoto.ID = tempPhoto.generateUniqueHashPhotoKey(photoFilename);

            tempPhoto.display_name = photoName;
            tempPhoto.copiedPath = photoFilename;
            string fileExtension = Path.GetExtension(photoFilename);
            tempPhoto.copyOverThefileToSecretDir(@tempPhoto.copiedPath, @Constants.PhotosFolderPath + tempPhoto.ID + fileExtension);

            tempPhoto.copiedPath = tempPhoto.ID + fileExtension;   
            //query for album from albumlist
            Album currentAlbum = new Album();
            currentAlbum = globalAlbums.getAlbum(key);
            
            currentAlbum.photoObjects.addPhotosAt(tempPhoto);
            populateObjectsIntoXML();
        }
    }
}
