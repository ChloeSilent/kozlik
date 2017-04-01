using System.Collections;
using UnityEngine;
using UnityEngine.UI; 
using System.Collections.Generic; // нужно для  [System.Serializable]


public class PanelsController : MonoBehaviour 
{
	public ButtonsController browseModeButtonsController;
	public ButtonsController objectPickerButtonsController;
	public ButtonsController quizButtonsController;
	public ButtonsController categoryPickerButtonsController;

	public Image objectPickerPanelImage;
	public Image categoryPickerPanelImage;
	public Image quizButtonsPanelImage;
	public Image questionPanelImage;

	public Text constantQuestionText; 
	public Text variativeQuestionText;

	public List<Item> itemList; //здесь хранится текущий контент
	public List<Item> fourVariantsItemsList;

	private int winnerId;

	void Start () 
	{
		//app start
		MakeMainVisible (0);
	}

	public	void GoMainMode (Item item) 
	{
		MakeMainInvisible();
		MakeMainVisible (item.Category);
		MakeBrowseInvisible ();
	}

	public	void GoBrowseMode (Item itemToBrowse) 
	{
		MakeBrowseVisible (itemToBrowse);
		MakeMainInvisible (); 
	}

	public void GoQuizMode ()
	{
		MakeMainInvisible (); 
		MakeBrowseInvisible (); 

		SelectFourRandomVariants ();
		RefreshQuizModeItemList (); 
		SetSomeVariantAsWinner ();
		RefreshVariativeQuestionText ();

		MakeQuizPanelsVisible (); 

		quizButtonsController.AddButtons ();
		quizButtonsController.TuneButtonsForQuiz ();
	}

	public void RefreshMainModeItemList(int categoryId)
	{
		objectPickerButtonsController.ChangeCategory (categoryId);
	}

	public void RefreshBrowseModeItemListTo (Item newItem)
	{
		browseModeButtonsController.itemList.RemoveAt(0); 
		browseModeButtonsController.itemList.Insert (0, newItem);
	}


	public void SelectFourRandomVariants ()
	{
		// выберем из objectPickerButtonsController.itemList 4 случайных варианта для викторины.
		// берем в цикле 4 раза случайный item из objectPickerButtonsController.itemList , и после проверки помещаем в лист вариантов fourVariantsItemsList
		for (int k = 0; k <4 ; k++) // k индекс листа fourVariantsItemsList [k]. нам нужно 4 варианта, поэтому цикл на 4 итерации, с нуля до трёх
		{
			// Наш лист из 12 элементов  нумеруется с 0 до 11. 11ый вариант это кнопка квиза, она не является валидным участником викторины
			// ,а значит нам нужны номера с 0 до 10.
			// Random.Range Note that max is exclusive, so using Random.Range( 0, 10 ) will return values between 0 and 9. 
			// значит range будет (0, 11)
			int i = Random.Range(0, 11); // i индекс листа objectPickerButtonsController.itemList [i] . рандомизированный.

			// кроме того, нужно обеспечить неповторяемость айтемов в fourVariantsItemsList
			// проверим, вдруг в fourVariantsItemsList уже есть такой item
			// обойдём в цикле с индексом "u" fourVariantsItemsList и сравним каждый его элемент 
			// с текущим выбранным претендентом objectPickerButtonsController.itemList [i] на помещение в список.
			// если окажется, что fourVariantsItemsList [u] == objectPickerButtonsController.itemList [i] то отметим его флажком
			bool isUnique = true;
			for (int u = 0; u < 4; u++) 
			{
				if (fourVariantsItemsList [u] == objectPickerButtonsController.itemList [i]) 
				{
					//такой вариант уже был, устанавливаем флаг в false
					isUnique = false;
				} 
				else 
				{
					// такого варианта среди выбранных нет, не трогаем флаг
				}
			}

			// проверка претендента закончена, смотрим на флаг-отчёт, решаем что делать
			if(isUnique==true)
			{
				//хороший предентент, копируем его в fourVariantsItemsList [k]
				fourVariantsItemsList [k]  = objectPickerButtonsController.itemList [i];
			}
			else
			{
				//плохой претендент, неуникальный, копировать не будем
				// текущиий  раунд выборов  претендента не удался, проведем его еще раз
				k--;
			}
		}

	}

	public void RefreshQuizModeItemList ()
	{
		//передаем сформированный лист из 4 вариантов в quiz
		for (int i = 0; i < 4; i++) 
		{
			quizButtonsController.itemList [i] = fourVariantsItemsList [i];
		}
		// здесь на первый взгляд хорошо бы обнулить fourVariantsItemsList [k]
		// потому, что он сохранится до и при следующем запуске квиза, и при выборе кандидатов на второй квиз претенденты будут
		// отбрасываться, если были в первом квизе, но если подумать, такое поведение нас устраивает,выглядит более разноообрано оставлем так
	}

	//выбираем что будем отгадывать
	public void SetSomeVariantAsWinner()
	{
		winnerId = Random.Range(0, 4);
	}

	//указываем победителя в variativeQuestionText
	public void RefreshVariativeQuestionText ()
	{
		variativeQuestionText.text = fourVariantsItemsList [winnerId].itemName;
	}

	//проверяем ответ викторины на правильность
	public void CheckIfWinner (Item selectedItem, GameObject selectedButton) //TODO много аргументов
	{
		if(selectedItem.itemName == fourVariantsItemsList [winnerId].itemName)
		{
			//угадал
			MakeQuizPanelsInvisible ();
			quizButtonsController.unTuneButtonsForQuiz ();
			quizButtonsController.RemoveAllButtons ();
			GoBrowseMode (selectedItem);
		}
		else
		{
			// не угадал
			selectedButton.GetComponent <SampleButton> ().MoveRedCrossForward ();
		}
	}

	public void MakeMainVisible (int categoryId)
	{
		objectPickerPanelImage.enabled = true;
		categoryPickerPanelImage.enabled = true;

		RefreshMainModeItemList (categoryId);//new

		objectPickerButtonsController.AddButtons (); 
		categoryPickerButtonsController.AddButtons ();
	}

	public void MakeMainInvisible ()
	{
		objectPickerPanelImage.enabled =false;
		categoryPickerPanelImage.enabled =false;

		objectPickerButtonsController.RemoveAllButtons();
		categoryPickerButtonsController.RemoveAllButtons();

	}

	public void MakeBrowseVisible (Item newItem)
	{
		RefreshBrowseModeItemListTo (newItem);
		browseModeButtonsController.AddButtons();

	}

	public void MakeBrowseInvisible ()
	{
		browseModeButtonsController.RemoveAllButtons();
	}

	public void MakeQuizPanelsVisible ()
	{
		quizButtonsPanelImage.enabled = true;
		questionPanelImage.enabled = true;
		constantQuestionText.enabled = true;
		variativeQuestionText.enabled = true;
	}

	public void MakeQuizPanelsInvisible ()
	{
		quizButtonsPanelImage.enabled = false;
		questionPanelImage.enabled = false;
		constantQuestionText.enabled = false;
		variativeQuestionText.enabled = false;
	}

}
