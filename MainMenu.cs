using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class MainMenu : Node2D
{
	private MenuLabel[] labels;
	private int selected;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.labels = new MenuLabel[3];

		this.labels[0] = this.GetNode<MenuLabel>("./UICanvas/Buttons/PlayLabel");
		this.labels[1] = this.GetNode<MenuLabel>("./UICanvas/Buttons/OptionsLabel");
		this.labels[2] = this.GetNode<MenuLabel>("./UICanvas/Buttons/QuitLabel");

		this.selected = 0;
		this.labels[0].SetSelected(true);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _Input(InputEvent @event)
	{

		if (@event.IsActionPressed("ui_up"))
		{
			this.labels[this.selected].SetSelected(false);
			GD.Print($"OldSelected: {selected}");
			this.selected = (this.selected + 2) % 3;
			GD.Print($"NewSelected: {selected}");
			this.labels[this.selected].SetSelected(true);
			return;
		}
		else if (@event.IsActionPressed("ui_down"))
		{
			this.labels[this.selected].SetSelected(false);
			GD.Print($"OldSelected: {selected}");
			this.selected = (this.selected + 1) % 3;
			GD.Print($"NewSelected: {selected}");
			this.labels[this.selected].SetSelected(true);
			return;
		}
		
		base._Input(@event);
	}
}
