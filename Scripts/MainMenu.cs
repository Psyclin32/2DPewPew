using Godot;
using System;

public partial class MainMenu : Node
{
	[Export] public Button StartButton;
	[Export] public Button QuitButton;

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
		GetNode<SceneLoader>("/root/SceneLoader").StartGame();
	}
	public void OnQuitGamePressed()
	{
		GetNode<SceneLoader>("/root/SceneLoader").QuitGame();
	}



}
