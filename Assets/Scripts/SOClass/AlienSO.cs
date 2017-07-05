using UnityEngine;

public enum Grid{ N, Y }

public enum AlienType{
	Greedy,
	Active,
	Sad
}

[System.Serializable]
public class CharacterRow {
	public Grid[] columns;
}

[CreateAssetMenu(fileName = "Alien_",menuName = "Cards/Alien",order = 2)]
public class AlienSO : ScriptableObject {
	[Header("Base Stats")]
	public int alienID;
	public string alienName = "Alien";
	public AlienType alienType;
	public int alienLevel = 1;
	public int alienGrowth = 100;
	public float alienHunger = 20;
	public float alienHygene = 20;
	public float alienHappiness = 20;
	public float alienHealth = 50;

	[Header("Mod")]
	public int coinMod = 1;
	public int alienGrowthMod = 0;

	public float alienHungerMod = 10;
	public float alienHygeneMod = 10;
	public float alienHappinessMod = 10;
	public float alienHealthMod = 50;

	[Header("Stats and Coin Settings")]
	public int alienGrowthGapPerLevel = 50;
	public int alienCoinGenerationHit = 1;
	public int alienCoinGenerationDuration = 60;
	public int alienGrowthHit = 1;
	public int alienGrowthDuration = 60;
	public float alienStatsDepletionHit = 1f;
	public float alienStatsDepletionHitSpecial = 1.5f;
	public float alienHealthDepletionHit = 1f;
	public int alienStatsDepletionDuration = 60;
	public int alienHealthDepletionDuration = 60;

	[Header("Reference")]
	public GameObject alienAnimationObject;
	public Sprite spriteFullBody;
	public Sprite spriteIcon;

	public CharacterRow[] rows;

	public int totalDot
	{
		get{
			int x = 0;
			for(int row = 0; row < rows.Length; row++)
				for(int col = 0;col < rows[0].columns.Length;col++)
					if(rows[row].columns[col] == Grid.Y) x++;
			return x;
		}
	}
}