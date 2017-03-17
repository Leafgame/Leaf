using Assets.Scripts.PlayerScripts;
using UnityEngine;

public class JumpCommand : Command
{
	public override void Execute(GameObject actor) { Jump(actor); }

	private PlatformerCharacter2D _player;

	private void Jump(GameObject actor)
	{
		if (_player == null) _player = actor.GetComponent<PlatformerCharacter2D>();
		//_player.Jump(true);
	}
}
