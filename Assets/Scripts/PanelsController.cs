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

	public Text variativeQuestionText;

	private void Awake()
	{
		quizController = FindObjectOfType<QuizController>();
	}

	public void GoMainMode (int desiredCategoryId)
	{
		DisableMainMode();
		DisableBrowseMode ();
		DisableQuizMode ();

		EnableMainMode(desiredCategoryId);
	}

	public void EnableMainMode (int desiredCategoryId)
	{
		objectPickerPanelImage.enabled = true;
		categoryPickerPanelImage.enabled = true;

		objectPickerButtonsController.FilterObjectPickerItemListTo(desiredCategoryId);
		categoryPickerButtonsController.FilterCategoryPickerItemList();

		objectPickerButtonsController.AddButtonsFromCurrentItemList (); 
		categoryPickerButtonsController.AddButtonsFromCurrentItemList ();

		objectPickerButtonsController.TuneButtonsForMain ();
		categoryPickerButtonsController.TuneButtonsForMain ();
	}

	// сбрасываем флаг, который отмечает, что для объекта  ранее  был выбран спрайт.
	// это приведет к рандомизации при следующем возвращении кнопки из пула
	public void RandomizeObjectPickerSprites() // TODO переместить
	{
        for (int i = 0; i < objectPickerButtonsController.currentItemList.Count; i++)
        {
			objectPickerButtonsController.currentItemList [i].spriteWasSelected = false;
		}
	}

	public void DisableMainMode ()
	{
		objectPickerPanelImage.enabled =false;
		categoryPickerPanelImage.enabled =false;

		objectPickerButtonsController.RemoveAllButtons();
		categoryPickerButtonsController.RemoveAllButtons();

		objectPickerButtonsController.currentItemList.Clear ();
		categoryPickerButtonsController.currentItemList.Clear ();
	}

	public	void GoBrowseMode () 
	{
		EnableBrowseMode ();
		DisableMainMode (); 
		DisableQuizMode ();
	}

	public void EnableBrowseMode ()
	{
		browseModeButtonsController.AddButtonsFromCurrentItemList();
		browseModeButtonsController.TuneButtonsForBrowse ();
		browseModeButtonsController.GetComponent<Image>().enabled = true;
	}

    // очищаем и перенаполняем список объектов для просмотра в полноэкранном режиме
    //в этом списке всегда будет один пункт
    public void ChangeBrowseModeItemListTo (Item desiredItem)
	{
		browseModeButtonsController.currentItemList.Clear ();
		browseModeButtonsController.currentItemList.Add (desiredItem);
	}

	public void DisableBrowseMode ()
	{
		browseModeButtonsController.TuneButtonsForMain();
		browseModeButtonsController.RemoveAllButtons();
		browseModeButtonsController.currentItemList.Clear ();
		browseModeButtonsController.GetComponent<Image>().enabled = false;
	}

	public void GoQuizMode ()
	{
		EnableQuizMode (); 
		DisableMainMode (); 
		DisableBrowseMode ();
	}

	public void EnableQuizMode ()
	{
		quizButtonsPanelImage.enabled = true;
		questionPanelImage.enabled = true;
		variativeQuestionText.enabled = true;

		// загадать новую загадку, т.е. выбать 4 кандидатов и одного из них назначить правильным
		quizController.PrepareQuiz();
		// теперь, когда у нас есть кандидаты, можно напилить из них кнопок
		quizButtonsController.AddButtonsFromCurrentItemList ();
		quizButtonsController.TuneButtonsForQuiz ();
	}

	public void RefreshQuizModeItemList ()
	{
		//передаем сформированный лист из 4 вариантов в quiz
		for (int i = 0; i < 4; i++) 
		{
			quizButtonsController.currentItemList.Add (quizController.fourVariantsItemsList [i]);
		}
	}

	//указываем победителя в variativeQuestionText
	public void RefreshVariativeQuestionText ()
	{
		variativeQuestionText.text = quizController.fourVariantsItemsList [quizController.winnerId].itemName;
	}


	public void DisableQuizMode ()
	{
		quizButtonsPanelImage.enabled = false;
		questionPanelImage.enabled = false;
		variativeQuestionText.enabled = false;

		quizButtonsController.RemoveAllButtons ();
		quizButtonsController.currentItemList.Clear();
	}
}
