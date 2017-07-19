using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiUnlockConditions : MonoBehaviour {
	public CollectionManager collectionManager;
	public EmojiUnlockConditions instance;

	const int gatherIconTarget = 250;
	const int tapCountTarget1 = 100;
	const int tapCountTarget2 = 500;
	const int tapCountTarget3 = 1000;
	const int saveCoinTarget = 25000;
	const int spentCoinTarget = 10000;
	const int levelTarget = 10;

	bool[,] emojiUnlockValue = new bool[1,40];

	void Awake(){
		if(instance != null && instance != this) { 
			Destroy(gameObject);
			return; 
		}
		else instance = this;
	}

	public void CheckUnlock (UnlockCondition condition)
	{
		Alien currPet = PlayerData.Instance.PlayerAlien;
		PlayerData instance = PlayerData.Instance;
		float petCriticalThreshold = currPet.GetAlienHappyTreshold ();
		bool conditionFulfilled = false;

		if (condition == UnlockCondition.CriticalHunger) {
			if ((float)(currPet.alienHunger / currPet.alienHungerMod) <= petCriticalThreshold) {
				conditionFulfilled = true;
			}	

		} else if (condition == UnlockCondition.CriticalHygiene) {
			if ((float)(currPet.alienHygene / currPet.alienHygeneMod) <= petCriticalThreshold) {
				conditionFulfilled = true;
			}

		} else if (condition == UnlockCondition.CriticalHappiness) {
			if ((float)(currPet.alienHappiness / currPet.alienHappinessMod) <= petCriticalThreshold) {
				conditionFulfilled = true;
			}

		} else if (condition == UnlockCondition.CriticalHealth) {
			if ((float)(currPet.alienHealth / currPet.alienHealthMod) <= petCriticalThreshold) {
				conditionFulfilled = true;
			}

		} else if (condition == UnlockCondition.EmptyHunger) {
			if ((float)(currPet.alienHunger / currPet.alienHungerMod) <= 0) {
				conditionFulfilled = true;
			}

		} else if (condition == UnlockCondition.EmptyHygiene) {
			if ((float)(currPet.alienHygene / currPet.alienHygeneMod) <= 0) {
				conditionFulfilled = true;
			}

		} else if (condition == UnlockCondition.EmptyHappiness) {
			if ((float)(currPet.alienHappiness / currPet.alienHappinessMod) <= 0) {
				conditionFulfilled = true;
			}

		} else if (condition == UnlockCondition.Empty3) {
			if ((float)(currPet.alienHunger / currPet.alienHungerMod) <= 0 &&
			    (float)(currPet.alienHygene / currPet.alienHygeneMod) <= 0 &&
			    (float)(currPet.alienHappiness / currPet.alienHappinessMod) <= 0) {
				conditionFulfilled = true;
			}

		} else if (condition == UnlockCondition.FullFed) {
			if (currPet.alienHunger == currPet.alienHungerMod) {
				conditionFulfilled = true;
			}

		} else if (condition == UnlockCondition.FullHygiene) {
			if (currPet.alienHygene == currPet.alienHygeneMod) {
				conditionFulfilled = true;
			}

		} else if (condition == UnlockCondition.FullHappiness) {
			if (currPet.alienHappiness == currPet.alienHappinessMod) {
				conditionFulfilled = true;
			}

		} else if (condition == UnlockCondition.FullAll) {
			if (currPet.alienHunger == currPet.alienHungerMod) {
				conditionFulfilled = true;
			}

		} else if (condition == UnlockCondition.FeedPosIconCount) {
			if (instance.feedPosCount == gatherIconTarget) {
				conditionFulfilled = true;
			}

		} else if (condition == UnlockCondition.CleanPosIconCount) {
			if (instance.cleanPosCount == gatherIconTarget) {
				conditionFulfilled = true;
			}

		} else if (condition == UnlockCondition.PlayPosIconCount) {
			if (instance.playPosCount == gatherIconTarget) {
				conditionFulfilled = true;
			}

		} else if (condition == UnlockCondition.NursePosIconCount) {
			if (instance.nursePosCount == gatherIconTarget) {
				conditionFulfilled = true;
			}

		} else if (condition == UnlockCondition.FeedNegIconCount) {
			if (instance.feedNegCount == gatherIconTarget) {
				conditionFulfilled = true;
			}

		} else if (condition == UnlockCondition.CleanNegIconCount) {
			if (instance.cleanNegCount == gatherIconTarget) {
				conditionFulfilled = true;
			}

		} else if (condition == UnlockCondition.PlayNegIconCount) {
			if (instance.playNegCount == gatherIconTarget) {
				conditionFulfilled = true;
			}

		} else if (condition == UnlockCondition.NurseNegIconCount) {
			if (instance.nurseNegCount == gatherIconTarget) {
				conditionFulfilled = true;
			}

		} else if (condition == UnlockCondition.TapCount1) {
			if (instance.petTapCount == tapCountTarget1) {
				conditionFulfilled = true;
			}

		} else if (condition == UnlockCondition.TapCount2) {
			if (instance.petTapCount == tapCountTarget2) {
				conditionFulfilled = true;
			}

		} else if (condition == UnlockCondition.TapCount3) {
			if (instance.petTapCount == tapCountTarget3) {
				conditionFulfilled = true;
			}

		} else if (condition == UnlockCondition.CoinCount) {
			if (instance.playerCoin == saveCoinTarget) {
				conditionFulfilled = true;
			}

		} else if (condition == UnlockCondition.SpendCoin) {
			if (instance.playerSpentCoin == spentCoinTarget) {
				conditionFulfilled = true;
			}

		} else if (condition == UnlockCondition.GoToSettings) {
			conditionFulfilled = true;

		} else if (condition == UnlockCondition.ReachLevel) {
			if (currPet.alienLevel == levelTarget) {
				conditionFulfilled = true;
			}

		} else if (condition == UnlockCondition.LongPlay) {

		} else if (condition == UnlockCondition.ConsecutiveLogin) {

		} else if (condition == UnlockCondition.Comeback) {

		} else if (condition == UnlockCondition.GameIdle) {

		} else if (condition == UnlockCondition.SendOff) {
			conditionFulfilled = true;

		} else if (condition == UnlockCondition.MuteSound) {
			conditionFulfilled = true;
		}

		if (CountUnlockedEmojis () == emojiUnlockValue.Length / 2) {
			emojiUnlockValue [0, (int)UnlockCondition.Collection1] = true;
			collectionManager.UnlockEmoji((int)UnlockCondition.Collection1);
		} else if (CountUnlockedEmojis () == emojiUnlockValue.Length-1) {
			emojiUnlockValue[0,(int)UnlockCondition.CollectionAll] = true;
			collectionManager.UnlockEmoji((int)UnlockCondition.CollectionAll);
		}

		if (conditionFulfilled) {
			emojiUnlockValue[0,(int)condition] = true;
			collectionManager.UnlockEmoji((int)condition);
		}
	}

	int CountUnlockedEmojis ()
	{
		int count = 0;
		for (int i = 0; i < emojiUnlockValue.Length; i++) {
			if (emojiUnlockValue [0,i] == true) {
				count++;
			}
		}
		return count;
	}
}
