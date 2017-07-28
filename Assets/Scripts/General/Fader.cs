using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {
	public delegate void FadeInFinished();
	public delegate void FadeOutFinished();
	public event FadeInFinished OnFadeInFinished;
	public event FadeOutFinished OnFadeOutFinished;

	Animator faderAnim;
	int fadeState = -1;

	void Awake()
	{ 
		faderAnim = GetComponent<Animator>(); 
		this.gameObject.SetActive(true); 
	}

	public void FadeIn(){ fadeState = 0; DoFade(); }
	public void FadeOut(){ fadeState = 1; DoFade(); }

	void DoFade(){ faderAnim.SetInteger("State",fadeState); }

	void OnFadingDone()
	{
		if(fadeState == 0){ if(OnFadeInFinished != null) OnFadeInFinished(); }
		else if(fadeState == 1) { if(OnFadeOutFinished != null) OnFadeOutFinished(); }

	}
}
