using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoBuddy.EventObjects
{
    public class PhotoEventArgs : EventArgs
    {
        private string photoId;

        public PhotoEventArgs(string photoId)
        {
            this.photoId = photoId;    
        }

        public string PhotoName
        {
            get
            {
                return this.photoId;
            }
        }
    }
}
