//-----------------------------------------------------------------------
// <copyright file="AlbumRepository.cs" company="Gold Rush">
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
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Xml.Linq;
    using PhotoBuddy.Common;

    /// <summary>
    /// Manages create, read, update and delete functions a collection of albums.
    /// </summary>
    public class AlbumRepository
    {
        ////private const string AlbumsTag = "albums";

        /// <summary>
        /// string literal: photo_buddy
        /// </summary>
        private const string PhotoBuddyTag = "photo_buddy";

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
        private readonly IDictionary<string, IAlbum> albums = new Dictionary<string, IAlbum>();

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumRepository"/> class.
        /// </summary>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public AlbumRepository()
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
                this.document = new XDocument(new XElement(PhotoBuddyTag));
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
        public IEnumerable<IAlbum> Albums
        {
            get
            {
                if (this.albums == null)
                {
                    return Enumerable.Empty<IAlbum>();
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
        public void DeletePhoto(IAlbum enclosingAlbum, IPhoto photoToDelete)
        {
            IAlbum album = null;
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
            IAlbum album = this.DetachAlbum(name);
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
            IAlbum album = this.GetAlbum(albumName);
            if (album == null)
            {
                return;
            }

            IPhoto photo = album.GetPhoto(photoId);
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
        public IAlbum AddAlbum(string albumName)
        {
            if (string.IsNullOrWhiteSpace(albumName) || Constants.MaxNameLength < albumName.Length)
            {
                throw new ArgumentException("albumName is invalid.", "albumName");
            }

            if (!this.albums.ContainsKey(albumName))
            {
                // Update XML
                XElement albumElement = XmlAlbum.CreateAlbumElement(albumName);
                this.document.Descendants().First().Add(albumElement);
                var album = new XmlAlbum(this, albumElement);

                // Update Album Index
                this.albums.Add(album.AlbumId, album);
                return album;
            }
            else
            {
                throw new ArgumentException("Album name already used in the repository.", "albumName");
            }
        }

        /// <summary>
        /// Collects the garbage.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <param name="filePath">The file path.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-11-02</para>
        /// </remarks>
        public void CollectGarbage(string photoId, string filePath)
        {
            if (!this.ContainsPhoto(photoId))
            {
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// Determines whether any photos with the specified photo id exist in the repository.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns>
        ///   <c>true</c> if the specified photo id exists in the repository; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-11-02</para>
        /// </remarks>
        public bool ContainsPhoto(string photoId)
        {
            return this.document
                .Descendants("photo")
                .Attributes("id_tag")
                .Any(attribute => attribute.Value == photoId);
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
        /// Searches for photos that match the specified terms.
        /// </summary>
        /// <param name="terms">The terms.</param>
        /// <returns>A detached album containing the photos.</returns>
        public IAlbum Search(params string[] terms)
        {
            var matches = from term in terms
                          from album in this.Albums
                          from photo in album.Photos
                          where photo.DisplayName.Contains(term)
                          select photo;

            var searchResults = new SearchResultAlbum(this, "Search Results", matches.Distinct());
            return searchResults;
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
        public IAlbum GetAlbum(string albumId)
        {
            IAlbum album = null;
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
            var albumNodes = this.document.Descendants("album");
            foreach (var albumInfo in albumNodes)
            {
                var album = new XmlAlbum(this, albumInfo);
                this.albums.Add(album.AlbumId, album);
            }
        }

        /// <summary>
        /// Adds the photo to an album.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="photoFileName">The photo filename.</param>
        /// <remarks>
        ///   <para>Author(s): Miguel Gonzales, Andrea Tan, Jim Counts</para>
        ///   <para>Modified: 2011-10-27</para>
        /// </remarks>
        public void AddPhotoToAlbum(string albumId, string displayName, string photoFileName)
        {
            // Copies the file to the secret location.
            string photoId = AlbumRepository.GeneratePhotoKey(photoFileName);
            string storagePath = AlbumRepository.StoreFile(photoFileName, photoId);
            string storageName = Path.GetFileName(storagePath);

            // Put the photo in the album data structure.
            IAlbum currentAlbum = this.GetAlbum(albumId);
            if (!currentAlbum.ContainsPhoto(photoId))
            {
                currentAlbum.AddPhoto(photoId, displayName, storageName);
                this.SaveAlbums();
            }
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
                    hashString.Append(hash[i].ToString("X2", CultureInfo.InvariantCulture));
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
        private IAlbum DetachAlbum(string name)
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