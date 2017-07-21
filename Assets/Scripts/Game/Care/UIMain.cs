using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMain : MonoBehaviour {
	public MainHUDController mainHUD;
	public GameObject panelGetEmoji;
	public GameObject panelSettings;
	public GameObject textAlienDead;

	[HideInInspector] public GameObject tempAlien;

	void OnEnable()
	{
		InitMain();
	}

	void InitMain()
	{
		if(PlayerData.Instance.playerEmojiID == -1){
//			if(!TutorialManager.Instance.TutorialDone) TutorialManager.Instance.ShowTutorial();
			mainHUD.gameObject.SetActive(false);
			panelGetEmoji.SetActive(true);
			if(PlayerData.Instance.emojiDead) textAlienDead.SetActive(true);
			else textAlienDead.SetActive(false);
		}else{
//			if(!TutorialManager.Instance.TutorialDone) TutorialManager.Instance.ShowTutorial();
//			if(!TutorialManager.Instance.TutorialDone && TutorialManager.Instance.TutorialIndex == 6) TutorialManager.Instance.ShowTutorial();
				
			panelGetEmoji.SetActive(false);
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
		InitMain();
	}
}
