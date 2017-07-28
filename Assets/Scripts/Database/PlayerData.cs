using UnityEngine;

public class PlayerData : MonoBehaviour {
	//singleton
	private static PlayerData instance = null;
	public static  PlayerData Instance{
		get{return instance;}
	}
		
	public RectTransform emojiParentTransform;
	GameObject playerAlien;
	Emoji playerEmoji;

	#region playerdata
	[HideInInspector] public int AlienClickCount{ get{return 5;} }
	const string Key_Player_Name = "PlayerName";
	const string Key_Player_Coin = "PlayerCoin";
	const string Key_Player_EmojiId = "PlayerEmoji/ID";
	const string Key_Game_Status = "GameStatus";
	const string Key_Done_Prologue = "PlayerDonePrologue";
	const string KEYPREF_PLAYERALIEN_ID = "PlayerAlien/ID";

	const string Key_Player_CoinSPENT = "PlayerCoinSpent";
	const string KEYPREF_PLAYERALIEN_PETTAPCOUNT = "PlayerAlien/PetTapCount";

	public string playerName{
		get{return PlayerPrefs.GetString(Key_Player_Name);}
		set{PlayerPrefs.SetString(Key_Player_Name,value);}
	}

	public int playerCoin{
		get{return PlayerPrefs.GetInt(Key_Player_Coin,1000);}
		set{PlayerPrefs.SetInt(Key_Player_Coin,value);}
	}
	public int playerSpentCoin {
		get{ return PlayerPrefs.GetInt (Key_Player_CoinSPENT, 0); }
		set{ PlayerPrefs.SetInt(Key_Player_CoinSPENT,value);}
	}
	public int playerAlienID{
		get{return PlayerPrefs.GetInt(KEYPREF_PLAYERALIEN_ID,-1);}
		set{PlayerPrefs.SetInt(KEYPREF_PLAYERALIEN_ID,value);}
	}
	public int playerEmojiID{
		get{return PlayerPrefs.GetInt(Key_Player_EmojiId,-1);}
		set{PlayerPrefs.SetInt(Key_Player_EmojiId,value);}
	}

	public GameStatus gameStatus{
		get{return (GameStatus) PlayerPrefs.GetInt(Key_Game_Status,0);}
		set{PlayerPrefs.SetInt(Key_Game_Status,(int)value);}
	}
	public int playerDonePrologue{
		get{return PlayerPrefs.GetInt(Key_Done_Prologue,0);}
		set{PlayerPrefs.SetInt(Key_Done_Prologue,value);}
	}
	public int petTapCount {
		get{ return PlayerPrefs.GetInt (KEYPREF_PLAYERALIEN_PETTAPCOUNT, 0); }
		set{ PlayerPrefs.SetInt(KEYPREF_PLAYERALIEN_PETTAPCOUNT,value);}
	}

	public Alien PlayerAlien{
		get{ return playerAlien.GetComponent<Alien>(); }
		set{ playerAlien = value.gameObject; }
	}
	public Emoji PlayerEmoji{
		get{ return playerEmoji; }
		set{ playerEmoji = value;}
	}
	#endregion

	public Emoji[] emojiData;

	public bool alienDead = false;
	[HideInInspector] public bool emojiDead = false;

	void Awake()
	{
//		PlayerPrefs.DeleteAll();
		//singleton
		if(instance != null && instance != this) { 
			Destroy(gameObject);
			return; 
		}
		else instance = this;

//		if(playerEmojiID != -1) LoadPlayerEmoji();
	}

	public void LoadPlayerEmoji()
	{
		if(playerEmoji == null){
			playerEmoji = emojiData[playerEmojiID];
			playerEmoji.OnEmojiDies += OnEmojiDies;

			playerEmoji.InitEmoji(emojiParentTransform);
			EmojiStatsController.Instance.Init();
		}
	}

	public void SetPlayerEmoji(int index)
	{
		playerEmojiID = index;
		playerEmoji = emojiData[index];
		playerEmoji.InitStats();
		playerEmoji.InitEmojiCollections();
	}
		
	void OnEmojiDies()
	{
		emojiDead = true;
		playerEmojiID = -1;

		//disable stats depletion
		EmojiStatsController.Instance.StopAllCoroutines();
		//animate emoji die, on tap: go to scene stork and display emoji dead dialogue
	}
}