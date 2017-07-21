using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class MainHUDController : MonoBehaviour {
	public AlienHUDMeter hungerMeter, hygeneMeter, happinessMeter, healthMeter;
	public Image imageGrowth;
	public Text textAlienName, textAlienType, textAlienLevel, textAlienGrowth;
	public Text textCoin;

	Emoji playerEmoji;

	void OnEnable()
	{
		if(PlayerData.Instance.playerAlienID != -1) 
			PlayerData.Instance.PlayerEmoji.OnEmojiModStats += UpdateStatsMeter;
		
	}

	void OnDisable()
	{
		if(PlayerData.Instance.playerAlienID != -1) 
			PlayerData.Instance.PlayerEmoji.OnEmojiModStats -= UpdateStatsMeter;
		
	}

	public void Init()
	{
		UpdateEmojiNameAndType();
		textCoin.text = PlayerData.Instance.playerCoin.ToString();

		if(playerEmoji == null || playerEmoji != PlayerData.Instance.PlayerEmoji) 
			playerEmoji = PlayerData.Instance.PlayerEmoji;

		hungerMeter.InitHUD( playerEmoji.emojiHungerMod, playerEmoji.emojiHunger);
		hygeneMeter.InitHUD( playerEmoji.emojiHygeneMod,  playerEmoji.emojiHygene);
		happinessMeter.InitHUD( playerEmoji.emojiHappinessMod,  playerEmoji.emojiHappiness);
		healthMeter.InitHUD( playerEmoji.emojiHealthMod,  playerEmoji.emojiHealth);

	}

	public void UpdateEmojiNameAndType()
	{
		textAlienName.text = PlayerData.Instance.PlayerEmoji.emojiName.ToUpper();
	}

	public void UpdateStatsMeter()
	{
//		Alien playerAlien = PlayerData.Instance.PlayerAlien;

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
			textCoin.text =  ((int)tempCoin).ToString();
			yield return new WaitForSeconds(Time.deltaTime);
		}
		textCoin.text = PlayerData.Instance.playerCoin.ToString();
	}
}