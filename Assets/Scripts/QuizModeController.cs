using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizModeController : MonoBehaviour
{
	public ButtonsController quizModeButtonsController;

	public List<Item> tempList;
	public ButtonsController objectPickerButtonsController;
	public ButtonsController quizButtonsController;
	public SoundController soundController;
	public ChoiseModeController choiseModeController;
	public Text variativeQuestionText;

	public int winnerId;


	public void EnterQuizMode(List<Item> itemList)
	{
		// загадать новую загадку, т.е. выбать 4 кандидатов и одного из них назначить правильным
		PrepareQuiz(itemList);

		quizModeButtonsController.AddButtonsFromCurrentItemList();
		quizModeButtonsController.TuneButtonsForQuiz();
	}

	public void LeaveQuizMode(Item item)
	{
		quizModeButtonsController.currentItemList.Clear();
		quizModeButtonsController.RemoveAllButtons();
		tempList.Clear();
	}

	public void PrepareQuiz(List<Item> itemList)
	{
		RecieveItemList(itemList);
		SelectFourRandomVariants();
		SendChosenVariantsToButtonsController();
		SetSomeVariantAsWinner();
		RefreshVariativeQuestionText();
		soundController.AskQiuzAudioQuestion(winnerId);
	}

	// TODO There should be a beeter way to copy list. Think about it.
	public void RecieveItemList(List<Item> itemList)
	{
		foreach (Item item in itemList)
		{
			tempList.Add(item);
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

	// Iterate through tempList, find item with "Quiz" name and remove it, beacause it is not siutable variant for a game
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
		if(tempList.Count < 5)
		{
			Debug.Log("Not enough items to make a quiz");
		}
		// if its enough, lets remove random items one by one until there is only 4 left
		else
		{
			while (tempList.Count > 4)
			{
				int randomIndex = Random.Range(0, (tempList.Count - 1));
				tempList.Remove(tempList[randomIndex]);
			}
		}
	}

	public void SetSomeVariantAsWinner()
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
			soundController.TellRight();
			return true;
		}
		else
		{
			// не угадал
			soundController.TellWrong();
			return false;
		}
	}

	public AudioClip ReturnWinnersAudioClip()
	{
		return tempList[winnerId].GetComponent<AudioSource>().clip;
	}

	//указываем победителя в variativeQuestionText
	public void RefreshVariativeQuestionText()
	{
		variativeQuestionText.text = tempList[winnerId].itemName;
	}
}
