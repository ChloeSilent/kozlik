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
	public Canvas settingsMenuCanvas;
	public Canvas browseCanvas;
	public Canvas quizCanvas;

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
		SettingsMenu,
		Browse,
		Quiz,
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
		// Load data from storage
		dataContainer.PrepareData();
	
		// After loading data game starts in Choise mode
		StartInChoiseModeWithFirstCategory();
	}

	private void StartInChoiseModeWithFirstCategory()
	{
		// Index 0 is used because we want start with catagory of first item in dataContainer
		EnableChoiseMode(dataContainer.allItemsList[0].Category);
	}

	public void SwitchFromChoiseToSettingsMenu()
	{
		EnableSettingsMenuMode();
		DisableChoiseMode();
	}

	public void SwitchFromSettingsMenuToChoise()
	{
		EnableChoiseMode(dataContainer.allItemsList[0].Category);
		DisableSettingsMenuMode();
	}

	public void SwitchFromChoiseToBrowse(ItemButton itemButton)
	{
		EnableBrowseMode(itemButton.item);
		DisableChoiseMode();
	}

	public void SwitchFromBrowseToChoise(ItemButton itemButton)
	{
		EnableChoiseMode(itemButton.item.Category);
		DisableBrowseMode();
	}

	public void SwitchFromChoiseToQuiz(ItemButton itemButton)
	{
		EnableQuizMode(itemButton.item.Category);
		DisableChoiseMode();
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

	private void EnableSettingsMenuMode()
	{
		currentMode = Mode.SettingsMenu;
		settingsMenuCanvas.enabled = true;
	}

	private void DisableSettingsMenuMode()
	{
		settingsMenuCanvas.enabled = false;
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
		soundController.AskQiuzAudioQuestion(quizModeController.winnerId); //TODO move
	}

	private void DisableQuizMode(Item item)
	{
		quizCanvas.enabled = true;
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
