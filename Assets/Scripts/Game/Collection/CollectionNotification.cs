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

	public void AddNotification()
	{
		GameObject tempNotifObj = Instantiate(notifObjPrefab,notificationParent);
		tempNotifObj.GetComponent<NotificationObject>().OnNotificationFired += OnNotificationFired;
		tempNotifObj.GetComponent<NotificationObject>().OnNotificationEnded += OnNotificationEnded;
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