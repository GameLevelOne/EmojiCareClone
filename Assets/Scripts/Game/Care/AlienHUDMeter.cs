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

	bool isLowStats = false;
	float t = 0f;
	float speed = 2f;
	bool fadingRed = false;

	public void ModHUD(float currentValue, float maxValue)
	{
		//print("currentValue = "+currentValue+" maxValue = "+maxValue);
		imageAmount.fillAmount = (float)(currentValue/maxValue);
		textAmount.text = Mathf.FloorToInt(currentValue).ToString()+"/"+ Mathf.FloorToInt(maxValue).ToString();
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