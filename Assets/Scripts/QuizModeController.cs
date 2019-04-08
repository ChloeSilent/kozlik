using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizModeController : MonoBehaviour
{
	public QuizController quizController;
	public ButtonsController quizModeButtonsController;


	public void EnterQuizMode(Item item)
	{
		// загадать новую загадку, т.е. выбать 4 кандидатов и одного из них назначить правильным
		quizController.PrepareQuiz();

		quizModeButtonsController.AddButtonsFromCurrentItemList();
		quizModeButtonsController.TuneButtonsForQuiz();
	}

	public void LeaveQuizMode(Item item)
	{
		quizModeButtonsController.currentItemList.Clear();
		quizModeButtonsController.RemoveAllButtons();
	}
}
