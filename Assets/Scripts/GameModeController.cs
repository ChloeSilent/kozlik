using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// This class controls changing game modes. 
// This is a FiniteStateMachine, so simultaneously game could be only in one of the predefined in enum modes. 
// When game changes mode corresponding canvases and all their children become disabled or enabled.

public class GameModeController : MonoBehaviour
{
	public OrganizeData dataContainer;

	public Canvas choiseCanvas;
	public Canvas settingsCanvas;
	public Canvas browseCanvas;
	public Canvas quizCanvas;
	public Canvas infoCanvas;
	public Canvas helpCanvas;
	public Canvas languageCanvas;
	public Canvas aboutAppCanvas;

	public ChoiseModeController choiseModeController;
	public BrowseModeController browseModeController;
	public QuizModeController quizModeController;
	public SoundController soundController;

	public UnityAction<ItemButton> SwitchCategoryListener;
	public UnityAction<ItemButton> SwitchFromChoiseToBrowseListener;
	public UnityAction<ItemButton> SwitchFromChoiseToQuizListener;
	public UnityAction<ItemButton> SwitchFromBrowseToChoiseListener;
	public UnityAction<ItemButton> TuneButtonToDisplayLetterListener;
		

	public Mode currentMode;
	public enum Mode
	{
		Choise,
		Settings,
		Browse,
		Quiz,
		Info,
		Help,
		Language,
		AboutApp,
	}

	private void OnEnable()
	{
		SwitchCategoryListener = new UnityAction<ItemButton> (SwitchCategory);
		SwitchFromChoiseToBrowseListener = new UnityAction<ItemButton> (SwitchFromChoiseToBrowse);
		SwitchCategoryListener = new UnityAction<ItemButton> (SwitchFromChoiseToQuiz);
		SwitchFromBrowseToChoiseListener = new UnityAction<ItemButton> (SwitchFromBrowseToChoise);
		TuneButtonToDisplayLetterListener = new UnityAction<ItemButton> (TuneButtonToDisplayLetter);

		EventManager.StartListening ("CategoryPickerButtonClicked", SwitchCategory);
		EventManager.StartListening ("ObjectPickerButtonClicked", SwitchFromChoiseToBrowse);
		EventManager.StartListening ("QuizButtonClicked", SwitchFromChoiseToQuiz);
		EventManager.StartListening ("FirstClickInBrowseMode", TuneButtonToDisplayLetterListener);
		EventManager.StartListening ("SecondClickInBrowseMode", SwitchFromBrowseToChoise);
		EventManager.StartListening ("QuizVariantClicked", CheckVariant);
	}

	private void OnDisable()
	{
		EventManager.StopListening ("CategoryPickerButtonClicked", SwitchCategory);
		EventManager.StopListening ("ObjectPickerButtonClicked", SwitchFromChoiseToBrowse);
		EventManager.StopListening ("QuizButtonClicked", SwitchFromChoiseToQuiz);
		EventManager.StopListening ("FirstClickInBrowseMode", TuneButtonToDisplayLetterListener);
		EventManager.StopListening ("SecondClickInBrowseMode", SwitchFromBrowseToChoise);
		EventManager.StopListening ("QuizVariantClicked", CheckVariant);
	}

	void Start()
	{
		// Game begins here!
		// First of all lets load data from storage
		dataContainer.PrepareData();
	
		// After loading data, game starts in Choise mode
		StartInChoiseModeWithFirstCategory();
	}

	private void StartInChoiseModeWithFirstCategory()
	{
		// Index 0 is used because we want start with catagory of first item in dataContainer
		EnableChoiseMode(dataContainer.allItemsList[0].Category);
	}

	public void SwitchFromChoiseToSettings()
	{
		EnableSettingsMode();
		DisableChoiseMode();
	}

	public void SwitchFromSettingsToChoise()
	{
		EnableChoiseMode(dataContainer.allItemsList[0].Category);
		DisableSettingsMode();
	}

	public void SwitchFromChoiseToBrowse(ItemButton itemButton)
	{
		DisableChoiseMode();
		EnableBrowseMode(itemButton.item);
	}

	public void SwitchFromBrowseToChoise(ItemButton itemButton)
	{
		EnableChoiseMode(itemButton.item.Category);
		DisableBrowseMode();
	}

