using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Button : Sprite
{
    string button;

    public Button(string button) : base(button)
    {
        this.button = button;
        SetOrigin(width / 2, height / 2);
        SetScaleXY(0.2f);
        SetXY(width / 2, height / 2);
    }
}
