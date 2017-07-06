using UnityEngine.UI;
using UnityEngine;

public class MainHUDController : MonoBehaviour {
	public AlienHUDMeter hungerMeter, hygeneMeter, happinessMeter, healthMeter;
	public Image imageGrowth;
	public Text textAlienName, textAlienType, textAlienLevel, textAlienGrowth;
	float hunger,hygene,happiness,health;

	public void InitStats()
	{
		hungerMeter.InitHUD(PlayerData.Instance.PlayerAlien.alienHungerMod,PlayerData.Instance.PlayerAlien.alienHunger);
		hygeneMeter.InitHUD(PlayerData.Instance.PlayerAlien.alienHygeneMod, PlayerData.Instance.PlayerAlien.alienHygene);
		happinessMeter.InitHUD(PlayerData.Instance.PlayerAlien.alienHappinessMod, PlayerData.Instance.PlayerAlien.alienHappiness);
		healthMeter.InitHUD(PlayerData.Instance.PlayerAlien.alienHealthMod, PlayerData.Instance.PlayerAlien.alienHealth);
	}

	public void StoreGatherData(int hunger, int hygene, int happiness, int health)
	{
		this.hunger = (float) hunger;
		this.hygene = (float) hygene;
		this.happiness = (float) happiness;
		this.health = (float) health;
		print(this.hunger+"/"+this.hygene+"/"+this.happiness+"/"+this.health);
	}

	void OnEnable()
	{
		if(PlayerData.Instance.playerAlienID != -1) {
			PlayerData.Instance.PlayerAlien.OnAlienDepleteStats += UpdateStatsMeter;
			PlayerData.Instance.PlayerAlien.OnAlienGetGrowth += UpdateAlienLevelAndGrowth;
		}
	}

	void OnDisable()
	{
		if(PlayerData.Instance.playerAlienID != -1) {
			PlayerData.Instance.PlayerAlien.OnAlienDepleteStats -= UpdateStatsMeter;
			PlayerData.Instance.PlayerAlien.OnAlienGetGrowth -= UpdateAlienLevelAndGrowth;
		}
	}

	public void UpdateAlienLevelAndGrowth()
	{
		textAlienLevel.text = "LVL "+ PlayerData.Instance.PlayerAlien.alienLevel.ToString();
		textAlienGrowth.text = PlayerData.Instance.PlayerAlien.alienGrowthMod.ToString() +"/"+ PlayerData.Instance.PlayerAlien.alienGrowth.ToString();
		imageGrowth.fillAmount = (float)PlayerData.Instance.PlayerAlien.alienGrowthMod/(float)PlayerData.Instance.PlayerAlien.alienGrowth;
	}

	public void UpdateStatsMeter()
	{
		Alien playerAlien = PlayerData.Instance.PlayerAlien;
		if(hunger != 0f && playerAlien.alienHungerMod+hunger >= 0f){ 
			print("hungerMod");
			hungerMeter.ModHUD(playerAlien.alienHungerMod+hunger,playerAlien.alienHunger);
			playerAlien.alienHungerMod += hunger;
		}
		if(hygene != 0f && playerAlien.alienHygeneMod+hygene >= 0f) {
			print("hygeneMod");
			hygeneMeter.ModHUD(playerAlien.alienHygeneMod+hygene, playerAlien.alienHygene);
			playerAlien.alienHygeneMod += hygene;
		}
		if(happiness != 0f && playerAlien.alienHappinessMod+happiness >= 0f) {
			print("happinessMod");
			happinessMeter.ModHUD(playerAlien.alienHappinessMod+happiness, playerAlien.alienHappiness);
			playerAlien.alienHappinessMod += happiness;
		}
		if(health != 0f && playerAlien.alienHealthMod+health >= 0f) {
			print("healthMod");
			healthMeter.ModHUD(playerAlien.alienHealthMod+health, playerAlien.alienHealth);
			playerAlien.alienHealthMod += health;
		}
		playerAlien.AdjustStats();
	}

	public void UpdateAlienNameAndType()
	{
		textAlienName.text = PlayerData.Instance.PlayerAlien.alienName;
		textAlienType.text = "Type: "+ PlayerData.Instance.PlayerAlien.alienType.ToString();
	}
}