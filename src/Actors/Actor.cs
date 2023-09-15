using Godot;

public partial class Actor : CharacterBody2D
{
    [Export]
    public float Gravity = 3000.0f;
    [Export]
    public Vector2 Speed = new(300.0f, 1000.0f);
}