//-----------------------------------------------------------------------
// <copyright file="Album.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Kendra Diaz, Thomas Donnellan, Eric Wei, Jim Counts
// Date: Nov 22 2011
// Modified date: Nov 26 2011
// Description: this class is responsible for converting an image to grayscale.
// Code from:http://stackoverflow.com/questions/2265910/c-convert-image-to-grayscale<summary>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace PhotoBuddy.Models
{
    public class BlackandWhite
    {
        /// <summary>
        /// Changs an image to grayscale.
        /// </summary>
        /// <param name="original"></param>
        /// <returns>Bitmap object</returns>
        /// <remarks>
        ///   <para>Author(s): Kendra Diaz, Thomas Donnellan, Eric Wei, Jim Counts</para>
        ///   <para>Modified: 2011-11-26</para>
        /// </remarks>
        public static Bitmap MakeGrayscale3(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][] 
         {
            new float[] {.3f, .3f, .3f, 0, 0},
             new float[] {.59f, .59f, .59f, 0, 0},
            new float[] {.11f, .11f, .11f, 0, 0},
             new float[] {0, 0, 0, 1, 0},
            new float[] {0, 0, 0, 0, 1}
          });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }

    }
}
