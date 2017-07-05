using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class AlienEyeController : MonoBehaviour {
	Image eyeImage;
	void Awake(){ 
		eyeImage = GetComponent<Image>();
	}
	void OnEnable(){
		StartCoroutine(Blink());
	}
	IEnumerator Blink(){
		while(true){
			float rnd = Random.Range(0.5f,5f);
			yield return new WaitForSeconds(rnd);
			eyeImage.enabled = true;
			yield return new WaitForSeconds(0.075f);
			eyeImage.enabled = false;
		}
	}
}