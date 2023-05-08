using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Collectables : EasyDraw
{
    public Vec2 position
    {
        get
        {
            return _position;
        }
        set
        {

        }
    }

    Vec2 _position;
    public int radius;
    string name;
    Sprite hole;
    Sprite gum;
    Sprite vulcan;

    public Collectables(string nName, Vec2 position, int pRadius, byte r, byte g, byte b) : base(pRadius * 2, pRadius * 2)
    {
        name = nName;
        radius = pRadius;
        _position.x = position.x;
        _position.y = position.y;
        WhichObject();
    }

    public void UpdateScreenPosition()
    {
        x = _position.x;
        y = _position.y;
    }

    void Update()
    {
        UpdateScreenPosition();
        
    }

    void Draw(byte r, byte g, byte b)
    {
        Fill(r, g, b);
        Stroke(r, g, b);
        Ellipse(radius, radius, 2 * radius, 2 * radius);
    }

    void WhichObject()
    {
        if (name == "BlackHole")
        {
            hole = new Sprite("hole.png");
            hole.SetOrigin(width / 2, height / 2);
            if (((MyGame)game).levelIndex == 1)
            {
                hole.SetScaleXY(0.12f);

            }
            else
            {
                hole.SetScaleXY(0.07f);
            }
            hole.SetXY(-radius / 2, -radius / 2);
            AddChild(hole);
        }
        else if (name == "PickUp")
        {
            gum = new Sprite("gum.png");
            gum.SetOrigin(width / 2, height / 2);
            if (((MyGame)game).levelIndex == 1)
            {
                gum.SetScaleXY(0.12f);

            }
            else
            {
                gum.SetScaleXY(0.03f);
            }
            gum.SetXY(-radius / 2 + 5, -radius / 2 + 5);
            AddChild(gum);
        }
        else if (name == "Goal")
        {
            vulcan = new Sprite("anthill.png");
            vulcan.SetOrigin(width / 2, height / 2);
            if (((MyGame)game).levelIndex == 1)
            {
                vulcan.SetScaleXY(0.10f);
                vulcan.SetXY(radius / 2 - 20, radius / 2 - 20);
            }
            else
            {
                vulcan.SetScaleXY(0.06f);
                vulcan.SetXY(radius / 2 - 15, radius / 2 - 15);
            }
            AddChild(vulcan);
        }
    }
    public string Name()
    {
        return name;
    }
}
