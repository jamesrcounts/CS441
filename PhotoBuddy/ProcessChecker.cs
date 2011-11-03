// -----------------------------------------------------------------------
// <copyright file="ProcessChecker.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace PhotoBuddy
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>
    /// Checks to see if there is already an instance of Photo Buddy running for 
    /// this user.  If there is, it will bring that instance to the front of the 
    /// screen.
    /// </summary>
    /// <remarks>Authors: Jim Counts and Eric Wei</remarks>
    /// <seealso cref="http://www.dotnetperls.com/single-instance-windows-form"/>
    internal static class ProcessChecker
    {
        /// <summary>
        /// Tries to show the PhotoBuddy window with a different process id than the current process.
        /// </summary>
        /// <remarks>
        /// Authors: Jim Counts and Eric Wei
        /// </remarks>
        public static void ShowWindow()
        {
            int currentProcessId = Process.GetCurrentProcess().Id;
            foreach (Process proc in Process.GetProcessesByName(Application.ProductName))
            {
                if (proc.Id != currentProcessId)
                {
                    NativeMethods.EnumWindows(EnumWindowsProc, new IntPtr(proc.Id));
                }
            }
        }

        /// <summary>
        /// Find and show a running window.
        /// </summary>
        /// <param name="windowHandle">The window handle.</param>
        /// <param name="otherProcessId">The process id of the other instance of PhotoBuddy, which we are looking for.</param>
        /// <returns>
        /// true if the caller should continue to enumerate windows; otherwise false
        /// </returns>
        /// <remarks>
        /// Authors: Jim Counts and Eric Wei
        /// </remarks>
        [SuppressMessage(
            "Microsoft.Usage",
            "CA1806:DoNotIgnoreMethodResults",
            MessageId = "PhotoBuddy.ProcessChecker+NativeMethods.GetWindowThreadProcessId(System.IntPtr,System.Int32@)",
            Justification = "The threadId returned by this method is not important to the program logic.")]
        private static bool EnumWindowsProc(IntPtr windowHandle, int otherProcessId)
        {
            int processId = 0;
            NativeMethods.GetWindowThreadProcessId(windowHandle, ref processId);
            if (processId != otherProcessId)
            {
                // Check the next process.
                return true;
            }

            // Restore the window.
            NativeMethods.ShowWindowAsync(windowHandle, NativeMethods.SW_SHOWNORMAL);
            NativeMethods.SetForegroundWindow(windowHandle);

            // Found what we were looking for, stop looking.
            return false;
        }

        /// <summary>
        /// Contains signatures for C++ functions
        /// </summary>
        /// <remarks>Authors: Jim Counts and Eric Wei</remarks>
        internal static class NativeMethods
        {
            /// <summary>
            /// Indicates that we want the window displayed.
            /// </summary>
            public const int SW_SHOWNORMAL = 1;

            /// <summary>
            /// Allows the .NET to provide a callback to the native method EnumWindowsProc.
            /// </summary>
            /// <param name="hWnd">A window handle.</param>
            /// <param name="lParam">Optional state.</param>
            /// <returns>true if the enumeration should continue; otherwise false.</returns>
            /// <remarks>Authors: Jim Counts and Eric Wei</remarks>
            public delegate bool EnumWindowsProcDel(IntPtr hWnd, int lParam);

            /// <summary>
            /// Shows a window created by a different thread.
            /// </summary>
            /// <param name="hWnd">A window handle.</param>
            /// <param name="nCmdShow">How to show the window (minimized, hidden, etc...)</param>
            /// <returns>False if the window was not hidden; otherwise true.</returns>
            /// <remarks>Authors: Jim Counts and Eric Wei</remarks>
            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

            /// <summary>
            /// Brings the thread that created the specified window to the foreground
            /// and activates the window.
            /// </summary>
            /// <param name="hWnd">A window handle.</param>
            /// <returns>true if the window was brought to the foreground; otherwise false.</returns>
            /// <remarks>Authors: Jim Counts and Eric Wei</remarks>
            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetForegroundWindow(IntPtr hWnd);

            /// <summary>
            /// Enumerates all the top level windows on screen until <paramref name="lpEnumFunc"/> returns false.
            /// </summary>
            /// <param name="lpEnumFunc">A predicate used to determine when to stop enumerating.</param>
            /// <param name="lParam">Pointer to an argument that may be passed to the predicate.</param>
            /// <returns>true if the function completed without errors; otherwise false</returns>
            /// <remarks>Authors: Jim Counts and Eric Wei</remarks>
            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool EnumWindows(EnumWindowsProcDel lpEnumFunc, IntPtr lParam);

            /// <summary>
            /// Retrieves the identifier of the thread that created the specified window.
            /// </summary>
            /// <param name="hWnd">A user handle.</param>
            /// <param name="lpdwProcessId">Receives the address of the process that created the window.</param>
            /// <returns>ID of the thread that created the window.</returns>
            /// <remarks>Authors: Jim Counts and Eric Wei</remarks>
            [DllImport("user32.dll")]
            public static extern int GetWindowThreadProcessId(IntPtr hWnd, ref int lpdwProcessId);
        }
    }
}
