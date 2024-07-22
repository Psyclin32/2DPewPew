using Godot;
using System;

public partial class SceneLoader : Node
{
    
    [Export] private string _sceneFolder;

    public void ChangeToScene(PackedScene sceneName) 
    {
        GetTree().ChangeSceneToPacked(sceneName);
    }

    public void ChangeToMenu()
    {
        GetTree().ChangeSceneToFile("res://Saved Nodes/LevelTemplates/MainMenu.tscn");
    }

}
