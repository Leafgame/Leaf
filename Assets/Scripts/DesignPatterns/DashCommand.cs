﻿using System;
using Assets.Scripts.PlayerScripts;
using UnityEngine;

public enum DashDirection
{
	Left,
	Right
};

public class DashCommand : Command
{
	private DashDirection _direction;

	public DashCommand(DashDirection direction)
	{
		_direction = direction;
	}

	public override void Execute(GameObject actor)
	{
		Dash(actor);
	}

	private PlatformerCharacter2D _player;
	private void Dash(GameObject actor)
	{
		if (_player == null) _player = actor.GetComponent<PlatformerCharacter2D>();
		_player.CheckDash(_direction);

	}
}
