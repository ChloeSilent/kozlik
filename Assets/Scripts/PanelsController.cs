using System.Collections;
using UnityEngine;
using UnityEngine.UI; 
using System.Collections.Generic; // нужно для  [System.Serializable] //kill ?
using UnityEngine.Networking.Types;


public class PanelsController : MonoBehaviour
{
	public ButtonsController browseModeButtonsController;
	public ButtonsController objectPickerButtonsController;
	public ButtonsController quizButtonsController;
	public ButtonsController categoryPickerButtonsController;
	public QuizController quizController;

	public Image objectPickerPanelImage;
	public Image categoryPickerPanelImage;
	public Image quizButtonsPanelImage;
	public Image questionPanelImage;

	public Text constantQuestionText;
	public Text variativeQuestionText;


	// TODO переименовать в инициализацию
	public void DelayedStart ()
	{
		//фильтруем объекты к категории по умолчанию
		objectPickerButtonsController.FilterObjectPickerItemListTo (0);
		// отфильтровываем  объекты  для размещения в нижней панели
		categoryPickerButtonsController.FilterCategoryPickerItemList();

		GoMainMode ();
	}

	public void GoMainMode ()
	{
		EnableMainMode ();
		DisableBrowseMode ();
		DisableQuizMode ();
	}

	public void EnableMainMode ()
	{
		objectPickerPanelImage.enabled = true;
		categoryPickerPanelImage.enabled = true;

		objectPickerButtonsController.AddButtonsFromCurrentItemList (); 
		categoryPickerButtonsController.AddButtonsFromCurrentItemList ();

		objectPickerButtonsController.TuneButtonsForMain ();
		categoryPickerButtonsController.TuneButtonsForMain ();
	}

    // очищаем список предметов для отображения в верхней панели и по-новой наполняем его предметами полученной категории
	public void ChangeObjectPickerItemListCategoryTo (int categoryId)
	{
		objectPickerButtonsController.currentItemList.Clear ();
		objectPickerButtonsController.FilterObjectPickerItemListTo (categoryId);
	}

    //чем этот метод отличается от предыдущего ?
    // почти ничем. отрефакторить,
    // TODO перенести фильтр категории (FilterCategoryPickerItemList) в метод выше, дальше везде использовать его
    public void RefreshMainModeItemLists (int categoryId)
	{
		objectPickerButtonsController.FilterObjectPickerItemListTo (categoryId);
		categoryPickerButtonsController.FilterCategoryPickerItemList();
	}

    // убрать из верхней панели все кнопки, создать новые взамен
	public void RepopulateObjectPicker()
	{
		objectPickerButtonsController.RemoveAllButtons ();
		objectPickerButtonsController.AddButtonsFromCurrentItemList ();
	}

    // сбрасываем флаг, который отмечает, что для объекта  ранее  был выбран спрайт.
    // это приведет к рандомизации при следующем возвращении кнопки из пула
    public void RandomizeObjectPickerSprites()
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
	}

    // очищаем и перенаполняем список объектов для просмотра в полноэкранном режиме
    // хоть в этом списке всегда будет один пункт,  но это не меняет его типа
    public void ChangeBrowseModeItemListTo (Item desiredItem)
	{
		browseModeButtonsController.currentItemList.Clear ();
		browseModeButtonsController.currentItemList.Add (desiredItem);
	}

	public void DisableBrowseMode ()
	{
		browseModeButtonsController.RemoveAllButtons();
		browseModeButtonsController.currentItemList.Clear ();
	}

	public void GoQuizMode ()
	{
		quizController.PrepareQuiz ();

		EnableQuizMode (); 

		DisableMainMode (); 
		DisableBrowseMode ();
	}

	public void EnableQuizMode ()
	{
		quizButtonsPanelImage.enabled = true;
		questionPanelImage.enabled = true;
		constantQuestionText.enabled = true;
		variativeQuestionText.enabled = true;

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
		constantQuestionText.enabled = false;
		variativeQuestionText.enabled = false;

		quizButtonsController.RemoveAllButtons ();
		quizButtonsController.currentItemList.Clear();
	}
}
