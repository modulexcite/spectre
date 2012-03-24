// Copyright (c) 2012 Marshall A. Greenblatt. All rights reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are
// met:
//
//    * Redistributions of source code must retain the above copyright
// notice, this list of conditions and the following disclaimer.
//    * Redistributions in binary form must reproduce the above
// copyright notice, this list of conditions and the following disclaimer
// in the documentation and/or other materials provided with the
// distribution.
//    * Neither the name of Google Inc. nor the name Chromium Embedded
// Framework nor the names of its contributors may be used to endorse
// or promote products derived from this software without specific prior
// written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
// A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
// OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
// SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
// LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
// THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
// ---------------------------------------------------------------------------
//
// This file was generated by the CEF translator tool and should not edited
// by hand. See the translator.README.txt file in the tools directory for
// more information.
//

#ifndef CEF_INCLUDE_CAPI_CEF_COMMAND_LINE_CAPI_H_
#define CEF_INCLUDE_CAPI_CEF_COMMAND_LINE_CAPI_H_
#pragma once

#ifdef __cplusplus
extern "C" {
#endif

#include "include/capi/cef_base_capi.h"


///
// Structure used to create and/or parse command line arguments. Arguments with
// '--', '-' and, on Windows, '/' prefixes are considered switches. Switches
// will always precede any arguments without switch prefixes. Switches can
// optionally have a value specified using the '=' delimiter (e.g.
// "-switch=value"). An argument of "--" will terminate switch parsing with all
// subsequent tokens, regardless of prefix, being interpreted as non-switch
// arguments. Switch names are considered case-insensitive. This structure can
// be used before cef_initialize() is called.
///
typedef struct _cef_command_line_t {
  ///
  // Base structure.
  ///
  cef_base_t base;

  ///
  // Initialize the command line with the specified |argc| and |argv| values.
  // The first argument must be the name of the program. This function is only
  // supported on non-Windows platforms.
  ///
  void (CEF_CALLBACK *init_from_argv)(struct _cef_command_line_t* self,
      int argc, const char* const* argv);

  ///
  // Initialize the command line with the string returned by calling
  // GetCommandLineW(). This function is only supported on Windows.
  ///
  void (CEF_CALLBACK *init_from_string)(struct _cef_command_line_t* self,
      const cef_string_t* command_line);

  ///
  // Reset the command-line switches and arguments but leave the program
  // component unchanged.
  ///
  void (CEF_CALLBACK *reset)(struct _cef_command_line_t* self);

  ///
  // Constructs and returns the represented command line string. Use this
  // function cautiously because quoting behavior is unclear.
  ///
  // The resulting string must be freed by calling cef_string_userfree_free().
  cef_string_userfree_t (CEF_CALLBACK *get_command_line_string)(
      struct _cef_command_line_t* self);

  ///
  // Get the program part of the command line string (the first item).
  ///
  // The resulting string must be freed by calling cef_string_userfree_free().
  cef_string_userfree_t (CEF_CALLBACK *get_program)(
      struct _cef_command_line_t* self);

  ///
  // Set the program part of the command line string (the first item).
  ///
  void (CEF_CALLBACK *set_program)(struct _cef_command_line_t* self,
      const cef_string_t* program);

  ///
  // Returns true (1) if the command line has switches.
  ///
  int (CEF_CALLBACK *has_switches)(struct _cef_command_line_t* self);

  ///
  // Returns true (1) if the command line contains the given switch.
  ///
  int (CEF_CALLBACK *has_switch)(struct _cef_command_line_t* self,
      const cef_string_t* name);

  ///
  // Returns the value associated with the given switch. If the switch has no
  // value or isn't present this function returns the NULL string.
  ///
  // The resulting string must be freed by calling cef_string_userfree_free().
  cef_string_userfree_t (CEF_CALLBACK *get_switch_value)(
      struct _cef_command_line_t* self, const cef_string_t* name);

  ///
  // Returns the map of switch names and values. If a switch has no value an
  // NULL string is returned.
  ///
  void (CEF_CALLBACK *get_switches)(struct _cef_command_line_t* self,
      cef_string_map_t switches);

  ///
  // Add a switch to the end of the command line. If the switch has no value
  // pass an NULL value string.
  ///
  void (CEF_CALLBACK *append_switch)(struct _cef_command_line_t* self,
      const cef_string_t* name);

  ///
  // Add a switch with the specified value to the end of the command line.
  ///
  void (CEF_CALLBACK *append_switch_with_value)(
      struct _cef_command_line_t* self, const cef_string_t* name,
      const cef_string_t* value);

  ///
  // True if there are remaining command line arguments.
  ///
  int (CEF_CALLBACK *has_arguments)(struct _cef_command_line_t* self);

  ///
  // Get the remaining command line arguments.
  ///
  void (CEF_CALLBACK *get_arguments)(struct _cef_command_line_t* self,
      cef_string_list_t arguments);

  ///
  // Add an argument to the end of the command line.
  ///
  void (CEF_CALLBACK *append_argument)(struct _cef_command_line_t* self,
      const cef_string_t* argument);
} cef_command_line_t;


///
// Create a new cef_command_line_t instance.
///
CEF_EXPORT cef_command_line_t* cef_command_line_create();


#ifdef __cplusplus
}
#endif

#endif  // CEF_INCLUDE_CAPI_CEF_COMMAND_LINE_CAPI_H_
