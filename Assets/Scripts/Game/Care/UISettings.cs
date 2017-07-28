using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour {
	public Image imageSound;
	public Sprite[] spriteSound;
	Animator thisAnim;

	void Awake()
	{
		thisAnim = GetComponent<Animator>();
	}

	public void Show()
	{
		Init();
		thisAnim.SetTrigger("Show");
	}

	void Init()
	{
		imageSound.sprite = spriteSound[(int)(PlayerPrefs.GetFloat("Settings/Sound",1))];
	}

	void Hide()
	{
		thisAnim.SetTrigger("Hide");
	}

	public void ButtonSoundOnClick()
	{
		SoundManager.Instance.PlaySFX(eSFX.BUTTON);
		if(PlayerPrefs.GetFloat("Settings/Sound",1) == 1f){
			SoundManager.Instance.SetVolume(0f);
		}else if(PlayerPrefs.GetFloat("Settings/Sound",1) == 0f){
			SoundManager.Instance.SetVolume(1f);
		}
		imageSound.sprite = spriteSound[(int)(PlayerPrefs.GetFloat("Settings/Sound",1))];
	}

	public void ButtonCreditOnClick()
	{
		SoundManager.Instance.PlaySFX(eSFX.BUTTON);
	}

	public void ButtonOkOnClick()
	{
		SoundManager.Instance.PlaySFX(eSFX.BUTTON);
		Hide();
	}
}
