using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AlienAnimationState{
	IDLE,
	SAD,
}

public class AlienAnimationController : MonoBehaviour {
	Animator alienAnim;
	[HideInInspector] public AlienAnimationState alienAnimState;
	void Awake()
	{
		alienAnim = GetComponent<Animator>();
	}

	public void ChangeAnimation(AlienAnimationState state)
	{
		alienAnimState = state;
		alienAnim.SetInteger("State",(int)alienAnimState);
	}
}