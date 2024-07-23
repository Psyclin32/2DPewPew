using Godot;
using System;

public partial class SceneLoader : Node
{
    
    [Export] private PackedScene GameScene;
    [Export] private PackedScene MainMenu;
    [Export] public PackedScene gameOver;

    public void ChangeToScene(PackedScene Scene) 
    {
        GetTree().ChangeSceneToPacked(Scene);
    }

    public void ChangeToMenu()
    {
        GetTree().ChangeSceneToPacked(MainMenu);
    }

    public void StartGame()
    {
        GetTree().ChangeSceneToPacked(GameScene);
    }

    public void QuitGame()
    {
        GetTree().Quit();
    }

}
