﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiUnlockConditions : MonoBehaviour {
	private static EmojiUnlockConditions instance;
	public static EmojiUnlockConditions Instance {
		get{ return instance;}
	}

	public delegate void EmotionUnlock();
	public event EmotionUnlock OnEmotionUnlock;

	const int gatherIconTarget = 250;
	const int saveCoinTarget = 25000;
	const int spentCoinTarget = 10000;
	const int loginCountTarget = 5;
	const int loginInterval = 7;
	const int goToSettingsCountTarget = 10;
	const int totalPlaytimeTarget = 30; //in hours
	const int sendOffTarget = 5;

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
		float petCriticalThreshold = 0.25f;

//		if (emojiUnlockValue [0, (int)condition] == false) {

		if (condition == UnlockCondition.CriticalHunger) {
			if ((float)(playerEmoji.emojiHunger / playerEmoji.emojiHungerMod) <= petCriticalThreshold) {
				PlayerData.Instance.PlayerEmoji.SetCollection((int)condition,1);
			}
		} else if (condition == UnlockCondition.CriticalHygiene) {
			if ((float)(playerEmoji.emojiHygene / playerEmoji.emojiHygeneMod) <= petCriticalThreshold) {
				PlayerData.Instance.PlayerEmoji.SetCollection((int)condition,1);
			}

		} else if (condition == UnlockCondition.CriticalHappiness) {
			if ((float)(playerEmoji.emojiHappiness / playerEmoji.emojiHappinessMod) <= petCriticalThreshold) {
				PlayerData.Instance.PlayerEmoji.SetCollection((int)condition,1);
			}

		} else if (condition == UnlockCondition.CriticalHealth) {
			if ((float)(playerEmoji.emojiHealth / playerEmoji.emojiHealthMod) <= petCriticalThreshold) {
				PlayerData.Instance.PlayerEmoji.SetCollection((int)condition,1);
			}

		} else if (condition == UnlockCondition.EmptyHunger) {
			if ((float)(playerEmoji.emojiHunger / playerEmoji.emojiHungerMod) <= 0) {
				PlayerData.Instance.PlayerEmoji.SetCollection((int)condition,1);
			}

		} else if (condition == UnlockCondition.EmptyHygiene) {
			if ((float)(playerEmoji.emojiHygene / playerEmoji.emojiHygeneMod) <= 0) {
				PlayerData.Instance.PlayerEmoji.SetCollection((int)condition,1);
			}

		} else if (condition == UnlockCondition.EmptyHappiness) {
			if ((float)(playerEmoji.emojiHappiness / playerEmoji.emojiHappinessMod) <= 0) {
				PlayerData.Instance.PlayerEmoji.SetCollection((int)condition,1);
			}

		} else if (condition == UnlockCondition.Empty3) {
			if ((float)(playerEmoji.emojiHunger / playerEmoji.emojiHungerMod) <= 0 &&
			   (float)(playerEmoji.emojiHygene / playerEmoji.emojiHygeneMod) <= 0 &&
			   (float)(playerEmoji.emojiHappiness / playerEmoji.emojiHappinessMod) <= 0) {
				PlayerData.Instance.PlayerEmoji.SetCollection((int)condition,1);
			}

		} else if (condition == UnlockCondition.FullFed) {
			if (playerEmoji.emojiHunger == playerEmoji.emojiHungerMod) {
				PlayerData.Instance.PlayerEmoji.SetCollection((int)condition,1);
			}

		} else if (condition == UnlockCondition.FullHygiene) {
			if (playerEmoji.emojiHygene == playerEmoji.emojiHygeneMod) {
				PlayerData.Instance.PlayerEmoji.SetCollection((int)condition,1);
			}

		} else if (condition == UnlockCondition.FullHappiness) {
			if (playerEmoji.emojiHappiness == playerEmoji.emojiHappinessMod) {
				PlayerData.Instance.PlayerEmoji.SetCollection((int)condition,1);
			}

		} else if (condition == UnlockCondition.FullAll) {
			if ((playerEmoji.emojiHunger == playerEmoji.emojiHungerMod) && (playerEmoji.emojiHygene == playerEmoji.emojiHygeneMod) && 
			(playerEmoji.emojiHappiness == playerEmoji.emojiHappinessMod) && (playerEmoji.emojiHealth == playerEmoji.emojiHealthMod)) {
				PlayerData.Instance.PlayerEmoji.SetCollection((int)condition,1);
			}

		} else if (condition == UnlockCondition.FeedPosIconCount) {
			if (playerEmoji.emojiFeedPositiveCount == gatherIconTarget) {
				PlayerData.Instance.PlayerEmoji.SetCollection((int)condition,1);
			}

		} else if (condition == UnlockCondition.CleanPosIconCount) {
			if (playerEmoji.emojiCleanPositiveCount == gatherIconTarget) {
				PlayerData.Instance.PlayerEmoji.SetCollection((int)condition,1);
			}

		} else if (condition == UnlockCondition.PlayPosIconCount) {
			if (playerEmoji.emojiPlayPositiveCount == gatherIconTarget) {
				PlayerData.Instance.PlayerEmoji.SetCollection((int)condition,1);
			}

		} else if (condition == UnlockCondition.NursePosIconCount) {
			if (playerEmoji.emojiNursePositiveCount == gatherIconTarget) {
				PlayerData.Instance.PlayerEmoji.SetCollection((int)condition,1);
			}

		} else if (condition == UnlockCondition.FeedNegIconCount) {
			if (playerEmoji.emojiFeedNegativeCount == gatherIconTarget) {
				PlayerData.Instance.PlayerEmoji.SetCollection((int)condition,1);
			}

		} else if (condition == UnlockCondition.CleanNegIconCount) {
			if (playerEmoji.emojiCleanNegativeCount == gatherIconTarget) {
				PlayerData.Instance.PlayerEmoji.SetCollection((int)condition,1);
			}

		} else if (condition == UnlockCondition.PlayNegIconCount) {
			if (playerEmoji.emojiPlayNegativeCount == gatherIconTarget) {
				PlayerData.Instance.PlayerEmoji.SetCollection((int)condition,1);
			}

		} else if (condition == UnlockCondition.NurseNegIconCount) {
			if (playerEmoji.emojiNurseNegativeCount == gatherIconTarget) {
				PlayerData.Instance.PlayerEmoji.SetCollection((int)condition,1);
			}

		} else if (condition == UnlockCondition.SpendCoin) {
			if (PlayerData.Instance.playerSpentCoin == spentCoinTarget) {
				PlayerData.Instance.PlayerEmoji.SetCollection((int)condition,1);
			}

		} else if (condition == UnlockCondition.CoinCount) {
			if (PlayerData.Instance.playerCoin == saveCoinTarget) {
				PlayerData.Instance.PlayerEmoji.SetCollection((int)condition,1);
			}

		} else if (condition == UnlockCondition.GoToSettings) {
			if (CountSettings ()) {
				PlayerData.Instance.PlayerEmoji.SetCollection((int)condition,1);
			}
		}

		if(OnEmotionUnlock != null) OnEmotionUnlock();

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
	public void CountLogin ()
	{
		if (PlayerPrefs.HasKey (KEY_LOGINTIME)) {
			if (DateTime.Now.CompareTo (LoginTime) < 0)
				return;
			else if (DateTime.Now.CompareTo (LoginTime) > 0) {
				TimeSpan duration = LoginTime - DateTime.Now;
				if (duration.Days == 1) {
					TotalLoginCount++;
				} else if (duration.Days >= loginInterval) {
					CheckUnlock (UnlockCondition.Comeback);
				}
				if (TotalLoginCount >= loginCountTarget) {
					CheckUnlock (UnlockCondition.LoginCount);
				}
			}
		}
	}

	public bool CountSettings ()
	{
		TotalSettingsCount++;
		if (TotalSettingsCount == goToSettingsCountTarget) {
			return true;
		} else
			return false;
	}

	public void CountPlaytime ()
	{
		TimeSpan duration = DateTime.Now - LoginTime;
		int hours = duration.Days * 24 + duration.Hours;
		TotalPlaytime += hours;
		if (TotalPlaytime >= totalPlaytimeTarget) {
			CheckUnlock (UnlockCondition.LongPlay);
		}
	}

	public void CountSendOff ()
	{
		SendOffCount++;
		if (SendOffCount >= sendOffTarget) {
			CheckUnlock (UnlockCondition.SendOff);
		}
	}
}
