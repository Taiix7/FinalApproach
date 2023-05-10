using GXPEngine;

class Player : EasyDraw
{
    public int radius { get { return _radius; } }

    public bool lightWeight = true;

    public Vec2 position;

    private int _radius;

    private float speed = 1f;
    private float weight = .01f;
    private float jumpSpeed = 5;

    private Vec2 velocity;
    private Vec2 acceleration;
    private Vec2 _oldPosition;

    private bool canJump;

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

    public void Gravity()
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
        Gravity();
        acceleration = new Vec2(0, weight);
    }

    public void Step()
    {
        PlayerPhysics();
        PlayerControl();

        velocity *= 0.99f;
        _oldPosition = position;
        UpdateScreenPosition();
    }

    void PlayerControl()
    {
        if (Input.GetKey(Key.A)) { position -= new Vec2(speed * Time.deltaTime, 0); }
        if (Input.GetKey(Key.D)) { position += new Vec2(speed * Time.deltaTime, 0); }

        if (!lightWeight) return;
        if (canJump && Input.GetKeyDown(Key.SPACE)) {
            velocity -= new Vec2(0, jumpSpeed);
            canJump = false;
        }
    }

    public void Land() { canJump = true; }
}

