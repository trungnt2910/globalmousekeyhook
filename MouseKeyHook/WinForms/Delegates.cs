using System;
using System.Collections.Generic;
using System.Text;

namespace Gma.System.MouseKeyHook.WinForms
{
    public delegate void MouseEventHandler(object sender, MouseEventArgs e);
    /// <summary>
    ///  Represents a method that will handle the <see cref='Control.KeyPress'/>
    ///  event of a <see cref='Control'/>.
    /// </summary>
    public delegate void KeyPressEventHandler(object sender, KeyPressEventArgs e);
    /// <summary>
    ///  Represents a method that will handle the <see cref='Control.KeyUp'/>
    ///  or <see cref='Control.KeyDown'/> event of a
    /// <see cref='Control'/>.
    /// </summary>
    public delegate void KeyEventHandler(object sender, KeyEventArgs e);
}
