﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UICollection : MonoBehaviour {
	public PanelEmojiDetail panelEmojiDetail;
	public PanelEmojiEmotion panelEmojiEmotion;
	public PanelEmojiEmotionDetail panelEmojiEmotionDetail;

	Animator thisAnim;

	void Awake()
	{
		thisAnim = GetComponent<Animator>();
	}

	void Init()
	{
		panelEmojiDetail.UpdateEmojiDetail();
		panelEmojiEmotion.Init();
		panelEmojiEmotionDetail.Init();
		if(!TutorialManager.Instance.TutorialDone) TutorialManager.Instance.ShowTutorial();
	}

	public void Show()
	{
		Init();
		thisAnim.SetTrigger("Show");
		SoundManager.Instance.PlaySFX(eSFX.BUTTON);
	}

	public void ButtonCloseOnClick()
	{
		SoundManager.Instance.PlaySFX(eSFX.BUTTONX);
		thisAnim.SetTrigger("Hide");
	}
}