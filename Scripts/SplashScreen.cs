using Godot;
using System;

public partial class SplashScreen : ColorRect
{
    [Export] public Timer Durration;
    public override void _Ready()
    {   
        //GD.Print(GetTreeStringPretty());

        Durration.Timeout +=
            () => GetNode<SceneLoader>("/root/SceneLoader").ChangeToMenu();
    }
}
