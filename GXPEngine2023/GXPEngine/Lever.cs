using GXPEngine;

public class Lever : EasyDraw
{
    public int radius { get { return _radius; } }
    public Vec2 position;
    public ResponsiveObject connectedObject;
    private int _radius;

    public Lever(int pRadius, Vec2 pPosition, ResponsiveObject connectedObject) : base(pRadius * 2 + 1, pRadius * 2 + 1)
    {
        this._radius = pRadius;
        this.position = pPosition;
        this.connectedObject = connectedObject;

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

    private void UpdateScreenPosition()
    {
        x = position.x;
        y = position.y;
    }

    public void UpdateColor(byte red, byte green, byte blue) {
        this.Draw(red, green, blue);
    }

    public bool IsMouseOver()
    {
        float distance = Mathf.Sqrt(Mathf.Pow(Input.mouseX - position.x, 2) + Mathf.Pow(Input.mouseY - position.y, 2));
        return distance <= radius;
    }
}
