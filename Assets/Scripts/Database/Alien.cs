using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Alien : MonoBehaviour{
	const float ALIEN_HAPPY_TRESHOLD = 0.25f;

	public delegate void AlienGetGrowth();
	public delegate void AlienDepleteStats();
	public delegate void AlienDies();
	public event AlienGetGrowth OnAlienGetGrowth;
	public event AlienDepleteStats OnAlienDepleteStats;
	public event AlienDies OnAlienDies;

	public AlienSO alienSO;
	[HideInInspector] public GameObject cloneObject;
	AlienAnimationController cloneObjectAnimationController;

	public void GenerateAlienAnimationObject(RectTransform parent)
	{
		cloneObject = Instantiate(alienSO.alienAnimationObject);
		RectTransform alienTransform = cloneObject.GetComponent<RectTransform>();
		alienTransform.SetParent(parent);
		alienTransform.anchoredPosition = Vector2.zero;
		alienTransform.localScale = Vector2.one;
		alienTransform.localRotation = Quaternion.Euler(Vector3.zero);

		cloneObjectAnimationController = cloneObject.GetComponent<AlienAnimationController>();
	}

	public void InitAlienStats()
	{
		print("INIT ALIEN STATS");
		alienLevel = alienSO.alienLevel;
		alienGrowth = alienSO.alienGrowth;

		alienHunger = alienSO.alienHunger;
		alienHygene = alienSO.alienHygene;
		alienHappiness = alienSO.alienHappiness;
		alienHealth = alienSO.alienHealth;

		alienGrowthMod = alienSO.alienGrowthMod;

		alienHungerMod = alienSO.alienHungerMod;
		alienHygeneMod = alienSO.alienHygeneMod;
		alienHappinessMod = alienSO.alienHappinessMod;
		alienHealthMod = alienSO.alienHealthMod;

		PlayerPrefs.Save();
	}

	void CheckAlienState()
	{
		if(cloneObjectAnimationController != null){
			if(IsAlienHappy()){
				if(cloneObjectAnimationController.alienAnimState != AlienAnimationState.IDLE) 
					cloneObjectAnimationController.ChangeAnimation(AlienAnimationState.IDLE);
			}else{
				if(cloneObjectAnimationController.alienAnimState != AlienAnimationState.SAD) 
					cloneObjectAnimationController.ChangeAnimation(AlienAnimationState.SAD);
			}
		}
	}

	bool IsAlienHappy()
	{
		if((float)(alienHungerMod/alienHunger) <= ALIEN_HAPPY_TRESHOLD || 
			(float)(alienHygeneMod/alienHygene) <= ALIEN_HAPPY_TRESHOLD ||
			(float)(alienHappinessMod/alienHappiness) <= ALIEN_HAPPY_TRESHOLD ||
			(float)(alienHealthMod/alienHealth) <= ALIEN_HAPPY_TRESHOLD){
			return false;
		}else{
			return true;
		}
	}

	void Update()
	{
		CheckAlienState();
	}

	public void IncreaseGrowth(int totalTick = 1)
	{
		if(alienGrowthMod >= alienGrowth) return;
		else alienGrowthMod += alienGrowtHit * totalTick;

		if(alienGrowthMod >= alienGrowth) alienGrowthMod = alienGrowth;
		if(alienGrowth <= 0) alienGrowthMod = 0;

		if(OnAlienGetGrowth != null) OnAlienGetGrowth();
	}

	public void LevelUp(){
		alienLevel++;
		alienGrowth += alienGrowthGapPerLevel;
		alienGrowthMod = 0;

		if(OnAlienGetGrowth != null) OnAlienGetGrowth();
	}

	/// <summary> depletes stats for how many minutes have passed. without a parameter, this method ticks once.</summary>
	public void DepleteAlienStats(int TotalTickPassed = 1)
	{
		if(alienHungerMod == 0 && alienHygeneMod == 0 && alienHappinessMod == 0){
			DepleteAlienHealth();
		}else{
			if(alienType == AlienType.Greedy){
				alienHungerMod    -= alienStatsDepletionHitSpecial * TotalTickPassed;
				alienHygeneMod    -= alienStatsDepletionHit * TotalTickPassed;
				alienHappinessMod -= alienStatsDepletionHit * TotalTickPassed;
			}else if(alienType == AlienType.Active){
				alienHungerMod    -= alienStatsDepletionHit * TotalTickPassed;
				alienHygeneMod    -= alienStatsDepletionHitSpecial * TotalTickPassed;
				alienHappinessMod -= alienStatsDepletionHit * TotalTickPassed;
			}else if(alienType == AlienType.Sad){
				alienHungerMod    -= alienStatsDepletionHit * TotalTickPassed;
				alienHygeneMod    -= alienStatsDepletionHit * TotalTickPassed;
				alienHappinessMod -= alienStatsDepletionHitSpecial * TotalTickPassed;
			}

			if(alienHungerMod <= 0f) alienHungerMod = 0f;
			if(alienHygeneMod <= 0f) alienHygeneMod = 0f;
			if(alienHappinessMod <= 0f) alienHappinessMod = 0f;
		}

		if(OnAlienDepleteStats != null) OnAlienDepleteStats();
	}

	public void DepleteAlienHealth(){
		alienHealthMod -= alienHealthDepletionHit;

		if(alienHealthMod <= 0){
			if(OnAlienDies != null){ 
				AlienStatsController.Instance.StopAllCoroutines();
				cloneObjectAnimationController = null;
				Destroy(cloneObject);
				ResetAlienData();
				OnAlienDies();
			}
			return;
		}

		if(OnAlienDepleteStats != null) OnAlienDepleteStats();
	}

	public void ResetAlienData()
	{
		print("RESET ALIEN DATA");
		PlayerPrefs.DeleteKey(KEYPREF_PLAYERALIEN_NAME);

		PlayerPrefs.DeleteKey(KEYPREF_PLAYERALIEN_LEVEL);
		PlayerPrefs.DeleteKey(KEYPREF_PLAYERALIEN_GROWTH);

		PlayerPrefs.DeleteKey(KEYPREF_PLAYERALIEN_HUNGER);
		PlayerPrefs.DeleteKey(KEYPREF_PLAYERALIEN_HYGENE);
		PlayerPrefs.DeleteKey(KEYPREF_PLAYERALIEN_HAPPINESS);
		PlayerPrefs.DeleteKey(KEYPREF_PLAYERALIEN_HEALTH);

		PlayerPrefs.DeleteKey(KEYPREF_PLAYERALIEN_HUNGERMOD);
		PlayerPrefs.DeleteKey(KEYPREF_PLAYERALIEN_HYGENEMOD);
		PlayerPrefs.DeleteKey(KEYPREF_PLAYERALIEN_HAPPINESSMOD);
		PlayerPrefs.DeleteKey(KEYPREF_PLAYERALIEN_HEALTHMOD);

		PlayerPrefs.DeleteKey(KEYPREF_PLAYERALIEN_COINMOD);
		PlayerPrefs.Save();
	}

	#region setter getter
	const string KEYPREF_PLAYERALIEN_NAME = "PlayerAlien/Name";

	const string KEYPREF_PLAYERALIEN_LEVEL = "PlayerAlien/Level";
	const string KEYPREF_PLAYERALIEN_GROWTH = "PlayerAlien/Growth";

	const string KEYPREF_PLAYERALIEN_HUNGER = "PlayerAlien/Hunger";
	const string KEYPREF_PLAYERALIEN_HYGENE = "PlayerAlien/Hygene";
	const string KEYPREF_PLAYERALIEN_HAPPINESS = "PlayerAlien/Happiness";
	const string KEYPREF_PLAYERALIEN_HEALTH = "PlayerAlien/Health";

	const string KEYPREF_PLAYERALIEN_GROWTHMOD = "PlayerAlien/GrowthMod";

	const string KEYPREF_PLAYERALIEN_HUNGERMOD = "PlayerAlien/HungerMod";
	const string KEYPREF_PLAYERALIEN_HYGENEMOD = "PlayerAlien/HygeneMod";
	const string KEYPREF_PLAYERALIEN_HAPPINESSMOD = "PlayerAlien/HappinessMod";
	const string KEYPREF_PLAYERALIEN_HEALTHMOD = "PlayerAlien/HealthMod";
	const string KEYPREF_PLAYERALIEN_COINMOD = "PlayerAlien/CoinMod";

	public string alienName{
		get{return PlayerPrefs.GetString(KEYPREF_PLAYERALIEN_NAME);}
		set{PlayerPrefs.SetString(KEYPREF_PLAYERALIEN_NAME,value); }
	}
	public AlienType alienType{
		get{return alienSO.alienType;}
	}
	public int alienLevel{
		get{return PlayerPrefs.GetInt(KEYPREF_PLAYERALIEN_LEVEL,1);}
		set{PlayerPrefs.SetInt(KEYPREF_PLAYERALIEN_LEVEL,value);}
	}
	public int alienGrowth{
		get{return PlayerPrefs.GetInt(KEYPREF_PLAYERALIEN_GROWTH);}
		set{PlayerPrefs.SetInt(KEYPREF_PLAYERALIEN_GROWTH,value);}
	}

	public float alienHunger{
		get{ return PlayerPrefs.GetFloat(KEYPREF_PLAYERALIEN_HUNGER);}
		set{ PlayerPrefs.SetFloat(KEYPREF_PLAYERALIEN_HUNGER,value);}
	}
	public float alienHygene{
		get{return PlayerPrefs.GetFloat(KEYPREF_PLAYERALIEN_HYGENE);}
		set{PlayerPrefs.SetFloat(KEYPREF_PLAYERALIEN_HYGENE,value);}
	}
	public float alienHappiness{
		get{return PlayerPrefs.GetFloat(KEYPREF_PLAYERALIEN_HAPPINESS);}
		set{PlayerPrefs.SetFloat(KEYPREF_PLAYERALIEN_HAPPINESS,value);}
	}
	public float alienHealth{
		get{return PlayerPrefs.GetFloat(KEYPREF_PLAYERALIEN_HEALTH);}
		set{PlayerPrefs.SetFloat(KEYPREF_PLAYERALIEN_HEALTH,value);}
	}

	public int alienGrowthMod{
		get{return PlayerPrefs.GetInt(KEYPREF_PLAYERALIEN_GROWTHMOD);}
		set{PlayerPrefs.SetInt(KEYPREF_PLAYERALIEN_GROWTHMOD,value);}
	}

	public float alienHungerMod{
		get{return PlayerPrefs.GetFloat(KEYPREF_PLAYERALIEN_HUNGERMOD);}
		set{PlayerPrefs.SetFloat(KEYPREF_PLAYERALIEN_HUNGERMOD,value);}
	}
	public float alienHygeneMod{
		get{return PlayerPrefs.GetFloat(KEYPREF_PLAYERALIEN_HYGENEMOD);}
		set{PlayerPrefs.SetFloat(KEYPREF_PLAYERALIEN_HYGENEMOD,value);}
	}
	public float alienHappinessMod{
		get{return PlayerPrefs.GetFloat(KEYPREF_PLAYERALIEN_HAPPINESSMOD);}
		set{PlayerPrefs.SetFloat(KEYPREF_PLAYERALIEN_HAPPINESSMOD,value);}
	}
	public float alienHealthMod{
		get{return PlayerPrefs.GetFloat(KEYPREF_PLAYERALIEN_HEALTHMOD);}
		set{PlayerPrefs.SetFloat(KEYPREF_PLAYERALIEN_HEALTHMOD,value);}
	}

	public int alienCoinMod{
		get{return PlayerPrefs.GetInt(KEYPREF_PLAYERALIEN_COINMOD);}
		set{PlayerPrefs.SetInt(KEYPREF_PLAYERALIEN_COINMOD,value);}
	}

	public int alienCoinGenerationHit{
		get{return alienSO.alienCoinGenerationHit;}
	}
	public int alienCoinGenerationDuration{
		get{return alienSO.alienCoinGenerationDuration;}
	}

	public int alienGrowthGapPerLevel{
		get{return alienSO.alienGrowthGapPerLevel;}
	}
	public int alienGrowtHit{
		get{return alienSO.alienGrowthHit;}
	}
	public int alienGrowthDuration{
		get{return alienSO.alienGrowthDuration;}
	}

	public float alienStatsDepletionHit{
		get{return alienSO.alienStatsDepletionHit;}
	}
	public float alienHealthDepletionHit{
		get{return alienSO.alienHealthDepletionHit;}
	}
	public float alienStatsDepletionHitSpecial{
		get{return alienSO.alienStatsDepletionHitSpecial;}
	}
	public int alienStatsDepletionDuration{
		get{return alienSO.alienStatsDepletionDuration;}
	}
	public int alienHealthDepletionDuration{
		get{return alienSO.alienHealthDepletionDuration;}
	}
	#endregion
}