//-----------------------------------------------------------------------
// <copyright file="EventArgs{T}.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace PhotoBuddy
{
    using System;

    /// <summary>
    /// Provides a generic way to pass data with an event.
    /// </summary>
    /// <typeparam name="T">The type of data to pass.</typeparam>
    /// <remarks>
    ///   <para>Author: Jim Counts and Eric Wei</para>
    ///   <para>Created: 2011-11-05</para>
    /// </remarks>
    public class EventArgs<T> : EventArgs
    {
        /// <summary>
        /// The data value to pass.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-05</para>
        /// </remarks>
        private readonly T value;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventArgs&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-05</para>
        /// </remarks>
        public EventArgs(T data)
        {
            this.value = data;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-05</para>
        /// </remarks>
        public T Data
        {
            get
            {
                return this.value;
            }
        }
    }
}
