using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eBGM{
	MAIN = 0,
	PROLOGUE
}

public enum eSFX{
	BUTTON = 0,
	BUTTONX,
	COIN,
	GATHER_RESULT,
	GATHER_SLOT_BUTTON,
	GATHER_SLOT_NEGATIVE,
	GATHER_SLOT_POSITIVE,
	LEVELUP,
	PROLOGUE_GET_EMOJI,
	WARNING,
	COIN_SPAWN,
	COIN_BUMP
}

public class SoundManager : MonoBehaviour {
	private static SoundManager instance = null;
	public static SoundManager Instance{ get{ return instance; } }

	public AudioClip[] BGM;
	public AudioClip[] SFX;

	AudioSource thisSource;

	void Awake()
	{
		if(instance != null && instance != this){
			Destroy(this.gameObject); return;
		}else instance = this;
		DontDestroyOnLoad(this.gameObject);
		thisSource = GetComponent<AudioSource>();
		SetVolume(PlayerPrefs.GetFloat("Settings/Sound",1));
	}

	public void PlayBGM(eBGM bgm)
	{
		if(thisSource.clip == BGM[(int)bgm])
			return;
		else{
			thisSource.clip = BGM[(int)bgm];
			thisSource.Play();
		}
	}

	public void PlaySFX(eSFX sfx)
	{
		thisSource.PlayOneShot(SFX[(int)sfx]);
	}

	public void StopPlaying()
	{
		thisSource.Stop();
	}

	public void SetVolume(float volume)
	{
		thisSource.volume = volume;
	}
}