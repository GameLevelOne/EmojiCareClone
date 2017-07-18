using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentEmojiData : MonoBehaviour {

	private int currEmojiIdx = 0;

	public void SetEmojiIdx (int idx){
		currEmojiIdx = idx;
	}

	public int GetEmojiIdx(){
		return currEmojiIdx;
	}
}
