using Godot;
using System;
using System.Security.Cryptography.X509Certificates;


[GlobalClass]  //needed for proper listing within Godot resources list as well as passing inspector values
public partial class UnitStats : Resource  //if we want an object to have this resource, export a UnitStats declaration to create an data instance for said object. 
{
	//Use for creating global flag and data objects (May create separately if needed, unlikely in short term)
	//Tracking vitals like HP + shields and other "Live" data that is used at gameplay layer.

	[Export]
	private int Health { get; set; }
	[Export]
	private int Shields { get; set; }
	[Export]
	private int Armor { get; set; }
	private enum FactionNames
	{
		Faction0,
		Faction1,
		Faction2,
		Other,
		Count  
	}
	private int[] _FactionRatings = new int[(int)FactionNames.Count]; 

	public UnitStats () : this(1,0,0) {} //sets default values if none passed.

	public UnitStats (int health, int armor, int shields)
	{
		Health = health;
		Armor = armor;
		Shields = shields;

	}




}
