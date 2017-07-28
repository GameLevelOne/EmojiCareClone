using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EmojiStats{
	HUNGER = 0,
	HYGENE,
	HAPPINESS,
	HEALTH
}

public class Emoji : MonoBehaviour {
	public EmojiSO emojiSO;
	public GameObject emojiPrefab; 

	Animator emojiAnimation;
	GameObject emojiClone;

	#region data
	const string Key_Emoji_Name = "PlayerEmoji/Name";
	const string Key_Emoji_Collection = "PlayerEmoji/Collection/";

	const string Key_Emoji_Hunger = "PlayerEmoji/Hunger";
	const string Key_Emoji_Hygene = "PlayerEmoji/Hygene";
	const string Key_Emoji_Happiness = "PlayerEmoji/Happiness";
	const string Key_Emoji_Health = "PlayerEmoji/Health";

	const string Key_Emoji_HungerMod = "PlayerEmoji/HungerMod";
	const string Key_Emoji_HygeneMod = "PlayerEmoji/HygeneMod";
	const string Key_Emoji_HappinessMod = "PlayerEmoji/HappinessMod";
	const string Key_Emoji_HealthMod = "PlayerEmoji/Healthmod";


	//collections
	const string Key_Emoji_Feed_Positive_Count = "PlayerEmoji/FeedPositiveCount";
	const string Key_Emoji_Feed_Negative_Count = "PlayerEmoji/FeedNegativeCount";
	const string Key_Emoji_Clean_Positive_Count = "PlayerEmoji/CleanPositiveCount";
	const string Key_Emoji_Clean_Negative_Count = "PlayerEmoji/CleanNegativeCount";
	const string Key_Emoji_Play_Positive_Count = "PlayerEmoji/PlayPositiveCount";
	const string Key_Emoji_Play_Negative_Count = "PlayerEmoji/PlayNegativeCount";
	const string Key_Emoji_Nurse_Positive_Count = "PlayerEmoji/NursePositiveCount";
	const string Key_Emoji_Nurse_Negative_Count = "PlayerEmoji/NurseNegativeCount";
	const string Key_Emoji_Tap_Count = "PlayerEmoji/TapCount";

	public string emojiName{
		get{ return PlayerPrefs.GetString(Key_Emoji_Name);}
		set{ PlayerPrefs.SetString(Key_Emoji_Name,value); }
	}
	public EmojiCollectionSO[] collectionSO{
		get{return emojiSO.collectionSO;}
	}

	public int emojiHunger{
		get{ return PlayerPrefs.GetInt(Key_Emoji_Hunger, emojiSO.emojiHunger);}
		set{ PlayerPrefs.SetInt(Key_Emoji_Hunger,value); }
	}
	public int emojiHygene{
		get{ return PlayerPrefs.GetInt(Key_Emoji_Hygene,emojiSO.emojiHygene);}
		set{ PlayerPrefs.SetInt(Key_Emoji_Hygene,value); }
	}
	public int emojiHappiness{
		get{ return PlayerPrefs.GetInt(Key_Emoji_Happiness,emojiSO.emojiHappiness);}
		set{ PlayerPrefs.SetInt(Key_Emoji_Happiness,value); }
	}
	public int emojiHealth{
		get{ return PlayerPrefs.GetInt(Key_Emoji_Health, emojiSO.emojiHealth);}
		set{ PlayerPrefs.SetInt(Key_Emoji_Health,value); }
	}

	public int emojiHungerMod{
		get{ return PlayerPrefs.GetInt(Key_Emoji_HungerMod, emojiSO.emojiHungerMod);}
		set{ PlayerPrefs.SetInt(Key_Emoji_HungerMod,value); }
	}
	public int emojiHygeneMod{
		get{ return PlayerPrefs.GetInt(Key_Emoji_HygeneMod, emojiSO.emojiHygeneMod);}
		set{ PlayerPrefs.SetInt(Key_Emoji_HygeneMod,value); }
	}
	public int emojiHappinessMod{
		get{ return PlayerPrefs.GetInt(Key_Emoji_HappinessMod, emojiSO.emojiHappinessMod);}
		set{ PlayerPrefs.SetInt(Key_Emoji_HappinessMod,value); }
	}
	public int emojiHealthMod{
		get{ return PlayerPrefs.GetInt(Key_Emoji_HealthMod, emojiSO.emojiHealthMod);}
		set{ PlayerPrefs.SetInt(Key_Emoji_HealthMod,value); }
	}

