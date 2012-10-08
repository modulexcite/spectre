﻿#region Copyright notice

// Copyright (C) 2012 Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

#region Namespace directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Spectre.Projections;

#endregion

namespace Crystalbyte.Spectre {
    public sealed class AsyncActivityController : NativeObject {
        private AsyncActivityController(IntPtr handle)
            : base(typeof (CefCallback), true) {
            NativeHandle = handle;
        }

        public static AsyncActivityController FromHandle(IntPtr handle) {
            return new AsyncActivityController(handle);
        }

        public bool IsCanceled { get; private set; }
        public bool IsPaused { get; private set; }

        public void Continue() {
            var r = MarshalFromNative<CefCallback>();
            var action = (ContCallback) Marshal.GetDelegateForFunctionPointer(r.Cont, typeof (ContCallback));
            action(NativeHandle);
        }

        public void Cancel() {
            var r = MarshalFromNative<CefCallback>();
            var action = (CancelCallback) Marshal.GetDelegateForFunctionPointer(r.Cancel, typeof (CancelCallback));
            action(NativeHandle);
            IsCanceled = true;
        }

        public void Pause() {
            IsPaused = true;
        }
    }
}