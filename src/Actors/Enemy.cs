using Godot;
using GodotUtilities;

[Scene]
public partial class Enemy : Actor
{
	[Export] public int ScoreValue { get; set; }

	private PlayerData _playerData;

	public override void _Ready()
	{
		base._Ready();

		Velocity = Velocity with { X = -Speed.X };
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		if (IsOnWall())
		{
			Velocity = Velocity with { X = Velocity.X * -1 };
		}

		Velocity = Velocity with { Y = Velocity.Y + Gravity * (float)delta };
		UpDirection = Vector2.Up;
		MoveAndSlide();
	}

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
		{
			WireNodes();
            _playerData = GetNode<PlayerData>($"/root/{nameof(PlayerData)}");
        }
    }

    public void OnStompDetectorBodyEntered(PhysicsBody2D body)
    {
        if (body.GlobalPosition.Y > GetNode<Area2D>("StompDetector").GlobalPosition.Y)
        {
            return;
        }

        Die();
    }

    private void Die()
    {
        GetNode<CollisionShape2D>(nameof(CollisionShape2D)).Disabled = true;
        QueueFree();
		_playerData.Score += ScoreValue;
    }
}
