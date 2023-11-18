using Godot;
using System;

public partial class MenuMotorcycle : Sprite2D
{

	private Vector2 Acceleration = new Vector2(0f, 0f); 
	private Vector2 velocity = new Vector2(0f, 0f);


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.velocity += this.Acceleration * (float)delta;
		this.Position += this.velocity * (float)delta;
	}

	public void SetAcceleration(float acceleration)
	{
		this.Acceleration = new Vector2(acceleration, 0f);
	}

	public void Stop()
	{
		this.velocity = Vector2.Zero;
		this.Acceleration = Vector2.Zero;
	}
}
