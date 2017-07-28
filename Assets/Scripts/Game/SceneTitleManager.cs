using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneTitleManager : MonoBehaviour {
	public Fader fader;

	void Start()
	{
		PlayerPrefs.DeleteAll();
		SoundManager.Instance.PlayBGM(eBGM.TITLE);
		fader.OnFadeOutFinished += LoadSceneMain;
	}

	void LoadSceneMain()
	{
		fader.OnFadeInFinished -= LoadSceneMain;
		if(PlayerData.Instance.playerDonePrologue == 0) SceneManager.LoadScene("SceneStork");
		else SceneManager.LoadScene("SceneMain");
	}

	public void ButtonPlayOnClick(){
		SoundManager.Instance.PlaySFX(eSFX.GATHER_RESULT);
		fader.FadeOut();
	}
}