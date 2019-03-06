using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	private OrganizeData dataContainer;
	private PanelsController panelsController;
	public ButtonsController objectPickerButtonsController;
	public ButtonsController categoryPickerButtonsController;
	public ButtonsController quizModeButtonsController;
	public ButtonsController browseModeButtonsController;
	private QuizController quizController;
	private GoogleMobileAdsScript adController;

	public Image settingsButtonImage;

	public Image backToGameButtonImage;
	public Text backToGameButtonText;

	public Image disableAdsButtonImage;
	public Text disableAdsButtonText;

	private void Awake()
	{
		dataContainer = FindObjectOfType<OrganizeData>();
		panelsController = FindObjectOfType<PanelsController>();
		quizController = FindObjectOfType<QuizController>();
		adController = FindObjectOfType<GoogleMobileAdsScript>();
	}

	void Start()
    {
		dataContainer.StartDataProcessing();
		// категория по умолчанию на старте имеет индекс ноль
		GoMainMode(dataContainer.allItemsList[0]);
	}

	public void GoMainMode(Item item)
	{
		DisableMainMode(); //TODO  это костыль! без него при смене категории все кнопки дублируются. подумать как переделать
		DisableBrowseMode();
		DisableQuizMode();
		DisableSettingsMode();
		EnableMainMode(item);
	}

	public void GoBrowseMode(Item item)
	{
		EnableBrowseMode(item);
		DisableMainMode();
		DisableQuizMode();
		DisableSettingsMode();
	}

	public void GoQuizMode()
	{
		EnableQuizMode();
		DisableMainMode();
		DisableBrowseMode();
		DisableSettingsMode();
	}

	public void GoSettingsMode()
	{
		EnableSettingsMode();
		DisableMainMode();
		DisableBrowseMode();
		DisableQuizMode();
	}

	public void EnableMainMode(Item item)
	{
		panelsController.EnableMainPanels();

		objectPickerButtonsController.FilterObjectPickerItemListTo(item);
		categoryPickerButtonsController.FilterCategoryPickerItemList();

		panelsController.RandomizeObjectPickerSprites();
		panelsController.RandomizeCategoryPickerSprites();

		objectPickerButtonsController.AddButtonsFromCurrentItemList();
		categoryPickerButtonsController.AddButtonsFromCurrentItemList();

		objectPickerButtonsController.TuneButtonsForMain();
		categoryPickerButtonsController.TuneButtonsForMain();

		adController.ShowAdsIfEnabled();

		settingsButtonImage.enabled = true;
	}

	public void DisableMainMode()
	{
		panelsController.DisableMainPanels();

		objectPickerButtonsController.currentItemList.Clear();
		categoryPickerButtonsController.currentItemList.Clear();

		objectPickerButtonsController.RemoveAllButtons();
		categoryPickerButtonsController.RemoveAllButtons();

		adController.TemporarilyHideAds();

		settingsButtonImage.enabled = false;
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

	public void EnableSettingsMode()
	{
		panelsController.EnableSettingsPanels();

		backToGameButtonImage.enabled = true;
		backToGameButtonText.enabled = true;

		disableAdsButtonImage.enabled = true;
		disableAdsButtonText.enabled = true;
	}

	public void DisableSettingsMode()
	{
		panelsController.DisableSettingsPanels();

		backToGameButtonImage.enabled = false;
		backToGameButtonText.enabled = false;

		disableAdsButtonImage.enabled = false;
		disableAdsButtonText.enabled = false;
	}
}
