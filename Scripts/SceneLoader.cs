using Godot;
using System;

public partial class SceneLoader : Node
{
    
    [Export] private string _sceneFolder;
    [Export] public PackedScene gameOver;

    public void ChangeToScene(PackedScene Scene) 
    {
        GetTree().ChangeSceneToPacked(Scene);
    }

    public void ChangeToMenu()
    {
        GetTree().ChangeSceneToFile("res://Saved Nodes/LevelTemplates/MainMenu.tscn");
    }

}
