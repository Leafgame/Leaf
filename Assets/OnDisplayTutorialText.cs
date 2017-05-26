using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnDisplayTutorialText : MonoBehaviour
{
	public string Key = "Key";
	public string Action = "Action";
	public Text Text;

	void Start ()
	{
		Text = transform.parent.GetComponent<Text>();
		Text.text = "Press " + Key + " to " + Action;
		StartCoroutine(DisableText(0.1f, 0f));
	}

	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag != "Player") return;
		StartCoroutine(EnableText(1f));
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag != "Player") return;
		StartCoroutine(DisableText(1f, 3f));


	}

	private IEnumerator DisableText(float fadeTime, float extraDelay)
	{
		yield return new WaitForSeconds(extraDelay);
		Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, 1);
		while (Text.color.a > 0.0f)
		{
			Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, Text.color.a - (Time.deltaTime / fadeTime));
			yield return null;
		}
	}

	private IEnumerator EnableText(float fadeTime)
	{
		Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, 0);
		while (Text.color.a < 1.0f)
		{
			Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, Text.color.a + (Time.deltaTime / fadeTime));
			yield return null;
		}
	}

}
