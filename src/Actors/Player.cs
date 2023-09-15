using Godot;

public partial class Player : Actor
{
	[Export] public float StompImpulse { get; set; } = 1000.0f;
	
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		bool isJumpInterrupted = Input.IsActionJustReleased("jump") && Velocity.Y < 0.0;
		Vector2 direction = GetDirection();
		Velocity = CalculateVelocity(Velocity, direction, Speed, isJumpInterrupted);
		UpDirection = Vector2.Up;
		MoveAndSlide();
	}

	public void OnEnemyDetectorAreaEntered(Area2D area)
	{
		Velocity = CalculateStompVelocity(Velocity, StompImpulse);
	}

	public void OnEnemyDetectorBodyEntered(Node2D body)
	{
		QueueFree();
	}

	private Vector2 GetDirection()
	{
		return new(
			Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left"),
			Input.IsActionJustPressed("jump") && IsOnFloor() ? -1.0f : 1.0f);
	}

	private Vector2 CalculateVelocity(
		Vector2 linearVelocity,
		Vector2 direction,
		Vector2 speed,
		bool isJumpInterrupted)
	{
		Vector2 velocity = linearVelocity;
		velocity.X = speed.X * direction.X;
		velocity.Y += Gravity * (float)GetPhysicsProcessDeltaTime();
		if (direction.Y == -1.0)
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
		Vector2 velocity = linearVelocity;
		velocity.Y = -impulse;
		return velocity;
	}

}
