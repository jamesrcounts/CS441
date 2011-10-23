//-----------------------------------------------------------------------
// <copyright file="AlbumRespository.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales and Andrea Tan
// Date: Sept 28 2011
// Modified date: Oct 9 2011
// High level Description: this class is responsible for populating the objects from the xmls,
//                         holding the of overall album and photos objects.
//                         collectors are the one that's responsible as a global object holders of
//                         everything in the bussienss rule.
//-----------------------------------------------------------------------
namespace PhotoBuddy.BussinessRule
{
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using PhotoBuddy.Common.CommonClass;
    using PhotoBuddy.DataAccessLayer;

    /// <summary>
    /// Handles album and photo data storage and retrieval.
    /// </summary>
    public class AlbumRespository
    {
        /// <summary>
        /// Provides API access to the data storage XML.
        /// </summary>
        private readonly XDocument document;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumRespository"/> class.
        /// </summary>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public AlbumRespository()
        {
            AllocatePhotoStorage();

            this.document = new XDocument();
            DataAccessBaseXML dataAccessXML = new DataAccessBaseXML();
            this.document = dataAccessXML.loadORinitializeInfoXML(Constants.XMLDataFilePath);

            Albums = new Albums();
            this.LoadAlbums();
        }
        
        /// <summary>
        /// Gets or sets the albums.
        /// </summary>
        /// <value>
        /// The albums.
        /// </value>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public Albums Albums { get; set; }

        /// <summary>
        /// Allocates the photo storage folder.
        /// </summary>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public static void AllocatePhotoStorage()
        {
            if (Directory.Exists(Constants.PhotosFolderPath))
            {
                return;
            }

            Directory.CreateDirectory(Constants.PhotosFolderPath);
        }

        /// <summary>
        /// Edits the name of the album.
        /// </summary>
        /// <param name="name">The old name.</param>
        /// <param name="updateName">The new name.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public void RenameAlbum(string name, string updateName)
        {
            Album tempAlbum = Albums.getAlbum(name);
            Albums.albumsList.Remove(name);
            tempAlbum.albumID = updateName;
            Albums.addAlbum(tempAlbum);
            this.SaveAlbums();
        }

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
        /// Saves changes to the albums since the last time SaveAlbums was called.
        /// </summary>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public void SaveAlbums()
        {
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
                albumIDAttr.Value = id;
                albumNode.Attributes.Append(albumIDAttr);

                XmlNode photosNode = doc.CreateElement("photos");

                if (albumObj.photoObjects != null)
                {
                    foreach (Photo photoObj in albumObj.photoObjects.PhotoTable.Values)
                    {
                        XmlNode photoNode = doc.CreateElement("photo");

                        XmlAttribute photoIDAttr = photoNode.OwnerDocument.CreateAttribute("id_tag");
                        photoIDAttr.Value = photoObj.PhotoId;
                        photoNode.Attributes.Append(photoIDAttr);

                        XmlElement photoCopiedPathNode = doc.CreateElement("copied_path");
                        photoCopiedPathNode.InnerText = photoObj.CopiedPath;

                        XmlElement photoDisplayNameELem = doc.CreateElement("display_name");
                        photoDisplayNameELem.InnerText = photoObj.DisplayName;

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
            var albumNodes = from node in this.document.Descendants("album")
                             select new
                             {
                                 id_tag = node.Attribute("id_tag").Value,
                                 photos = node.Element("photos")
                             };

            foreach (var albumInfo in albumNodes)
            {
                var album = new Album() { albumID = albumInfo.id_tag };

                // traverse through all the photos in particular album
                var photoList = new Photos();
                foreach (var photoInfo in albumInfo.photos.Descendants("photo"))
                {
                    // photo
                    var tempPhoto = new Photo()
                                        {
                                            PhotoId = photoInfo.Attribute("id_tag").Value,
                                            DisplayName = photoInfo.Element("display_name").Value,
                                            CopiedPath = photoInfo.Element("copied_path").Value
                                        };

                    photoList.Add(tempPhoto);
                }

                album.photoObjects = photoList;
                Albums.addAlbum(album);
            }
        }

        /// <summary>
        /// Adds the photo to an album.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="photoFilename">The photo filename.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public void AddPhotoToAlbum(string albumId, string displayName, string photoFilename)
        {
            Photo tempPhoto = new Photo()
            {
                PhotoId = Photo.GeneratePhotoKey(photoFilename),
                DisplayName = displayName,
                CopiedPath = photoFilename
            };

            // Combines two paths without having to worry about whether path1 ends with a '\' character
            string path = Path.Combine(Constants.PhotosFolderPath, tempPhoto.PhotoId);

            // Changes or adds the original file extension to the new path
            string fileExtension = Path.GetExtension(photoFilename);
            path = Path.ChangeExtension(path, fileExtension);

            // Copies the file to the secret location.
            Photo.StoreFile(@tempPhoto.CopiedPath, path);

            tempPhoto.CopiedPath = tempPhoto.PhotoId + fileExtension;
            
            // query for album from album list
            Album currentAlbum = Albums.getAlbum(albumId);

            currentAlbum.photoObjects.Add(tempPhoto);
            this.SaveAlbums();
        }
    }
}