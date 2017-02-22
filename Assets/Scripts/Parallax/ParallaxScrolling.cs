using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
	/// <summary>
	/// Parallax scrolling script that should be assigned to a layer
	/// </summary>
	public class ParallaxScrolling : MonoBehaviour
	{
		/// <summary>
		/// Scrolling speed
		/// </summary>
		public Vector2 Speed = new Vector2(2, 2);

		/// <summary>
		/// Moving direction
		/// </summary>
		public Vector2 Direction = new Vector2(-1, 0);

		/// <summary>
		/// Movement should be applied to camera
		/// </summary>
		public bool IsLinkedToCamera = false;

		/// <summary>
		/// 1 - Background is infinite
		/// </summary>
		public bool IsLooping = false;

		/// <summary>
		/// 2 - List of children with a renderer.
		/// </summary>
		private List<SpriteRenderer> _backgroundPart;


		protected virtual void Start()
		{
			// For infinite background only
			if (IsLooping)
			{
				// Get all the children of the layer with a renderer
				_backgroundPart = new List<SpriteRenderer>();

				for (var i = 0; i < transform.childCount; i++)
				{
					var child = transform.GetChild(i);
					var spriteRenderer = child.GetComponent<SpriteRenderer>();

					// Add only the visible children
					if (spriteRenderer != null)
					{
						_backgroundPart.Add(spriteRenderer);
					}
				}
				_backgroundPart = _backgroundPart.OrderBy(
				  t => t.transform.position.x
				).ToList();
			}
		}

		protected virtual void Update()
		{
			var movement = new Vector3(Speed.x * Direction.x, Speed.y * Direction.y, 0);
			movement *= Time.deltaTime;
			transform.Translate(movement);

			// 4 - Loop
			if (IsLooping)
			{
				// Get the first object.
				// The list is ordered from left (x position) to right.
				var firstChild = _backgroundPart.FirstOrDefault();

				if (firstChild != null)
				{
					// Check if the child is already (partly) before the camera.
					// We test the position first because the IsVisibleFrom
					// method is a bit heavier to execute.
					if (firstChild.transform.position.x < Camera.main.transform.position.x)
					{
						// If the child is already on the left of the camera,
						// we test if it's completely outside and needs to be
						// recycled.
						if (firstChild.IsVisibleFrom(Camera.main) == false)
						{
							// Get the last child position.
							var lastChild = _backgroundPart.LastOrDefault();

							var lastPosition = lastChild.transform.position;
							var lastSize = (lastChild.bounds.max - lastChild.bounds.min);

							// Set the position of the recyled one to be AFTER
							// the last child.
							// Note: Only work for horizontal scrolling currently.
							firstChild.transform.position = new Vector3(lastPosition.x + lastSize.x, firstChild.transform.position.y,
								firstChild.transform.position.z);

							// Set the recycled child to the last position
							// of the backgroundPart list.
							_backgroundPart.Remove(firstChild);
							_backgroundPart.Add(firstChild);
						}
					}
				}
			}
		}
	}
}
