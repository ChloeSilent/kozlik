using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

	public Mode currentMode;
	public enum Mode
	{
		Choise,
		SettingsMenu,
		Browse,
		Quiz,
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
		EnableChoiseMode(dataContainer.allItemsList[0]);
	}

	public void SwitchFromChoiseToSettingsMenu()
	{
		EnableSettingsMenuMode();
		DisableChoiseMode();
	}

	public void SwitchFromSettingsMenuToChoise()
	{
		EnableChoiseMode(dataContainer.allItemsList[0]);
		DisableSettingsMenuMode();
	}

	public void SwitchFromChoiseToBrowse(Item item)
	{
		EnableBrowseMode(item);
		DisableChoiseMode();
	}

	public void SwitchFromBrowseToChoise(Item item)
	{
		EnableChoiseMode(item);
		DisableBrowseMode(item);
	}

	public void SwitchFromChoiseToQuiz(Item item)
	{
		EnableQuizMode(item);
		DisableChoiseMode();
	}

	public void SwitchFromQuizToBrowse(Item item)
	{
		DisableQuizMode(item);
		EnableBrowseMode(item);
	}

	public void SwitchCategory(Item item)
	{
		DisableChoiseMode();
		EnableChoiseMode(item);
	}

	private void EnableChoiseMode(Item item)
	{
		currentMode = Mode.Choise;
		choiseCanvas.enabled = true;
		choiseModeController.EnterChoiseMode(item);
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

	private void DisableBrowseMode(Item item)
	{
		browseCanvas.enabled = false;
		browseModeController.LeaveBrowseMode(item);
	}

	private void EnableQuizMode(Item item)
	{
		currentMode = Mode.Quiz;
		quizCanvas.enabled = true;
		quizModeController.EnterQuizMode(item);
	}

	private void DisableQuizMode(Item item)
	{
		quizCanvas.enabled = true;
		quizModeController.LeaveQuizMode(item);
	}
}
