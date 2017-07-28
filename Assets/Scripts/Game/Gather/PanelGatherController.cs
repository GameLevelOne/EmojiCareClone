using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum EmojiNeedCategory{
	HUNGER,
	HYGENE,
	HAPPINESS,
	HEALTH
}

public class PanelGatherController : MonoBehaviour {
	const int MAX_ATTEMP = 10;
	const int MAX_PLUS_ITEM = 10;
	const int MAX_MINUS_ITEM = 4;
	const int MAX_MINUS_HEALTH = 1;

	public TextAttemp textAttemp;
	public Text[] textScore;

	public PanelGatherResultController panelResult;
	public Sprite[] spriteFeed;
	public Sprite[] spriteClean;
	public Sprite[] spritePlay;
	public Sprite[] spriteNurse;
	public Sprite spriteCoin;
	public GatherSlot[] gatherSlots;

	 EmojiNeedCategory emojiNeedCategory;
	int attemp;
	int[] attempResult;
	int[] gatherScore  = new int[4]{0,0,0,0};

	void OnDisable()
	{
		for(int i = 0;i<gatherSlots.Length;i++) gatherSlots[i].OnRevealSlot -= OnUseAttemp;
	}

	public void InitGatherStats(EmojiNeedCategory category)
	{
		attemp = MAX_ATTEMP;
		textAttemp.SetAttemp(MAX_ATTEMP);
		attempResult = new int[MAX_ATTEMP];
		gatherScore = new int[4]{0,0,0,0};
		emojiNeedCategory = category;

		for(int i = 0;i<textScore.Length;i++) textScore[i].enabled = false;

		for(int i = 0;i<gatherSlots.Length;i++){
			gatherSlots[i].OnRevealSlot += OnUseAttemp;
			gatherSlots[i].InitSlot(category);
		} 

		InitCategoryItem();
		GetComponent<Animator>().SetTrigger("Animate");
	}

	void InitCategoryItem()
	{
		switch(emojiNeedCategory){
		case  EmojiNeedCategory.HUNGER:
			SetGatherSlotContents(spriteFeed[0],spriteClean[1]);
			break;
		case  EmojiNeedCategory.HYGENE:
			SetGatherSlotContents(spriteClean[0],spritePlay[1]);
			break;
		case  EmojiNeedCategory.HAPPINESS: 
			SetGatherSlotContents(spritePlay[0],spriteFeed[1]);
			break;
		case  EmojiNeedCategory.HEALTH:
			SetGatherSlotContentHealth();
			break;
		default: break;
		}
	}
		
	void SetGatherSlotContents(Sprite positiveSprite, Sprite negativeSprite)
	{
		List<int> randBoard = new List<int>();

		for (int i=0; i<gatherSlots.Length;i++) randBoard.Add(i);

		for(int i = 0;i<MAX_PLUS_ITEM;i++){ //setup positive need items
			int rndIndex = Random.Range(0,randBoard.Count);
			int rndSlot = randBoard[rndIndex];
			randBoard.RemoveAt(rndIndex);

			gatherSlots[rndSlot].SetContent(positiveSprite,1);
		}

		for(int i = 0;i<MAX_MINUS_ITEM;i++){ //setup negative need items
			int rndIndex = Random.Range(0,randBoard.Count);
			int rndSlot = randBoard[rndIndex];
			randBoard.RemoveAt(rndIndex);

			gatherSlots[rndSlot].SetContent(negativeSprite,-1);
		}

		GenerateCoin(randBoard);
	}

	void SetGatherSlotContentHealth()
	{
		List<int> randBoard = new List<int>();

		for (int i=0; i<gatherSlots.Length;i++) randBoard.Add(i);

		for(int i = 0;i<MAX_PLUS_ITEM;i++){ //setup positive health item
			int rndIndex = Random.Range(0,randBoard.Count);
			int rndSlot = randBoard[rndIndex];
			randBoard.RemoveAt(rndIndex);

			gatherSlots[rndSlot].SetContent(spriteNurse[0],4);
		}

		for(int i = 0;i<(MAX_MINUS_ITEM/3);i++){ //setup negative items (feed, clean, play)
			int rndIndex = Random.Range(0,randBoard.Count);
			int rndSlot = randBoard[rndIndex];
			randBoard.RemoveAt(rndIndex);

			gatherSlots[rndSlot].SetContent(spriteFeed[1],1);

			rndIndex = Random.Range(0,randBoard.Count);
			rndSlot = randBoard[rndIndex];
			randBoard.RemoveAt(rndIndex);

			gatherSlots[rndSlot].SetContent(spriteClean[1],2);

			rndIndex = Random.Range(0,randBoard.Count);
			rndSlot = randBoard[rndIndex];
			randBoard.RemoveAt(rndIndex);

			gatherSlots[rndSlot].SetContent(spritePlay[1],3);
		}

		GenerateCoin(randBoard);
	}

	void GenerateCoin(List<int> randBoard)
	{
		int rndCoinAmount = Random.Range(0,6); //random between 0 - 5 coins
		for(int i = 0;i < rndCoinAmount; i++){
			int rndIndex = Random.Range(0,randBoard.Count);
			int rndSlot = randBoard[rndIndex];
			randBoard.RemoveAt(rndIndex);

			gatherSlots[rndSlot].SetContent(spriteCoin,5);
		}
	}

	void OnUseAttemp(int key)
	{
		int tempIndex = MAX_ATTEMP - attemp;
		attempResult[tempIndex] = key;
		attemp--;
		textAttemp.SetAttemp(attemp);

		CheckScores(key);

		if(attemp <= 0){
			ShowResult();
		}
	}

