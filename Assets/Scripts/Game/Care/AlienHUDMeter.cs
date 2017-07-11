using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public enum AlienNeedCategory{
	HUNGER,
	HYGENE,
	HAPPINESS,
	HEALTH
}

public class AlienHUDMeter : MonoBehaviour {
	public Image imageAmount;
	public Text textAmount;
	public TextMod textMod;
	public bool isHealthHUD;

	bool isLowStats = false;
	float t = 0f;
	float speed = 2f;
	float lowStatsTreshold = 0.25f;
	bool fadingRed = false;

	bool hasInit = false;
	public float lastValue = 0;

	public void InitHUD(float currentValue, float maxValue){
		if(hasInit == false){
			lastValue = currentValue;
			imageAmount.fillAmount = (float)(currentValue/maxValue);
			textAmount.text = Mathf.FloorToInt(currentValue).ToString()+"/"+ Mathf.FloorToInt(maxValue).ToString();
			hasInit = true;
		}
	}

	public void ModHUD(float currentValue, float maxValue)
	{
//		print("last value = "+lastValue+", currentValue = "+currentValue);
		StartCoroutine(CoroutineModHUD(currentValue,maxValue));
	}
		
	IEnumerator CoroutineModHUD(float currentValue, float maxValue)
	{
		int difference = Mathf.CeilToInt(currentValue-lastValue);

		textMod.Animate(difference);

		float t = 0f;
		while(t <= 1f){
			t += (Time.deltaTime * 2f);
			float tempMod = Mathf.Lerp(lastValue,currentValue,t);
			imageAmount.fillAmount = tempMod/maxValue;
			textAmount.text = Mathf.FloorToInt(tempMod).ToString()+"/"+ Mathf.FloorToInt(maxValue).ToString();
			yield return new WaitForSeconds(Time.deltaTime);
		}
		lastValue = currentValue;
	}

	void Update()
	{
		if(isHealthHUD && imageAmount.fillAmount > 0.5f) GetComponent<Button>().interactable = false;
		else if(isHealthHUD && imageAmount.fillAmount <= 0.5f) GetComponent<Button>().interactable = true;

		if(imageAmount.fillAmount <= lowStatsTreshold && isLowStats == false){ 
			isLowStats = true;
			fadingRed = true;
			t = 0f;
		}
		else if(imageAmount.fillAmount > lowStatsTreshold && isLowStats == true){ 
			isLowStats = false;
			fadingRed = false;
			imageAmount.color = Color.white;
			GetComponent<Image>().color = Color.white;
		}

		if(isLowStats){
			//glow red and white
			if(fadingRed){
				t += Time.deltaTime * speed;
				imageAmount.color = Color.Lerp(Color.white,Color.red,t);
				GetComponent<Image>().color = Color.Lerp(Color.white,Color.red,t);
				if(t >= 1f){
					t = 1f;
					fadingRed = false;
				}
			}else{
				t -= Time.deltaTime * speed;
				imageAmount.color = Color.Lerp(Color.white,Color.red,t);
				GetComponent<Image>().color = Color.Lerp(Color.white,Color.red,t);
				if(t <= 0f){
					t = 0f;
					fadingRed = true;
				}
			}
		}
	}
}