	public void SwitchFromChoiseToQuiz(ItemButton itemButton)
	{
		DisableChoiseMode();
		EnableQuizMode(itemButton.item.Category);
	}

	public void SwitchFromQuizToBrowse(Item item)
	{
		DisableQuizMode(item);
		EnableBrowseMode(item);
	}

	public void SwitchCategory(ItemButton itemButton)
	{
		DisableChoiseMode();
		EnableChoiseMode(itemButton.item.Category);
	}

	public void SwitchFromSettingsToInfo()
	{
		DisableSettingsMode();
		EnableInfoMode();
	}

	public void SwitchFromInfoToSettings()
	{
		DisableInfoMode();
		EnableSettingsMode();
	}

	public void SwitchFromSettingsToHelp()
	{
		DisableSettingsMode();
		EnableHelpMode();
	}

	public void SwitchFromHelpToSettings()
	{
		DisableHelpMode();
		EnableSettingsMode();
	}

	public void SwitchFromSettingsToLanguage()
	{
		DisableSettingsMode();
		EnableLanguageMode();
	}

	public void SwitchFromLanguageToSettings()
	{
		DisableLanguageMode();
		EnableSettingsMode();
	}

	public void SwitchFromInfoToAboutApp()
	{
		DisableInfoMode();
		EnableAboutAppMode();
	}

	public void SwitchFromAboutAppToInfo()
	{
		DisableAboutAppMode();
		EnableInfoMode();
	}

	private void EnableAboutAppMode()
	{
		currentMode = Mode.AboutApp; 
		aboutAppCanvas.enabled = true;
	}

	private void DisableAboutAppMode()
	{
		aboutAppCanvas.enabled = false;
	}

	private void EnableInfoMode()
	{
		currentMode = Mode.Info; 
		infoCanvas.enabled = true;
	}

	private void DisableInfoMode()
	{
		infoCanvas.enabled = false;
	}

	private void EnableHelpMode()
	{
		currentMode = Mode.Help; 
		helpCanvas.enabled = true;
	}

	private void DisableHelpMode()
	{
		helpCanvas.enabled = false;
	}

	private void EnableLanguageMode()
	{
		currentMode = Mode.Language; 
		languageCanvas.enabled = true;
	}

	private void DisableLanguageMode()
	{
		languageCanvas.enabled = false;
	}
	
	private void EnableChoiseMode(int category)
	{
		currentMode = Mode.Choise; //TODO move up
		choiseCanvas.enabled = true;
		choiseModeController.EnterChoiseMode(category);
	}

	private void DisableChoiseMode()
	{
		choiseCanvas.enabled = false;
		choiseModeController.LeaveChoiseMode();
	}

	private void EnableSettingsMode()
	{
		currentMode = Mode.Settings;
		settingsCanvas.enabled = true;
	}

	private void DisableSettingsMode()
	{
		settingsCanvas.enabled = false;
	}

	private void EnableBrowseMode(Item item)
	{
		currentMode = Mode.Browse;
		browseCanvas.enabled = true;
		browseModeController.EnterBrowseMode(item);
	}

	private void DisableBrowseMode()
	{
		browseCanvas.enabled = false;
		browseModeController.LeaveBrowseMode();
	}

	private void EnableQuizMode(int category)
	{
		currentMode = Mode.Quiz;
		quizCanvas.enabled = true;
		quizModeController.EnterQuizMode(category);
		soundController.AskQiuzAudioQuestion(quizModeController.winnerId);
	}

	private void DisableQuizMode(Item item)
	{
		quizCanvas.enabled = false;
		quizModeController.LeaveQuizMode();
	}

	private void CheckVariant(ItemButton itemButton)
	{
		bool guessWasSuccessfull = quizModeController.CheckIfWinner(itemButton.item);
		if (guessWasSuccessfull)
		{
			SwitchFromQuizToBrowse(itemButton.item);
			soundController.TellRight();
		}
		else
		{
			itemButton.MoveRedCrossForward();
			soundController.TellWrong();
		}
	}

	private void TuneButtonToDisplayLetter(ItemButton itemButton)
	{
		itemButton.DisplayInitialLetterFullscreeen();
	}
}
