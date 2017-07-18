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

	Button slotButton;

	public int Key{
		get{return key;}
	}

	void Awake()
	{
		slotButton = GetComponent<RectTransform>().GetChild(1).GetComponent<Button>();
	}

	public void InitSlot(AlienNeedCategory category)
	{
		slotButton.interactable = true;
		contentImage.gameObject.GetComponent<Animator>().SetInteger("State",0);
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
		slotButton.interactable = false;
		SoundManager.Instance.PlaySFX(eSFX.GATHER_SLOT_BUTTON);
		ShowContent();
		if(OnRevealSlot != null) OnRevealSlot(key);
	}

	public void ShowContent(Color clr = default(Color))
	{
		Animator contentAnimator = contentImage.gameObject.GetComponent<Animator>();
		AnimatorStateInfo info = contentAnimator.GetCurrentAnimatorStateInfo(0);

		if(!isEmpty && info.IsName("Small (0)")){
			if(clr == default(Color)) clr = Color.white;
			contentImage.color = clr;
			contentAnimator.SetInteger("State",1);
		}
	}
}