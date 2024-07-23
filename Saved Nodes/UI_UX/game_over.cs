using Godot;
using System;

public partial class game_over : Control
{
	public SceneLoader sceneLoader;
	[ExportGroup("Buttons")]
	[Export] public Button Restart;
	[Export] public Button MainMenu;
	[Export] public Button Quit;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sceneLoader = GetNode<SceneLoader>("/root/SceneLoader");

		Restart.Pressed += OnRestart;
		MainMenu.Pressed += OnMainMenu;
		Quit.Pressed += OnQuit;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
	public void OnRestart()
	{
		sceneLoader.StartGame();
	}
	public void OnMainMenu()
	{
		sceneLoader.ChangeToMenu();
	}
	public void OnQuit()
	{
		sceneLoader.QuitGame();
	}
}
