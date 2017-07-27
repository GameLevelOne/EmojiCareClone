using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelEmojiEmotionDetail : MonoBehaviour {
	public Text textEmotionName;
	public Text textEmotionUnlockCondition;
	public Image imageEmotion;

	public void ShowDetail(int index)
	{
		Emoji playerEmoji = PlayerData.Instance.PlayerEmoji;
	}
}
