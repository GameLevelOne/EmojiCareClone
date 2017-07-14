using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObj : MonoBehaviour {
	public Vector2 coinDestination = new Vector2(-313f,406f);
	public delegate void CoinDestroyed(GameObject obj);
	public event CoinDestroyed OnCoinDestroyed;

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
		StartCoroutine(PlayerGetCoin());
	}

	IEnumerator PlayerGetCoin()
	{
		rigidBody.gravityScale = 0f;
		GetComponent<CircleCollider2D>().enabled = false;

		float t = 0f;
		float x,y;
		while (t <= 1f){
			t += Time.deltaTime;
			x = Mathf.Lerp(coinTransform.anchoredPosition.x,coinDestination.x,t);
			y = Mathf.Lerp(coinTransform.anchoredPosition.y,coinDestination.y,t);
			GetComponent<RectTransform>().anchoredPosition = new Vector2(x,y);
			yield return new WaitForSeconds(Time.deltaTime);
		}

		if(OnCoinDestroyed != null) OnCoinDestroyed(gameObject);
		Destroy(gameObject);
	}
}