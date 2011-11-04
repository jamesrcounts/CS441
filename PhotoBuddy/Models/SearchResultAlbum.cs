using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoBuddy.Models
{
    public class SearchResultAlbum : IAlbum
    {
        IAlbum decoratedAlbum;

        IList<IPhoto> photoBucket = new List<IPhoto>();

        public SearchResultAlbum(AlbumRepository albumRepository, string albumId, IEnumerable<IPhoto> photos)
        {
            var album = new Album(albumRepository, albumId, Enumerable.Empty<IPhoto>());
            this.decoratedAlbum = album;
            foreach (var photo in photos)
            {
                this.photoBucket.Add(photo);
            }
        }

        public string AlbumId
        {
            get
            {
                return this.decoratedAlbum.AlbumId;
            }
            set
            {
                this.decoratedAlbum.AlbumId = value;
            }
        }

        public IEnumerable<IPhoto> Photos
        {
            get
            {
                return this.photoBucket;
            }
        }

        public AlbumRepository Repository
        {
            get { return this.decoratedAlbum.Repository; }
        }

        public int Count
        {
            get { return this.photoBucket.Count; }
        }

        public System.Drawing.Image CoverPhoto
        {
            get { return this.photoBucket.First().Image; }
        }

        public void AddPhoto(IPhoto photo)
        {
            this.photoBucket.Add(photo);
        }

        public void AddPhoto(string photoId, string displayName, string fileName)
        {
            var photo = new Photo(this, photoId, displayName, fileName);
            this.AddPhoto(photo);
        }

        public IPhoto GetPhoto(string photoId)
        {
            return this.photoBucket.SingleOrDefault(photo => photo.PhotoId == photoId);
        }

        public void Delete()
        {
            return;
        }

        public void RemovePhoto(string photoId)
        {
            this.GetPhoto(photoId).Delete();
        }

        public bool ContainsPhoto(string photoId)
        {
            return this.GetPhoto(photoId) != null;
        }

        public bool ContainsName(string displayName)
        {
            return this.photoBucket.Any(photo => photo.DisplayName == displayName);
        }
    }
}
