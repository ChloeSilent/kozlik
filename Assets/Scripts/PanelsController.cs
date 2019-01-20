using UnityEngine;
using UnityEngine.UI;

public class PanelsController : MonoBehaviour
{
	public ButtonsController browseModeButtonsController;
	public ButtonsController objectPickerButtonsController;
	public ButtonsController quizButtonsController;
	public ButtonsController categoryPickerButtonsController;
	private QuizController quizController;

	public Image objectPickerPanelImage;
	public Image categoryPickerPanelImage;
	public Image quizButtonsPanelImage;
	public Image questionPanelImage;
	public Image browseModePanelImage;

	public Text variativeQuestionText;

	void Awake()
	{
		quizController = FindObjectOfType<QuizController>();
	}

	public void EnableMainPanels ()
	{
		objectPickerPanelImage.enabled = true;
		categoryPickerPanelImage.enabled = true;
	}

	public void DisableMainPanels()
	{
		objectPickerPanelImage.enabled = false;
		categoryPickerPanelImage.enabled = false;
	}

	public void EnableBrowsePanels ()
	{
		browseModePanelImage.enabled = true;
	}

	public void DisableBrowsePanels()
	{
		browseModePanelImage.enabled = false;
	}

	public void EnableQuizPanels()
	{
		quizButtonsPanelImage.enabled = true;
		questionPanelImage.enabled = true;
		variativeQuestionText.enabled = true;
	}

	public void DisableQuizPanels()
	{
		quizButtonsPanelImage.enabled = false;
		questionPanelImage.enabled = false;
		variativeQuestionText.enabled = false;
	}

	// очищаем и перенаполняем список объектов для просмотра в полноэкранном режиме
	//в этом списке всегда будет один пункт
	public void ChangeBrowseModeItemListTo (Item desiredItem) // TODO move
	{
		browseModeButtonsController.currentItemList.Clear (); // дубликат?
		browseModeButtonsController.currentItemList.Add (desiredItem);
	}

	//указываем победителя в variativeQuestionText
	public void RefreshVariativeQuestionText ()
	{
		variativeQuestionText.text = quizController.fourVariantsItemsList [quizController.winnerId].itemName;
	}

	// сбрасываем флаг, который отмечает, что для объекта  ранее  был выбран спрайт.
	// это приведет к рандомизации при следующем возвращении кнопки из пула
	public void RandomizeObjectPickerSprites() // TODO переместить
	{
		for (int i = 0; i < objectPickerButtonsController.currentItemList.Count; i++)
		{
			objectPickerButtonsController.currentItemList[i].spriteWasSelected = false;
		}
	}
}
