using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Security;
using Crystalbyte.Spectre.Projections.Internal;

namespace Crystalbyte.Spectre.Projections
{
	[SuppressUnmanagedCodeSecurity]
	public static class CefCookieCapi {
		[DllImport(CefAssembly.Name, EntryPoint = "cef_cookie_manager_get_global_manager", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefCookieManagerGetGlobalManager();
		
		[DllImport(CefAssembly.Name, EntryPoint = "cef_cookie_manager_create_manager", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr CefCookieManagerCreateManager(IntPtr path);
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefCookieManager {
		public CefBase Base;
		public IntPtr SetSupportedSchemes;
		public IntPtr VisitAllCookies;
		public IntPtr VisitUrlCookies;
		public IntPtr SetCookie;
		public IntPtr DeleteCookies;
		public IntPtr SetStoragePath;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct CefCookieVisitor {
		public CefBase Base;
		public IntPtr Visit;
	}
	
	[SuppressUnmanagedCodeSecurity]
	public static class CefCookieCapiDelegates {
		public delegate void SetSupportedSchemesCallback(IntPtr self, IntPtr schemes);
		public delegate int VisitAllCookiesCallback(IntPtr self, IntPtr visitor);
		public delegate int VisitUrlCookiesCallback(IntPtr self, IntPtr url, int includehttponly, IntPtr visitor);
		public delegate int SetCookieCallback(IntPtr self, IntPtr url, IntPtr cookie);
		public delegate int DeleteCookiesCallback(IntPtr self, IntPtr url, IntPtr cookieName);
		public delegate int SetStoragePathCallback(IntPtr self, IntPtr path);
		public delegate int VisitCallback(IntPtr self, IntPtr cookie, int count, int total, ref int deletecookie);
	}
	
}