using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SubScene{
	UI_MAIN,
	UI_GATHER,
	UI_CAPTUREALIEN,
}

public class SceneMainManager : MonoBehaviour {
	public List<GameObject> uiSubScenes;
	public Fader fader;

	SubScene tempSubScene;
	int gatherCategory = -1;

	void Start(){
		tempSubScene = SubScene.UI_MAIN;
		ShowSubScene();
	}

	/// <summary>show 1 UI Object at a time, fader included</summary>
	void  ShowSubScene()
	{
		foreach(GameObject UIObj in uiSubScenes) UIObj.SetActive(false); 
		uiSubScenes[(int)tempSubScene].SetActive(true);
		if(tempSubScene == SubScene.UI_MAIN && PlayerData.Instance.playerAlienID != -1){
			fader.OnFadeInFinished += UpdateStatsAfterGather;
		}
		else if(tempSubScene == SubScene.UI_GATHER){
			uiSubScenes[(int)SubScene.UI_GATHER].GetComponent<UIGather>().InitUIGather((AlienNeedCategory)gatherCategory);
		} 

		fader.OnFadeOutFinished -= ShowSubScene;
		fader.FadeIn();
	}

	void UpdateStatsAfterGather()
	{
		fader.OnFadeInFinished -= UpdateStatsAfterGather;
		MainHUDController hudController = uiSubScenes[0].GetComponent<UIMain>().mainHUD;
		hudController.UpdateStatsMeter();
	}

	public void ChangeSubScene(int subScene)
	{
		tempSubScene = (SubScene)subScene;
		fader.OnFadeOutFinished += ShowSubScene;
		fader.FadeOut();
	}

	public void ChangeToGatherSubScene(int category)
	{
		tempSubScene = SubScene.UI_GATHER;
		gatherCategory = category;
		fader.OnFadeOutFinished += ShowSubScene;
		fader.FadeOut();
	}
}