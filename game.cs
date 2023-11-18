using Godot;
using System;

public partial class game : Node2D
{

	private Node2D ActiveScene;
	private FadeRect SceneFader;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//this.ActiveScene = new MainMenu();
		//this.AddChild(this.ActiveScene);

		this.SceneFader = this.GetNode<FadeRect>("./ShaderLayer/FadeRect");
		this.SceneFader.SetFade(1.0d);
		this.SceneFader.TriggerFade(0.0, 5.0);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
