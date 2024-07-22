using Godot;
using System;

public partial class MainMenu : Node
{
	[Export] public Button StartButton;
	[Export] public Button QuitButton;

	[Export] public PackedScene LevelScene;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StartButton.Pressed += OnStartGamePressed;
		QuitButton.Pressed += OnQuitGamePressed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void OnStartGamePressed()
	{
		GetNode<SceneLoader>("/root/SceneLoader").ChangeToScene(LevelScene);
	}
	public void OnQuitGamePressed()
	{
		GetTree().Quit();
	}



}
