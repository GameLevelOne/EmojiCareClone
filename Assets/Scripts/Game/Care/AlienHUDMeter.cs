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

	bool isLowStats = false;
	float t = 0f;
	float speed = 2f;
	bool fadingRed = false;

	float lastValue = 0;

	public void InitHUD(float currentValue, float maxValue){
		lastValue = currentValue;
		imageAmount.fillAmount = (float)(currentValue/maxValue);
		textAmount.text = Mathf.FloorToInt(currentValue).ToString()+"/"+ Mathf.FloorToInt(maxValue).ToString();
	}

	public void ModHUD(float currentValue, float maxValue)
	{
		StartCoroutine(CoroutineModHUD(currentValue,maxValue));
	}
		
	IEnumerator CoroutineModHUD(float currentValue, float maxValue)
	{
//		print("LastValue = "+lastValue+", CurrentValue = "+currentValue);
		int difference = Mathf.CeilToInt(lastValue+currentValue);
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
		if(imageAmount.fillAmount <= 0.25f && isLowStats == false){ 
			isLowStats = true;
			fadingRed = true;
			t = 0f;
		}
		else if(imageAmount.fillAmount > 0.25f && isLowStats == true){ 
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