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
//                         everything in the business rule.
//-----------------------------------------------------------------------
namespace PhotoBuddy.BusinessRule
{
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
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
            this.document = dataAccessXML.LoadXml(Constants.XmlDataFilePath);

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
        /// Deletes the specified album.
        /// </summary>
        /// <param name="albumName">Name of the album.</param>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-27</para>
        /// </remarks>
        public void DeleteAlbum(string albumName)
        {
            Albums.AlbumList.Remove(albumName);
            this.SaveAlbums();
        }

        /// <summary>
        /// Deletes the photo.
        /// </summary>
        /// <param name="enclosingAlbum">The current album.</param>
        /// <param name="photoToDelete">The photo to delete.</param>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-27</para>
        /// </remarks>
        public void DeletePhoto(Album enclosingAlbum, Photo photoToDelete)
        {
            Album album = Albums.GetAlbum(enclosingAlbum.AlbumId);
            album.RemovePhoto(photoToDelete.PhotoId);
            this.SaveAlbums();
        }
        
        /// <summary>
        /// Edits the name of the album.
        /// </summary>`
        /// <param name="name">The old name.</param>
        /// <param name="updateName">The new name.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public void RenameAlbum(string name, string updateName)
        {
            Album tempAlbum = Albums.GetAlbum(name);
            Albums.AlbumList.Remove(name);
            tempAlbum.AlbumId = updateName;
            Albums.AddAlbum(tempAlbum);
            this.SaveAlbums();
        }

        /// <summary>
        /// Rename the photo in the album.
        /// </summary>
        /// <param name="albumName">The album ID</param>
        /// <param name="newName">The new display name for the photo</param>
        /// <param name="photoId">The photo ID</param>
        public void RenamePhotoInAlbum(string albumName, string newName, string photoId)
        {
            Album tempAlbum = Albums.GetAlbum(albumName);
            Albums.AlbumList.Remove(albumName);
            Photo tempPhoto = tempAlbum.GetPhoto(photoId);
            tempAlbum.RemovePhoto(photoId);
            tempPhoto.DisplayName = newName;
            tempAlbum.AddPhoto(tempPhoto);
            Albums.AddAlbum(tempAlbum);
            this.SaveAlbums();
        }

        /// <summary>
        /// Adds an album with the specified album name to the repository, then returns the new album to the caller.
        /// </summary>
        /// <param name="albumName">Name of the album.</param>
        /// <returns>The newly created album.</returns>
        /// <remarks>
        ///   <para>Author(s): Miguel Gonzales, Andrea Tan, Jim Counts</para>
        ///   <para>Modified: 2011-10-26</para>
        /// </remarks>
        public Album AddAlbum(string albumName)
        {
            if (string.IsNullOrWhiteSpace(albumName) || Constants.MaxNameLength < albumName.Length)
            {
                throw new System.ArgumentException("albumName", "albumName is invalid.");
            }

            var album = new Album(this, albumName);
            this.Albums.AddAlbum(album);
            return album;
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

            var album = Albums.GetAlbum(albumId);
            album.ReplacePhotos(photosToAdd);
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

            foreach (Album albumObj in Albums.AlbumList.Values)
            {
                XmlNode albumNode = doc.CreateElement("album");
                XmlAttribute albumIDAttr = albumNode.OwnerDocument.CreateAttribute("id_tag");
                string id = albumObj.AlbumId;
                albumIDAttr.Value = id;
                albumNode.Attributes.Append(albumIDAttr);

                XmlNode photosNode = doc.CreateElement("photos");

                ////if (albumObj.PhotoList != null)
                ////{
                foreach (Photo photoObj in albumObj.Photos)
                {
                    XmlNode photoNode = doc.CreateElement("photo");

                    XmlAttribute photoIDAttr = photoNode.OwnerDocument.CreateAttribute("id_tag");
                    photoIDAttr.Value = photoObj.PhotoId;
                    photoNode.Attributes.Append(photoIDAttr);

                    XmlElement photoCopiedPathNode = doc.CreateElement("copied_path");
                    photoCopiedPathNode.InnerText = photoObj.FileName;

                    XmlElement photoDisplayNameELem = doc.CreateElement("display_name");
                    photoDisplayNameELem.InnerText = photoObj.DisplayName;

                    photosNode.AppendChild(photoNode);
                    photoNode.AppendChild(photoCopiedPathNode);
                    photoNode.AppendChild(photoDisplayNameELem);
                }
                ////}

                albumNode.AppendChild(photosNode);
                albumsNode.AppendChild(albumNode);
            }

            doc.Save(Constants.XmlDataFilePath);
        }

