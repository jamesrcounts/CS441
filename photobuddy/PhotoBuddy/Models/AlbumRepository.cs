﻿//-----------------------------------------------------------------------
// <copyright file="AlbumRepository.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales, Andrea Tan, Jim Counts and Eric Wei
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
    using System.Xml.Linq;
    using PhotoBuddy.Common;

    /// <summary>
    /// Manages create, read, update and delete functions a collection of albums.
    /// </summary>
    public class AlbumRepository
    {
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
        ///   <para>Author: Jim Counts and Eric Wei</para>
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
            // if dir dosnt exist creat it
            if (!Directory.Exists(Constants.PhotosFolderPath))
            {
                Directory.CreateDirectory(Constants.PhotosFolderPath);
            }

            // load existing data from file
            if (File.Exists(Constants.XmlDataFilePath))
            {
                this.document = XDocument.Load(Constants.XmlDataFilePath);
            }
            else
            {
                // or create file
                this.document = new XDocument(new XElement(PhotoBuddyTag));
                this.document.Save(Constants.XmlDataFilePath);
            }

            this.LoadAlbums();
        }

        /// <summary>
        /// Gets the collection of albums.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
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
        ///   <para>Author: Jim Counts and Eric Wei</para>
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
        /// Stores the file.
        /// </summary>
        /// <param name="sourcePath">The source path.</param>
        /// <param name="photoId">The photo id.</param>
        /// <returns>The path to the file's location in storage.</returns>
        /// <remarks>
        /// Author(s): Miguel Gonzales, Andrea Tan, Jim Counts and Eric Wei
        /// </remarks>
        public static string StoreFile(string sourcePath, string photoId)
        {
            // Combines two paths without having to worry about whether path1 ends with a '\' character
            string destinationPath = Path.Combine(Constants.PhotosFolderPath, photoId);

            // Changes or adds the original file extension to the new path
            destinationPath = Path.ChangeExtension(destinationPath, Path.GetExtension(sourcePath));
            if (!File.Exists(destinationPath))
            {
                File.Copy(sourceFileName: sourcePath, destFileName: destinationPath, overwrite: true);
            }

            return destinationPath;
        }

        /// <summary>
        /// Deletes the specified album form the index.
        /// </summary>
        /// <param name="albumName">Name of the album.</param>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei and Eric Wei</para>
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
        ///   <para>Authors: Jim Counts and Eric Wei and Eric Wei</para>
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
        /// <para>Author(s): Miguel Gonzales, Andrea Tan, Jim Counts and Eric Wei</para>
        /// <para>Modified: 2011-10-28</para>
        /// </remarks>
        public void RenameAlbum(string name, string updateName)
        {
            IAlbum album = this.DetachAlbum(name);
            if (album == null)
            {
                return;
            }

            updateName = updateName.Trim();
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
           
            photo.DisplayName = displayName;
           
            this.SaveAlbums();
        }

        /// <summary>
        /// Adds an album with the specified album name to the repository, then returns the new album to the caller.
        /// </summary>
        /// <param name="albumName">Name of the album.</param>
        /// <returns>The newly created album.</returns>
        /// <remarks>
        ///   <para>Author(s): Miguel Gonzales, Andrea Tan, Jim Counts and Eric Wei</para>
        ///   <para>Modified: 2011-10-28</para>
        /// </remarks>
        public IAlbum AddAlbum(string albumName)
        {
            this.ThrowOnInvalidAlbumName(albumName);

            // Update XML
            XElement albumElement = XmlAlbum.CreateAlbumElement(albumName);
            this.document.Descendants().First().Add(albumElement);
            var album = new XmlAlbum(this, albumElement);

            // Update Album Index
            this.albums.Add(album.AlbumId, album);
            return album;
        }

        /// <summary>
        /// Collects the garbage.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <param name="filePath">The file path.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
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
        ///   <para>Author: Jim Counts and Eric Wei</para>
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
        /// <para>Author(s): Miguel Gonzales, Andrea Tan, Jim Counts and Eric Wei</para>
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
                          where photo.DisplayName.ToUpperInvariant().Contains(term)
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
        ///   <para>Author: Jim Counts and Eric Wei</para>
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
        /// Modified: 11/17/2011 Kendra New definition handles names with white spaces and makes names not case sensitive
        /// </remarks>
        public bool IsExistingAlbumName(string albumName)
        {
            // if album name exists return true else false
            albumName = albumName.Trim();
            albumName = albumName.ToLower();

            // lower albumName
            foreach (string keyAlbum in this.albums.Keys)
            {
                string existingAlbum = keyAlbum.Trim();
                existingAlbum = existingAlbum.ToLower();
                if (albumName == existingAlbum)
                {
                    return true;
                }
            }

            return false;
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
        /// Throws an exception when the name the album is invalid.
        /// </summary>
        /// <param name="albumName">Name of the album.</param>
        /// <remarks>
        /// Author: Jim Counts and Eric Wei
        /// Created: 2011-11-04
        /// </remarks>
        private void ThrowOnInvalidAlbumName(string albumName)
        {
            if (string.IsNullOrWhiteSpace(albumName) || Constants.MaxNameLength < albumName.Length)
            {
                throw new ArgumentException("albumName is invalid.", "albumName");
            }

            if (this.albums.ContainsKey(albumName))
            {
                throw new ArgumentException("Album name already used in the repository.", "albumName");
            }
        }

        /// <summary>
        /// Removes the album with the specified name from the album collection and returns the detached album to the caller.
        /// </summary>
        /// <param name="name">The album name.</param>
        /// <returns>If an album exists with the specified name it is returned; otherwise null.</returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
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