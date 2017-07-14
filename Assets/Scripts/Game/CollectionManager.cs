using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour {
	public GameObject boxEmotionPrefab;
	public GameObject emotionParentObj;

	int width = 6;
	int height = 6;

	void Start () {
		GenerateEmotionCollection(21);
	}

	void GenerateEmotionCollection (int emotCount)
	{
		int row = emotCount / height;
		int lastCol = emotCount % width;

		Debug.Log (row);
		Debug.Log (lastCol);

		if (lastCol != 0) {
			row++;
		}

		for (int i = 0; i < row; i++) {
			for (int j = 0; j < width; j++) {
				if (lastCol != 0 && i == (row - 1)) {
					width = lastCol;
				} 
				GameObject obj = Instantiate (boxEmotionPrefab) as GameObject;
				obj.transform.position = new Vector3 ((-245f + (j * 100f)), (170f - (i * 100f)), 0);
				obj.transform.SetParent (emotionParentObj.transform, false);
			}
		}
	}
}
