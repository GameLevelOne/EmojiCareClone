using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObj : MonoBehaviour {
	public delegate void CoinDestroyed(GameObject obj);
	public event CoinDestroyed OnCoinDestroyed;

	Vector2 coinDestination = new Vector2(25f,600f);
	Rigidbody2D rigidBody;
	RectTransform coinTransform;

	bool isClicked = false;
	bool bump = false;

	void Awake()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		coinTransform = GetComponent<RectTransform>();
		SoundManager.Instance.PlaySFX(eSFX.COIN_SPAWN);
	}

	void Start ()
	{
		rigidBody.AddForce(new Vector2(Random.Range(-2500f,2500f),50000f));
		StartCoroutine(CoinAutoCollect());
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "BumpSound") bump = true;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(bump) {
			bump = false;
			SoundManager.Instance.PlaySFX(eSFX.COIN_BUMP);
		}
	}

	public void OnPointerClick()
	{
		if(isClicked == true) return;

		isClicked = true;
		StopCoroutine(CoinAutoCollect());
		StartCoroutine(CoinAbsorb());
	}

	IEnumerator CoinAutoCollect()
	{
		yield return new WaitForSeconds(10f);
		isClicked = true;
		StartCoroutine(CoinAbsorb());
	}

	IEnumerator CoinAbsorb()
	{
		if(!TutorialManager.Instance.TutorialDone && TutorialManager.Instance.TutorialIndex == 1) TutorialManager.Instance.ShowTutorial();
		rigidBody.gravityScale = 0f;
		GetComponent<CircleCollider2D>().enabled = false;

		float t = 0f;
		float x,y;
		while (t <= 1f){
			t += Time.deltaTime;
			x = Mathf.Lerp(coinTransform.anchoredPosition.x,coinDestination.x,t);
			y = Mathf.Lerp(coinTransform.anchoredPosition.y,coinDestination.y,t);
			GetComponent<RectTransform>().anchoredPosition = new Vector2(x,y);
			if(Mathf.Abs(y-coinDestination.y) < 1f || Mathf.Abs(x-coinDestination.x) < 1f) break;
			yield return new WaitForSeconds(Time.deltaTime);
		}

		if(OnCoinDestroyed != null) OnCoinDestroyed(gameObject);
		SoundManager.Instance.PlaySFX(eSFX.COIN);
		Destroy(gameObject);
	}
}