using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICaptureAlien : MonoBehaviour {
	public CapturePanelController capturePanelController;
	public PanelGetAlien panelGetAlien;

	void OnEnable()
	{
		InitCaptureAlien();
	}

	void OnDisable()
	{
		panelGetAlien.onSubmitNameDone -= OnSubmitNameDone;
		capturePanelController.OnPlayerCapturedAlien -= OnPlayerCapturedAlien;
	}

	void InitCaptureAlien(){
		if(!TutorialManager.Instance.TutorialDone) TutorialManager.Instance.ShowTutorial();
		panelGetAlien.gameObject.SetActive(false);
		capturePanelController.InitCaptureAlien();

		capturePanelController.OnPlayerCapturedAlien += OnPlayerCapturedAlien;
		panelGetAlien.onSubmitNameDone += OnSubmitNameDone;
	}

	void OnPlayerCapturedAlien(int index)
	{
		PlayerData.Instance.SetPlayerAlien(index);
		panelGetAlien.gameObject.SetActive(true);
		panelGetAlien.SetAlienPhoto(PlayerData.Instance.alienData[index].alienSO.spriteFullBody);
	}

	void OnSubmitNameDone()
	{
		transform.parent.GetComponent<SceneMainManager>().ChangeSubScene((int)SubScene.UI_MAIN);
	}
}