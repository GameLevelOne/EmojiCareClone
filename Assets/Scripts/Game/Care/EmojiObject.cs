using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EmojiObject : MonoBehaviour {
	const string Key_Hit = "EmojiHit";
	int EmojiHit{
		get{return PlayerPrefs.GetInt(Key_Hit,0);}
		set{PlayerPrefs.SetInt(Key_Hit,value);}
	}

	public void EmojiOnClick()
	{
		if(EmojiHit >= 5){
			EmojiHit = 0;
			CoinSpawner.Instance.GenerateCoinObject();
		}else{
			EmojiHit++;
		}
	}
}
