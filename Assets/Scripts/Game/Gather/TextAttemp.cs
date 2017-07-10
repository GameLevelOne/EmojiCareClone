using UnityEngine.UI;
using UnityEngine;

public class TextAttemp : MonoBehaviour {
	Animator thisAnim;
	Text thisText;

	void Awake()
	{
		thisAnim = GetComponent<Animator>();
		thisText = GetComponent<Text>();
	}

	public void SetAttemp(int attemp){
		thisText.text = attemp.ToString();
		thisAnim.SetTrigger("Hit");
	}
}
