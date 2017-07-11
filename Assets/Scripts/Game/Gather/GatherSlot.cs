using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class GatherSlot : MonoBehaviour {
	public delegate void RevealSlot(int key);
	public event RevealSlot OnRevealSlot;

	public Image contentImage;
	public Image blockImage;
	int key = 0; 
	bool isEmpty = true;
	Sprite defaultSprite;

	public int Key{
		get{return key;}
	}

	public void InitSlot(AlienNeedCategory category)
	{
		contentImage.gameObject.GetComponent<Animator>().SetInteger("State",0);
		blockImage.gameObject.SetActive(true);
		defaultSprite=blockImage.sprite;
		SetEmpty(category);
	}

	/// <summary>
	/// <para>set content with key value.</para>
	/// <para>-1 = negative</para> 
	/// <para> 0 = empty</para>
	/// <para> 1 = positive</para>
	/// <para>for nursing, key value is different.</para>
	/// <para>0 = empty</para>
	/// <para>1 = hunger</para>
	/// <para>2 = hygene</para>
	/// <para>3 = happiness</para>
	/// <para>4 = health</para>
	/// <para>5 = coin (for all category)</para>
	/// </summary>
	public void SetContent(Sprite sprite, int key)
	{
		contentImage.sprite = sprite;
		contentImage.enabled = true;
		this.key = key;
		isEmpty = false;

	}

	public void SetEmpty(AlienNeedCategory category)
	{
		contentImage.sprite = null;
		contentImage.enabled = false;
		isEmpty = true;
		if(category == AlienNeedCategory.HEALTH){
			key = -1;
		}else{
			key = 0;
		}
	}

	public void ImageBlockOnClick()
	{
//		Debug.Log(blockImage);
		StartCoroutine(WaitForAnim(blockImage.gameObject,defaultSprite));

		if(OnRevealSlot != null){
			OnRevealSlot(key);
		}
		if(!isEmpty){
			contentImage.gameObject.GetComponent<Animator>().SetInteger("State",1);
		}
	}

	IEnumerator WaitForAnim(GameObject obj,Sprite spr){
		yield return new WaitForSeconds(0.3f);
		obj.SetActive(false);
		blockImage.sprite=spr;
	}
}