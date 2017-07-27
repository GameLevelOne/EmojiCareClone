using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CollectionManager : MonoBehaviour {
	public EmojiCollectionSO[] emojiDataSO;
	public GameObject boxEmojiPrefab;
	public GameObject boxPetPrefab;
	public GameObject emojiPanelPrefab;
	public GameObject emojiParentObj;
	public GameObject petParentObj;
	public GameObject petDetailPanel;

	public Image currSelectedEmojiIcon;
	public Text currSelectedEmojiName;
	public Text currSelectedEmojiUnlockCondition;
	public Text currSelectedPetDate;
	public Text currSelectedPetStatus;

	GameObject[] petObjects;
	GameObject[] emojiPanels;
	GameObject[,] emojiObjects;

	int totalPet = 4;
	int maxEmoji = 36;
	int currSelectedPetIdx = 0;
	int currSelectedEmojiIdx= 0;
	int currTotalEmoji = 0;

	void Start () {
		InitCollection();
	}

	void InitCollection ()
	{
		petObjects = new GameObject[totalPet];
		emojiObjects = new GameObject[totalPet,maxEmoji];
		emojiPanels = new GameObject[totalPet];

		for (int i = 0; i < totalPet; i++) {
			Debug.Log("new loop");
			GameObject obj = Instantiate(boxPetPrefab) as GameObject;
			obj.name = "Pet"+i;
			obj.transform.position = new Vector3(-225f + (i*150f),330f,0);
			obj.transform.SetParent(petParentObj.transform,false);
			obj.GetComponent<Button>().onClick.AddListener(OnSelectPet);
			obj.GetComponent<CurrentPetData>().SetPetIdx(i);

			GameObject panelObj = Instantiate(emojiPanelPrefab) as GameObject;
			panelObj.transform.SetParent(emojiParentObj.transform,false);
			panelObj.name = "PanelEmoji"+i;
			emojiPanels[i]=panelObj;

			int temp = Random.Range(15,23);
			temp=36;
			obj.GetComponent<CurrentPetData>().SetTotalEmoji(temp);
			petObjects[i]=obj;

			GenerateEmojiCollection(i,temp,emojiPanels[i]);
		}

		currTotalEmoji = petObjects[currSelectedPetIdx].GetComponent<CurrentPetData>().GetTotalEmoji();

		UpdatePetDisplay();
		UpdateEmojiDisplay(currTotalEmoji);
		UpdateEmojiPanelDisplay();
	}

	void OnSelectPet (){
		GameObject currObj = EventSystem.current.currentSelectedGameObject;
		currSelectedPetIdx = currObj.GetComponent<CurrentPetData>().GetPetIdx();
		currTotalEmoji = currObj.GetComponent<CurrentPetData>().GetTotalEmoji();
		int currCollection = currObj.GetComponent<CurrentPetData>().GetTotalEmoji();
		petDetailPanel.SetActive(true);
		UpdatePetDisplay();
		UpdatePetDetails();
		UpdateEmojiDisplay(currTotalEmoji);
		UpdateEmojiPanelDisplay();
	}

	void OnSelectEmoji (){
		GameObject currObj = EventSystem.current.currentSelectedGameObject;
		currSelectedEmojiIdx = currObj.GetComponent<CurrentEmojiData>().GetEmojiIdx();
		UpdateEmojiDisplay(currTotalEmoji);
		UpdateEmojiDetails();
	}

	void GenerateEmojiCollection (int currPet,int emojiCount,GameObject panelObj)
	{
		int width = 6;
		int height = 6;
		int row = (emojiCount / height);
		int lastCol = (emojiCount % width);
		int idx = 0;

		if (lastCol != 0) {
			row++;
		}

		for (int i = 0; i < row; i++) {
			if (lastCol != 0 && i == (row - 1)) {
					width = lastCol;
				} 
			for (int j = 0; j < width; j++) {
				Debug.Log(idx);
				GameObject obj = Instantiate (boxEmojiPrefab) as GameObject;
				obj.name = "Emoji" + idx;
				obj.transform.position = new Vector3 ((-220f + (j * 90f)), (92f - (i * 90f)), 0);
				obj.transform.SetParent (panelObj.transform, false);

				obj.GetComponent<Button> ().onClick.AddListener (OnSelectEmoji);
				obj.GetComponent<CurrentEmojiData> ().SetEmojiIdx (idx);
				obj.transform.GetChild(1).GetComponent<Image>().sprite = emojiDataSO[idx].emotionIcon;
				emojiObjects [currPet,idx] = obj;
				SetEmojiColor(obj,emojiDataSO[idx].emojiUnlockStatus);
				idx++;
			}
		}
	}

	void SetEmojiColor (GameObject obj, bool isUnlocked)
	{
		Color color = Color.white;
		if (!isUnlocked) {
			color = Color.gray;
		}
		for (int i = 1; i <= 3; i++) {
			obj.transform.GetChild(i).GetComponent<Image>().color = color;
		}
	}

	void UpdatePetDetails(){
		//replace contents later
		currSelectedPetDate.text = "Last sent off: "+"temp";
		currSelectedPetStatus.text = "Status: "+"alive";
	}

	void UpdatePetDisplay ()
	{
		for (int i = 0; i < petObjects.Length; i++) {
			if (i == currSelectedPetIdx) {
				petObjects [i].transform.GetChild (0).gameObject.SetActive (true);
			} else {
				petObjects [i].transform.GetChild (0).gameObject.SetActive (false);
			}	
		}
	}

	void UpdateEmojiDisplay (int totalEmoji)
	{
		for (int i = 0; i < totalEmoji; i++) {
			if (i == currSelectedEmojiIdx) {
				emojiObjects [currSelectedPetIdx,i].transform.GetChild (0).gameObject.SetActive (true);
			} else {
				emojiObjects[currSelectedPetIdx,i].transform.GetChild(0).gameObject.SetActive(false);
			}
		}
	}

	void UpdateEmojiDetails(){
		currSelectedEmojiIcon.sprite = emojiDataSO[currSelectedEmojiIdx].emotionIcon;
		currSelectedEmojiName.text = emojiDataSO[currSelectedEmojiIdx].emotionName;
		currSelectedEmojiUnlockCondition.text = emojiDataSO[currSelectedEmojiIdx].emotionUnlockConditionDesc;
	}

	void UpdateEmojiPanelDisplay ()
	{
		for (int i = 0; i < emojiPanels.Length; i++) {
			if (i == currSelectedPetIdx) {
				emojiPanels [i].SetActive (true);
			} else {
				emojiPanels [i].SetActive (false);
			}
		}
	}

	public void UnlockEmoji (int idx){
		emojiDataSO[idx].emojiUnlockStatus = true;
	}
}
