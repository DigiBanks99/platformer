using Godot;
using GodotUtilities;

[Scene]
public partial class Enemy : Actor
{
    [Export] public int ScoreValue { get; set; }

    [Node($"/root/{nameof(PlayerData)}")]
    private PlayerData _playerData;
    [Node]
    private Area2D _stompDetector;

    public override void _Ready()
    {
        WireNodes();

        Velocity = Velocity with { X = -Speed.X };
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Velocity = Velocity with { X = Velocity.X * (IsOnWall() ? -1 : 1) };

        MoveAndSlide();
    }

    public void OnStompDetectorAreaEntered(Area2D area)
    {
        if (area.GlobalPosition.Y > _stompDetector.GlobalPosition.Y)
        {
            return;
        }

        Die();
    }

    private void Die()
    {
        _playerData.Score += ScoreValue;
        QueueFree();
    }
}
