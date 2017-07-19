using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickBox : MonoBehaviour {
	Animator thisAnim;
	void Awake() { thisAnim = GetComponent<Animator>(); }
	void OnEnable() { 
		AnimatorStateInfo stateInfo = thisAnim.GetCurrentAnimatorStateInfo(0);
		if(stateInfo.IsName("On") == false) {
			thisAnim.ResetTrigger("Click");
			thisAnim.ResetTrigger("Blink");
			thisAnim.SetTrigger("Reset");
		}
		StartCoroutine(RandomBlink()); 
	}
	public void OnDisable() { StopCoroutine(RandomBlink()); }

	IEnumerator RandomBlink()
	{
		while(true){
			yield return new WaitForSeconds(Random.Range(2f,10f));
			if(Random.value <= 0.5f) thisAnim.SetTrigger("Blink");
		}
	}
}