using Godot;

public partial class QuitButton : Button
{
    public void OnButtonUp()
    {
        GetTree().Quit();
    }
}
