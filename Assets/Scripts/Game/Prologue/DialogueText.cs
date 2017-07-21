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

	public void Show (string content, int clickCount)
	{
		//textContent = content;
		thisText.text=content;
//		if (clickCount == 0) {
//			StartCoroutine (CoroutineAnimateText ());
//		} else {
//			Debug.Log("stop");
//			StopCoroutine(CoroutineAnimateText());
//			thisText.text = textContent;
//		}
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