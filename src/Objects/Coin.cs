using Godot;
using GodotUtilities;

[Scene]
public partial class Coin : Area2D
{
    [Export] public int ScoreValue { get; set; }
    [Node]
    private AnimationPlayer _animationPlayer;
    private PlayerData _playerData;

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
            _playerData = GetNode<PlayerData>($"/root/{nameof(PlayerData)}");
        }
    }

    public void OnBodyEntered(Node2D body)
    {
        Picked();
    }

    private void Picked()
    {
        _animationPlayer.Play("fade_out");
        _playerData.Score += ScoreValue;
    }
}
