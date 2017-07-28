using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTextAnimation : MonoBehaviour {
	public delegate void NextAction();
	public event NextAction OnNextAction;

	public GameObject Arrow;

	float speed = 2f;

	Text thisText;
	string textContent;

	bool isAnimating = false;

	void Awake()
	{
		thisText = GetComponent<Text>();
	}

	public void Show (string content)
	{
		textContent = content;
		StartCoroutine("CoroutineAnimateText");
	}

	IEnumerator CoroutineAnimateText()
	{
		isAnimating = true;
		Arrow.SetActive(false);
		string temp = string.Empty;
		for(int i = 0;i<textContent.Length;i++){
			temp += textContent[i];
			thisText.text = temp;
			SoundManager.Instance.PlaySFX(eSFX.TYPING);
			yield return new WaitForSeconds(Time.fixedDeltaTime*speed);
		}
		thisText.text = textContent;
		Arrow.SetActive(true);
		isAnimating = false;
	}

	public void ButtonSkipOnClick()
	{
		if(isAnimating){
			StopCoroutine("CoroutineAnimateText");
			isAnimating = false;
			Arrow.SetActive(true);
			thisText.text = textContent;
		}else{
			if(OnNextAction != null) OnNextAction();
		}
	}
}