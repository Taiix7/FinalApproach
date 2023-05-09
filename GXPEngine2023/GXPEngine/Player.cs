using GXPEngine;
using System.Collections.Generic;

class Player : EasyDraw
{
    public int radius { get { return _radius; } }

    public Vec2 position;

    public float weight = .1f;

    private int _radius;
    private Vec2 velocity;
    private Vec2 acceleration;

    private Vec2 _oldPosition;

    

    public Player(int pRadius, Vec2 pPosition) : base(pRadius * 2 + 1, pRadius * 2 + 1)
    {
        this._radius = pRadius;
        this.position = pPosition;

        UpdateScreenPosition();
        SetOrigin(_radius, _radius);
        Draw(255, 255, 255);
    }

    void Draw(byte red, byte green, byte blue)
    {
        Fill(red, green, blue);
        Stroke(red, green, blue);
        Ellipse(_radius, _radius, 2 * _radius, 2 * _radius);
    }
    public void FollowMouse()
    {
        velocity += acceleration;
        position += velocity;
    }

    private void UpdateScreenPosition()
    {
        x = position.x;
        y = position.y;
    }

    void PlayerPhysics()
    {
        acceleration = new Vec2(0, weight);
    }

    public void Step()
    {
        FollowMouse();
        PlayerPhysics();

        velocity *= 0.99f;
        _oldPosition = position;
        UpdateScreenPosition();
    }

    void PlayerControl() {
        if (Input.GetKey(Key.A)) { }
    }

}

