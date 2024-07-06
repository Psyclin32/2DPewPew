using Godot;
using System;
using System.Diagnostics;

public partial class StatsClass : Node2D
{

	public enum _statNames
	{
		Health,
		Shields,
		Armor,
		INVALID_STAT = -1,
	}
	// eumn the names of each stat for menus and UI and even editor elements. 

	private int[] _statValues = new int[sizeof(_statNames) - 1];
	//private array body for stats. Scales with enums?? useful??
	private int[] defaultValues = { 10, 20, 5 }; // HP/SHIELD/AR
	//need get() set(). First time ever doing this yet I always see these shits lol
	public void SetStatValues()
	{	
			_statValues = defaultValues;	
	} 
	public int[] GetStatValues()
	{
		return _statValues;
	}
	public int GetStatValues(_statNames mystats)
	{	
		switch (mystats)
		{
					
		case _statNames.Health:	
			return _statValues[0];
		
		case _statNames.Shields:	
			return _statValues[1];

		case _statNames.Armor:	
			return _statValues[2];

		default :
			return _statValues[4];
		}
	}


	public string MethodTest()
	{
		return "testing custom class method call";
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
