using Godot;
using System;



public partial class UnitData : Resource
{
   [Export]
    public int Health { get; set; }
    public UnitData() : this(0) { }

    public UnitData(int health)
    {
        Health = health;
    }
}

