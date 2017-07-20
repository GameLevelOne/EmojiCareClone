using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Emoji : MonoBehaviour {
	public delegate void EmojiTickStats();
	public delegate void EmojiDies();
	public event EmojiTickStats OnEmojiTickStats;
	public event EmojiDies OnEmojiDies;

	Animator emojiAnimation;

	#region data
	public string emojiName{
		get{return "0";}
		set{ }
	}
	public int emojiHunger{
		get{return 0;}
		set{ }
	}
	public int emojiHygene{
		get{return 0;}
		set{ }
	}
	public int emojiHappiness{
		get{return 0;}
		set{ }
	}
	public int emojiHealth{
		get{return 0;}
		set{ }
	}
	public int emojiHungerMod{
		get{return 0;}
		set{ }
	}
	public int emojiHygeneMod{
		get{return 0;}
		set{ }
	}
	public int emojiHappinessMod{
		get{return 0;}
		set{ }
	}
	public int emojiHealthMod{
		get{return 0;}
		set{ }
	}
	#endregion

	public void InitEmoji()
	{
		
	}

	void InitStats()
	{

	}

	public void ModStats(int ticks = -1)
	{
		
	}

	void AdjustStats()
	{
		
	}

	public void ResetData()
	{

	}
}