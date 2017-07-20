using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneTitleManager : MonoBehaviour {
	public Fader fader;

	void Start()
	{
//		PlayerPrefs.DeleteAll();
		SoundManager.Instance.PlayBGM(eBGM.MAIN);
		fader.OnFadeOutFinished += LoadSceneMain;
	}

	void LoadSceneMain()
	{
		fader.OnFadeInFinished -= LoadSceneMain;
		SceneManager.LoadScene(1);
	}

	public void ButtonPlayOnClick(){
		SoundManager.Instance.PlaySFX(eSFX.GATHER_SLOT_POSITIVE);
		fader.FadeOut();
	}
}