	public int emojiFeedPositiveCount{
		get{return PlayerPrefs.GetInt(Key_Emoji_Feed_Positive_Count,0);}
		set{PlayerPrefs.SetInt(Key_Emoji_Play_Positive_Count,value);}
	}
	public int emojiFeedNegativeCount{
		get{return PlayerPrefs.GetInt(Key_Emoji_Feed_Negative_Count,0);}
		set{PlayerPrefs.SetInt(Key_Emoji_Play_Positive_Count,value);}
	}
	public int emojiCleanPositiveCount{
		get{return PlayerPrefs.GetInt(Key_Emoji_Clean_Positive_Count,0);}
		set{PlayerPrefs.SetInt(Key_Emoji_Play_Positive_Count,value);}
	}
	public int emojiCleanNegativeCount{
		get{return PlayerPrefs.GetInt(Key_Emoji_Clean_Negative_Count,0);}
		set{PlayerPrefs.SetInt(Key_Emoji_Play_Positive_Count,value);}
	}
	public int emojiPlayPositiveCount{
		get{return PlayerPrefs.GetInt(Key_Emoji_Play_Positive_Count,0);}
		set{PlayerPrefs.SetInt(Key_Emoji_Play_Positive_Count,value);}
	}
	public int emojiPlayNegativeCount{
		get{return PlayerPrefs.GetInt(Key_Emoji_Play_Positive_Count,0);}
		set{PlayerPrefs.SetInt(Key_Emoji_Play_Positive_Count,value);}
	}
	public int emojiNursePositiveCount{
		get{return PlayerPrefs.GetInt(Key_Emoji_Nurse_Positive_Count,0);}
		set{PlayerPrefs.SetInt(Key_Emoji_Play_Positive_Count,value);}
	}
	public int emojiNurseNegativeCount{
		get{return PlayerPrefs.GetInt(Key_Emoji_Nurse_Negative_Count,0);}
		set{PlayerPrefs.SetInt(Key_Emoji_Play_Positive_Count,value);}
	}
	public int emojiTapCount{
		get{return PlayerPrefs.GetInt(Key_Emoji_Tap_Count,0);}
		set{PlayerPrefs.SetInt(Key_Emoji_Play_Positive_Count,value);}
	}
	#endregion

	#region delegate events
	public delegate void EmojiModStats();
	public delegate void EmojiDies();
	public delegate void EmojiSentOff();
	public event EmojiModStats OnEmojiModStats;
	public event EmojiDies OnEmojiDies;
	public event EmojiSentOff OnEmojiSentOff;
	#endregion

	public void InitEmoji(RectTransform parent)
	{
		InstantiateEmojiObject(parent);
	}

	void InstantiateEmojiObject(RectTransform parent)
	{
		if(emojiClone == null){
			emojiClone = Instantiate(emojiPrefab,parent);
			emojiAnimation = emojiClone.GetComponent<Animator>();
		}
	}

	public void InitStats()
	{
		emojiHunger = emojiSO.emojiHunger;
		emojiHygene = emojiSO.emojiHygene;
		emojiHappiness = emojiSO.emojiHappiness;
		emojiHealth = emojiSO.emojiHealth;

		emojiHungerMod = emojiSO.emojiHungerMod;
		emojiHygeneMod = emojiSO.emojiHygeneMod;
		emojiHappinessMod = emojiSO.emojiHappinessMod;
		emojiHealthMod = emojiSO.emojiHealthMod;

		emojiPlayPositiveCount = 0;
		emojiPlayNegativeCount = 0;
		emojiCleanPositiveCount = 0;
		emojiCleanNegativeCount = 0;
		emojiPlayPositiveCount = 0;
		emojiPlayNegativeCount = 0;
		emojiNursePositiveCount = 0;
		emojiNurseNegativeCount = 0;
		emojiTapCount = 0;

		PlayerPrefs.Save();
	}

	public void InitEmojiCollections()
	{
		PlayerPrefs.SetInt(Key_Emoji_Collection+"0",2);
		for(int i = 1; i<collectionSO.Length; i++) {
			string tempKey = Key_Emoji_Collection+i.ToString();
			PlayerPrefs.SetInt(tempKey,0);
		}
	}

