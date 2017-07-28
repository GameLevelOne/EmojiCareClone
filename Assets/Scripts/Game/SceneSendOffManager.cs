using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class SceneSendOffManager : MonoBehaviour {
	public Fader fader;
	public Animator sendOffAnim;
	public Text textCompletion;
	public Text textDuration;



	IEnumerator Start(){
		yield return new WaitForSeconds(1f);
		PlayerData.Instance.GameDuration = DateTime.Now;
		DoSendOff();
	}

	void DoSendOff()
	{
		
		textCompletion.text = "COMPLETION: "+CalculateCompletion()+" %";
		textDuration.text = "Duration: "+CalculateDuration();

		PlayerData.Instance.PlayerEmoji.ResetData();
		PlayerData.Instance.ResetData();
		PlayerData.Instance.playerEmojiID = -1;

		sendOffAnim.SetTrigger("Animate");
	}


	string CalculateCompletion()
	{
		int complete = 0;
		for(int i = 0;i<PlayerData.Instance.PlayerEmoji.collectionSO.Length;i++){
			if(PlayerData.Instance.PlayerEmoji.GetCollection(i) > 0) complete++;
		}

		return (((float)complete / PlayerData.Instance.PlayerEmoji.collectionSO.Length)*100).ToString("F2");
	}

	string CalculateDuration()
	{
		TimeSpan duration = DateTime.Now - PlayerData.Instance.GameDuration;

		return duration.Days.ToString()+"D:"+duration.Hours.ToString()+"H:"+duration.Minutes.ToString()+"M:"+duration.Seconds+"S";
	}

	public void ButtonOkOnClick(){
		SoundManager.Instance.PlaySFX(eSFX.BUTTON);
		PlayerData.Instance.gameStatus = GameStatus.SEND_OFF;
		fader.OnFadeOutFinished += GoToSceneStork;
		fader.FadeOut();
	}

	void GoToSceneStork()
	{
		fader.OnFadeOutFinished -= GoToSceneStork;
		SceneManager.LoadScene("SceneStork");
	}
}
