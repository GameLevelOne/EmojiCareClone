using UnityEngine.UI;
using UnityEngine;

public class PanelGatherResultController : MonoBehaviour {
	public Sprite[] spriteCategory;
	public Image imagePositive, imageNegative;
	public Text textPositive, textNegative;
	public Text textHunger, textHygene, textHappiness, textHealth;

	public GameObject content, contentHealth;

	int positive, negative;
	int hunger, hygene, happiness, health;

	public void ShowResult(AlienNeedCategory category, int[] result)
	{
		Alien playerAlien = PlayerData.Instance.PlayerAlien;

		if(category == AlienNeedCategory.HEALTH){
			hunger = hygene = happiness = health = 0;

			for(int i = 0;i<result.Length;i++){
				switch(result[i]){
				case 1: hunger++; break;
				case 2: hygene++; break;
				case 3: happiness++; break;
				case 4: health++; break;
				default: break;
				}
			}

			textHunger.text = "-"+hunger.ToString();
			textHygene.text = "-"+hygene.ToString();
			textHappiness.text = "-"+happiness.ToString();
			textHealth.text = "+"+health.ToString();

			playerAlien.alienHungerMod -= hunger;
			playerAlien.alienHygeneMod -= hygene;
			playerAlien.alienHappinessMod -= happiness;
			playerAlien.alienHealthMod += health;

			content.SetActive(false);
			contentHealth.SetActive(true);
			AlienStatsController.Instance.CheckAlienStatsForGrowth();
		}else{
			positive = negative = 0;

			for(int i = 0;i<result.Length;i++){
				if(result[i] > 0) positive++;
				else if(result[i] < 0) negative++;
			}
			textPositive.text = "+"+positive.ToString();
			textNegative.text = "-"+negative.ToString();

			imagePositive.sprite = spriteCategory[(int)category];
			switch(category){
			case AlienNeedCategory.HUNGER: 
				playerAlien.alienHungerMod += positive;
				playerAlien.alienHygeneMod -= negative;
				imageNegative.sprite = spriteCategory[1]; 
				break;
			case AlienNeedCategory.HYGENE: 
				playerAlien.alienHygeneMod += positive;
				playerAlien.alienHappinessMod -= negative;
				imageNegative.sprite = spriteCategory[2]; 
				break;
			case AlienNeedCategory.HAPPINESS:
				playerAlien.alienHappinessMod += positive;
				playerAlien.alienHungerMod -= negative;
				imageNegative.sprite = spriteCategory[0];
				break;
			default: break;
			}
			content.SetActive(true);
			contentHealth.SetActive(false);
		}

		//adjust so the stats will not go over or lower the limit (0 until maximum stats available)
		if(playerAlien.alienHungerMod >= playerAlien.alienHunger) 
			playerAlien.alienHungerMod = playerAlien.alienHunger;
		if(playerAlien.alienHygeneMod >= playerAlien.alienHygene) 
			playerAlien.alienHygeneMod = playerAlien.alienHygene;
		if(playerAlien.alienHappinessMod >= playerAlien.alienHappiness) 
			playerAlien.alienHappinessMod = playerAlien.alienHappiness;
		if(playerAlien.alienHealthMod >= playerAlien.alienHealth)
			playerAlien.alienHealthMod = playerAlien.alienHealth;

		if(playerAlien.alienHungerMod <= 0f) playerAlien.alienHungerMod = 0f;
		if(playerAlien.alienHygeneMod <= 0f) playerAlien.alienHygeneMod = 0f;
		if(playerAlien.alienHappinessMod <= 0f) playerAlien.alienHappinessMod = 0f;
		if(playerAlien.alienHealthMod <= 0f) playerAlien.alienHealthMod = 0f;

		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
}