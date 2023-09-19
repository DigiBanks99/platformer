using Godot;
using GodotUtilities;

[Scene]
public partial class UserInterface : Control
{
    private SceneTree _sceneTree;
    [Node($"/root/{nameof(PlayerData)}")]
    private PlayerData _playerData;
    [Node]
    private ColorRect _pauseOverlay;
    [Node]
    private Label _scoreLabel;
    [Node("PauseOverlay/Title")]
    private Label _title;
    private bool _paused;

    public bool Paused
    {
        get => _paused; set
        {
            _paused = value;
            _sceneTree.Paused = _paused;
            _pauseOverlay.Visible = _paused;
        }
    }

    public override void _Ready()
    {
        _sceneTree = GetTree();
        WireNodes();

        _playerData.ScoreUpdated += OnScoreUpdated;
        _playerData.PlayerDied += OnPlayerDied;

        OnScoreUpdated(_playerData.Score);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("pause"))
        {
            Paused = !Paused;
            GetViewport().SetInputAsHandled(); // stops propogation
        }
    }

    private void OnScoreUpdated(int score)
    {
        _scoreLabel.Text = $"Score: {score}";
    }

    private void OnPlayerDied()
    {
        Paused = true;
        _title.Text = "You've died";
    }
}
