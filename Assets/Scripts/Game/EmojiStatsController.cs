using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EmojiStatsController : MonoBehaviour {
	private static EmojiStatsController instance = null;
	public static EmojiStatsController Instance{
		get{ return instance; }
	}

	const string Key_LastTimePlay = "LastTimePlayed";
	const string Key_LastTimePaused = "LastTimePaused";

	public DateTime lastTimePlay{
		get{return DateTime.Parse(PlayerPrefs.GetString(Key_LastTimePlay));}
		set{PlayerPrefs.SetString(Key_LastTimePlay,value.ToString());}
	}
	public DateTime lastTimePaused{
		get{ return DateTime.Parse(PlayerPrefs.GetString(Key_LastTimePaused));}
		set{PlayerPrefs.SetString(Key_LastTimePaused,value.ToString());}
	}

	bool hasDoneInit = false;
	public bool isTickingStats = false;
	int tickDelay = 60;
	float statsTreshold = 0.25f;

	Emoji playerEmoji;

	void Awake()
	{
		if(instance != null && instance != this) { 
			Destroy(gameObject);
			return; 
		}
		else instance = this;

		CancelNotification();
	}

	void CancelNotification()
	{
		for (int i = 0; i < 5; i++) {
			try { LocalNotification.CancelNotification(i); }
			catch (Exception e) { Debug.LogWarning("LocalNotification is not running: "+e.ToString()); }
		}
	}

	public void Init()
	{
		if(PlayerData.Instance.playerEmojiID == -1){
			PlayerPrefs.DeleteKey(Key_LastTimePlay);
			PlayerPrefs.DeleteKey(Key_LastTimePaused);
		}else{
			if(!hasDoneInit){
				hasDoneInit = true;
				playerEmoji = PlayerData.Instance.PlayerEmoji;
				CalculateEmojiStats();
			}
		}
	}

	void CalculateEmojiStats()
	{
		if(PlayerPrefs.HasKey(Key_LastTimePlay)){
			if(DateTime.Now.CompareTo(lastTimePlay) < 0) return;
			else if(DateTime.Now.CompareTo(lastTimePlay) > 0){
				int totalTicks = GetTotalTicks(DateTime.Now - lastTimePlay);
				PlayerPrefs.DeleteKey(Key_LastTimePlay);
				playerEmoji.TickStats(totalTicks);
			}
		}

		if(PlayerData.Instance.playerEmojiID != 1 && !isTickingStats) 
			StartCoroutine(CoroutineStartTickStats());
	}

	void CalculateEmojiStatsAfterPause()
	{
		if(PlayerPrefs.HasKey(Key_LastTimePaused)){
			if(DateTime.Now.CompareTo(lastTimePaused) < 0) return;
			else if(DateTime.Now.CompareTo(lastTimePaused) > 0){
				int totalTicks = GetTotalTicks(DateTime.Now - lastTimePaused);
				PlayerPrefs.DeleteKey(Key_LastTimePaused);
				playerEmoji.TickStats(totalTicks);
			}
		}

		if(PlayerData.Instance.playerEmojiID != 1 && !isTickingStats) 
			StartCoroutine(CoroutineStartTickStats());
	}

	IEnumerator CoroutineStartTickStats()
	{
		isTickingStats = true;
		while(true){
			yield return new WaitForSeconds(tickDelay);
			playerEmoji.TickStats();
		}
	}

	int GetTotalTicks(TimeSpan duration)
	{
		print(duration);
		int daytoMin = duration.Days * 24 * 60;
		int hourToMin = duration.Hours * 60;
		int min = duration.Minutes;
		int secToMin = Mathf.FloorToInt((float)(duration.Seconds / tickDelay));
		return daytoMin + hourToMin + min + secToMin;
	}

	#region plugin methods
	void FireNotifications ()
	{
		if (PlayerData.Instance.playerAlienID != -1) {
			long statDurMS = tickDelay * 1000;

			float hungerTimer = playerEmoji.emojiHungerMod - playerEmoji.emojiHunger * statsTreshold;
			float hygieneTimer = playerEmoji.emojiHygeneMod - playerEmoji.emojiHygene * statsTreshold;
			float happinessTimer = playerEmoji.emojiHappinessMod - playerEmoji.emojiHappiness * statsTreshold;
			float healthTimer = playerEmoji.emojiHealthMod - playerEmoji.emojiHealth * statsTreshold;

			float deathTimer = playerEmoji.emojiHealthMod * statDurMS;

			string gameTitle = "Emoji Care";

			if (hungerTimer > 0) LocalNotification.SendNotification 		( (int)EmojiStats.HUNGER, 	(long)(hungerTimer * statDurMS + 1000), 	gameTitle, "Come feed your emoji!", new Color32 (0xff, 0x44, 0x44, 255));
			else if (hungerTimer <= 0) LocalNotification.SendNotification 	( (int)EmojiStats.HUNGER, 	6000, 										gameTitle, "Come feed your emoji!", new Color32 (0xff, 0x44, 0x44, 255));

			if (hygieneTimer > 0) LocalNotification.SendNotification 		( (int)EmojiStats.HYGENE, 	(long)(hygieneTimer * statDurMS + 2000), 	gameTitle, "Come clean your emoji!", new Color32 (0xff, 0x44, 0x44, 255));
			else if (hygieneTimer <= 0) LocalNotification.SendNotification  ( (int)EmojiStats.HYGENE,  	7000,  										gameTitle, "Come clean your emoji!", new Color32 (0xff, 0x44, 0x44, 255));

			if (happinessTimer > 0) LocalNotification.SendNotification 		( (int)EmojiStats.HAPPINESS,(long)(happinessTimer * statDurMS), 		gameTitle, "Come play with your emoji!", new Color32 (0xff, 0x44, 0x44, 255));
			else if (happinessTimer <= 0) LocalNotification.SendNotification( (int)EmojiStats.HAPPINESS,5000,  										gameTitle, "Come play with your emoji!", new Color32 (0xff, 0x44, 0x44, 255));

			if (healthTimer > 0) LocalNotification.SendNotification 		( (int)EmojiStats.HEALTH,  	(long)(healthTimer * statDurMS + 3000),  	gameTitle, "Your emoji is dying!", new Color32 (0xff, 0x44, 0x44, 255));
			 else if (healthTimer <= 0) LocalNotification.SendNotification 	( (int)EmojiStats.HEALTH+1, (long)(deathTimer+4000), 	 				gameTitle, "Your emoji is dead!",  new Color32 (0xff, 0x44, 0x44, 255));
		}
	}
	#endregion

	void OnApplicationPause(bool pauseStatus)
	{
		if(pauseStatus){
			FireNotifications();
			StopAllCoroutines();
			isTickingStats = false;
			if(PlayerData.Instance.playerEmojiID != -1) lastTimePaused = DateTime.Now;
		}else{
			CancelNotification();
			if(PlayerData.Instance.playerEmojiID != -1 && PlayerPrefs.HasKey(Key_LastTimePaused)) 
				CalculateEmojiStatsAfterPause();
		}
	}

	void OnApplicationQuit()
	{
		StopAllCoroutines();
		isTickingStats = false;
		if(PlayerData.Instance.playerEmojiID != -1) lastTimePlay = DateTime.Now;
		PlayerPrefs.Save();
	}
}