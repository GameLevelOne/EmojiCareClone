using UnityEngine.UI;
using UnityEngine;

public class AlienDisplay : MonoBehaviour {
	public Image charImageDisp;
	public Text charDotAmountDisp;

	int charDotAmount;

	public void AssignCharacter(Alien alien)
	{
		charImageDisp.sprite = alien.alienSO.spriteFullBody;
		charDotAmount = alien.alienSO.totalDot;
		charDotAmountDisp.text = charDotAmount.ToString();
	}

	public void UpdateDotDisp()
	{
		charDotAmount--;
		charDotAmountDisp.text = charDotAmount.ToString();
	}

	public int CharDotAmount{ get{ return charDotAmount; } }
}