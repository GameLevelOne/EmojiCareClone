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
		gameObject.SetActive(false);
	}

	public void ButtonSoundOnClick()
	{
		int tempSound = PlayerPrefs.GetInt("Settings/Sound",0);
		if(tempSound == 1) tempSound = 0;
		else tempSound = 1;
		
		imageButtonSound.sprite = sprSound[tempSound];
		PlayerPrefs.SetInt("Settings/Sound",tempSound);
	}

	public void ButtonCreditOnClick()
	{
		//show credit
	}
}
