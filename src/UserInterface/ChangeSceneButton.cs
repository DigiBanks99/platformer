using Godot;
using System.Collections.Generic;

[Tool]
public partial class ChangeSceneButton : Button
{
    [Export(PropertyHint.File, "*.tscn")]
    public string NextScenePath { get; set; }

    public void OnButtonUp()
    {
        GetTree().ChangeSceneToFile(NextScenePath);
    }

    public override string[] _GetConfigurationWarnings()
    {
        List<string> warnings = new();

        if (string.IsNullOrWhiteSpace(NextScenePath))
        {
            warnings.Add("The Next Scene Path must be set for the button");
        }

        return warnings.ToArray();
    }
}
