using UnityEngine.UI;
using UnityEngine;

public class MainHUDController : MonoBehaviour {
	public AlienHUDMeter hungerMeter, hygeneMeter, happinessMeter, healthMeter;
	public Image imageGrowth;
	public Text textAlienName, textAlienType, textAlienLevel, textAlienGrowth;

	public void InitStats()
	{
		hungerMeter.InitHUD(PlayerData.Instance.PlayerAlien.alienHungerMod,PlayerData.Instance.PlayerAlien.alienHunger);
		hygeneMeter.InitHUD(PlayerData.Instance.PlayerAlien.alienHygeneMod, PlayerData.Instance.PlayerAlien.alienHygene);
		happinessMeter.InitHUD(PlayerData.Instance.PlayerAlien.alienHappinessMod, PlayerData.Instance.PlayerAlien.alienHappiness);
		healthMeter.InitHUD(PlayerData.Instance.PlayerAlien.alienHealthMod, PlayerData.Instance.PlayerAlien.alienHealth);
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
		if(playerAlien.alienHungerMod >= 0f){ 
			print("hungerMod");
			hungerMeter.ModHUD(playerAlien.alienHungerMod,playerAlien.alienHunger);
		}
		if(playerAlien.alienHygeneMod >= 0f) {
			print("hygeneMod");
			hygeneMeter.ModHUD(playerAlien.alienHygeneMod, playerAlien.alienHygene);
		}
		if(playerAlien.alienHappinessMod >= 0f) {
			print("happinessMod");
			happinessMeter.ModHUD(playerAlien.alienHappinessMod, playerAlien.alienHappiness);
		}
		if(playerAlien.alienHealthMod >= 0f) {
			print("healthMod");
			healthMeter.ModHUD(playerAlien.alienHealthMod, playerAlien.alienHealth);
		}
		playerAlien.AdjustStats();
	}

	public void UpdateAlienNameAndType()
	{
		textAlienName.text = PlayerData.Instance.PlayerAlien.alienName.ToUpper();
		textAlienType.text = "TYPE: "+ PlayerData.Instance.PlayerAlien.alienType.ToString().ToUpper();
	}
}