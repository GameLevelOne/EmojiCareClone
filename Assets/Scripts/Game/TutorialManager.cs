using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {
	private static TutorialManager instance = null;
	public static TutorialManager Instance{
		get{return instance;}
	}

	const string  Key_Tutorial = "Tutorial";
	const string  Key_Tutorial_Index = "Tutorial/Index";

	public bool TutorialDone{
		get{return PlayerPrefs.GetInt( Key_Tutorial,0) == 1 ? true : false;}
		set{PlayerPrefs.SetInt( Key_Tutorial,value == true ? 1 : 0); }
	}
	public int TutorialIndex{
		get{return PlayerPrefs.GetInt( Key_Tutorial_Index,0);}
		set{PlayerPrefs.SetInt( Key_Tutorial_Index,value);}
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
		print("Tutorial = "+TutorialDone);
	}

	public void ShowTutorial()
	{
		print("TUTORIAL");
		foreach(GameObject a in tutorialObjects) a.SetActive(false);
		tutorialPanel.SetActive(true);
		tutorialObjects[TutorialIndex].SetActive(true);
		
	}

	public void TutorialPanelOnClick()
	{
		tutorialPanel.SetActive(false);
		TutorialIndex++;
		if(!TutorialDone &&TutorialIndex == 2) ShowTutorial();

		if(TutorialIndex == tutorialObjects.Count) TutorialDone = true;
	}

	void OnApplicationQuit()
	{
		if(TutorialIndex <= 2) TutorialIndex = 0;
	}
}