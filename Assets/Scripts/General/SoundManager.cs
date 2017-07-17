using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eBGM{
	MENU = 0
}

public enum eSFX{
	BUTTON = 0
}

public class SoundManager : MonoBehaviour {
	private static SoundManager instance = null;
	public static SoundManager Instance{ get{ return instance; } }

	void Awake()
	{
		if(instance != null && instance != this){
			Destroy(this.gameObject); return;
		}else instance = this;
		DontDestroyOnLoad(this.gameObject);
	}
}