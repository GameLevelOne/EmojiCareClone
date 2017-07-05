using UnityEngine.UI;
using UnityEngine;

public class SearchCell : MonoBehaviour {

	public Image cellContent;
	public bool full = false;
	int index;

	void Awake()
	{
		cellContent = GetComponent<Image>();

	}

	public void AssignCellContent(Sprite gridSpr, int idx){
		
		cellContent.sprite = gridSpr;
		index = idx;
		full = true;
	}

	void CheckCell()
	{
		if(!full){
			return;
		}else if(full){
			CapturePanelController parent = transform.parent.GetComponent<CapturePanelController>();
			parent.charDisp[index].UpdateDotDisp();
			int temp = parent.charDisp[index].CharDotAmount;
			if(temp == 0){
				//store captured alien data 
				parent.SetPlayerCapturedAlien(index);
			}
		}
	}

	public void ResetContent()
	{
		GetComponent<RectTransform>().GetChild(0).gameObject.SetActive(true);
		full = false;
	}

	public void ImgBlockOnClick()
	{
		CheckCell();
	}
}
