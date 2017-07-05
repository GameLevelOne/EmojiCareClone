using UnityEngine.UI;
using UnityEngine;

public class MainHUDController : MonoBehaviour {
	public AlienHUDMeter hungerMeter, hygeneMeter, happinessMeter, healthMeter;
	public Image imageGrowth;
	public Text textAlienName, textAlienType, textAlienLevel, textAlienGrowth;
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
		hungerMeter.ModHUD(PlayerData.Instance.PlayerAlien.alienHungerMod,PlayerData.Instance.PlayerAlien.alienHunger);
		hygeneMeter.ModHUD(PlayerData.Instance.PlayerAlien.alienHygeneMod, PlayerData.Instance.PlayerAlien.alienHygene);
		happinessMeter.ModHUD(PlayerData.Instance.PlayerAlien.alienHappinessMod, PlayerData.Instance.PlayerAlien.alienHappiness);
		healthMeter.ModHUD(PlayerData.Instance.PlayerAlien.alienHealthMod, PlayerData.Instance.PlayerAlien.alienHealth);
	}

	public void UpdateAlienNameAndType()
	{
		textAlienName.text = PlayerData.Instance.PlayerAlien.alienName;
		textAlienType.text = "Type: "+ PlayerData.Instance.PlayerAlien.alienType.ToString();
	}
}