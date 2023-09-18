using Godot;
using GodotUtilities;

[Scene]
public partial class Coin : Area2D
{
    [Node]
    private AnimationPlayer _animationPlayer;

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }

    public void OnBodyEntered(Node2D body)
    {
        _animationPlayer.Play("fade_out");
    }
}
