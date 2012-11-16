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
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre.Scripting {
    public sealed class JavaScriptDialogCallback : NativeTypeAdapter {
        private JavaScriptDialogCallback(IntPtr handle)
            : base(typeof (CefJsdialogCallback)) {
            Handle = handle;
        }

        public void Resume(bool success, string message = "") {
            var r = MarshalFromNative<CefJsdialogCallback>();
            var action = (CefJsdialogHandlerCapiDelegates.ContCallback6)
                         Marshal.GetDelegateForFunctionPointer(r.Cont,
                                                               typeof (CefJsdialogHandlerCapiDelegates.ContCallback6));
            var input = new StringUtf16(message);
            action(Handle, Convert.ToInt32(success), input.Handle);
            input.Free();
        }

        public static JavaScriptDialogCallback FromHandle(IntPtr handle) {
            return new JavaScriptDialogCallback(handle);
        }
    }
}
