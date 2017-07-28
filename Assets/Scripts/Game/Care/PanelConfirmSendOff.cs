using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelConfirmSendOff : MonoBehaviour {
	public Fader fader;
	Animator thisAnim;

	void Awake()
	{
		thisAnim = GetComponent<Animator>();
	}

	public void ButtonOkOnClick()
	{
		SoundManager.Instance.PlaySFX(eSFX.BUTTON);
		fader.OnFadeOutFinished += GoToSceneSendOff;
		fader.FadeOut();
	}

	public void ButtonXOnClick()
	{
		thisAnim.SetTrigger("Hide");
		SoundManager.Instance.PlaySFX(eSFX.BUTTONX);
	}

	void GoToSceneSendOff()
	{
		fader.OnFadeOutFinished -= GoToSceneSendOff;
		SceneManager.LoadScene("SceneSendOff");
	}
}
