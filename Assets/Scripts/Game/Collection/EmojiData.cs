using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnlockCondition{
	CriticalHunger,
	CriticalHygiene,
	CriticalHappiness,
	CriticalHealth,
	EmptyHunger,
	EmptyHygiene,
	EmptyHappiness,
	Empty3,
	EmptyHealth,
	FullHunger,

	FullHygiene,
	FullHappiness,
	FullAll,
	FeedPosIconCount,
	CleanPosIconCount,
	PlayPosIconCount,
	NursePosIconCount,
	FeedNegIconCount,
	CleanNegIconCount,
	PlayNegIconCount,

	NurseNegIconCount,
	TapCount1,
	TapCount2,
	TapCount3,
	CoinCount,
	SpendCoin,
	GoToSettings,
	GoToCollection,
	NameEmoji,
	ReachLevel,

	FirstPlay,
	LongPlay,
	ConsecutiveLogin,
	Comeback,
	GameIdle,
	SendOff1,
	SendOff2,
	Collection1,
	CollectionAll,
	MuteSound

}

public class EmojiData : MonoBehaviour {
	public Sprite[] emojiSprites = new Sprite[40];
	public string[] conditionText = new string[40];
	public string[] emojiName = new string[40];

	UnlockCondition[] emojiUnlockCondition = new UnlockCondition[40];

	void Start(){
		SetUnlockCondition();
		SetUnlockConditionText();
		SetEmojiName();
	}

	void SetUnlockCondition ()
	{
		for (int i = 0; i < 40; i++) {
			emojiUnlockCondition[i] = (UnlockCondition)i;
		}
	}

	void SetUnlockConditionText ()
	{
		conditionText [0] = "Lower HUNGER meter to a certain value";
		conditionText [1] = "Lower HYGIENE meter to a certain value";
		conditionText [2] = "Lower HAPPINESS meter to a certain value";
		conditionText [3] = "Lower HEALTH meter to a certain value";
		for (int i = 4; i < conditionText.Length; i++) {
			conditionText[i]="Temp condition";
		}
	}

	void SetEmojiName ()
	{
		for (int i = 0; i < emojiName.Length; i++) {
			emojiName[i] = "TEMPNAME";
		}
	}
}
