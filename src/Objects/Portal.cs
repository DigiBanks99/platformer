using Godot;
using GodotUtilities;
using System.Collections.Generic;

[Tool]
[Scene]
public partial class Portal : Area2D
{
    [Node]
    private AnimationPlayer _animationPlayer;

    [Export]
    public PackedScene NextScene { get; set; }

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }

    public override string[] _GetConfigurationWarnings()
    {
        List<string> warnings = new();
        if (NextScene is null)
        {
            warnings.Add("The Next Scene property needs to be initialized with a Scene");
        }

        return warnings.ToArray();
    }

    public void OnBodyEntered(Node2D body)
    {
        Teleport();
    }

    public async void Teleport()
    {
        _animationPlayer.Play("fade_in");
        // GDScript equivalent: yield(_animationPlayer, "animation_finished")
        await ToSignal(_animationPlayer, AnimationPlayer.SignalName.AnimationFinished);

        GetTree().ChangeSceneToPacked(NextScene);
    }
}
