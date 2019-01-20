using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
	private OrganizeData dataContainer;
	private PanelsController panelsController;
	public ButtonsController objectPickerButtonsController;
	public ButtonsController categoryPickerButtonsController;
	public ButtonsController quizModeButtonsController;
	public ButtonsController browseModeButtonsController;
	private QuizController quizController;

	private void Awake()
	{
		dataContainer = FindObjectOfType<OrganizeData>();
		panelsController = FindObjectOfType<PanelsController>();
		quizController = FindObjectOfType<QuizController>();
	}

	void Start()
    {
		dataContainer.StartDataProcessing();
		// категория по умолчанию на старте имеет индекс ноль
		GoMainMode(dataContainer.allItemsList[0]);
	}

	public void GoMainMode(Item item)
	{
		DisableMainMode();
		DisableBrowseMode();
		DisableQuizMode();
		EnableMainMode(item);
	}

	public void GoBrowseMode(Item item)
	{
		EnableBrowseMode(item);
		DisableMainMode();
		DisableQuizMode();
	}

	public void GoQuizMode()
	{
		EnableQuizMode();
		DisableMainMode();
		DisableBrowseMode();
	}

	public void EnableMainMode(Item item)
	{
		panelsController.EnableMainPanels();

		objectPickerButtonsController.FilterObjectPickerItemListTo(item);
		categoryPickerButtonsController.FilterCategoryPickerItemList();

		panelsController.RandomizeObjectPickerSprites();

		objectPickerButtonsController.AddButtonsFromCurrentItemList();
		categoryPickerButtonsController.AddButtonsFromCurrentItemList();

		objectPickerButtonsController.TuneButtonsForMain();
		categoryPickerButtonsController.TuneButtonsForMain();
	}

	public void DisableMainMode()
	{
		panelsController.DisableMainPanels();

		objectPickerButtonsController.currentItemList.Clear();
		categoryPickerButtonsController.currentItemList.Clear();

		objectPickerButtonsController.RemoveAllButtons();
		categoryPickerButtonsController.RemoveAllButtons();
	
	}

	public void EnableBrowseMode(Item item)
	{
		panelsController.EnableBrowsePanels();

		browseModeButtonsController.currentItemList.Add(item);

		browseModeButtonsController.AddButtonsFromCurrentItemList();

		browseModeButtonsController.TuneButtonsForBrowse();
	}

	public void DisableBrowseMode()
	{
		panelsController.DisableBrowsePanels();

		browseModeButtonsController.currentItemList.Clear();

		browseModeButtonsController.RemoveAllButtons();
	}

	public void EnableQuizMode()
	{
		panelsController.EnableQuizPanels();

		// загадать новую загадку, т.е. выбать 4 кандидатов и одного из них назначить правильным
		quizController.PrepareQuiz();
		
		quizModeButtonsController.AddButtonsFromCurrentItemList();
		
		quizModeButtonsController.TuneButtonsForQuiz();
	}

	public void DisableQuizMode()
	{
		panelsController.DisableQuizPanels();

		quizModeButtonsController.currentItemList.Clear();

		quizModeButtonsController.RemoveAllButtons();
	}
}
