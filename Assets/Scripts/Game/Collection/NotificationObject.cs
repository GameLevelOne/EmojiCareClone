using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NotificationObject : MonoBehaviour {
	public delegate void NotificationEvent(GameObject g);
	public event NotificationEvent OnNotificationFired;
	public event NotificationEvent OnNotificationEnded;

	public Image imageNewEmotion;
	public Text textNewEmotion;

	Animator thisAnim;
	void Awake()
	{
		thisAnim = GetComponent<Animator>();
	}
	
	public void Show()
	{
		if(OnNotificationFired != null) OnNotificationFired(gameObject);
		SoundManager.Instance.PlaySFX(eSFX.WARNING);
		thisAnim.SetTrigger("Show");
		StartCoroutine("CoroutineAutoHide");
	}

	IEnumerator CoroutineAutoHide()
	{
		yield return new WaitForSeconds(4f);
		transform.GetChild(0).GetComponent<Button>().interactable = false;
		thisAnim.SetTrigger("Hide");
	}

	public void ObjectOnClick()
	{
		SoundManager.Instance.PlaySFX(eSFX.BUTTONX);
		transform.GetChild(0).GetComponent<Button>().interactable = false;
		StopAllCoroutines();
		thisAnim.SetTrigger("Hide");
	}

	public void KillObject()
	{
		if(OnNotificationEnded != null) OnNotificationEnded(gameObject);
		Destroy(gameObject);
	}
}