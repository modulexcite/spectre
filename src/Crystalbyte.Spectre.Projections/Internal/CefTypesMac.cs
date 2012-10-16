#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 3 as published by
// the Free Software Foundation.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

#region Using directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace Crystalbyte.Spectre.Projections.Internal {
    [StructLayout(LayoutKind.Sequential)]
    public struct MacCefMainArgs {
        public int Argc;
        public IntPtr Argv;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MacCefWindowInfo {
        public CefStringUtf16 WindowName;
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public int Hidden;
        public IntPtr ParentView;
        public IntPtr View;
    }
}
