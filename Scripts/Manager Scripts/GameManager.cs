using Godot;
using System;

public partial class GameManager : Node
{
	public GlobalVars Globals;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Globals = GetNode("/root/Globals") as GlobalVars;
		//GD.Print(Globals.Player);
		//GetNode("/root/Main").AddChild(Globals.Player);
		GD.Print(GetTreeStringPretty());
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	//Example Methods
	public void StartGame()
	{

		//LoadGlobals and needed Data Containers based on level selection. 
		//Need reference to player character and level data

	}
	public void LoadLevel()
	{

	}
	public void OnPlayerDeath()
	{
		//display score, UI for menu options

	}
	public void OnLevelComplete()
	{
		//display score and either return to Main menue or continue to next level

	}
	public void LoadData()
	{
		//needs to be passed specific data to be loaded and likely returns an objetc or packed scene which is then managed by who called it. 

	}
	public void SaveData()
	{
		//Need to learn how and what to save. 

	}
	public void SpawnEnemies()
	{
		//Likely divorced from "Environments" data as those are separate assets?
		//Could be combined for now with parameter settings 

	}
}
