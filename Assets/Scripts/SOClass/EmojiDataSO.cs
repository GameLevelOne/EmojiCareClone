using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "EmojiData_",menuName = "Cards/EmojiData",order = 4)]
public class EmojiDataSO : ScriptableObject {
	public int emojiHunger = 50;
	public int emojiHygene = 50;
	public int emojiHappiness = 50;
	public int emojiHealth = 100;

	[Header("Mod variable")]
	public int emojiHungerMod = 25;
	public int emojiHygeneMod = 25;
	public int emojiHappinessMod = 25;
	public int emojiHealthMod = 100;

	[Header("Reference")]
	public Sprite[] emojiSprites;
}