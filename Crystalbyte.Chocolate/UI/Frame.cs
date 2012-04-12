﻿#region Namespace Directives

using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings;
using Crystalbyte.Chocolate.Scripting;

#endregion

namespace Crystalbyte.Chocolate.UI {
    public sealed class Frame : Adapter {
        private readonly StringUtf16 _aboutBlank;

        private Frame(IntPtr handle)
            : base(typeof (CefFrame), true) {
            NativeHandle = handle;
            _aboutBlank = new StringUtf16("about:blank");
        }

        protected override void DisposeNative() {
            if (_aboutBlank.NativeHandle != IntPtr.Zero) {
                _aboutBlank.Free();
            }
            base.DisposeNative();
        }

        public static Frame FromHandle(IntPtr handle) {
            return new Frame(handle);
        }

        public bool IsFocused {
            get {
                var reflection = MarshalFromNative<CefFrame>();
                var function = (IsFocusedCallback)
                    Marshal.GetDelegateForFunctionPointer(reflection.IsFocused, typeof(IsFocusedCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsMain {
            get {
                var reflection = MarshalFromNative<CefFrame>();
                var function = (IsMainCallback)
                    Marshal.GetDelegateForFunctionPointer(reflection.IsMain, typeof(IsMainCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public bool IsValid {
            get {
                var reflection = MarshalFromNative<CefFrame>();
                var function = (IsValidCallback)
                    Marshal.GetDelegateForFunctionPointer(reflection.IsValid, typeof(IsValidCallback));
                var value = function(NativeHandle);
                return Convert.ToBoolean(value);
            }
        }

        public void Navigate(string address) {
            var u = new StringUtf16(address);
            var reflection = MarshalFromNative<CefFrame>();
            var action = (LoadUrlCallback)
                Marshal.GetDelegateForFunctionPointer(reflection.LoadUrl, typeof(LoadUrlCallback));
            action(NativeHandle, u.NativeHandle);
            u.Free();
        }

        public void Render(string source) {
            var u = new StringUtf16(source);
            var reflection = MarshalFromNative<CefFrame>();
            var action = (LoadStringCallback)
                Marshal.GetDelegateForFunctionPointer(reflection.LoadString, typeof(LoadStringCallback));
            action(NativeHandle, u.NativeHandle, _aboutBlank.NativeHandle);
            u.Free();
        }

        public void Navigate(Uri address) {
            Navigate(address.AbsoluteUri);
        }

        public long Id {
            get {
                var reflection = MarshalFromNative<CefFrame>();
                var function = (GetIdentifierCallback)
                    Marshal.GetDelegateForFunctionPointer(reflection.GetIdentifier, typeof(GetIdentifierCallback));
                return function(NativeHandle);
            }
        }

        public string Name {
            get {
                var reflection = MarshalFromNative<CefFrame>();
                var function = (GetNameCallback)
                    Marshal.GetDelegateForFunctionPointer(reflection.GetName, typeof(GetNameCallback));
                var handle = function(NativeHandle);
                return StringUtf16.ReadStringAndFree(handle);
            }
        }

        public Frame Parent {
            get {
                var reflection = MarshalFromNative<CefFrame>();
                var function = (GetParentCallback)
                    Marshal.GetDelegateForFunctionPointer(reflection.GetParent, typeof(GetParentCallback));
                var handle = function(NativeHandle);
                return handle == IntPtr.Zero ? null : FromHandle(handle);
            }
        }

        public string Url {
            get {
                var reflection = MarshalFromNative<CefFrame>();
                var function = (GetUrlCallback)
                    Marshal.GetDelegateForFunctionPointer(reflection.GetUrl, typeof(GetUrlCallback));
                var handle = function(NativeHandle);
                return handle == IntPtr.Zero ? string.Empty : StringUtf16.ReadStringAndFree(handle);
            }
        }

        public Browser Browser {
            get { 
                var reflection = MarshalFromNative<CefFrame>();
                var function = (GetBrowserCallback)
                    Marshal.GetDelegateForFunctionPointer(reflection.GetBrowser, typeof(GetBrowserCallback));
                var handle = function(NativeHandle);
                return Browser.FromHandle(handle);
            }
        }

        public ScriptingContext Context {
            get {
                var reflection = MarshalFromNative<CefFrame>();
                var function = (GetV8contextCallback)
                    Marshal.GetDelegateForFunctionPointer(reflection.GetV8context, typeof(GetV8contextCallback));
                var handle = function(NativeHandle);
                return ScriptingContext.FromHandle(handle);
            }
        }

        public void SelectAll() {
            var reflection = MarshalFromNative<CefFrame>();
            var action = (SelectAllCallback)
                Marshal.GetDelegateForFunctionPointer(reflection.SelectAll, typeof(SelectAllCallback));
            action(NativeHandle);
        }

        public void Copy() {
            var reflection = MarshalFromNative<CefFrame>();
            var action = (CopyCallback)
                Marshal.GetDelegateForFunctionPointer(reflection.Copy, typeof(CopyCallback));
            action(NativeHandle);
        }

        public void Cut() {
            var reflection = MarshalFromNative<CefFrame>();
            var action = (CutCallback)
                Marshal.GetDelegateForFunctionPointer(reflection.Cut, typeof(CutCallback));
            action(NativeHandle);
        }

        public void Undo() {
            var reflection = MarshalFromNative<CefFrame>();
            var action = (UndoCallback)
                Marshal.GetDelegateForFunctionPointer(reflection.Undo, typeof(UndoCallback));
            action(NativeHandle);
        }

        public void Delete() {
            var reflection = MarshalFromNative<CefFrame>();
            var action = (DelCallback)
                Marshal.GetDelegateForFunctionPointer(reflection.Del, typeof(DelCallback));
            action(NativeHandle);
        }

        public void Redo() {
            var reflection = MarshalFromNative<CefFrame>();
            var action = (RedoCallback)
                Marshal.GetDelegateForFunctionPointer(reflection.Redo, typeof(RedoCallback));
            action(NativeHandle);
        }

        public void Paste() {
            var reflection = MarshalFromNative<CefFrame>();
            var action = (PasteCallback)
                Marshal.GetDelegateForFunctionPointer(reflection.Paste, typeof(PasteCallback));
            action(NativeHandle);
        }

        public void ExecuteJavascript(string code) {
            ExecuteJavascript(code, "about:blank", 0);
        }

        public void ExecuteJavascript(string code, string scriptUrl) {
            ExecuteJavascript(code, scriptUrl, 0);
        }

        public void ExecuteJavascript(string code, string scriptUrl, int line) {
            var s = new StringUtf16(scriptUrl);
            var c = new StringUtf16(code);
            var reflection = MarshalFromNative<CefFrame>();
            var action = (ExecuteJavaScriptCallback) Marshal.GetDelegateForFunctionPointer(reflection.ExecuteJavaScript,
                                                                                           typeof (
                                                                                               ExecuteJavaScriptCallback
                                                                                               ));
            action(NativeHandle, c.NativeHandle, s.NativeHandle, line);
            c.Free();
            s.Free();
        }
    }
}