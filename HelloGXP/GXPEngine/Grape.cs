using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Grape : AnimationSprite
{

    public Grape() : base("grape_sheet.png", 4, 2)
    {
        SetOrigin(width / 2, height / 2);
        SetScaleXY(0.05f);
    }

    void Update()
    {
        if (((MyGame)game).levelIndex == 2)
        {
            SetScaleXY(0.03f);
        }
    }
}

public class Tomato : AnimationSprite
{
    public Tomato() : base("tomato_sheet.png", 5, 3)
    {
        SetOrigin(width / 2, height / 2);
        SetScaleXY(0.05f);
    }

    void Update()
    {
        if (((MyGame)game).levelIndex == 3)
        {
            SetScaleXY(0.03f);
        }
    }

}

public class Candy : AnimationSprite
{
    public Candy() : base("candy_sheet.png", 10,6)
    {
        SetOrigin(width / 2, height / 2);
        SetScaleXY(0.05f);        
    }

    void Update()
    {
        if (((MyGame)game).levelIndex == 4 || ((MyGame)game).levelIndex == 5)
        {
            SetScaleXY(0.05f);
        }
    }

}