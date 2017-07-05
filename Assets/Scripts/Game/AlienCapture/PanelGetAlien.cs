using UnityEngine.UI;
using UnityEngine;

public class PanelGetAlien : MonoBehaviour {
	public delegate void SubmitNameDone();
	public event SubmitNameDone onSubmitNameDone;

	public InputField alienNameField;
	public Text warningText;
	public Image imageAlienPhoto;

	public void SetAlienPhoto(Sprite photo){
		imageAlienPhoto.sprite = photo;
	}

	public void ButtonOkOnClick()
	{
		if(alienNameField.text == string.Empty)
		{
			warningText.gameObject.SetActive(true);
		}else{
			//save name
			PlayerData.Instance.PlayerAlien.alienName = alienNameField.text;
			if(onSubmitNameDone != null) onSubmitNameDone();
			gameObject.SetActive(false);
			imageAlienPhoto.sprite = null;
			PlayerData.Instance.alienDead = false;
		}
	}
}
