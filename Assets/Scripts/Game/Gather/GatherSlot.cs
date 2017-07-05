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

	public void InitSlot()
	{
		blockImage.gameObject.SetActive(true);
		defaultSprite=blockImage.sprite;
		SetEmpty();
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
	/// </summary>
	public void SetContent(Sprite sprite, int key)
	{
		contentImage.sprite = sprite;
		contentImage.enabled = true;
		this.key = key;
		isEmpty = false;
	}

	void SetEmpty()
	{
		contentImage.sprite = null;
		contentImage.enabled = false;
		isEmpty = true;
	}

	public void ImageBlockOnClick()
	{
		Debug.Log(blockImage);
		StartCoroutine(WaitForAnim(blockImage.gameObject,defaultSprite));

		if(OnRevealSlot != null){
			OnRevealSlot(key);
		}
		if(!isEmpty){
			contentImage.gameObject.GetComponent<Animator>().SetInteger("State",1);
		}
	}

	public void ResetContentAnimation()
	{
		contentImage.gameObject.GetComponent<Animator>().SetInteger("State",0);
	}

	IEnumerator WaitForAnim(GameObject obj,Sprite spr){
		yield return new WaitForSeconds(0.3f);
		obj.SetActive(false);
		blockImage.sprite=spr;
	}
}