﻿// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using Microsoft.Win32.SafeHandles;
using System.Windows;

namespace Gma.System.MouseKeyHook.WinApi
{
    internal class HookProcedureHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private static bool _closing;

        static HookProcedureHandle()
        {
            Application.Current.Exit += (sender, e) => { _closing = true; };
        }

        public HookProcedureHandle()
            : base(true)
        {
        }

        protected override bool ReleaseHandle()
        {
            //NOTE Calling Unhook during processexit causes deley
            if (_closing) return true;
            return HookNativeMethods.UnhookWindowsHookEx(handle) != 0;
        }
    }
}