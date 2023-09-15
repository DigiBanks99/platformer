using Godot;

public partial class Enemy : Actor
{
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

	public void OnStompDetectorBodyEntered(PhysicsBody2D body)
	{
		if (body.GlobalPosition.Y > GetNode<Area2D>("StompDetector").GlobalPosition.Y)
		{
			return;
		}

		GetNode<CollisionShape2D>(nameof(CollisionShape2D)).Disabled = true;
		QueueFree();
	}
}
