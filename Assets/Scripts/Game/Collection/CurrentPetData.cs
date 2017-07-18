using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPetData : MonoBehaviour {

	private int petIdx = 0;
	private int totalEmoji = 0;

	public void SetPetIdx(int idx){
		petIdx = idx;
	}

	public void SetTotalEmoji (int count){
		totalEmoji=count;
	}

	public int GetPetIdx (){
		return petIdx;
	}	

	public int GetTotalEmoji (){
		return totalEmoji;
	}
}
