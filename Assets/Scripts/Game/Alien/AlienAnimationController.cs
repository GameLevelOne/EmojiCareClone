using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AlienAnimationState{
	IDLE,
	SAD,
}

public class AlienAnimationController : MonoBehaviour {
	Animator alienAnim;
	[HideInInspector] public AlienAnimationState alienAnimState;

	const int tapCountTarget1 = 100;
	const int tapCountTarget2 = 500;
	const int tapCountTarget3 = 1000;

	void Awake()
	{
		alienAnim = GetComponent<Animator>();
	}

	public void ChangeAnimation(AlienAnimationState state)
	{
		alienAnimState = state;
		alienAnim.SetInteger("State",(int)alienAnimState);
	}
	//------------------------------------------------
	const string KEYPREF_HIT = "AlienHit";
	int AlienHit{
		get{return PlayerPrefs.GetInt(KEYPREF_HIT,0);}
		set{PlayerPrefs.SetInt(KEYPREF_HIT,value);}
	}

	public void OnAlienClicked ()
	{
		if (AlienHit >= PlayerData.Instance.AlienClickCount) {
			AlienHit = 0;
			CoinSpawner.Instance.GenerateCoinObject ();
		} else
			AlienHit++;

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