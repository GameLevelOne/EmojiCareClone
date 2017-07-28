using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionNotification : MonoBehaviour {
	public Image imageNotifIcon;
	public GameObject notifObjPrefab;
	public RectTransform notificationParent;
	public List<GameObject> notificationObjects;

	bool notifIsFiring = false;

	void Start()
	{
		EmojiUnlockConditions.Instance.OnEmotionUnlock += AddNotification;
	}

	public void AddNotification(int index)
	{
		GameObject tempNotifObj = Instantiate(notifObjPrefab,notificationParent);
		tempNotifObj.GetComponent<NotificationObject>().OnNotificationFired += OnNotificationFired;
		tempNotifObj.GetComponent<NotificationObject>().OnNotificationEnded += OnNotificationEnded;
		tempNotifObj.GetComponent<NotificationObject>().imageNewEmotion.sprite = PlayerData.Instance.PlayerEmoji.collectionSO[index].emotionIcon;
		tempNotifObj.GetComponent<NotificationObject>().textNewEmotion.text = PlayerData.Instance.PlayerEmoji.collectionSO[index].emotionName;
		notificationObjects.Add(tempNotifObj);
		FireNotification();
	}

	void OnNotificationFired(GameObject g) { notifIsFiring = true; }
	void OnNotificationEnded(GameObject g)
	{
		g.GetComponent<NotificationObject>().OnNotificationFired -= OnNotificationFired;
		g.GetComponent<NotificationObject>().OnNotificationEnded -= OnNotificationEnded;

		notifIsFiring = false;
		FireNotification();
	}

	void FireNotification()
	{
		if(notificationObjects.Count > 0 && notifIsFiring == false){
			notificationObjects[0].GetComponent<NotificationObject>().Show();
			notificationObjects.RemoveAt(0);
		}
	}
}