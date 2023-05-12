using GXPEngine;

public class ResponsiveObject : EasyDraw
{
    public int radius { get { return _radius; } }
    public Vec2 position;

    private int _radius;


    public ResponsiveObject(int pRadius, Vec2 pPosition) : base(pRadius * 2 + 1, pRadius * 2 + 1)
    {
        this._radius = pRadius;
        this.position = pPosition;

        UpdateScreenPosition();
        SetOrigin(_radius, _radius);
        Draw(255, 255, 255);
    }

    public void Draw(byte red, byte green, byte blue)
    {
        Fill(red, green, blue);
        Stroke(red, green, blue);
        Ellipse(_radius, _radius, 2 * _radius, 2 * _radius);
    }

    private void UpdateScreenPosition()
    {
        x = position.x;
        y = position.y;
    }
    public void UpdateColor(byte red, byte green, byte blue)
    {
        this.Draw(red, green, blue);
    }
}
