using Godot;
using System;

public partial class Motorcycle : Node2D
{
	private const float turningAccel = 7f;
	private const float forwardAccel = 30f;
	private const float brakeAccel = 40f;

	private Vector2 inputVec = Vector2.Zero;
	private Vector2 velocity = Vector2.Zero;
	private float deadband = 0.1f;

	[Signal] public delegate void UpdateSpeedEventHandler(int speed);
	[Signal] public delegate void SetBrakeLightEventHandler(bool enabled);


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.velocity.X += this.inputVec.X * turningAccel * (float)delta; 					// Input vector base accel
		this.velocity.X = Godot.Mathf.Clamp(this.velocity.X, -this.velocity.Y, this.velocity.Y);				// No sliding while stopped


		this.velocity.Y += this.inputVec.Y * ((this.inputVec.Y > 0) ? forwardAccel : brakeAccel) * (float)delta; // Input vector base accel
		this.velocity.Y -= this.velocity.Y * 0.15f * (float)delta;							// Add engine braking
		this.velocity.Y = Godot.Mathf.Max(this.velocity.Y, 0f);								// No going backwards

		float newRotation = Godot.Mathf.Sign(this.inputVec.X) * this.inputVec.X * this.inputVec.X * Godot.Mathf.Pi / 3f;
		if (this.velocity.Y < 60f)
		{
			newRotation *= this.velocity.Y * (1f/60f);
		}

		this.Position += new Vector2(this.velocity.X, 0f);
		this.Rotation = newRotation;

		this.EmitSignal(SignalName.UpdateSpeed, (int)this.velocity.Y);
		this.EmitSignal(SignalName.SetBrakeLight, this.inputVec.Y < 0f);
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		this.inputVec = new Vector2(
			Input.GetActionStrength("turn_right") - Input.GetActionStrength("turn_left"),
			Input.GetActionStrength("accel") - Input.GetActionStrength("brake"));
		if (Godot.Mathf.Abs(inputVec.X) < this.deadband) inputVec.X = 0f;
		if (Godot.Mathf.Abs(inputVec.Y) < this.deadband) inputVec.Y = 0f;
		return;
	}
}
