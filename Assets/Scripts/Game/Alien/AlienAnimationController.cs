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

	public void OnAlienClicked()
	{
		if(AlienHit >= PlayerData.Instance.AlienClickCount)
		{
			AlienHit = 0;
			CoinSpawner.Instance.GenerateCoinObject();
		}else AlienHit++;
	}
}