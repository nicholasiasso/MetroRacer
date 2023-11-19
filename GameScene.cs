using Godot;
using System;

public partial class GameScene : Node2D
{
    internal Action<GameEvent> SendGameEvent { get; set; }
}
