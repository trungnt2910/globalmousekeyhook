// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Collections.Generic;
using System.Linq;
using Gma.System.MouseKeyHook.WinForms;
using Gma.System.MouseKeyHook.WinApi;

namespace Gma.System.MouseKeyHook.Implementation
{
    internal abstract class KeyListener : BaseListener, IKeyboardEvents
    {
        protected KeyListener(Subscribe subscribe)
            : base(subscribe)
        {
        }

        public event KeyEventHandler KeyDown;
        public event KeyPressEventHandler KeyPress;
        public event EventHandler<KeyDownTxtEventArgs> KeyDownTxt;
        public event KeyEventHandler KeyUp;

        public void InvokeKeyDown(KeyEventArgsExt e)
        {
            var handler = KeyDown;
            if (handler == null || e.Handled || !e.IsKeyDown)
                return;
            handler(this, e);
        }

        public void InvokeKeyPress(KeyPressEventArgsExt e)
        {
            var handler = KeyPress;
            if (handler == null || e.Handled || e.IsNonChar)
                return;
            handler(this, e);
        }

        public void InvokeKeyDownTxt(KeyDownTxtEventArgs e)
        {
            var handler = KeyDownTxt;
            if (handler == null || e.KeyEvent.Handled || !e.KeyEvent.IsKeyDown)
                return;
            handler(this, e);
        }

        public void InvokeKeyUp(KeyEventArgsExt e)
        {
            var handler = KeyUp;
            if (handler == null || e.Handled || !e.IsKeyUp)
                return;
            handler(this, e);
        }

        protected override bool Callback(CallbackData data)
        {
            var eDownUp = GetDownUpEventArgs(data);

            InvokeKeyDown(eDownUp);

            if (KeyPress != null || KeyDownTxt != null)
            {
                var pressEventArgs = GetPressEventArgs(data).ToList();

                foreach (var pressEventArg in pressEventArgs)
                    InvokeKeyPress(pressEventArg);

                var downTxtEventArgs = GetDownTxtEventArgs(eDownUp, pressEventArgs);
                InvokeKeyDownTxt(downTxtEventArgs);
            }

            InvokeKeyUp(eDownUp);

            return !eDownUp.Handled;
        }

        private KeyDownTxtEventArgs GetDownTxtEventArgs(KeyEventArgsExt eDownUp, IEnumerable<KeyPressEventArgsExt> pressEventArgs)
        {
            var charsCollection = pressEventArgs.Where(e => !e.IsNonChar).Select(e => e.KeyChar);
            var chars = string.Join(string.Empty, charsCollection);
            return new KeyDownTxtEventArgs(eDownUp, chars);
        }

        protected abstract IEnumerable<KeyPressEventArgsExt> GetPressEventArgs(CallbackData data);
        protected abstract KeyEventArgsExt GetDownUpEventArgs(CallbackData data);
    }
}