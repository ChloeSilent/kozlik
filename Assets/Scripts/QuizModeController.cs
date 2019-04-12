using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuizModeController : MonoBehaviour
{
	public ButtonsController quizModeButtonsController;

	public List<Item> tempList;
	public ButtonsController objectPickerButtonsController;
	public ButtonsController quizButtonsController;
	
	public Text variativeQuestionText;

	public int winnerId;

	private OrganizeData dataContainer;

	private void Awake()
	{
		dataContainer = FindObjectOfType<OrganizeData>();
	}

	public void EnterQuizMode(int category)
	{
		PrepareQuiz(category);
		quizModeButtonsController.AddButtonsFromCurrentItemList();
		quizModeButtonsController.TuneButtonsForQuiz();
	}

	public void LeaveQuizMode()
	{
		quizModeButtonsController.currentItemList.Clear();
		quizModeButtonsController.RemoveAllButtons();
		tempList.Clear();
	}

	// Make a new riddle, i.e. choose 4 random variants and randomly choose one as a winner
	public void PrepareQuiz(int category)
	{
		RecieveItemList(category);
		SelectFourRandomVariants();
		SendChosenVariantsToButtonsController();
		SetSomeVariantAsWinner();
		RefreshVariativeQuestionText();
	}

	public void RecieveItemList(int category)
	{
		foreach (Item item in dataContainer.allItemsList)
		{
			if (item.isACategory != true && item.Category == category)
			{
				tempList.Add(item);
			}
		}
	}

	public void SendChosenVariantsToButtonsController() 
	{
		for (int i = 0; i < 4; i++)
		{
			quizButtonsController.currentItemList.Add(tempList[i]);
		}
	}

	// Choose four random items from the list which we recieved from choiseMode
	public void SelectFourRandomVariants()
	{
		// First lets delete quiz button, as it cannot be a variant is quiz game
		DeleteQuizItemButtonFromList();

		// Now we have 11 or less items in list.
		// All of them are valid variants for a quiz.
		// Lets decrease number to four, as we need only four items for a quiz game, eleven is too much.
		DecreaseItemListCountToFour();
	}

	// Iterate through tempList, find item with "Quiz" name and remove it, because it is not siutable variant for a game
	private void DeleteQuizItemButtonFromList()
	{
		for (int i=0; i<tempList.Count; i++)
		{
			if (tempList[i].name == "Quiz")
			{
				tempList.Remove(tempList[i]);
			}
		}
	}

	private void DecreaseItemListCountToFour()
	{
		//Check if its enough item to make a quiz
		if(tempList.Count < 4)
		{
			Debug.Log("Not enough items to make a quiz");
		}
		// if its enough, lets remove random items one by one until there is only 4 left
		else
		{
			while (tempList.Count > 4)
			{
				int randomIndex = Random.Range(0, tempList.Count);
				tempList.Remove(tempList[randomIndex]);
			}
		}
	}

	private void SetSomeVariantAsWinner()
	{
		// Randomly choose one item of the list as correct answer
		winnerId = Random.Range(0, tempList.Count);
	}

	//проверяем ответ викторины на правильность
	public bool CheckIfWinner(Item clickedItem)
	{
		if (clickedItem.itemName == tempList[winnerId].itemName)
		{
			//угадал
			return true;
		}
		else
		{
			// не угадал
			return false;
		}
	}

	public AudioClip ReturnWinnersAudioClip()
	{
		return tempList[winnerId].GetComponent<AudioSource>().clip;
	}

	//указываем победителя в variativeQuestionText
	private void RefreshVariativeQuestionText()
	{
		variativeQuestionText.text = tempList[winnerId].itemName;
	}
}
