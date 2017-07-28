using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelEmojiEmotion : MonoBehaviour {
	public PanelEmojiEmotionDetail panelEmotionDetail;

	public Button[] emotionButtons;
	public Image[] emotionContents;
	public GameObject[] emotionHighlights;
	public GameObject[] emotionNotifications;

	Emoji playerEmoji;

	public void Init()
	{
		for(int i = 0;i<emotionHighlights.Length;i++){ 
			emotionHighlights[i].SetActive(false);
			emotionButtons[i].interactable = false;
			emotionContents[i].color = Color.black;
		}

		playerEmoji = PlayerData.Instance.PlayerEmoji;

		for(int i = 0;i<playerEmoji.collectionSO.Length;i++){
			
			emotionContents[i].sprite = playerEmoji.collectionSO[i].emotionIcon;
			switch(playerEmoji.GetCollection(i)){
			case 0: emotionContents[i].color = Color.black;
					emotionNotifications[i].SetActive(false);
					emotionButtons[i].interactable = false;
					break;

			case 1: emotionContents[i].color = Color.white;
					emotionNotifications[i].SetActive(true);
					emotionButtons[i].interactable = true;
					break;

			case 2: emotionContents[i].color = Color.white;
					emotionNotifications[i].SetActive(false);
					emotionButtons[i].interactable = true;
					break;
			}
		}
	}

	public void ButtonEmotionOnClick(int index)
	{
		SoundManager.Instance.PlaySFX(eSFX.BUTTON);
		if(emotionNotifications[index].activeSelf) emotionNotifications[index].SetActive(false);
		HighlightEmotion(index);
		panelEmotionDetail.ShowDetail(index);
		if(playerEmoji.GetCollection(index) == 1) playerEmoji.SetCollection(index,2);
	}

	void HighlightEmotion(int index)
	{
		for(int i = 0;i<emotionHighlights.Length;i++) emotionHighlights[i].SetActive(false);
		emotionHighlights[index].SetActive(true);
	}
}