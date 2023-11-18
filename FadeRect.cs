using Godot;
using System;

public partial class FadeRect : ColorRect
{

	private double fadeRatio = 0.0d;
	private double targetFadeRatio = 1.0d;
	private double stepPerSecond = 0.0d;
	private bool isFading = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Visible = true;
		this.Size = this.GetViewport().GetVisibleRect().Size;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (this.isFading)
		{
			bool check = fadeRatio < targetFadeRatio;
			fadeRatio += stepPerSecond * delta;
			bool updated = fadeRatio < targetFadeRatio;

			if (check != updated)
			{
				fadeRatio = targetFadeRatio;
				this.stepPerSecond = 0.0d;
				this.isFading = false;
			}

			this.UpdateFade();
		}
	}

	public void SetFade(double fadeRatio)
	{
		this.fadeRatio = fadeRatio;
		this.UpdateFade();
	}

	public void TriggerFade(double targetFadeRatio, double durationSeconds)
	{
		this.isFading = true;
		this.targetFadeRatio = targetFadeRatio;
		if (durationSeconds == 0.0d) durationSeconds = 0.001d; 
		this.stepPerSecond = (targetFadeRatio - fadeRatio) / durationSeconds;
		GD.Print($"Setting StepPerSecond to {this.stepPerSecond}");
	}

	private void UpdateFade()
	{
		GD.Print($"Setting Fade to {this.fadeRatio}");
		((ShaderMaterial)this.Material).SetShaderParameter("fadeRatio", (float)this.fadeRatio);
	}
}
