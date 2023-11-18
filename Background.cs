using Godot;
using System;

public partial class Background : ColorRect
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Size = this.GetViewport().GetVisibleRect().Size;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
