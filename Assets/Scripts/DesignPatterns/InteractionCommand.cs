using UnityEngine;

public class InteractionCommand : Command
{
	public override void Execute(GameObject actor)
	{
		Interact(actor);
	}

	private void Interact(GameObject actor)
	{
		
	}
}
