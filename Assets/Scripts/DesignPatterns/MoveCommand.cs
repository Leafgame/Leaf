using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts.PlayerScripts;

public class MoveCommand : Command
{
	private PlatformerCharacter2D _player;

	public override void Execute(GameObject actor)
	{
		Move(actor);
	}

	private void Move(GameObject actor)
	{
		if (_player == null) _player = actor.GetComponent<PlatformerCharacter2D>();
		var h = Input.GetAxis("Horizontal");
		_player.Move(h);
	}
}
