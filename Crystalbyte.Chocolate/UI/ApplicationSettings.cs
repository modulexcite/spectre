using System;
using System.Runtime.InteropServices;
using Crystalbyte.Chocolate.Bindings.Internal;

namespace Crystalbyte.Chocolate.UI {
    public sealed class ApplicationSettings : Adapter {

        public ApplicationSettings() 
            : base(typeof(CefSettings)) {
            NativeHandle = Marshal.AllocHGlobal(NativeSize);
            MarshalToNative(new CefSettings {
                Size = NativeSize,
                SingleProcess = true,
                LogSeverity = CefLogSeverity.LogseverityVerbose
            });
        }

        public GraphicsImplementation Graphics {
            get { 
                var reflection = MarshalFromNative<CefSettings>();
                return (GraphicsImplementation) reflection.GraphicsImplementation;
            }
            set { 
                var reflection = MarshalFromNative<CefSettings>();
                reflection.GraphicsImplementation = (CefGraphicsImplementation) value;
                MarshalToNative(reflection);
            }
        }

        public LogSeverity LogSeverity
        {
            get
            {
                var reflection = MarshalFromNative<CefSettings>();
                return (LogSeverity)reflection.LogSeverity;
            }
            set
            {
                var reflection = MarshalFromNative<CefSettings>();
                reflection.LogSeverity = (CefLogSeverity)value;
                MarshalToNative(reflection);
            }
        }

        public bool IsMessageLoopMultiThreaded
        {
            get
            {
                var reflection = MarshalFromNative<CefSettings>();
                return reflection.MultiThreadedMessageLoop;
            }
            set
            {
                var reflection = MarshalFromNative<CefSettings>();
                reflection.MultiThreadedMessageLoop = value;
                MarshalToNative(reflection);
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