﻿#region Licensing notice

// Copyright (C) 2012, Alexander Wieser-Kuciel <alexander.wieser@crystalbyte.de>
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

namespace Crystalbyte.Spectre.Samples.Support {
    public static class Html {
        public static string Image(string source, string alt) {
            return string.Format("<img src=\"{0}\" alt=\"{1}\" />", source, alt);
        }

        public static string Header(string text) {
            return string.Format("<h1>{0}</h1>", text);
        }

        public static string Span(string text) {
            return string.Format("<span>{0}</span>", text);
        }
    }
}
