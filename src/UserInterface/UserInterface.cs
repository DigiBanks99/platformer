using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class UserInterface : Control
{
	private SceneTree _sceneTree;
    [Node]
    private ColorRect _pauseOverlay;
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
    }

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        GD.Print($"Paused: {Paused}, Event: {@event}");
        if (@event.IsActionPressed("pause"))
        {
            Paused = !Paused;
            GetViewport().SetInputAsHandled(); // stops propogation
        }
    }
}
