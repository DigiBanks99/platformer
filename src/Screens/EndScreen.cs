using Godot;
using GodotUtilities;

[Scene]
public partial class EndScreen : Control
{
    [Node($"/root/{nameof(PlayerData)}")]
    private PlayerData _playerData;
    [Node]
    private Label _label;

    public override void _Ready()
    {
        WireNodes();

        _label.Text = string.Format(_label.Text, _playerData.Score, _playerData.Deaths);
    }
}