	void CheckScores(int key)
	{
		if(emojiNeedCategory == EmojiNeedCategory.HUNGER){
			if(key == -1){
				gatherScore[(int) EmojiNeedCategory.HYGENE]--;
				PlayerData.Instance.PlayerEmoji.emojiCleanNegativeCount++;
				EmojiUnlockConditions.Instance.CheckUnlock(UnlockCondition.CleanNegIconCount);
				SoundManager.Instance.PlaySFX(eSFX.GATHER_SLOT_NEGATIVE);
			}
			else if(key == 1){
				gatherScore[(int) EmojiNeedCategory.HUNGER]++;
				PlayerData.Instance.PlayerEmoji.emojiFeedPositiveCount++;
				EmojiUnlockConditions.Instance.CheckUnlock(UnlockCondition.FeedPosIconCount);
				SoundManager.Instance.PlaySFX(eSFX.GATHER_SLOT_POSITIVE);
			}

		}else if(emojiNeedCategory ==  EmojiNeedCategory.HYGENE){
			if(key == -1){ 
				gatherScore[(int) EmojiNeedCategory.HAPPINESS]--;
				PlayerData.Instance.PlayerEmoji.emojiPlayNegativeCount++;
				EmojiUnlockConditions.Instance.CheckUnlock(UnlockCondition.PlayNegIconCount);
				SoundManager.Instance.PlaySFX(eSFX.GATHER_SLOT_NEGATIVE);

			}
			else if(key == 1){ 
				gatherScore[(int) EmojiNeedCategory.HYGENE]++;
				PlayerData.Instance.PlayerEmoji.emojiCleanPositiveCount++;
				EmojiUnlockConditions.Instance.CheckUnlock(UnlockCondition.CleanPosIconCount);
				SoundManager.Instance.PlaySFX(eSFX.GATHER_SLOT_POSITIVE);
			}

		}else if(emojiNeedCategory ==  EmojiNeedCategory.HAPPINESS){
			if(key == -1){
				gatherScore[(int) EmojiNeedCategory.HUNGER]--;
				PlayerData.Instance.PlayerEmoji.emojiFeedNegativeCount++;
				EmojiUnlockConditions.Instance.CheckUnlock(UnlockCondition.FeedNegIconCount);
				SoundManager.Instance.PlaySFX(eSFX.GATHER_SLOT_NEGATIVE);
			}
			else if(key == 1){
				gatherScore[(int) EmojiNeedCategory.HAPPINESS]++;
				PlayerData.Instance.PlayerEmoji.emojiPlayPositiveCount++;
				EmojiUnlockConditions.Instance.CheckUnlock(UnlockCondition.PlayPosIconCount);
				SoundManager.Instance.PlaySFX(eSFX.GATHER_SLOT_POSITIVE);
			}

		}else if(emojiNeedCategory ==  EmojiNeedCategory.HEALTH){
			switch(key){
			case 1: gatherScore[(int) EmojiNeedCategory.HUNGER]--; 
					PlayerData.Instance.PlayerEmoji.emojiFeedNegativeCount++;
					EmojiUnlockConditions.Instance.CheckUnlock(UnlockCondition.FeedNegIconCount);
					SoundManager.Instance.PlaySFX(eSFX.GATHER_SLOT_NEGATIVE); 
					break;

			case 2: gatherScore[(int) EmojiNeedCategory.HYGENE]--; 
					PlayerData.Instance.PlayerEmoji.emojiCleanNegativeCount++;
					EmojiUnlockConditions.Instance.CheckUnlock(UnlockCondition.CleanNegIconCount);
					SoundManager.Instance.PlaySFX(eSFX.GATHER_SLOT_NEGATIVE);
					break;

			case 3: gatherScore[(int) EmojiNeedCategory.HAPPINESS]--; 
					PlayerData.Instance.PlayerEmoji.emojiPlayNegativeCount++;
					EmojiUnlockConditions.Instance.CheckUnlock(UnlockCondition.PlayNegIconCount);
					SoundManager.Instance.PlaySFX(eSFX.GATHER_SLOT_NEGATIVE);
					break;

			case 4: gatherScore[(int) EmojiNeedCategory.HEALTH]++;
					PlayerData.Instance.PlayerEmoji.emojiNursePositiveCount++;
					EmojiUnlockConditions.Instance.CheckUnlock(UnlockCondition.NursePosIconCount);
					SoundManager.Instance.PlaySFX(eSFX.GATHER_SLOT_POSITIVE);
					break;

			default: break;
			}
		}

		UpdateDisplayScore();
	}

	void UpdateDisplayScore()
	{
		for(int i=0;i<textScore.Length;i++){
			if(gatherScore[i] == 0) textScore[i].enabled = false;
			else{
				string plusMin = gatherScore[i] > 0 ? "+" : gatherScore[i] < 0 ? "-" : string.Empty;
				Color color = gatherScore[i] > 0 ? Color.green : gatherScore[i] < 0 ? Color.red : Color.white;
				textScore[i].text = plusMin + gatherScore[i].ToString();
				textScore[i].color = color;
				textScore[i].enabled = true;
			}
		}
	}

	void ShowResult()
	{
		for(int i = 0;i<gatherSlots.Length;i++){ 
			Button tempButton = gatherSlots[i].transform.GetChild(1).GetComponent<Button>();
			if(tempButton.interactable == true){
				tempButton.gameObject.GetComponent<Animator>().SetTrigger("Click");
				gatherSlots[i].ShowContent(new Color(0.5f,0.5f,0.5f));
			}
		}
		panelResult.ShowResult(emojiNeedCategory,attempResult);
	}
}