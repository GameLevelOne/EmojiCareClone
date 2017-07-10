using UnityEngine.UI;
using UnityEngine;

public class PanelGatherResultController : MonoBehaviour {
	public Sprite[] spriteCategory;
	public Image imagePositive, imageNegative;
	public Text textPositive, textNegative;
	public Text textHunger, textHygene, textHappiness, textHealth;

	public GameObject content, contentHealth;
	public MainHUDController hudController;
	public Button buttonOk;

	int positive, negative;
	int hunger, hygene, happiness, health;

	Animator thisAnim;

	void Awake()
	{
		thisAnim = GetComponent<Animator>();
	}

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
		playerAlien.AdjustStats();
		buttonOk.interactable = true;
		thisAnim.SetTrigger("Show");
	}
}