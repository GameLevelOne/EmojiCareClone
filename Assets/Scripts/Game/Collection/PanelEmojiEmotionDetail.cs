using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelEmojiEmotionDetail : MonoBehaviour {
	public Text textEmotionName;
	public Text textEmotionUnlockCondition;
	public Image imageEmotion;

	public void Init()
	{
		imageEmotion.color = new Color(1,1,1,0);
		textEmotionName.text = textEmotionUnlockCondition.text = string.Empty;
	}

	public void ShowDetail(int index)
	{
		EmojiCollectionSO emojiCollectionSO = PlayerData.Instance.PlayerEmoji.collectionSO[index];
		imageEmotion.color = Color.white;
		imageEmotion.sprite = emojiCollectionSO.emotionIcon;
		textEmotionName.text = emojiCollectionSO.emotionName;
		textEmotionUnlockCondition.text = emojiCollectionSO.emotionUnlockConditionDesc;
	}
}
