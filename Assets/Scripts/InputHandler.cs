using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputGetter
{
    Dictionary<KeyCode, EnumPlayerInput> keyMap = new Dictionary<KeyCode, EnumPlayerInput>();

    public InputGetter()
    {
        this.keyMap[KeyCode.UpArrow] = EnumPlayerInput.MoveUp;
        this.keyMap[KeyCode.LeftArrow] = EnumPlayerInput.MoveLeft;
        this.keyMap[KeyCode.RightArrow] = EnumPlayerInput.MoveRight;
        this.keyMap[KeyCode.DownArrow] = EnumPlayerInput.MoveDown;
    }

    public EnumPlayerInput GetPlayerInput() 
    {
        KeyCode[] keys = this.keyMap.Keys.ToArray<KeyCode>();
        for (int i = 0; i < keys.Length; i++) 
        {
            if (Input.GetKeyDown(keys[i])) {
                return this.keyMap[keys[i]];
            }
        }

        return EnumPlayerInput.None;
    }
}
