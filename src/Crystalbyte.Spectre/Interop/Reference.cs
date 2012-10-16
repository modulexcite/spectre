﻿#region Licensing notice

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
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre.Interop {
    internal static class Reference {
        public static void Decrement(IntPtr handle) {
            var obj = (CefBase) Marshal.PtrToStructure(handle, typeof (CefBase));
            var function =
                (ReleaseCallback) Marshal.GetDelegateForFunctionPointer(obj.Release, typeof (ReleaseCallback));
            function(handle);
        }

        public static void Decrement(RefCountedNativeObject item) {
            var obj = (CefBase) Marshal.PtrToStructure(item.NativeHandle, typeof (CefBase));
            var function =
                (ReleaseCallback) Marshal.GetDelegateForFunctionPointer(obj.Release, typeof (ReleaseCallback));
            function(item.NativeHandle);
        }

        public static void Increment(RefCountedNativeObject item) {
            var obj = (CefBase) Marshal.PtrToStructure(item.NativeHandle, typeof (CefBase));
            var function =
                (AddRefCallback) Marshal.GetDelegateForFunctionPointer(obj.AddRef, typeof (AddRefCallback));
            function(item.NativeHandle);
        }

        public static void Increment(IntPtr handle) {
            var obj = (CefBase) Marshal.PtrToStructure(handle, typeof (CefBase));
            var function =
                (AddRefCallback) Marshal.GetDelegateForFunctionPointer(obj.AddRef, typeof (AddRefCallback));
            function(handle);
        }

        public static int GetReferenceCounter(IntPtr handle) {
            var obj = (CefBase) Marshal.PtrToStructure(handle, typeof (CefBase));
            var function =
                (AddRefCallback) Marshal.GetDelegateForFunctionPointer(obj.GetRefct, typeof (AddRefCallback));
            return function(handle);
        }
    }
}
