using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour {
	private static CoinSpawner instance = null;
	public static CoinSpawner Instance{
		get{return instance;}
	}

	public MainHUDController hudController;
	public GameObject coinObject;
	public List<GameObject> coinObjects = new List<GameObject>();

	void Awake()
	{
		if (instance != null && instance != this) {
			Destroy(this.gameObject);  // destroy any other singleton object of this class
			return;
		} else instance = this;
	}

	public void GenerateCoinObject()
	{
		GameObject coinInstance = Instantiate(coinObject);
		RectTransform tempTransform = coinInstance.GetComponent<RectTransform>();
		tempTransform.SetParent(GetComponent<RectTransform>());
		tempTransform.anchoredPosition = Vector2.zero;
		tempTransform.localScale = Vector2.one;
		tempTransform.localRotation = Quaternion.identity;
		coinInstance.GetComponent<CoinObj>().OnCoinDestroyed += OnCoinDestroyed;
		coinObjects.Add(coinInstance);
		if(coinObjects.Count > 10) coinObjects[0].GetComponent<CoinObj>().OnPointerClick();
	}

	public void AbsorbAllCoins()
	{
		foreach(GameObject coinObject in coinObjects){
			if(coinObject != null) coinObject.GetComponent<CoinObj>().OnPointerClick();
		}
	}

	void OnCoinDestroyed(GameObject obj)
	{
		obj.GetComponent<CoinObj>().OnCoinDestroyed -= OnCoinDestroyed;
		coinObjects.Remove(obj);
		hudController.ModCoin(10);
	}	

	
}