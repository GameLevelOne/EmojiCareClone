using UnityEngine.UI;
using UnityEngine;

public class UIGather : MonoBehaviour {
	public PanelGatherController panelGatherController;
	public Image imageCategory;
	public Sprite[] spriteCategory;

	public void InitUIGather(EmojiNeedCategory category)
	{
//		if(!TutorialManager.Instance.TutorialDone) TutorialManager.Instance.ShowTutorial();
		panelGatherController.InitGatherStats(category);
		imageCategory.sprite = spriteCategory[(int)category];
	}
}
