//-----------------------------------------------------------------------
// <copyright file="Format.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Jim Counts and Eric Wei
// Date: Nov 5 2011
// Modified date: Nov 26 2011
// Description: this class is responsible in formating strings.
//-----------------------------------------------------------------------
namespace PhotoBuddy
{
    using System.Globalization;

    /// <summary>
    /// Provides helper methods to format strings.
    /// </summary>
    /// <remarks>
    ///   <para>Author: Jim Counts and Eric Wei</para>
    ///   <para>Created: 2011-10-23</para>
    /// </remarks>
    public static class Format
    {
        /// <summary>
        /// Formats the specified string using the current culture.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The objects to format.</param>
        /// <returns>
        /// A formatted string.
        /// </returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-23</para>
        /// </remarks>
        public static string Culture(string format, params object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, format, args);
        }

        /// <summary>
        /// Formats the specified string using the invariant culture.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The objects to format.</param>
        /// <returns>A formatted string.</returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-24</para>
        /// </remarks>
        public static string Invariant(string format, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, format, args);
        }
    }
}