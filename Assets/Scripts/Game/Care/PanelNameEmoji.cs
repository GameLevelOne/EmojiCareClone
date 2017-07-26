using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelNameEmoji : MonoBehaviour {
	public delegate void NameEmojiDone();
	public event NameEmojiDone OnNameEmojiDone;

	public InputField fieldEmojiName;
	Animator thisAnim;

	void Awake()
	{
		thisAnim = GetComponent<Animator>();
	}

	public void Show()
	{
		thisAnim.SetTrigger("Show");
	}

	public void ButtonOkOnClick(){
		if(PlayerData.Instance.playerDonePrologue == 0){ 
			PlayerData.Instance.SetPlayerEmoji(0);
			PlayerData.Instance.playerDonePrologue = 1;
		}
		PlayerData.Instance.PlayerEmoji.emojiName = fieldEmojiName.text;

		thisAnim.SetTrigger("Hide");
		if(OnNameEmojiDone != null) OnNameEmojiDone();
	}

}