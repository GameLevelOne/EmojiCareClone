using UnityEngine.UI;
using UnityEngine;

public class PanelGatherResultController : MonoBehaviour {
	public Sprite[] spriteCategory;
	public Image imagePositive, imageNegative;
	public Text textPositive, textNegative;
	public Text textHunger, textHygene, textHappiness, textHealth;

	public GameObject content, contentHealth;
	public MainHUDController hudController;

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

			hudController.StoreGatherData(-1 * hunger,-1 * hygene,-1 * happiness,health);

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
				hudController.StoreGatherData(positive,-1 * negative,0,0);
				imageNegative.sprite = spriteCategory[1]; 
				break;
			case AlienNeedCategory.HYGENE: 
				hudController.StoreGatherData(0,positive,-1 * negative,0);
				imageNegative.sprite = spriteCategory[2]; 
				break;
			case AlienNeedCategory.HAPPINESS:
				hudController.StoreGatherData(-1 * negative,0,positive,0);
				imageNegative.sprite = spriteCategory[0];
				break;
			default: break;
			}
			content.SetActive(true);
			contentHealth.SetActive(false);
		}
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
}