using UnityEngine.UI;
using UnityEngine;

public class TextMod : MonoBehaviour {
	Animator thisAnim;
	Text thisText;

	void Awake()
	{
		thisAnim = GetComponent<Animator>();
		thisText = GetComponent<Text>();
	}

	public void Animate(int mod)
	{
		if(mod == 0) return;
		else if(mod > 0) thisText.text = "+"+mod;
		else if(mod < 0) thisText.text = "-"+mod;
		thisAnim.SetInteger("mod",mod);
//		print("terpanggil");
	}

	/// <summary>this method is used as an animation trigger</summary>
	void ResetAnimatorParameterModValue()
	{
		thisAnim.SetInteger("mod",0);
	}
}
