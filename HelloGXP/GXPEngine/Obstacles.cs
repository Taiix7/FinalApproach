using System;
using GXPEngine;


public class Obstacles : EasyDraw
{

    CollisionInfo col;
    public Vec2 position
    {
        get
        {
            return _position;
        }
    }

    Vec2 _position;

    int _radius;

    public Obstacles(float pX, float pY, int _pRadius) : base(_pRadius * 2, _pRadius * 2)
    {

        _radius = _pRadius;
        _position.x = pX;
        _position.y = pY;

        Rect(_pRadius, _pRadius, _pRadius, _pRadius);
        SetColor(255, 0, 0);
        SetOrigin(width / 2, height / 2);
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
}

