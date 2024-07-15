using Godot;
using System;
using System.Diagnostics;

public partial class UnitClass : RigidBody2D
{

[Export]
public UnitStats MyStats; //create an instances of a UnitStats Resource
// Is edited in the inspector for now where its default values can be configured. 

    public override void _Ready()
    {
        base._Ready();
    }

    public void TakeDamage(int damage)  //damage stored as possitives
    {
        if (MyStats.Armor > damage)
        {
            return;
        }
        else
        {
            MyStats.ChangeHealth(-(damage-MyStats.Armor)); // passing damage as negative value
        }
        GD.Print(MyStats.Health);
    }




}
