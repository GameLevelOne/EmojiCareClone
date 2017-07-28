﻿using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class MainHUDController : MonoBehaviour {
	public AlienHUDMeter hungerMeter, hygeneMeter, happinessMeter, healthMeter;
	public Text textEmojiName;
	public Text textPlayerCoin;
	public GameObject notificationIcon;
	public GameObject buttonSendOff;
	public Button[] OnTutorialButtons;
	Emoji playerEmoji;

	void Awake()
	{
		PlayerData.Instance.OnEmojiDie += TurnOffHUD;
	}

	void OnDestroy()
	{
		PlayerData.Instance.OnEmojiDie -= TurnOffHUD;
	}

	void OnEnable()
	{
		
		if(PlayerData.Instance.playerEmojiID != -1 && PlayerData.Instance.PlayerEmoji != null) {
			
			PlayerData.Instance.PlayerEmoji.OnEmojiModStats += UpdateStatsMeter;
			EmojiUnlockConditions.Instance.OnEmotionUnlock += CheckforNewEmotion;
		}
	}

	void OnDisable()
	{
		if(PlayerData.Instance.playerEmojiID != -1 && PlayerData.Instance.PlayerEmoji != null) {
			PlayerData.Instance.PlayerEmoji.OnEmojiModStats -= UpdateStatsMeter;
			EmojiUnlockConditions.Instance.OnEmotionUnlock -= CheckforNewEmotion;
		}
	}
		
	public void Init()
	{
		textPlayerCoin.text = PlayerData.Instance.playerCoin.ToString();

		if(playerEmoji == null || playerEmoji != PlayerData.Instance.PlayerEmoji) 
			playerEmoji = PlayerData.Instance.PlayerEmoji;

		UpdateName();

		hungerMeter.InitHUD( playerEmoji.emojiHungerMod, playerEmoji.emojiHunger);
		hygeneMeter.InitHUD( playerEmoji.emojiHygeneMod,  playerEmoji.emojiHygene);
		happinessMeter.InitHUD( playerEmoji.emojiHappinessMod,  playerEmoji.emojiHappiness);
		healthMeter.InitHUD( playerEmoji.emojiHealthMod,  playerEmoji.emojiHealth);

		CheckforNewEmotion(0); //angka nya cm dummy, dipake di delegate event
		CheckForSendOff();
	}

	void CheckForSendOff()
	{
		int amount = 0;
		for(int i = 0;i<playerEmoji.collectionSO.Length;i++){
			if(playerEmoji.GetCollection(i) > 0){
				amount++;
			}
		}
		if(((float)amount/(float)playerEmoji.collectionSO.Length) >= 0.5f){
			buttonSendOff.SetActive(true);
		}else{
			buttonSendOff.SetActive(false);
		}
	}

	public void CheckforNewEmotion(int index)
	{
		print("CHECKED NEW EMOTION");
		for(int i = 0;i<playerEmoji.collectionSO.Length;i++){
			if(playerEmoji.GetCollection(i) == 1){
				notificationIcon.SetActive(true);
				return;
			}
		}
		notificationIcon.SetActive(false);
	}

	public void UpdateName()
	{
		textEmojiName.text = playerEmoji.emojiName.ToUpper();
	}

	public void UpdateStatsMeter()
	{
		if(playerEmoji != null){
			if(playerEmoji.emojiHungerMod >= 0f) 		hungerMeter.ModHUD(playerEmoji.emojiHungerMod,		playerEmoji.emojiHunger);
			if(playerEmoji. emojiHygeneMod >= 0f)		hygeneMeter.ModHUD(playerEmoji.emojiHygeneMod, 		playerEmoji.emojiHygene);
			if(playerEmoji. emojiHappinessMod >= 0f) happinessMeter.ModHUD(playerEmoji.emojiHappinessMod,   playerEmoji.emojiHappiness);
			if(playerEmoji. emojiHealthMod >= 0f) 		healthMeter.ModHUD(playerEmoji.emojiHealthMod, 		playerEmoji.emojiHealth);
		}
	}

	public void ModCoin(int amount)
	{
		int temp = PlayerData.Instance.playerCoin;
		PlayerData.Instance.playerCoin += amount;
		StartCoroutine(CoroutineModCoin(temp));
	}

	IEnumerator CoroutineModCoin(int current)
	{
		float t = 0;
		while(t <= 1f){
			t+=Time.deltaTime*10;
			float tempCoin = Mathf.Lerp(current,PlayerData.Instance.playerCoin,t);
			textPlayerCoin.text =  ((int)tempCoin).ToString();
			yield return new WaitForSeconds(Time.deltaTime);
		}
		textPlayerCoin.text = PlayerData.Instance.playerCoin.ToString();
	}

	void TurnOffHUD()
	{
		PlayerData.Instance.OnEmojiDie -= TurnOffHUD;
		gameObject.SetActive(false);
	}
}