	/// <summary>
	/// ONLY USED in EmojiStatsController.cs If you want to modify stats, use ModStats instead. -DsD
	/// </summary>
	public void TickStats(int ticks = 1)
	{
		for(int i = 0; i < ticks;i++){
			if(emojiHungerMod <= 0 || 
				emojiHygeneMod <= 0 || 
				emojiHappinessMod <= 0)
				TickHealth();
			
			emojiHungerMod--; 	  
			emojiHygeneMod--; 		 
			emojiHappinessMod--;
		}

		AdjustStats();
		if(OnEmojiModStats != null) OnEmojiModStats();
	}

	/// <summary>
	/// ONLY USED in EmojiStatsController.cs If you want to modify stats, use ModStats instead. -DsD
	/// </summary>
	public void TickHealth()
	{
		emojiHealthMod--;

		if(emojiHealthMod <= 0){
			PlayerData.Instance.gameStatus = GameStatus.EMOJI_DIE;
			emojiClone.GetComponent<Animator>().SetTrigger("Glitch");
			emojiClone.GetComponent<Button>().onClick.RemoveListener(emojiClone.GetComponent<EmojiObject>().EmojiOnClick);
			if(OnEmojiDies != null) OnEmojiDies();
		}
	}

	public void ModStats(EmojiStats stat, int value)
	{
		switch(stat){
		case EmojiStats.HUNGER: 
			emojiHungerMod += value;
			break;
		case EmojiStats.HYGENE: 
			emojiHygeneMod += value;
			break;
		case EmojiStats.HAPPINESS: 
			
			emojiHappinessMod += value;
			break;
		case EmojiStats.HEALTH: 
			emojiHealthMod += value;
			break;
		}

		AdjustStats();
		if(OnEmojiModStats != null) OnEmojiModStats();
	}

	public void AdjustStats()
	{
		if(emojiHungerMod > emojiHunger) emojiHungerMod = emojiHunger;
		if(emojiHygeneMod > emojiHygene) emojiHygeneMod = emojiHygene;
		if(emojiHappinessMod > emojiHappiness) emojiHappinessMod = emojiHappiness;
		if(emojiHealthMod > emojiHealth) emojiHealthMod = emojiHealth;

		if(emojiHungerMod < 0) emojiHungerMod = 0;
		if(emojiHygeneMod < 0) emojiHygeneMod = 0;
		if(emojiHappinessMod < 0) emojiHappinessMod = 0;
		if(emojiHealthMod < 0) emojiHealthMod = 0;
	}

	public int GetCollection(int index)
	{
		return PlayerPrefs.GetInt(Key_Emoji_Collection+index.ToString());
	}

	/// <summary>
	/// <para>Values:</para>
	/// <para>0 = locked</para>
	/// <para>1 = unlocked</para>
	/// <para>2 = viewed in UI Collection</para>
	/// </summary>
	public void SetCollection(int index, int value)
	{
		PlayerPrefs.SetInt(Key_Emoji_Collection+index.ToString(),value);
	}

	public void ResetData()
	{
		PlayerPrefs.DeleteKey(Key_Emoji_Name);

		PlayerPrefs.DeleteKey(Key_Emoji_Hunger);
		PlayerPrefs.DeleteKey(Key_Emoji_Hygene);
		PlayerPrefs.DeleteKey(Key_Emoji_Happiness);
		PlayerPrefs.DeleteKey(Key_Emoji_Health);
		PlayerPrefs.DeleteKey(Key_Emoji_HungerMod);
		PlayerPrefs.DeleteKey(Key_Emoji_HygeneMod);
		PlayerPrefs.DeleteKey(Key_Emoji_HappinessMod);
		PlayerPrefs.DeleteKey(Key_Emoji_HealthMod);

		PlayerPrefs.DeleteKey(Key_Emoji_Feed_Positive_Count);
		PlayerPrefs.DeleteKey(Key_Emoji_Feed_Negative_Count);
		PlayerPrefs.DeleteKey(Key_Emoji_Clean_Positive_Count);
		PlayerPrefs.DeleteKey(Key_Emoji_Clean_Negative_Count);
		PlayerPrefs.DeleteKey(Key_Emoji_Play_Positive_Count);
		PlayerPrefs.DeleteKey(Key_Emoji_Play_Negative_Count);
		PlayerPrefs.DeleteKey(Key_Emoji_Nurse_Positive_Count);
		PlayerPrefs.DeleteKey(Key_Emoji_Nurse_Negative_Count);
		PlayerPrefs.DeleteKey(Key_Emoji_Tap_Count);

		PlayerPrefs.Save();
	}
}