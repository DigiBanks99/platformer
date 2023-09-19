using Godot;
using GodotUtilities;

[Scene]
public partial class RetryButton : Button
{
    [Node($"/root/{nameof(PlayerData)}")]
    private PlayerData _playerData;

    public override void _Ready()
    {
        WireNodes();
    }

    public void OnButtonUp()
    {
        _playerData.Score = 0;

        GetTree().Paused = false;
        GetTree().ReloadCurrentScene();
    }
}
