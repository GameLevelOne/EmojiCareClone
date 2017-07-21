using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnlockCondition{
	//9
	CriticalHunger,
	CriticalHygiene,
	CriticalHappiness,
	CriticalHealth,
	EmptyHunger,
	EmptyHygiene,
	EmptyHappiness,
	Empty3,
	FullFed,

	//10
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

	//7
	NurseNegIconCount,
	TapCount1,
	TapCount2,
	TapCount3,
	CoinCount,
	SpendCoin,
	GoToSettings,

	//8
	LongPlay,//total playtime
	LoginCount,
	Comeback, 
	GameIdle, //TODO pending
	SendOff, 
	Collection1,
	CollectionAll,
	MuteSound
}

[CreateAssetMenu(fileName = "Emoji",menuName = "Cards/Emoji",order = 3)]
public class EmojiSO : ScriptableObject {

	public string emojiName = "temp";
	public Sprite emojiIcon;
	public UnlockCondition emojiUnlockConditionType;
	public string emojiUnlockConditionDesc = "temp description";
	public bool emojiUnlockStatus = false;
}
