using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameStatus{
	PROLOGUE,
	SEND_OFF,
	EMOJI_DIE
}

public class SceneStorkManager : MonoBehaviour {
	public DialogueController dialogueController;
	public PanelNameEmoji panelNameEmoji;
	public Fader fader;

	void Start()
	{
		dialogueController.OnDialogueFinish += OnDialogueFinished;
		panelNameEmoji.OnNameEmojiDone += OnNameEmojiDone;
		SoundManager.Instance.PlayBGM(eBGM.AMBIENCE_STORK);
		dialogueController.ExecuteDialogue();
	}

	void OnDialogueFinished()
	{
		dialogueController.OnDialogueFinish -= OnDialogueFinished;
		panelNameEmoji.Show();
	}

	void OnNameEmojiDone()
	{
		panelNameEmoji.OnNameEmojiDone -= OnNameEmojiDone;
		fader.OnFadeOutFinished += GoToSceneMain;
		fader.FadeOut();
	}

	void GoToSceneMain()
	{
		fader.OnFadeInFinished -= GoToSceneMain;
		SceneManager.LoadScene("SceneMain");
	}


}