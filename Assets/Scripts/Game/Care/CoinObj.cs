using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObj : MonoBehaviour {
	RectTransform CoinUI;

	Rigidbody2D rigidBody;
	RectTransform coinTransform;

	void Awake()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		coinTransform = GetComponent<RectTransform>();
	}

	void Start ()
	{
		rigidBody.AddForce(new Vector2(Random.Range(-2500f,2500f),50000f));
	}

	public void OnPointerClick()
	{
		
	}

	IEnumerator PlayerGetCoin()
	{
		rigidBody.gravityScale = 0f;
		GetComponent<CircleCollider2D>().enabled = false;

		float t = 0f;
		float x,y;
		while (t <= 1f){
			t += Time.deltaTime;
			x = Mathf.Lerp(coinTransform.anchoredPosition.x,CoinUI.anchoredPosition.x,t);
			y = Mathf.Lerp(coinTransform.anchoredPosition.y,CoinUI.anchoredPosition.y,t);
			GetComponent<RectTransform>().anchoredPosition = new Vector2(x,y);
			yield return new WaitForSeconds(Time.deltaTime);
		}
		
		Destroy(gameObject);
	}
}
