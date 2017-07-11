using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMain : MonoBehaviour {
	public MainHUDController mainHUD;
	public GameObject panelCaptureAlien;
	public GameObject panelSettings;
	public GameObject textAlienDead;

	[HideInInspector] public GameObject tempAlien;

	void OnEnable()
	{
		InitMain();
	}

	void InitMain()
	{
		if(PlayerData.Instance.playerAlienID == -1){
			if(!TutorialManager.Instance.TutorialDone) TutorialManager.Instance.ShowTutorial();
			mainHUD.gameObject.SetActive(false);
			panelCaptureAlien.SetActive(true);
			if(PlayerData.Instance.alienDead) textAlienDead.SetActive(true);
			else textAlienDead.SetActive(false);
		}else{
			if(!TutorialManager.Instance.TutorialDone) TutorialManager.Instance.ShowTutorial();
			if(!TutorialManager.Instance.TutorialDone && TutorialManager.Instance.TutorialIndex == 6) TutorialManager.Instance.ShowTutorial();
				
			panelCaptureAlien.SetActive(false);
			mainHUD.gameObject.SetActive(true);
			mainHUD.UpdateAlienNameAndType();
			mainHUD.UpdateAlienLevelAndGrowth();
			mainHUD.UpdatePlayerCoin();
			mainHUD.InitStats();
		}
	}

	public void ButtonSettingsOnClick()
	{
		panelSettings.SetActive(true);
	}
}
