using UnityEngine.UI;
using System.Collections;
using UnityEngine;

public class PanelGatherResultController : MonoBehaviour {
	public Sprite[] spriteCategory;
	public Image imagePositive, imageNegative;
	public Text textPositive, textNegative;
	public Text textHunger, textHygene, textHappiness, textHealth;
	public Text textCoin;

	public GameObject content, contentHealth;
	public MainHUDController hudController;
	public Button buttonOk;

	int positive, negative;
	int hunger, hygene, happiness, health;
	int coin;

	Animator thisAnim;

	void Awake()
	{
		thisAnim = GetComponent<Animator>();
	}

	public void ShowResult(EmojiNeedCategory category, int[] result)
	{
		Emoji playerEmoji = PlayerData.Instance.PlayerEmoji;
		coin = 0;
		if(category == EmojiNeedCategory.HEALTH){
			hunger = hygene = happiness = health = 0;

			for(int i = 0;i<result.Length;i++){
				switch(result[i]){
				case 1: hunger++; break;
				case 2: hygene++; break;
				case 3: happiness++; break;
				case 4: health++; break;
				default: break;
				}

				if(result[i] == 5) coin++;
			}

			textHunger.text = "-"+hunger.ToString();
			textHygene.text = "-"+hygene.ToString();
			textHappiness.text = "-"+happiness.ToString();
			textHealth.text = "+"+health.ToString();

			playerEmoji.ModStats(EmojiStats.HUNGER,-1*hunger);
			playerEmoji.ModStats(EmojiStats.HYGENE,-1*hygene);
			playerEmoji.ModStats(EmojiStats.HAPPINESS,-1*happiness);
			playerEmoji.ModStats(EmojiStats.HEALTH,health);
			content.SetActive(false);
			contentHealth.SetActive(true);
		}else{
			positive = negative = 0;

			for(int i = 0;i<result.Length;i++){
				if(result[i] == 1) positive++;
				else if(result[i] == -1) negative++;

				if(result[i] == 5) coin++;
			}
			textPositive.text = "+"+positive.ToString();
			textNegative.text = "-"+negative.ToString();
			imagePositive.sprite = spriteCategory[(int)category];
			switch(category){
			case EmojiNeedCategory.HUNGER: 
				playerEmoji.ModStats(EmojiStats.HUNGER,positive);
				playerEmoji.ModStats(EmojiStats.HYGENE,-negative);
				imageNegative.sprite = spriteCategory[1];
				break;
			case EmojiNeedCategory.HYGENE: 
				playerEmoji.ModStats(EmojiStats.HYGENE,positive);
				playerEmoji.ModStats(EmojiStats.HAPPINESS,-negative);
				imageNegative.sprite = spriteCategory[2];
				break;
			case EmojiNeedCategory.HAPPINESS:
				playerEmoji.ModStats(EmojiStats.HAPPINESS,positive);
				playerEmoji.ModStats(EmojiStats.HUNGER,-negative);
				imageNegative.sprite = spriteCategory[0];
				break;
			default: break;
			}
			content.SetActive(true);
			contentHealth.SetActive(false);

		}
		int playerGetCoin = coin*10;
		PlayerData.Instance.playerCoin += playerGetCoin;
		textCoin.text = playerGetCoin.ToString();

		buttonOk.interactable = true;
		StartCoroutine(DelayShow());
	}

	IEnumerator DelayShow()
	{
		SoundManager.Instance.PlaySFX(eSFX.GATHER_RESULT);
		yield return new WaitForSeconds(1.5f);
		thisAnim.SetTrigger("Show");
	}
}