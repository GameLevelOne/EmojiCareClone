using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelEmojiDetail : MonoBehaviour {
	public Text textEmojiName;
	public Text textEmojiStatus;
	public Text textEmojiDate;

	public void UpdateEmojiDetail()
	{
		Emoji playerEmoji = PlayerData.Instance.PlayerEmoji;
		textEmojiName.text = playerEmoji.emojiName;
		textEmojiStatus.text = PlayerData.Instance.gameStatus == GameStatus.PROLOGUE ? "ALIVE" : PlayerData.Instance.gameStatus == GameStatus.SEND_OFF ? "SENT OFF" : "DEAD";
		textEmojiDate.text = "-";
	}
}
