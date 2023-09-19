using Godot;

public partial class PlayerData : Node
{
    private int _score;
    private int _deaths;

    public int Score
    {
        get => _score; set
        {
            _score = value;
            EmitSignal(nameof(ScoreUpdatedEventHandler).Replace("EventHandler", ""), _score);
        }
    }

    public int Deaths
    {
        get => _deaths; set
        {
            _deaths = value;
            EmitSignal(nameof(PlayerDiedEventHandler).Replace("EventHandler", ""));
        }
    }

    [Signal]
    public delegate void ScoreUpdatedEventHandler(int score);
    [Signal]
    public delegate void PlayerDiedEventHandler();

    public void Reset()
    {
        Score = 0;
        Deaths = 0;
    }
}
