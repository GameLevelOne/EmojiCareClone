using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {
	private static TutorialManager instance = null;
	public static TutorialManager Instance{
		get{return instance;}
	}

	const string KEYPREF_TUTORIAL = "Tutorial";
	const string KEYPREF_TUTORIAL_INDEX = "Tutorial/Index";

	public bool TutorialDone{
		get{return PlayerPrefs.GetInt(KEYPREF_TUTORIAL,0) == 1 ? true : false;}
		set{PlayerPrefs.SetInt(KEYPREF_TUTORIAL,value == true ? 1 : 0); }
	}
	public int TutorialIndex{
		get{return PlayerPrefs.GetInt(KEYPREF_TUTORIAL_INDEX,0);}
		set{PlayerPrefs.SetInt(KEYPREF_TUTORIAL_INDEX,value);}
	}

	public GameObject tutorialPanel;
	public List<GameObject> tutorialObjects = new List<GameObject>();

	void Awake()
	{
		if(instance != null && instance != this) { 
			Destroy(gameObject);
			return; 
		}
		else instance = this;
	}

	public void ShowTutorial()
	{
		foreach(GameObject a in tutorialObjects) a.SetActive(false);
		tutorialPanel.SetActive(true);
		tutorialObjects[TutorialIndex].SetActive(true);
		
	}

	public void TutorialPanelOnClick()
	{
		tutorialPanel.SetActive(false);
		TutorialIndex++;
		if(TutorialIndex == 2 || TutorialIndex == 4){
			if(!TutorialDone) ShowTutorial();
		}

		if(TutorialIndex == tutorialObjects.Count) TutorialDone = true;
	}

	void OnApplicationQuit()
	{
		if(TutorialIndex == 1 || TutorialIndex == 2 || TutorialIndex == 3) TutorialIndex = 0;
		else if(TutorialIndex == 5 || TutorialIndex == 6) TutorialIndex = 4;
	}
}