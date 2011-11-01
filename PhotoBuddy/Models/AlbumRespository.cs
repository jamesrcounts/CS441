//-----------------------------------------------------------------------
// <copyright file="AlbumRespository.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales, Andrea Tan, Jim Counts
// Date: Sept 28 2011
// Modified date: Oct 31 2011
// High level Description: this class is responsible for populating the objects from the XMLs,
//                         holding the of overall album and photos objects.
//                         collectors are the one that's responsible as a global object holders of
//                         everything in the business rule.
//-----------------------------------------------------------------------
namespace PhotoBuddy.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Xml.Linq;
    using PhotoBuddy.Common;

    /// <summary>
    /// Manages create, read, update and delete functions a collection of albums.
    /// </summary>
    public class AlbumRespository
    {
        /// <summary>
        /// Provides API access to the data storage XML.
        /// </summary>
        private readonly XDocument document = new XDocument();

        /// <summary>
        /// Backing store for indexed album list.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-28</para>
        /// </remarks>
        private readonly IDictionary<string, Album> albums = new Dictionary<string, Album>();

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumRespository"/> class.
        /// </summary>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public AlbumRespository()
        {
            if (!Directory.Exists(Constants.PhotosFolderPath))
            {
                Directory.CreateDirectory(Constants.PhotosFolderPath);
            }

            if (File.Exists(Constants.XmlDataFilePath))
            {
                this.document = XDocument.Load(Constants.XmlDataFilePath);
            }
            else
            {
                this.document = new XDocument(new XElement("photo_buddy", new XElement("albums")));
                this.document.Save(Constants.XmlDataFilePath);
            }

            this.LoadAlbums();
        }

        /// <summary>
        /// Gets the collection of albums.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-28</para>
        /// </remarks>
        public IEnumerable<Album> Albums
        {
            get
            {
                if (this.albums == null)
                {
                    return Enumerable.Empty<Album>();
                }

                return this.albums.Values;
            }
        }

        /// <summary>
        /// Gets the album count.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-28</para>
        /// </remarks>
        public int Count
        {
            get
            {
                return this.albums.Count;
            }
        }

        /// <summary>
        /// Deletes the specified album form the index.
        /// </summary>
        /// <param name="albumName">Name of the album.</param>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-27</para>
        /// </remarks>
        public void DeleteAlbum(string albumName)
        {
            if (this.albums.Remove(albumName))
            {
                this.SaveAlbums();
            }
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
            Album album = null;
            if (this.albums.TryGetValue(enclosingAlbum.AlbumId, out album))
            {
                album.RemovePhoto(photoToDelete.PhotoId);
                this.SaveAlbums();
            }
        }

        /// <summary>
        /// Edits the name of the album.
        /// </summary>`
        /// <param name="name">The old name.</param>
        /// <param name="updateName">The new name.</param>
        /// <remarks>
        /// <para>Author(s): Miguel Gonzales, Andrea Tan, Jim Counts</para>
        /// <para>Modified: 2011-10-28</para>
        /// </remarks>
        public void RenameAlbum(string name, string updateName)
        {
            Album album = this.DetatchAlbum(name);
            if (album == null)
            {
                return;
            }

            album.AlbumId = updateName;
            this.albums.Add(album.AlbumId, album);
            this.SaveAlbums();
        }

        /// <summary>
        /// Rename the photo in the album.
        /// </summary>
        /// <param name="albumName">The album ID</param>
        /// <param name="displayName">The new display name for the photo</param>
        /// <param name="photoId">The photo ID</param>
        public void RenamePhotoInAlbum(string albumName, string displayName, string photoId)
        {
            Album album = this.GetAlbum(albumName);
            if (album == null)
            {
                return;
            }

            Photo photo = album.GetPhoto(photoId);
            if (photo == null)
            {
                return;
            }

            ////       album.RemovePhoto(photoId);
            photo.DisplayName = displayName;
            ////   album.AddPhoto(photo);
            ////  this.albums.Add(album.AlbumId, album);
            this.SaveAlbums();
        }

        /// <summary>
        /// Adds an album with the specified album name to the repository, then returns the new album to the caller.
        /// </summary>
        /// <param name="albumName">Name of the album.</param>
        /// <returns>The newly created album.</returns>
        /// <remarks>
        ///   <para>Author(s): Miguel Gonzales, Andrea Tan, Jim Counts</para>
        ///   <para>Modified: 2011-10-28</para>
        /// </remarks>
        public Album AddAlbum(string albumName)
        {
            if (string.IsNullOrWhiteSpace(albumName) || Constants.MaxNameLength < albumName.Length)
            {
                throw new ArgumentException("albumName", "albumName is invalid.");
            }

            if (!this.albums.ContainsKey(albumName))
            {
                // Update XML
                XElement albumElement = Album.CreateAlbumElement(albumName);
                this.document.Descendants("albums").First().Add(albumElement);
                var album = new Album(this, albumElement);

                // Update Album Index
                this.albums.Add(album.AlbumId, album);
                return album;
            }
            else
            {
                throw new ArgumentException("albumName", "Album name already used in the repository.");
            }
        }

        /// <summary>
        /// Saves changes to the albums since the last time SaveAlbums was called.
        /// </summary>
        /// <remarks>
        /// <para>Author(s): Miguel Gonzales, Andrea Tan, Jim Counts</para>
        /// <para>Modified: 2011-10-29</para>
        /// </remarks>
        public void SaveAlbums()
        {
            this.document.Save(Constants.XmlDataFilePath);
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
            Album album = null;
            if (this.albums.TryGetValue(albumId, out album))
            {
                return album;
            }

            return null;
        }

        /// <summary>
        /// Determines whether the specified album name is already used.
        /// </summary>
        /// <param name="albumName">Name of the album.</param>
        /// <returns>
        ///   <c>true</c> if the specified album name is already used; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public bool IsExistingAlbumName(string albumName)
        {
            return this.albums.ContainsKey(albumName);
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
            var albumNodes = this.document.Descendants("album");
            ////select new
            ////{
            ////    id_tag = node.Attribute("id_tag").Value,
            ////    photos = node.Element("photos")
            ////};

            foreach (var albumInfo in albumNodes)
            {
                var album = new Album(this, albumInfo);

                // traverse through all the photos in particular album
                ////var photoList = new Photos();
                ////foreach (var photoInfo in albumInfo.Descendants("photo"))
                ////{
                ////    // photo
                ////    var photo = new Photo(
                ////        photoInfo.Attribute("id_tag").Value,
                ////        photoInfo.Element("display_name").Value,
                ////        photoInfo.Element("copied_path").Value);

                ////    ////photoList.Add(photo);
                ////    album.AddPhoto(photo);
                ////}

                ////album.ReplacePhotos(photoList);
                this.albums.Add(album.AlbumId, album);
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
            Album currentAlbum = this.GetAlbum(albumId);
            ////Photo photo = new Photo(photoId, displayName, storageName);
            ////currentAlbum.PhotoList.Add(photo);
            currentAlbum.AddPhoto(photoId, displayName, storageName);
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

        /// <summary>
        /// Removes the album with the specified name from the album collection and returns the detached album to the caller.
        /// </summary>
        /// <param name="name">The album name.</param>
        /// <returns>If an album exists with the specified name it is returned; otherwise null.</returns>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-28</para>
        /// </remarks>
        private Album DetatchAlbum(string name)
        {
            var album = this.GetAlbum(name);
            if (album == null)
            {
                return null;
            }

            this.albums.Remove(name);
            return album;
        }
    }
}