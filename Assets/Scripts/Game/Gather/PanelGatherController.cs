﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGatherController : MonoBehaviour {
	const int MAX_ATTEMP = 10;
	const int MAX_PLUS_ITEM = 10;
	const int MAX_MINUS_ITEM = 5;
	const int MAX_MINUS_HEALTH = 1;

	[Header("HUD")]
	public Text textAttemp;
	public Text[] textScore;

	[Header("Data")]
	public PanelGatherResultController panelResult;
	public Sprite spriteFeed;
	public Sprite spriteClean;
	public Sprite spritePlay;
	public Sprite spriteNurse;
	public GatherSlot[] gatherSlots;

	AlienNeedCategory alienNeedCategory;
	int attemp;
	int[] attempResult;
	int[] gatherScore  = new int[4]{0,0,0,0};

	void OnDisable()
	{
		for(int i = 0;i<gatherSlots.Length;i++) gatherSlots[i].OnRevealSlot -= OnUseAttemp;
	}

	public void InitGatherStats(AlienNeedCategory category)
	{
		panelResult.Hide();
		attemp = MAX_ATTEMP;
		textAttemp.text = attemp.ToString();
		attempResult = new int[MAX_ATTEMP];
		gatherScore = new int[4]{0,0,0,0};
		alienNeedCategory = category;

		for(int i = 0;i<textScore.Length;i++) textScore[i].enabled = false;

		for(int i = 0;i<gatherSlots.Length;i++){
			gatherSlots[i].OnRevealSlot += OnUseAttemp;
			gatherSlots[i].InitSlot();
		} 

		InitCategoryItem();
	}

	void InitCategoryItem()
	{
		switch(alienNeedCategory){
		case AlienNeedCategory.HUNGER:
			SetGatherSlotContents(spriteFeed,spriteClean);
			break;
		case AlienNeedCategory.HYGENE:
			SetGatherSlotContents(spriteClean,spritePlay);
			break;
		case AlienNeedCategory.HAPPINESS: 
			SetGatherSlotContents(spritePlay,spriteFeed);
			break;
		case AlienNeedCategory.HEALTH:
			SetGatherSlotContentHealth();
			break;
		default: break;
		}
	}

	void SetGatherSlotContents(Sprite positiveSprite, Sprite negativeSprite)
	{
		List<int> randBoard = new List<int>();

		for (int i=0; i<gatherSlots.Length;i++) randBoard.Add(i);

		for(int i = 0;i<MAX_PLUS_ITEM;i++){
			int rndIndex = Random.Range(0,randBoard.Count);
			int rndSlot = randBoard[rndIndex];
			randBoard.RemoveAt(rndIndex);

			gatherSlots[rndSlot].SetContent(positiveSprite,1);
		}

		for(int i = 0;i<MAX_MINUS_ITEM;i++){
			int rndIndex = Random.Range(0,randBoard.Count);
			int rndSlot = randBoard[rndIndex];
			randBoard.RemoveAt(rndIndex);

			gatherSlots[rndSlot].SetContent(negativeSprite,-1);
		}
	}

	void SetGatherSlotContentHealth()
	{
		List<int> randBoard = new List<int>();

		for (int i=0; i<gatherSlots.Length;i++) randBoard.Add(i);

		for(int i = 0;i<MAX_PLUS_ITEM;i++){
			int rndIndex = Random.Range(0,randBoard.Count);
			int rndSlot = randBoard[rndIndex];
			randBoard.RemoveAt(rndIndex);

			gatherSlots[rndSlot].SetContent(spriteNurse,4);
		}

		for(int i = 0;i<(MAX_MINUS_ITEM/3);i++){
			int rndIndex = Random.Range(0,randBoard.Count);
			int rndSlot = randBoard[rndIndex];
			randBoard.RemoveAt(rndIndex);

			gatherSlots[rndSlot].SetContent(spriteFeed,1);
		}

		for(int i = 0;i<(MAX_MINUS_ITEM/3);i++){
			int rndIndex = Random.Range(0,randBoard.Count);
			int rndSlot = randBoard[rndIndex];
			randBoard.RemoveAt(rndIndex);

			gatherSlots[rndSlot].SetContent(spriteClean,2);
		}

		for(int i = 0;i<(MAX_MINUS_ITEM/3);i++){
			int rndIndex = Random.Range(0,randBoard.Count);
			int rndSlot = randBoard[rndIndex];
			randBoard.RemoveAt(rndIndex);

			gatherSlots[rndSlot].SetContent(spritePlay,3);
		}
	}

	void OnUseAttemp(int key)
	{
		int tempIndex = MAX_ATTEMP - attemp;
		attempResult[tempIndex] = key;
		attemp--;
		textAttemp.text = attemp.ToString();

		CheckScores(key);

		if(attemp <= 0){
			ShowResult();
		}
	}

	void CheckScores(int key)
	{
		if(alienNeedCategory == AlienNeedCategory.HUNGER){
			if(key < 0){// -
				gatherScore[(int)AlienNeedCategory.HYGENE]--;
			}else if(key > 0){// +
				gatherScore[(int)AlienNeedCategory.HUNGER]++;
			}
		}else if(alienNeedCategory == AlienNeedCategory.HYGENE){
			if(key < 0){// -
				gatherScore[(int)AlienNeedCategory.HAPPINESS]--;
			}else if(key > 0){// +
				gatherScore[(int)AlienNeedCategory.HYGENE]++;
			}
		}else if(alienNeedCategory == AlienNeedCategory.HAPPINESS){
			if(key < 0){// -
				gatherScore[(int)AlienNeedCategory.HUNGER]--;
			}else if(key > 0){// +
				gatherScore[(int)AlienNeedCategory.HAPPINESS]++;
			}
		}else if(alienNeedCategory == AlienNeedCategory.HEALTH){
			switch(key){
			case 1: gatherScore[(int)AlienNeedCategory.HUNGER]--; break;
			case 2: gatherScore[(int)AlienNeedCategory.HYGENE]--; break;
			case 3: gatherScore[(int)AlienNeedCategory.HAPPINESS]--; break;
			case 4: gatherScore[(int)AlienNeedCategory.HEALTH]++; break;
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
		for(int i = 0;i<gatherSlots.Length;i++) gatherSlots[i].OnRevealSlot -= OnUseAttemp;
		panelResult.ShowResult(alienNeedCategory,attempResult);
	}

	public void ResetAnimation()
	{
		for(int i = 0;i<gatherSlots.Length;i++) gatherSlots[i].ResetContentAnimation();
	}
}