        /// <summary>
        /// Gets the specified album.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <returns>
        /// An album.
        /// </returns>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-26</para>
        /// </remarks>
        public Album GetAlbum(string albumId)
        {
            return this.Albums.GetAlbum(albumId);
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
                var album = new Album(this, albumInfo.id_tag);

                // traverse through all the photos in particular album
                var photoList = new Photos();
                foreach (var photoInfo in albumInfo.photos.Descendants("photo"))
                {
                    // photo
                    var photo = new Photo(
                        photoInfo.Attribute("id_tag").Value,
                        photoInfo.Element("display_name").Value,
                        photoInfo.Element("copied_path").Value);

                    photoList.Add(photo);
                }

                album.ReplacePhotos(photoList);
                Albums.AddAlbum(album);
            }
        }

        /// <summary>
        /// Adds the photo to an album.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="photoFilename">The photo filename.</param>
        /// <remarks>
        ///   <para>Author(s): Miguel Gonzales, Andrea Tan, Jim Counts</para>
        ///   <para>Modified: 2011-10-27</para>
        /// </remarks>
        public void AddPhotoToAlbum(string albumId, string displayName, string photoFilename)
        {
            // Copies the file to the secret location.
            string photoId = AlbumRespository.GeneratePhotoKey(photoFilename);
            string storagePath = AlbumRespository.StoreFile(photoFilename, photoId);
            string storageName = Path.GetFileName(storagePath);

            // Put the photo in the album data structure.
            Album currentAlbum = Albums.GetAlbum(albumId);
            Photo photo = new Photo(photoId, displayName, storageName);
            ////currentAlbum.PhotoList.Add(photo);
            currentAlbum.AddPhoto(photo);
            this.SaveAlbums();
        }

        /// <summary>
        /// Stores the file.
        /// </summary>
        /// <param name="sourcePath">The source path.</param>
        /// <param name="photoId">The photo id.</param>
        /// <returns>The path to the file's location in storage.</returns>
        /// <remarks>
        /// Author(s): Miguel Gonzales, Andrea Tan, Jim Counts, Eric Wei
        /// </remarks>
        private static string StoreFile(string sourcePath, string photoId)
        {
            // Combines two paths without having to worry about whether path1 ends with a '\' character
            string destinationPath = Path.Combine(Constants.PhotosFolderPath, photoId);

            // Changes or adds the original file extension to the new path
            string fileExtension = Path.GetExtension(sourcePath);
            destinationPath = Path.ChangeExtension(destinationPath, fileExtension);
            if (!File.Exists(destinationPath))
            {
                File.Copy(sourceFileName: sourcePath, destFileName: destinationPath, overwrite: true);
            }

            return destinationPath;
        }

        /// <summary>
        /// Generates the photo key for the specified file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>A unique string which identifies the file by its contents.</returns>
        /// <remarks>
        /// Author(s): Miguel Gonzales, Andrea Tan, Jim Counts, Eric Wei
        /// </remarks>
        private static string GeneratePhotoKey(string filePath)
        {
            // Reading the bytes of the actual file contents
            byte[] source = File.ReadAllBytes(filePath);

            // Computing the SHA 256 Hash value
            using (SHA256Managed hashAlgorithm = new SHA256Managed())
            {
                byte[] hash = hashAlgorithm.ComputeHash(source);

                // Convert to HEX encoded string
                StringBuilder hashString = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    hashString.Append(hash[i].ToString("X2"));
                }

                return hashString.ToString();
            }
        }
    }
}