using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneTitleManager : MonoBehaviour {
	public Fader fader;

	void Start()
	{
//		PlayerPrefs.DeleteAll();
		fader.OnFadeOutFinished += LoadSceneMain;
	}

	void LoadSceneMain()
	{
		fader.OnFadeInFinished -= LoadSceneMain;
		SceneManager.LoadScene(1);
	}

	public void ButtonPlayOnClick(){
		fader.FadeOut();
	}
}
