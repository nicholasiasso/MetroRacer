using Godot;
using System;

public partial class MenuLabel : Label
{
	[Export] private string BaseText = "";

	private Color notSelectedColor = new Color(0f, 0f, 0f);
	private Color selectedColor = new Color(1f, 1f, 1f);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Text = BaseText;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetSelected(bool selected)
	{
		// GD.Print($"ButtonSelected: {selected}");
		if (!selected)
		{
			this.Text = BaseText;
			this.LabelSettings.FontColor = notSelectedColor;
			return;
		}
		this.Text = $"- {BaseText} -";
		this.LabelSettings.FontColor = selectedColor;
		return;
	}
}
