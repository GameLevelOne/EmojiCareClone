using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiUnlockConditions : MonoBehaviour {
	private static EmojiUnlockConditions instance;
	public static EmojiUnlockConditions Instance {
		get{ return instance;}
	}

	public delegate void EmotionUnlock(int index);
	public event EmotionUnlock OnEmotionUnlock;

	const int gatherIconTarget = 250;
	const int saveCoinTarget = 25000;
	const int spentCoinTarget = 10000;
	const int tapCount1 = 100;
	const int tapCount2 = 500;
	const int tapCount3 = 1000;
	const int loginCountTarget = 5;
	const int loginInterval = 7;
	const int goToSettingsCountTarget = 10;
	const int totalPlaytimeTarget = 30; //in hours
	const int sendOffTarget = 5;
	const float EmojiStatsThreshold = 0.25f;

//	bool[,] emojiUnlockValue = new bool[1,40];

	void Awake(){
		if(instance != null && instance != this) { 
			Destroy(gameObject);
			return; 
		}
		else instance = this;
	}

	public void CheckUnlock (UnlockCondition condition)
	{
		Emoji playerEmoji = PlayerData.Instance.PlayerEmoji;

		//1
			   if (condition == UnlockCondition.CriticalHunger) {
			if ((float)(playerEmoji.emojiHungerMod / (float)playerEmoji.emojiHunger) <= EmojiStatsThreshold) ValidateUnlockCondition(playerEmoji,condition);
		//2
		} else if (condition == UnlockCondition.CriticalHygiene) {
			if ((float)(playerEmoji.emojiHygeneMod / (float)playerEmoji.emojiHygene) <= EmojiStatsThreshold) ValidateUnlockCondition(playerEmoji,condition);
		//3
		} else if (condition == UnlockCondition.CriticalHappiness) {
			if ((float)(playerEmoji.emojiHappinessMod / (float)playerEmoji.emojiHappiness) <= EmojiStatsThreshold) ValidateUnlockCondition(playerEmoji,condition);
		//4
		} else if (condition == UnlockCondition.CriticalHealth) {
			if ((float)(playerEmoji.emojiHealthMod /(float) playerEmoji.emojiHealth) <= EmojiStatsThreshold) ValidateUnlockCondition(playerEmoji,condition);
		//5
		} else if (condition == UnlockCondition.EmptyHunger) {
			if ((float)(playerEmoji.emojiHungerMod / (float)playerEmoji.emojiHunger) <= 0) ValidateUnlockCondition(playerEmoji,condition);
		//6
		} else if (condition == UnlockCondition.EmptyHygiene) {
			if ((float)(playerEmoji.emojiHygeneMod /(float) playerEmoji.emojiHygene) <= 0) ValidateUnlockCondition(playerEmoji,condition);
		//7
		} else if (condition == UnlockCondition.EmptyHappiness) {
			if ((float)(playerEmoji.emojiHappinessMod / (float)playerEmoji.emojiHappiness) <= 0) ValidateUnlockCondition(playerEmoji,condition);
		//8
		} else if (condition == UnlockCondition.Empty3) {
			if ((float)(playerEmoji.emojiHungerMod / (float) playerEmoji.emojiHunger) <= 0 &&
				(float)(playerEmoji.emojiHygeneMod / (float)playerEmoji.emojiHygene) <= 0 &&
				(float)(playerEmoji.emojiHappinessMod / (float)playerEmoji.emojiHappiness) <= 0) ValidateUnlockCondition(playerEmoji,condition);
		//9
		} else if (condition == UnlockCondition.FullFed) {
			if (playerEmoji.emojiHungerMod >= playerEmoji.emojiHunger) ValidateUnlockCondition(playerEmoji,condition);
		//10
		} else if (condition == UnlockCondition.FullHygiene) {
			if (playerEmoji.emojiHygeneMod >= playerEmoji.emojiHygene) ValidateUnlockCondition(playerEmoji,condition);
		//11
		} else if (condition == UnlockCondition.FullHappiness) {
			if (playerEmoji.emojiHappinessMod == playerEmoji.emojiHappiness) ValidateUnlockCondition(playerEmoji,condition);
		//12
		} else if (condition == UnlockCondition.FullAll) {
			if ((playerEmoji.emojiHungerMod >= playerEmoji.emojiHunger) && 
				(playerEmoji.emojiHygeneMod >= playerEmoji.emojiHygene) && 
				(playerEmoji.emojiHappinessMod >= playerEmoji.emojiHappiness) && 
				(playerEmoji.emojiHealthMod >= playerEmoji.emojiHealth)) ValidateUnlockCondition(playerEmoji,condition);
		//13
		} else if (condition == UnlockCondition.FeedPosIconCount) {
			if (playerEmoji.emojiFeedPositiveCount == gatherIconTarget) ValidateUnlockCondition(playerEmoji,condition);
		//14
		} else if (condition == UnlockCondition.CleanPosIconCount) {
			if (playerEmoji.emojiCleanPositiveCount == gatherIconTarget) ValidateUnlockCondition(playerEmoji,condition);
		//15
		} else if (condition == UnlockCondition.PlayPosIconCount) {
			if (playerEmoji.emojiPlayPositiveCount == gatherIconTarget) ValidateUnlockCondition(playerEmoji,condition);
		//16
		} else if (condition == UnlockCondition.NursePosIconCount) {
			if (playerEmoji.emojiNursePositiveCount == gatherIconTarget) ValidateUnlockCondition(playerEmoji,condition);
		//17
		} else if (condition == UnlockCondition.FeedNegIconCount) {
			if (playerEmoji.emojiFeedNegativeCount == gatherIconTarget) ValidateUnlockCondition(playerEmoji,condition);
		//18
		} else if (condition == UnlockCondition.CleanNegIconCount) {
			if (playerEmoji.emojiCleanNegativeCount == gatherIconTarget) ValidateUnlockCondition(playerEmoji,condition);
		//19
		} else if (condition == UnlockCondition.PlayNegIconCount) {
			if (playerEmoji.emojiPlayNegativeCount == gatherIconTarget) ValidateUnlockCondition(playerEmoji,condition);
		//20
		} else if (condition == UnlockCondition.NurseNegIconCount) {
			if (playerEmoji.emojiNurseNegativeCount == gatherIconTarget) ValidateUnlockCondition(playerEmoji,condition);
		//21
		} else if (condition == UnlockCondition.TapCount1) {
			if (playerEmoji.emojiTapCount == tapCount1) ValidateUnlockCondition(playerEmoji,condition);
		//22
		} else if (condition == UnlockCondition.TapCount2) {
			if (playerEmoji.emojiTapCount == tapCount2) ValidateUnlockCondition(playerEmoji,condition);
		//23
		} else if (condition == UnlockCondition.TapCount3) {
			if (playerEmoji.emojiTapCount == tapCount3) ValidateUnlockCondition(playerEmoji,condition);
		}
//			if (CountUnlockedEmojis () == emojiUnlockValue.Length / 2) {
//				emojiUnlockValue [0, (int)UnlockCondition.Collection1] = true;
//				collectionManager.UnlockEmoji ((int)UnlockCondition.Collection1);
//			} else if (CountUnlockedEmojis () == emojiUnlockValue.Length - 1) {
//				emojiUnlockValue [0, (int)UnlockCondition.CollectionAll] = true;
//				collectionManager.UnlockEmoji ((int)UnlockCondition.CollectionAll);
//			}
//		}

//		if (conditionFulfilled) {
//			emojiUnlockValue[0,(int)condition] = true;
			//collectionManager.UnlockEmoji((int)condition);
//		}
	}

//	int CountUnlockedEmojis ()
//	{
//		int count = 0;
//		for (int i = 0; i < emojiUnlockValue.Length; i++) {
//			if (emojiUnlockValue [0,i] == true) {
//				count++;
//			}
//		}
//		return count;
//	}

	void ValidateUnlockCondition(Emoji playerEmoji, UnlockCondition condition)
	{
		if(playerEmoji.GetCollection((int)condition) > 0) return;
		else playerEmoji.SetCollection((int)condition,1);

		if(OnEmotionUnlock != null) OnEmotionUnlock((int)condition);
	}

	const string KEY_LOGINTIME = "LoginTime";
	const string KEY_LOGINCOUNT = "LoginCount";
	const string KEY_PLAYTIME = "PlayTime";
	const string KEY_SETTINGSCOUNT = "SettingsCount";
	const string KEY_SENDOFFCOUNT = "SendOffCount";

	public DateTime LoginTime{
		get{ return DateTime.Parse(PlayerPrefs.GetString(KEY_LOGINTIME)); }
		set{ PlayerPrefs.SetString(KEY_LOGINTIME,value.ToString()); }
	}

	public int TotalPlaytime {
		get{ return PlayerPrefs.GetInt (KEY_PLAYTIME, 0); }
		set{ PlayerPrefs.SetInt (KEY_PLAYTIME, value); }
	}

	public int TotalLoginCount {
		get{ return PlayerPrefs.GetInt (KEY_LOGINCOUNT, 0); }
		set{ PlayerPrefs.SetInt(KEY_LOGINCOUNT,value);}
	}

	public int TotalSettingsCount { //count how many times user goes to setting menu
		get{ return PlayerPrefs.GetInt (KEY_SETTINGSCOUNT, 0); }
		set{ PlayerPrefs.SetInt(KEY_SETTINGSCOUNT,value);}
	}

	public int SendOffCount {
		get{ return PlayerPrefs.GetInt (KEY_SENDOFFCOUNT, 0); }
		set{ PlayerPrefs.SetInt(KEY_SENDOFFCOUNT,value);}
	}

	//to be called on every time game starts
//	public void CountLogin ()
//	{
//		if (PlayerPrefs.HasKey (KEY_LOGINTIME)) {
//			if (DateTime.Now.CompareTo (LoginTime) < 0)
//				return;
//			else if (DateTime.Now.CompareTo (LoginTime) > 0) {
//				TimeSpan duration = LoginTime - DateTime.Now;
//				if (duration.Days == 1) {
//					TotalLoginCount++;
//				} else if (duration.Days >= loginInterval) {
//					CheckUnlock (UnlockCondition.Comeback);
//				}
//				if (TotalLoginCount >= loginCountTarget) {
//					CheckUnlock (UnlockCondition.LoginCount);
//				}
//			}
//		}
//	}

	public bool CountSettings ()
	{
		TotalSettingsCount++;
		if (TotalSettingsCount == goToSettingsCountTarget) {
			return true;
		} else
			return false;
	}

//	public void CountPlaytime ()
//	{
//		TimeSpan duration = DateTime.Now - LoginTime;
//		int hours = duration.Days * 24 + duration.Hours;
//		TotalPlaytime += hours;
//		if (TotalPlaytime >= totalPlaytimeTarget) {
//			CheckUnlock (UnlockCondition.LongPlay);
//		}
//	}
//
//	public void CountSendOff ()
//	{
//		SendOffCount++;
//		if (SendOffCount >= sendOffTarget) {
//			CheckUnlock (UnlockCondition.SendOff);
//		}
//	}
}
