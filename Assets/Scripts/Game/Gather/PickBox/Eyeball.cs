using System.Collections;
using UnityEngine;

public class Eyeball : MonoBehaviour {
	Animator thisAnim;
	void Awake() { thisAnim = GetComponent<Animator>(); } 
	void OnEnable() { StartCoroutine(RandomPeek()); }
	public void OnDisable()
	{ 
		print("HA");
		StopCoroutine(RandomPeek()); 
		thisAnim.ResetTrigger("Up");
		thisAnim.ResetTrigger("Down");
		thisAnim.ResetTrigger("Left");
		thisAnim.ResetTrigger("Right");
	}

	IEnumerator RandomPeek()
	{
		while(true){
			yield return new WaitForSeconds(Random.Range(2.5f,10f));
			switch(Random.Range(0,6)){
			case 0: thisAnim.SetTrigger("Up"); break;
			case 1: thisAnim.SetTrigger("Down"); break;
			case 2: thisAnim.SetTrigger("Left"); break;
			case 3: thisAnim.SetTrigger("Right"); break;
			default: break;
			}
		}
	}
}