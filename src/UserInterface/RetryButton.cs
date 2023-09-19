using Godot;

public partial class RetryButton : Button
{
    private PlayerData _playerData;

    public void OnButtonUp()
    {
        _playerData = GetNode<PlayerData>($"/root/{nameof(PlayerData)}");
        _playerData.Score = 0;

        GetTree().Paused = false;
        GetTree().ReloadCurrentScene();
    }
}
