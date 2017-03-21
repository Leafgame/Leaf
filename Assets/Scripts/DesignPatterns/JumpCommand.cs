using Assets.Scripts.PlayerScripts;
using UnityEngine;

public class JumpCommand : Command
{
	public override void Execute(GameObject actor) { Jump(actor); }

	private PlayerItemsController _player;

	private void Jump(GameObject actor)
	{
		if (_player == null) _player = actor.GetComponent<PlayerItemsController>();
		//_player.Jump(true);
	}
}
