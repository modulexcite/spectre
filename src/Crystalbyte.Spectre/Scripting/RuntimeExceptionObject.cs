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
using Crystalbyte.Spectre.Interop;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre.Scripting {
    public sealed class RuntimeExceptionObject : NativeObject {
        public RuntimeExceptionObject()
            : base(typeof (CefV8exception)) {
            NativeHandle = Marshal.AllocHGlobal(NativeSize);
        }

        public string Message {
            get {
                var r = MarshalFromNative<CefV8exception>();
                var function = (GetMessageCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetMessage, typeof (GetMessageCallback));
                var handle = function(NativeHandle);
                return StringUtf16.ReadString(handle);
            }
        }

        protected override void DisposeNative() {
            if (NativeHandle != IntPtr.Zero) {
                Marshal.FreeHGlobal(NativeHandle);
            }
            base.DisposeNative();
        }
    }
}
