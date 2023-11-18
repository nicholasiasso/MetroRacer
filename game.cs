using Godot;
using System;

public partial class game : Node2D
{

	private MainMenu ActiveScene;
	private FadeRect SceneFader;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.ActiveScene = GD.Load<PackedScene>("res://main_menu.tscn").Instantiate<MainMenu>();
		this.ActiveScene.sendGameEvent = this.SendSceneEvent;
		this.AddChild(this.ActiveScene);

		this.SceneFader = this.GetNode<FadeRect>("./ShaderLayer/FadeRect");
		this.SceneFader.SetFade(1.0d);
		this.SceneFader.TriggerFade(0.0, 5.0);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SendSceneEvent(GameEvent gameEvent)
	{
		GD.Print($"{nameof(game)}.{nameof(this.SendSceneEvent)}({gameEvent.GetType().Name}): Processing Game Event");
		switch (gameEvent)
		{
			case EndSceneEvent endSceneEvent:
				this.SceneFader.TriggerFade(
					1.0,
					this.SceneFader.FastFadeSpeed,
					() => 
					{
						this.RemoveChild(this.ActiveScene);
						this.ActiveScene.QueueFree();
						this.ActiveScene = default;
					});
					break;
			default:
				GD.Print($"{nameof(game)}.{nameof(SendSceneEvent)}: Unrecognized {nameof(GameEvent)} {gameEvent.GetType().Name}");
				break;
		}
	}
}

public class GameEvent
{
	public static string EndScene = nameof(EndScene);

}

public class EndSceneEvent : GameEvent
{
	public string nextScene {get; private set; }
	public EndSceneEvent(string nextScene)
	{
		this.nextScene = nextScene;
		return;
	}
}
