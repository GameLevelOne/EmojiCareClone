using UnityEngine;

public class PlayerData : MonoBehaviour {
	//singleton
	private static PlayerData instance = null;
	public static  PlayerData Instance{
		get{return instance;}
	}

	GameObject playerAlien;
	public RectTransform alienObjectParentTransform;

	[Header("Data")]
	public Alien[] alienData;

	public bool alienDead = false;

	void Awake()
	{
		//singleton
		if(instance != null && instance != this) { 
			Destroy(gameObject);
			return; 
		}
		else instance = this;
		DontDestroyOnLoad(gameObject);

		if(playerAlienID != -1) LoadPlayerAlien();
	}

	public void LoadPlayerAlien(){
		PlayerAlien = alienData[playerAlienID];
		PlayerAlien.OnAlienDies += OnAlienDies;
		GenerateAlienObject();
	}

	public void SetPlayerAlien(int index)
	{
		playerAlienID = index;
		PlayerAlien = PlayerData.Instance.alienData[index];
		PlayerAlien.InitAlienStats();
		GenerateAlienObject();
	}

	public Alien PlayerAlien{
		get{ return playerAlien.GetComponent<Alien>(); }
		set{ playerAlien = value.gameObject; }
	}

	void GenerateAlienObject()
	{
		if(PlayerAlien.cloneObject == null){
			PlayerAlien.GenerateAlienAnimationObject(alienObjectParentTransform);
			AlienStatsController.Instance.InitStatsController();
		}
	}

	//Setter Getter -------------------------------------------------------------------------------
	[HideInInspector] public int AlienClickCount{ get{return 5;} }
	const string KEYPREF_PLAYERCOIN = "PlayerCoin";
	const string KEYPREF_PLAYERCOINSPENT = "PlayerCoinSpent";
	const string KEYPREF_PLAYERALIEN_ID = "PlayerAlien/ID";

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
		get{return PlayerPrefs.GetInt(KEYPREF_PLAYERCOIN,1000);}
		set{PlayerPrefs.SetInt(KEYPREF_PLAYERCOIN,value);}
	}

	public int playerSpentCoin {
		get{ return PlayerPrefs.GetInt (KEYPREF_PLAYERCOINSPENT, 0); }
		set{ PlayerPrefs.SetInt(KEYPREF_PLAYERCOINSPENT,value);}
	}

	public int playerAlienID{
		get{return PlayerPrefs.GetInt(KEYPREF_PLAYERALIEN_ID,-1);}
		set{PlayerPrefs.SetInt(KEYPREF_PLAYERALIEN_ID,value);}
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

	void OnAlienDies()
	{
		alienDead = true;
		playerAlienID = -1;

		AlienStatsController.Instance.isStartIncreasingGrowth = false;
		AlienStatsController.Instance.isStatsDepletingStats = false;
	}
		
	public void RestoreAlienStats()
	{
		if(PlayerAlien != null){
			PlayerAlien.alienHungerMod = PlayerAlien.alienHunger;
			PlayerAlien.alienHygeneMod = PlayerAlien.alienHygene;
			PlayerAlien.alienHappinessMod = PlayerAlien.alienHappiness;
			PlayerAlien.alienHealthMod = PlayerAlien.alienHealth;
		}
	}
}