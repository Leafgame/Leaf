using Assets.Scripts;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
	[CustomEditor(typeof(ParallaxOption))]
	public class ParallaxOptionEditor : UnityEditor.Editor {

		private ParallaxOption _options;

		protected virtual void Awake() {
			_options = (ParallaxOption)target;
		}

		public override void OnInspectorGUI() {
			DrawDefaultInspector();

			if(GUILayout.Button("Save Position")) {
				_options.SavePosition();
				EditorUtility.SetDirty(_options);
			}

			if(GUILayout.Button("Restore Position"))
				_options.RestorePosition();
		}
	}
}