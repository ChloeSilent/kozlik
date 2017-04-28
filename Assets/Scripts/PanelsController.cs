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

	void Start () 
	{
		//app start
		objectPickerButtonsController.FilterObjectPickerItemListTo (0);
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

	public void ChangeObjectPickerItemListCategoryTo (int categoryId)
	{
		objectPickerButtonsController.currentItemList.Clear ();
		objectPickerButtonsController.FilterObjectPickerItemListTo (categoryId);
	}

	public void RefreshMainModeItemLists (int categoryId)
	{
		objectPickerButtonsController.FilterObjectPickerItemListTo (categoryId);
		categoryPickerButtonsController.FilterCategoryPickerItemList();
	}

	public void RepopulateObjectPicker()
	{
		objectPickerButtonsController.RemoveAllButtons ();
		objectPickerButtonsController.AddButtonsFromCurrentItemList ();
	}

	public void RandomizeObjectPickerSprites()
	{
		for(int i = 0; i < 11 ; i++)
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
