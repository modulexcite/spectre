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
using System.Collections.Generic;
using System.Runtime.InteropServices;

#endregion

namespace Crystalbyte.Spectre.Scripting {
    internal sealed class JavaScriptObjectCollection : List<JavaScriptObject>, IReadOnlyCollection<JavaScriptObject> {
        private static readonly int PointerSize = Marshal.SizeOf(typeof (IntPtr));

        public JavaScriptObjectCollection(IntPtr listHandle, int argumentCount) {
            // TODO: List is only populated on construction
            if (argumentCount < 1) {
                return;
            }
            var current = listHandle;
            for (var i = 0; i < argumentCount; i++) {
                var handle = Marshal.ReadIntPtr(current);
                Add(JavaScriptObject.FromHandle(handle));
                current = new IntPtr(current.ToInt64() + PointerSize);
            }
        }
    }
}