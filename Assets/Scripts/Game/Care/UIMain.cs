using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMain : MonoBehaviour {
	public MainHUDController mainHUD;
	public GameObject panelGetEmoji;
	public GameObject panelSettings;
	public GameObject textEmojiDead;

	[HideInInspector] public GameObject tempAlien;

	public void Init()
	{
		if(PlayerData.Instance.playerEmojiID == -1){
			mainHUD.gameObject.SetActive(false);
			panelGetEmoji.SetActive(true);
			if(PlayerData.Instance.emojiDead) textEmojiDead.SetActive(true);
			else textEmojiDead.SetActive(false);
		}else{
			if(!TutorialManager.Instance.TutorialDone) TutorialManager.Instance.ShowTutorial();
			PlayerData.Instance.LoadPlayerEmoji();
			panelGetEmoji.SetActive(false);
			if(PlayerData.Instance.PlayerEmoji.emojiName == string.Empty)
			mainHUD.gameObject.SetActive(true);
			mainHUD.Init();
		}
	}

	public void ButtonSettingsOnClick()
	{
		EmojiUnlockConditions.Instance.CheckUnlock(UnlockCondition.GoToSettings);
		SoundManager.Instance.PlaySFX(eSFX.BUTTON);
		panelSettings.GetComponent<Animator>().SetTrigger("Show");
	}

	public void ButtonGetEmojiOnClick()
	{
		PlayerData.Instance.SetPlayerEmoji(0);
		Init();
	}
}