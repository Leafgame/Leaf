using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts.PlayerScripts;

public class MoveCommand : Command
{
	private PlayerItemsController _player;

	public override void Execute(GameObject actor)
	{
		Move(actor);
	}

	private void Move(GameObject actor)
	{
		if (_player == null) _player = actor.GetComponent<PlayerItemsController>();
		var h = Input.GetAxis("Horizontal");
		_player.FacingDirection(h);
	}
}
