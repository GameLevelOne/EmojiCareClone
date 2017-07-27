using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelEmojiEmotion : MonoBehaviour {
	public PanelEmojiEmotionDetail panelEmotionDetail;

	public void ButtonEmotionOnClick(int index)
	{
		//turn off all highlight
		//turn on selected highlight
		//reveal details
		panelEmotionDetail.ShowDetail(index);
	}

}
