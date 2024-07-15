using Godot;
using System;
using System.Diagnostics;

public partial class GeneralUnit : RigidBody2D
{

[Export]
public UnitStatsResource unitStats; //create an instances of the UnitStatsResource
// Is edited in the inspector for now where its default values can be configured. 

    public override void _Ready()
    {
        base._Ready();
    }

    public void TakeDamage(int damage)  //damage stored as possitives
    {
        if (unitStats.Armor > damage)
        {
            return;
        }
        else
        {
            unitStats.ChangeHealth(-(damage-unitStats.Armor)); // passing damage as negative value
        }
        GD.Print(unitStats.Health);
    }




}
