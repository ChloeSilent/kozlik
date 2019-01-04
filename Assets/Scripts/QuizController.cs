using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class QuizController : MonoBehaviour 
{
	public List<Item> fourVariantsItemsList;
	public ButtonsController objectPickerButtonsController;
	public ButtonsController quizButtonsController;
	public PanelsController panelsController;
    public SoundController soundController;

	public int winnerId;

	public void PrepareQuiz()
	{
		SelectFourRandomVariants ();
		panelsController.RefreshQuizModeItemList (); 
		SetSomeVariantAsWinner ();
		panelsController.RefreshVariativeQuestionText ();
		soundController.AskQiuzAudioQuestion(winnerId);
	}

    // выбирает четырех случайных участников викторины
	public void SelectFourRandomVariants ()
	{
		// выберем в цикле 4 раза случайный item из objectPickerButtonsController.currentItemList ,
		//и после проверки помещаем в лист вариантов fourVariantsItemsList
		for (int k = 0; k <4 ; k++) // k индекс листа fourVariantsItemsList [k]. нам нужно 4 варианта, поэтому цикл на 4 итерации, с нуля до трёх
		{
			// Наш лист элементов нумеруется с 0. Последний элемпнт- это кнопка квиза, она не является валидным участником викторины
			// Random.Range: Note that max is exclusive, so using Random.Range( 0, 10 ) will return values between 0 and 9. 
			// значит range будет (0, 11)
			int i = Random.Range(0, objectPickerButtonsController.currentItemList.Count-1); // i индекс листа objectPickerButtonsController.currentItemList [i]

			// проверка претендента на уникальность
			bool isUnique = CheckIfPretendentIsUnique (i);

			// проверка претендента закончена, смотрим на флаг-отчёт, решаем что делать
			if(isUnique==true)
			{
				//хороший предентент, копируем его в fourVariantsItemsList [k]
				fourVariantsItemsList [k]  = objectPickerButtonsController.currentItemList [i];
			}
			else
			{
				//плохой претендент, неуникальный, копировать не будем
				// текущиий  раунд выборов  претендента не удался, проведем его еще раз
				k--;
			}
		}
	}

	public bool CheckIfPretendentIsUnique(int i)
	{
		// нужно обеспечить неповторяемость айтемов в fourVariantsItemsList
		// проверим, вдруг в fourVariantsItemsList уже есть такой item
		// обойдём в цикле с индексом "u" fourVariantsItemsList и сравним каждый его элемент 
		// с текущим выбранным претендентом objectPickerButtonsController.currentItemList [i] на помещение в список.
		// если окажется, что fourVariantsItemsList [u] == objectPickerButtonsController.currentItemList [i] то отметим его флажком
		bool isUnique = true;
		for (int u = 0; u < 4; u++) 
		{
			if (fourVariantsItemsList [u] == objectPickerButtonsController.currentItemList [i]) 
			{
				//такой вариант уже был в fourVariantsItemsList, устанавливаем флаг в false
				isUnique = false;
			} 
			else 
			{
				// такого варианта среди уже выбранных нет, не трогаем флаг
			}
		}
		return isUnique;
	}

	//назначаем правильный вариант 
	public void SetSomeVariantAsWinner()
	{
		winnerId = Random.Range(0, 4); 
	}

	//проверяем ответ викторины на правильность
	public bool CheckIfWinner (Item clickedItem)
	{
		if (clickedItem.itemName == fourVariantsItemsList [winnerId].itemName) 
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
		return fourVariantsItemsList[winnerId].GetComponent<AudioSource>().clip;
	}
}
