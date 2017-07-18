using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentEmojiData : MonoBehaviour {

	private int currEmojiIdx = 0;
	private bool isUnlocked = false;

	public void SetEmojiIdx (int idx){
		currEmojiIdx = idx;
	}

	public void SetUnlockBool (bool unlocked){
		isUnlocked = unlocked;
	}

	public int GetEmojiIdx(){
		return currEmojiIdx;
	}

	public bool GetUnlockBool(){
		return isUnlocked;
	}
}
