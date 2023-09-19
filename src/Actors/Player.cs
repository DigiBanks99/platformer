using Godot;
using GodotUtilities;

[Scene]
public partial class Player : Actor
{
    [Node($"/root/{nameof(PlayerData)}")]
    private PlayerData _playerData;

    [Export] public float StompImpulse { get; set; } = 1000.0f;

    public override void _Ready()
    {
        WireNodes();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        bool isJumpInterrupted = Input.IsActionJustReleased("jump") && Velocity.Y < 0.0f;
        Vector2 direction = GetDirection();
        Velocity = CalculateVelocity(Velocity, direction, Speed, isJumpInterrupted);
        float snap = direction.Y == 0.0f ? Vector2.Down.Y * 60.0f : Vector2.Zero.Y;
        MoveAndSlide();
        FloorSnapLength = snap;
    }

    public void OnStompDetectorAreaEntered(Area2D area)
    {
        Velocity = CalculateStompVelocity(Velocity, StompImpulse);
    }

    public void OnEnemyDetectorBodyEntered(Node2D body)
    {
        Die();
    }

    private Vector2 GetDirection()
    {
        return new(
            Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left"),
            Input.IsActionJustPressed("jump") && IsOnFloor() ? -Input.GetActionStrength("jump") : 0.0f);
    }

    private Vector2 CalculateVelocity(
        Vector2 linearVelocity,
        Vector2 direction,
        Vector2 speed,
        bool isJumpInterrupted)
    {
        Vector2 velocity = linearVelocity;
        velocity.X = speed.X * direction.X;
        if (direction.Y != 0.0f)
        {
            velocity.Y = Speed.Y * direction.Y;
        }
        if (isJumpInterrupted)
        {
            velocity.Y = 0.0f;
        }
        return velocity;
    }

    private Vector2 CalculateStompVelocity(Vector2 linearVelocity, float impulse)
    {
        var stompJump = Input.IsActionJustPressed("jump") ? -Speed.Y : -impulse;
        return new Vector2(linearVelocity.X, stompJump);
    }

    private void Die()
    {
        _playerData.Deaths++;
        QueueFree();
    }

}
