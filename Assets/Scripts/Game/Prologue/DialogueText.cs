using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueText : MonoBehaviour {
	public float animationSpeed = 5f;

	Text thisText;
	string textContent;

	void Awake()
	{
		thisText = GetComponent<Text>();
	}

	public void Show(string content)
	{
		textContent = content;
		StartCoroutine(CoroutineAnimateText());
	}

	IEnumerator CoroutineAnimateText()
	{
		string temp = string.Empty;
		for(int i = 0;i<textContent.Length;i++){
			temp += textContent[i];
			thisText.text = temp;
			yield return new WaitForSeconds(Time.deltaTime*animationSpeed);
		}
	}
}