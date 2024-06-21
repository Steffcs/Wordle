using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyInputEventArgs : EventArgs
{
    public char KeyChar { get; private set; }

    public KeyInputEventArgs(char keyChar)
    {
        KeyChar = keyChar;
    }
}