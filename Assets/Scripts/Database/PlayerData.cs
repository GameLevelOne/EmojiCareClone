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
	const string KEYPREF_PLAYERALIEN_ID = "PlayerAlien/ID";


	public int playerCoin{
		get{return PlayerPrefs.GetInt(KEYPREF_PLAYERCOIN,1000);}
		set{PlayerPrefs.SetInt(KEYPREF_PLAYERCOIN,value);}
	}

	public int playerAlienID{
		get{return PlayerPrefs.GetInt(KEYPREF_PLAYERALIEN_ID,-1);}
		set{PlayerPrefs.SetInt(KEYPREF_PLAYERALIEN_ID,value);}
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