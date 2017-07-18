using UnityEngine.UI;
using UnityEngine;

public class UISettings : MonoBehaviour {
	public Image imageButtonSound;
	public Sprite[] sprSound;

	void OnEnable()
	{
		InitSettings();
	}

	void InitSettings()
	{
		int tempSound = PlayerPrefs.GetInt("Settings/Sound",0);
		imageButtonSound.sprite = sprSound[tempSound];
	}

	public void ButtonCloseOnClick()
	{
		SoundManager.Instance.PlaySFX(eSFX.BUTTONX);
		GetComponent<Animator>().SetTrigger("Hide");
	}

	public void ButtonSoundOnClick()
	{
		float tempSound = PlayerPrefs.GetFloat("Settings/Sound",0);
		if(tempSound == 1f) tempSound = 0f;
		else tempSound = 1f;
		
		imageButtonSound.sprite = sprSound[(int)tempSound];
		PlayerPrefs.SetFloat("Settings/Sound",tempSound);

		SoundManager.Instance.SetVolume(tempSound);
		SoundManager.Instance.PlaySFX(eSFX.BUTTON);
	}

	public void ButtonCreditOnClick()
	{
		SoundManager.Instance.PlaySFX(eSFX.BUTTON);
		//show credit
	}
}