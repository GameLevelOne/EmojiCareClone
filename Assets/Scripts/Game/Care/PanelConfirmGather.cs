using UnityEngine.UI;
using UnityEngine;

public class PanelConfirmGather : MonoBehaviour {
	public MainHUDController hudController;
	public SceneMainManager sceneMainManager;
	public Text textWarning;

	const string WARNING_NOT_ENOUGH_GOLD = "NOT ENOUGH GOLD (NEED 100G)";
	const string WARNING_USE_GOLD = "GATHER COSTS 100G";

	int category;
	Animator thisAnim;

	bool isEnoughGold = false;

	void Awake()
	{
		thisAnim = GetComponent<Animator>();
	}

	public void ButtonCategoryOnClick(int category)
	{
		this.category = category;
		isEnoughGold = PlayerData.Instance.playerCoin >= 100 ? true : false;
		textWarning.text = isEnoughGold ? WARNING_USE_GOLD : WARNING_NOT_ENOUGH_GOLD;

		SoundManager.Instance.PlaySFX(eSFX.BUTTON);
		if(!isEnoughGold) SoundManager.Instance.PlaySFX(eSFX.WARNING);

		thisAnim.SetTrigger("Show");
	}

	public void ButtonOkOnClick()
	{
		if(isEnoughGold){
			hudController.ModCoin(-100);
			sceneMainManager.ChangeToGatherSubScene(category);
		}

		SoundManager.Instance.PlaySFX(eSFX.BUTTON);
		thisAnim.SetTrigger("Hide");
	}

	public void ButtonXOnClick()
	{
		SoundManager.Instance.PlaySFX(eSFX.BUTTONX);
		thisAnim.SetTrigger("Hide");
	}
}