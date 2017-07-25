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
	const string Key_Player_Coin = "PlayerCoin";
	const string Key_Player_EmojiId = "PlayerEmoji/ID";
	const string KEYPREF_PLAYERALIEN_ID = "PlayerAlien/ID";

	const string Key_Player_CoinSPENT = "PlayerCoinSpent";
	const string KEYPREF_PLAYERALIEN_FEEDPOSCOUNT = "PlayerAlien/FeedPosCount";
	const string KEYPREF_PLAYERALIEN_CLEANPOSCOUNT = "PlayerAlien/CleanPosCount";
	const string KEYPREF_PLAYERALIEN_PLAYPOSCOUNT = "PlayerAlien/PlayPosCount";
	const string KEYPREF_PLAYERALIEN_NURSEPOSCOUNT = "PlayerAlien/NursePosCount";
	const string KEYPREF_PLAYERALIEN_FEEDNEGCOUNT = "PlayerAlien/FeedNegCount";
	const string KEYPREF_PLAYERALIEN_CLEANNEGCOUNT = "PlayerAlien/CleanNegCount";
	const string KEYPREF_PLAYERALIEN_PLAYNEGCOUNT = "PlayerAlien/PlayNegCount";
	const string KEYPREF_PLAYERALIEN_NURSENEGCOUNT = "PlayerAlien/NurseNegCount";
	const string KEYPREF_PLAYERALIEN_PETTAPCOUNT = "PlayerAlien/PetTapCount";

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
	public int petTapCount {
		get{ return PlayerPrefs.GetInt (KEYPREF_PLAYERALIEN_PETTAPCOUNT, 0); }
		set{ PlayerPrefs.SetInt(KEYPREF_PLAYERALIEN_PETTAPCOUNT,value);}
	}
	public int feedPosCount {
		get{ return PlayerPrefs.GetInt (KEYPREF_PLAYERALIEN_FEEDPOSCOUNT, 0); }
		set{ PlayerPrefs.SetInt(KEYPREF_PLAYERALIEN_FEEDPOSCOUNT,value);}
	}
	public int cleanPosCount {
		get{ return PlayerPrefs.GetInt (KEYPREF_PLAYERALIEN_CLEANPOSCOUNT, 0); }
		set{ PlayerPrefs.SetInt(KEYPREF_PLAYERALIEN_CLEANPOSCOUNT,value);}
	}
	public int playPosCount {
		get{ return PlayerPrefs.GetInt (KEYPREF_PLAYERALIEN_CLEANPOSCOUNT, 0); }
		set{ PlayerPrefs.SetInt(KEYPREF_PLAYERALIEN_PLAYPOSCOUNT,value);}
	}
	public int nursePosCount {
		get{ return PlayerPrefs.GetInt(KEYPREF_PLAYERALIEN_NURSEPOSCOUNT,0);}
		set{PlayerPrefs.SetInt(KEYPREF_PLAYERALIEN_NURSEPOSCOUNT,value);}
	}
	public int feedNegCount {
		get{ return PlayerPrefs.GetInt (KEYPREF_PLAYERALIEN_FEEDNEGCOUNT, 0); }
		set{ PlayerPrefs.SetInt(KEYPREF_PLAYERALIEN_FEEDNEGCOUNT,value);}
	}
	public int cleanNegCount {
		get{ return PlayerPrefs.GetInt (KEYPREF_PLAYERALIEN_CLEANNEGCOUNT, 0); }
		set{ PlayerPrefs.SetInt(KEYPREF_PLAYERALIEN_CLEANNEGCOUNT,value);}
	}
	public int playNegCount {
		get{ return PlayerPrefs.GetInt (KEYPREF_PLAYERALIEN_PLAYNEGCOUNT, 0); }
		set{ PlayerPrefs.SetInt(KEYPREF_PLAYERALIEN_PLAYNEGCOUNT,value);}
	}
	public int nurseNegCount {
		get{ return PlayerPrefs.GetInt (KEYPREF_PLAYERALIEN_NURSENEGCOUNT, 0); }
		set{ PlayerPrefs.SetInt(KEYPREF_PLAYERALIEN_NURSENEGCOUNT,value);}
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

		if(playerEmojiID != -1) LoadPlayerEmoji();
	}

	public void LoadPlayerEmoji()
	{
		playerEmoji = emojiData[playerEmojiID];
		playerEmoji.OnEmojiDies += OnEmojiDies;

		playerEmoji.InitEmoji(emojiParentTransform);
		EmojiStatsController.Instance.Init();
	}

	public void SetPlayerEmoji(int index)
	{
		playerEmojiID = index;
		PlayerEmoji = emojiData[index];
		playerEmoji.InitEmoji(emojiParentTransform);
		playerEmoji.InitStats();
		EmojiStatsController.Instance.Init();
	}
		
	void OnEmojiDies()
	{
		emojiDead = true;
		playerEmojiID = -1;

		//disable stats depletion
	}
}