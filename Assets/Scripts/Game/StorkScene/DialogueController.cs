using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {
	public delegate void DialogueFinish();
	public event DialogueFinish OnDialogueFinish;

	public Image storkImage;
	public DialogueTextAnimation dialogueTextAnimation;
	public Animator textBoxAnim;
	public Animator faderStork;
	public Animator panelPlayerName;
	public Animator panelYesNo;
	public InputField fieldName;
	public Button buttonSkip;

	public Sprite[] storkSprites;
	public string[] prologueDialogue;
	public string[] sendOffDialogue;
	public string[] emojiDieDialogue;

	GameStatus status;
	int dialogueCounter = 0;

	public void ExecuteDialogue()
	{
		status = (GameStatus) PlayerData.Instance.gameStatus;

		if(status == GameStatus.PROLOGUE) dialogueTextAnimation.OnNextAction += PromptPlayerName;
		else dialogueTextAnimation.OnNextAction += ShowNextDialogue;

		StartCoroutine(InitStorkArrive());
	}

	IEnumerator InitStorkArrive()
	{
		yield return new WaitForSeconds(1f);
		SoundManager.Instance.PlaySFX(eSFX.STORK_FLYIN);
		yield return new WaitForSeconds(0.5f);
		SoundManager.Instance.PlaySFX(eSFX.STORK_FLYIN);
		yield return new WaitForSeconds(0.5f);
		SoundManager.Instance.PlaySFX(eSFX.STORK_COME);
		yield return new WaitForSeconds(1f);
		SoundManager.Instance.PlaySFX(eSFX.STORK_STOPFAIL);
		yield return new WaitForSeconds(0.7f);
		SoundManager.Instance.PlaySFX(eSFX.STORK_HITDOOR);
		yield return new WaitForSeconds(1f);
		SoundManager.Instance.PlaySFX(eSFX.STORK_KNOCK);
		yield return new WaitForSeconds(0.8f);
		faderStork.SetTrigger("Fade");

		StartCoroutine(InitDialogue());
	}

	IEnumerator InitDialogue()
	{
		textBoxAnim.SetTrigger("Show");
		yield return new WaitForSeconds(7f/12f);
		ShowNextDialogue();
	}

	void ShowNextDialogue()
	{
		switch(status){
		case GameStatus.PROLOGUE:
			ChangeStorkSprite();
			if(dialogueCounter == 21 || dialogueCounter == 25 || dialogueCounter == 29){
				print("PROMPT");
				dialogueTextAnimation.OnNextAction -= ShowNextDialogue;
				dialogueTextAnimation.OnNextAction += PromptPlayerYesNo;
			}

			string temp = prologueDialogue[dialogueCounter];
			if(dialogueCounter == 2)  temp += (PlayerData.Instance.playerName+",");
			dialogueTextAnimation.Show(temp); 
			if(!(dialogueCounter == 25 || dialogueCounter == 29)) dialogueCounter++;
			if(dialogueCounter == prologueDialogue.Length){
				dialogueTextAnimation.OnNextAction -= ShowNextDialogue;
				dialogueTextAnimation.OnNextAction += DialogueFinished;
			}
			break;
		case GameStatus.SEND_OFF: 
			dialogueTextAnimation.Show(sendOffDialogue[dialogueCounter]); 
			dialogueCounter++;
			break;
		case GameStatus.EMOJI_DIE: 
			dialogueTextAnimation.Show(emojiDieDialogue[dialogueCounter]);
			dialogueCounter++; 
			break;
		default: break;
		}

	}

	void ChangeStorkSprite()
	{
		if(dialogueCounter == 8 || dialogueCounter == 30 || dialogueCounter == 31){
			storkImage.sprite = storkSprites[1];
		}else if(dialogueCounter == 22 || dialogueCounter == 23 || dialogueCounter == 24 || dialogueCounter == 26 || dialogueCounter == 27 || dialogueCounter == 28 || dialogueCounter == 29){
			storkImage.sprite = storkSprites[2];
		}else if(dialogueCounter == 16 || dialogueCounter == 17 || dialogueCounter == 18 || dialogueCounter == 19 || dialogueCounter == 20 || dialogueCounter == 21|| dialogueCounter == 32){
			storkImage.sprite = storkSprites[3];
		}else storkImage.sprite = storkSprites[0];
	}

	#region prologue
	void PromptPlayerName(){
		buttonSkip.interactable = false;
		dialogueTextAnimation.OnNextAction -= PromptPlayerName;
		panelPlayerName.SetTrigger("Show");
	}
	void PromptPlayerYesNo(){
		buttonSkip.interactable = false;
		dialogueTextAnimation.OnNextAction -= PromptPlayerYesNo;
		panelYesNo.SetTrigger("Show");
	}
	#endregion

	public void PlayerNameButtonOkOnClick()
	{
		SoundManager.Instance.PlaySFX(eSFX.BUTTON);
		buttonSkip.interactable = true;

		PlayerData.Instance.playerName = fieldName.text;
		panelPlayerName.SetTrigger("Hide");
		ShowNextDialogue();
		dialogueTextAnimation.OnNextAction += ShowNextDialogue;
	}

	public void ButtonYesOnClick(){
		SoundManager.Instance.PlaySFX(eSFX.BUTTON);
		buttonSkip.interactable = true;

		dialogueCounter = 30;
		panelYesNo.SetTrigger("Hide");
		ShowNextDialogue();
		dialogueTextAnimation.OnNextAction += ShowNextDialogue;
	}
	public void ButtonNoOnClick(){
		SoundManager.Instance.PlaySFX(eSFX.BUTTON);
		buttonSkip.interactable = true;

		if(dialogueCounter == 21 || dialogueCounter == 25) dialogueCounter++;
		else if(dialogueCounter == 29) dialogueCounter = 22;
		panelYesNo.SetTrigger("Hide");
		ShowNextDialogue();
		dialogueTextAnimation.OnNextAction += ShowNextDialogue;
	}

	void DialogueFinished()
	{
		textBoxAnim.SetTrigger("Hide");
		if(OnDialogueFinish != null) OnDialogueFinish();
	}
}