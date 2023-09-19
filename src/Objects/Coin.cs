using Godot;
using GodotUtilities;

[Scene]
public partial class Coin : Area2D
{
    [Export] public int ScoreValue { get; set; }
    [Node]
    private AnimationPlayer _animationPlayer;
    [Node($"/root/{nameof(PlayerData)}")]
    private PlayerData _playerData;

    public override void _Ready()
    {
        WireNodes();
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
