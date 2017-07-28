using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class MainHUDController : MonoBehaviour {
	public AlienHUDMeter hungerMeter, hygeneMeter, happinessMeter, healthMeter;
	public Text textEmojiName;
	public Text textPlayerCoin;
	public GameObject notificationIcon;

	Emoji playerEmoji;

	void Start(){ EmojiUnlockConditions.Instance.OnEmotionUnlock += CheckforNewEmotion; }
	void OnDestroy(){ EmojiUnlockConditions.Instance.OnEmotionUnlock -= CheckforNewEmotion; }

	void OnEnable()
	{
		if(PlayerData.Instance.playerEmojiID != -1 && PlayerData.Instance.PlayerEmoji != null) {
			PlayerData.Instance.PlayerEmoji.OnEmojiModStats += UpdateStatsMeter;
		}
	}

	void OnDisable()
	{
		if(PlayerData.Instance.playerEmojiID != -1 && PlayerData.Instance.PlayerEmoji != null) {
			PlayerData.Instance.PlayerEmoji.OnEmojiModStats -= UpdateStatsMeter;
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

		CheckforNewEmotion();
	}

	public void CheckforNewEmotion()
	{
		for(int i = 0;i<playerEmoji.collectionSO.Length;i++){
			if(playerEmoji.GetCollection(i) == 1){
				notificationIcon.SetActive(true);
				break;
			}
		}
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
}