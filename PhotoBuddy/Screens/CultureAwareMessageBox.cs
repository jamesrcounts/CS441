//-----------------------------------------------------------------------
// <copyright file="CultureAwareMessageBox.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace PhotoBuddy.Screens
{
    using System.Globalization;
    using System.Windows.Forms;

    /// <summary>
    /// Displays a message box, setting the right-to-left reading property according to the parent control
    /// setting, or the current culture.
    /// </summary>
    /// <seealso cref="http://msdn.microsoft.com/en-us/library/ms182191.aspx"/>
    /// <remarks>
    /// <para>Author: Jim Counts and Eric Wei</para>
    /// <para>Created: 2011-11-02</para>
    /// </remarks>
    public static class CultureAwareMessageBox
    {
        /// <summary>
        /// Shows the specified message.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <returns>
        /// The DialogResult
        /// </returns>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-02</para>
        /// </remarks>
        public static DialogResult Show(
            IWin32Window owner,
            string text,
            string caption,
            MessageBoxButtons buttons,
            MessageBoxIcon icon)
        {
            MessageBoxOptions options = default(MessageBoxOptions);
            if (IsRightToLeft(owner))
            {
                options |= MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign;
            }

            return MessageBox.Show(
                owner,
                text,
                caption,
                buttons,
                icon,
                MessageBoxDefaultButton.Button1,
                options);
        }

        /// <summary>
        /// Determines whether [is right to left] [the specified owner].
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <returns>
        ///   <c>true</c> if owner is right to left; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// <para>Author: Jim Counts and Eric Wei</para>
        /// <para>Created: 2011-11-02</para>
        /// </remarks>
        private static bool IsRightToLeft(IWin32Window owner)
        {
            Control control = owner as Control;

            if (control != null)
            {
                return control.RightToLeft == RightToLeft.Yes;
            }

            // If no parent control is available, ask the CurrentUICulture
            // if we are running under right-to-left.
            return CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft;
        }
    }
}
