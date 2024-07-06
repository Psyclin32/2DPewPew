using Godot;
using System;
using System.Diagnostics;

public partial class Asteroid : RigidBody2D
{

    public override void _Ready()
    {

        Debug.Print("Asteroid Ready!");

        //BodyEntered += OnBodyEntered();


        base._Ready();
    }


    private void OnBodyEntered(Node2D body)
    {
        // register hit - change sprites
        Debug.Print("Hit Detected!");
        

    }

}   

