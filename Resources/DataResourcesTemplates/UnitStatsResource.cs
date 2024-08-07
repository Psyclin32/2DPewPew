using Godot;
using System;
using System.Security.Cryptography.X509Certificates;


[GlobalClass]  //needed for proper listing within Godot resources list as well as passing inspector values
public partial class UnitStatsResource : Resource  //if we want an object to have this resource, export a UnitStatsResource declaration to create an data instance for said object. 
{
	//Use for creating global flag and data objects (May create separately if needed, unlikely in short term)
	//Tracking vitals like HP + shields and other "Live" data that is used at gameplay layer.

	[ExportGroup("Unit Stats")]
	[Export] public int MaxHealth { get; set; }
	[Export] public int Health { get; set; }	
	[Export] public int MaxShields { get; set; }
	[Export] public int Shields { get; set; }
	[Export] public int TotalArmor { get; set; }
	
	
	public enum FactionNames
	{
		Faction0,
		Faction1,
		Faction2,
		Other,
		Count  
	}
	private int[] _FactionRatings = new int[(int)FactionNames.Count]; 

	public UnitStatsResource () 
	{
		Health = 100;
		MaxHealth = 100;
		Shields = 50;
		MaxShields = 50;
		TotalArmor = 1;
	} //sets default values if none passed. 
	//Not likely to be used if using insperctor defaults. 

	public UnitStatsResource (int health, int shields, int armor)
	{
		Health = health;
		Shields = shields;
		TotalArmor = armor;
	
	}
	public int GetMaxHealth()
	{
		return MaxHealth;
	}
	public int GetMaxShields()
	{
		return MaxShields;
	}
	public void ChangeHealth(int deltaHP) //damage is signed. Intended to accomodate for both damage and healing. 
	{
		if(deltaHP + Health >= MaxHealth)  //If greater than MaxHP, cap
		{
			Health = MaxHealth;
		}
		else if (Health + deltaHP <= 0)  //If negative, keep at zero 
		{
			Health = 0;
		}
		else
		{
			Health += deltaHP;   //apply change
		}
	}
	public void ChangeShields(int delta) //damage is signed. Intended to accomodate for both damage and healing. 
	{
		if(delta + Shields >= MaxShields)  //If greater than MaxHP, cap
		{
			Shields = MaxShields;
		}
		else if (Shields + delta <= 0)  //If negative, keep at zero 
		{
			Shields = 0;
		}
		else
		{
			Shields += delta;   //apply change
		}
	}
	public void ChangeArmor(int delta) //damage is signed. Intended to accomodate for both damage and healing. 
	{
		if(TotalArmor + delta <= 0 )   // clamp to zero
		{
			TotalArmor = 0;
		}
		else
		{
			TotalArmor += delta; //apply change
		}
	}
	





}
