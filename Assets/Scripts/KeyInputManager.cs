using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyInputManager : MonoBehaviour
{
    public static event EventHandler<KeyInputEventArgs> OnKeyPressed;
    public static event EventHandler OnBackspacePressed;

    private static readonly Dictionary<KeyCode, char> SUPPORTED_KEYS = new Dictionary<KeyCode, char>
    {
        { KeyCode.A, 'A' }, { KeyCode.B, 'B' }, { KeyCode.C, 'C' },
        { KeyCode.D, 'D' }, { KeyCode.E, 'E' }, { KeyCode.F, 'F' },
        { KeyCode.G, 'G' }, { KeyCode.H, 'H' }, { KeyCode.I, 'I' },
        { KeyCode.J, 'J' }, { KeyCode.K, 'K' }, { KeyCode.L, 'L' },
        { KeyCode.M, 'M' }, { KeyCode.N, 'N' }, { KeyCode.O, 'O' },
        { KeyCode.P, 'P' }, { KeyCode.Q, 'Q' }, { KeyCode.R, 'R' },
        { KeyCode.S, 'S' }, { KeyCode.T, 'T' }, { KeyCode.U, 'U' },
        { KeyCode.V, 'V' }, { KeyCode.W, 'W' }, { KeyCode.X, 'X' },
        { KeyCode.Y, 'Y' }, { KeyCode.Z, 'Z' }
    };

    private void Update()
    {
        foreach (var key in SUPPORTED_KEYS)
        {
            if (Input.GetKeyDown(key.Key))
            {
                OnKeyPressed?.Invoke(this, new KeyInputEventArgs(key.Value));
                return; // Only handle one key press per frame
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            OnBackspacePressed?.Invoke(this, EventArgs.Empty);
        }
    }
}
