using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EmojiObject : MonoBehaviour {
	public Image imageBody;
	const string Key_Hit = "EmojiHit";

	const int tapCountTarget1 = 100;
	const int tapCountTarget2 = 500;
	const int tapCountTarget3 = 1000;

	int EmojiHit{
		get{return PlayerPrefs.GetInt(Key_Hit,0);}
		set{PlayerPrefs.SetInt(Key_Hit,value);}
	}

	void OnEnable()
	{
		StartCoroutine("CoroutineChangeRandomEmotion");
	}

	void OnDisable()
	{
		StopAllCoroutines();
	}

	void OnDestroy()
	{
		StopAllCoroutines();
	}

	IEnumerator CoroutineChangeRandomEmotion()
	{
		List<Sprite> emotions = new List<Sprite>();
		Emoji playerEmoji = PlayerData.Instance.PlayerEmoji;
		for(int i = 0;i< playerEmoji.collectionSO.Length;i++)
		{
			if( playerEmoji.GetCollection(i) > 0){
				emotions.Add(playerEmoji.collectionSO[i].emotionIcon);
			}
		}

		while(true){
			yield return new WaitForSeconds(Random.Range(5f,10f));
			int rnd = Random.Range(0,emotions.Count);
			imageBody.sprite = emotions[rnd];
		}
	}

	public void EmojiOnClick()
	{
		if(EmojiHit >= 5){
			EmojiHit = 0;
			CoinSpawner.Instance.GenerateCoinObject();
		}else EmojiHit++;

		Emoji playerEmoji = PlayerData.Instance.PlayerEmoji;

		playerEmoji.emojiTapCount++;
		EmojiUnlockConditions.Instance.CheckUnlock(UnlockCondition.TapCount1);
		EmojiUnlockConditions.Instance.CheckUnlock(UnlockCondition.TapCount2);
		EmojiUnlockConditions.Instance.CheckUnlock(UnlockCondition.TapCount3);		
	}

	public void EmojiGlitchOnClick()
	{
		if(PlayerData.Instance.emojiDead == true){
			GetComponent<Button>().interactable = false;
			GetComponent<Animator>().SetTrigger("Dead");
		}
	}

	public void GoToSceneStork()
	{
		SceneManager.LoadScene("SceneStork");
	}
}
