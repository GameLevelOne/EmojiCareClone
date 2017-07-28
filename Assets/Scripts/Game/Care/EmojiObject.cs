using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EmojiObject : MonoBehaviour {
	const string Key_Hit = "EmojiHit";

	const int tapCountTarget1 = 100;
	const int tapCountTarget2 = 500;
	const int tapCountTarget3 = 1000;

	int EmojiHit{
		get{return PlayerPrefs.GetInt(Key_Hit,0);}
		set{PlayerPrefs.SetInt(Key_Hit,value);}
	}

	public void EmojiOnClick()
	{
		if(EmojiHit >= 5){
			EmojiHit = 0;
			CoinSpawner.Instance.GenerateCoinObject();
		}else EmojiHit++;

		PlayerData.Instance.petTapCount++;

		if (PlayerData.Instance.petTapCount == tapCountTarget1) {
			EmojiUnlockConditions.Instance.CheckUnlock (UnlockCondition.TapCount1);
		} else if (PlayerData.Instance.petTapCount == tapCountTarget2) {
			EmojiUnlockConditions.Instance.CheckUnlock (UnlockCondition.TapCount2);
		} else if (PlayerData.Instance.petTapCount == tapCountTarget3) {
			EmojiUnlockConditions.Instance.CheckUnlock(UnlockCondition.TapCount3);
		}
	}
}
