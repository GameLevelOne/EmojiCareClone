using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PrologueTextBox : MonoBehaviour {
	public DialogueText dialogueText;

	public string[] dialogues;

	Animator thisAnim;
	int clickCount = 0;
	int textCount = 0; //1 for show all, 2 for next
	string animTrigger_Show = "Show";
	string animTrigger_Hide = "Hide";

	void Awake()
	{
		thisAnim = GetComponent<Animator>();
		thisAnim.SetTrigger(animTrigger_Show);
	}

	void Start(){
		InitPrologue();
	}

	void InitPrologue()
	{
		dialogueText.Show(dialogues[textCount],clickCount);
	}

	public void DialoguePanelOnClick ()
	{
//		clickCount++;
		textCount++;
//
//		if (clickCount == 2) {
//			clickCount = 0;
//			textCount++;
//		}

		dialogueText.Show (dialogues [textCount],clickCount);

		if(textCount >=1)
			textCount=0;

	}

}