using Godot;
using System;

public partial class FadeRect : ColorRect
{
	[Export] public float DefaultFadeSpeed = 5.0f;
	[Export] public float FastFadeSpeed = 0.5f; 

	private double fadeRatio = 0.0d;
	private double targetFadeRatio = 1.0d;
	private double stepPerSecond = 0.0d;
	private bool isFading = false;
	private Action onComplete;
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
				if (this.onComplete != default)
				{
					this.onComplete();
					this.onComplete = default;
				}
			}

			this.UpdateFade();
		}
	}

	public void SetFade(double fadeRatio)
	{
		this.fadeRatio = fadeRatio;
		this.UpdateFade();
	}

	public void TriggerFade(double targetFadeRatio, double durationSeconds, Action onComplete = default)
	{
		this.isFading = true;
		this.onComplete = onComplete;
		this.targetFadeRatio = targetFadeRatio;
		if (durationSeconds == 0.0d) durationSeconds = 0.001d; 
		this.stepPerSecond = (targetFadeRatio - fadeRatio) / durationSeconds;
	}

	private void UpdateFade()
	{
		((ShaderMaterial)this.Material).SetShaderParameter("fadeRatio", (float)this.fadeRatio);
	}
}
