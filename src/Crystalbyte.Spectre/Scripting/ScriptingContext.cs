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
using Crystalbyte.Spectre.UI;

#endregion

namespace Crystalbyte.Spectre.Scripting {
    public sealed class ScriptingContext : RefCountedNativeObject {
        private ScriptingContext(IntPtr handle)
            : base(typeof (CefV8context)) {
            NativeHandle = handle;
        }

        public static ScriptingContext Current {
            get {
                var handle = CefV8Capi.CefV8contextGetCurrentContext();
                return FromHandle(handle);
            }
        }

        public static ScriptingContext Active {
            get {
                var handle = CefV8Capi.CefV8contextGetEnteredContext();
                return FromHandle(handle);
            }
        }

        public bool IsAlive {
            get { return ContextRegistrar.Current.IsContextAlive(this); }
        }

        public Browser Browser {
            get {
                var r = MarshalFromNative<CefV8context>();
                var function = (GetBrowserCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetBrowser, typeof (GetBrowserCallback));
                var handle = function(NativeHandle);
                return Browser.FromHandle(handle);
            }
        }

        public Frame Frame {
            get {
                var r = MarshalFromNative<CefV8context>();
                var function = (GetContextFrameCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetFrame,
                                                                     typeof (GetContextFrameCallback));
                var handle = function(NativeHandle);
                return Frame.FromHandle(handle);
            }
        }

        public JavaScriptObject Document {
            get {
                var r = MarshalFromNative<CefV8context>();
                var function = (GetGlobalCallback)
                               Marshal.GetDelegateForFunctionPointer(r.GetGlobal, typeof (GetGlobalCallback));
                var handle = function(NativeHandle);
                return JavaScriptObject.FromHandle(handle);
            }
        }

        internal static ScriptingContext FromHandle(IntPtr handle) {
            return new ScriptingContext(handle);
        }

        public bool TryEnter() {
            if (NativeHandle == IntPtr.Zero) {
                return false;
            }
            var r = MarshalFromNative<CefV8context>();
            var function = (EnterCallback)
                           Marshal.GetDelegateForFunctionPointer(r.Enter, typeof (EnterCallback));
            var value = function(NativeHandle);
            return Convert.ToBoolean(value);
        }

        public void Enter() {
            var success = TryEnter();
            if (!success) {
                throw new RuntimeException("Failed to enter context.");
            }
        }

        public void Exit() {
            var success = TryExit();
            if (!success) {
                throw new RuntimeException("Failed to exit context.");
            }
        }

        public bool TryExit() {
            if (NativeHandle == IntPtr.Zero) {
                return false;
            }
            var r = MarshalFromNative<CefV8context>();
            var function = (ExitCallback)
                           Marshal.GetDelegateForFunctionPointer(r.Exit, typeof (ExitCallback));
            var value = function(NativeHandle);
            return Convert.ToBoolean(value);
        }

        public bool IsSame(ScriptingContext other) {
            var r = MarshalFromNative<CefV8context>();
            var function = (IsSameCallback)
                           Marshal.GetDelegateForFunctionPointer(r.IsSame, typeof (IsSameCallback));

            Reference.Increment(other);

            var value = function(NativeHandle, other.NativeHandle);
            return Convert.ToBoolean(value);
        }

        public bool Evaluate(string code, out JavaScriptObject result, out RuntimeExceptionObject exception) {
            result = JavaScriptObject.Null;
            exception = new RuntimeExceptionObject();
            var str = new StringUtf16(code);

            var r = MarshalFromNative<CefV8context>();
            var function = (EvalCallback)
                           Marshal.GetDelegateForFunctionPointer(r.Eval, typeof (EvalCallback));

            Reference.Increment(result.NativeHandle);
            Reference.Increment(exception.NativeHandle);

            var success = function(NativeHandle, str.NativeHandle, result.NativeHandle, exception.NativeHandle);
            return Convert.ToBoolean(success);
        }

        #region Nested type: GetContextFrameCallback

        private delegate IntPtr GetContextFrameCallback(IntPtr self);

        #endregion
    }
}
