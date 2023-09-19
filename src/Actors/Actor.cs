using Godot;

public partial class Actor : CharacterBody2D
{
    [Export]
    public float Gravity = 3500.0f;
    [Export]
    public Vector2 Speed = new(400.0f, 500.0f);

    public override void _PhysicsProcess(double delta)
    {
        GD.Print($"Pre-gravity (V): {Velocity}");
        Velocity = Velocity with { Y = Velocity.Y + (Gravity * (float)delta) };
        GD.Print($"Post-gravity (V): {Velocity}");
    }
}