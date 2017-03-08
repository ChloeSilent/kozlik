using System.Collections;
using UnityEngine;
using UnityEngine.UI; 
using System.Collections.Generic; // нужно для  [System.Serializable]

[System.Serializable] // выводит в инспектор
public class Item 
{
	public string itemName;
	public Sprite  icon;
	public int Category;
}

public class PanelsController : MonoBehaviour 
{
	public GameObject mainMenuPanel;
	public GameObject objectPickerPanel;
	public GameObject categoryPickerPanel;
	public GameObject browseModePanel;
	public GameObject quizModePanel;
	public Item item; // а нужно ли это ?
	public List<Item> itemList;
	private List<Item> tempItemsList;
	public List<Item> fourVariantsItemsList;

	void Start () 
	{
		//app start
		MakeMainVisible ();
	}

	public	void GoBrowseMode (Item itemToBrowse) 
	{
		MakeBrowseVisible (itemToBrowse);
		MakeMainInvisible (); 
	}

	public	void GoMainMode (Item item) 
	{
		MakeMainVisible ();
		MakeBrowseInvisible ();
	}

	// в аргументе указано кто призвал квиз
	public void GoQuizMode ()
	{
		MakeMainInvisible (); 
		RefreshQuizModeItemListTo (); 
		MakeQuizVisible (); 

	}

	// в аргументе item с целевой категорией
	public void RefreshQuizModeItemListTo ()
	{
		//храним ссылку на objectPickerPanel
		ButtonsController objectPickerPanel = GameObject.Find ("ObjectPickerPanel").GetComponent<ButtonsController>();

		//ссылка на панель, которую будем тюнить
		ButtonsController quizModePanel = GameObject.Find ("QuizModePanel").GetComponent<ButtonsController>();

		// выберем из objectPickerPanel.itemList 4 случайных варианта для викторины.
		// берем в цикле 4 раза случайный item из objectPickerPanel.itemList , и после проверки помещаем в лист вариантов fourVariantsItemsList
		for (int k = 0; k <4 ; k++) // k индекс листа fourVariantsItemsList [k]. нам нужно 4 варианта, поэтому цикл на 4 итерации, с нуля до трёх
		{
			// Наш лист из 12 элементов  нумеруется с 0 до 11. 11ый вариант это кнопка квиза, она не является валидным участником викторины
			// ,а значит нам нужны номера с 0 до 10.
			// Random.Range Note that max is exclusive, so using Random.Range( 0, 10 ) will return values between 0 and 9. 
			// значит range будет (0, 11)
			int i = Random.Range(0, 11); // i индекс листа objectPickerPanel.itemList [i] . рандомизированный.

			// кроме того, нужно обеспечить неповторяемость айтемов в fourVariantsItemsList
			// проверим, вдруг в fourVariantsItemsList уже есть такой item
			// обойдём в цикле с индексом "u" fourVariantsItemsList и сравним каждый его элемент 
			// с текущим выбранным претендентом objectPickerPanel.itemList [i] на помещение в список.
			// если окажется, что fourVariantsItemsList [u] == objectPickerPanel.itemList [i] то отмеитм его флажком
			bool isUnique = true;
			for (int u = 0; u < 4; u++) 
			{
				if (fourVariantsItemsList [u] == objectPickerPanel.itemList [i]) 
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
				fourVariantsItemsList [k]  = objectPickerPanel.itemList [i];
			}
			else
			{
				//плохой претендент, неуникальный, копировать не будем
				// текущиий  раунд выборов  претендента не удался, проведем его еще раз
				k--;
			}
		}

		//наконец, передаем сформированный лист из 4 вариантов в quiz
		for (int i = 0; i < 4; i++) 
		{
			quizModePanel.itemList [i] = fourVariantsItemsList [i];
		}
		// здесь на первый взгляд хорошо бы обнулить fourVariantsItemsList [k]
		// потому, что он сохранится до и при следующем запуске квиза, и при выборе кандидатов на второй квиз претенденты будут
		// отбрасываться, если были в первом квизе, но если подумать, такое поведение нас устраивает,выглядит более разноообрано оставлем так
	}

	public void MakeQuizVisible ()
	{
		quizModePanel.GetComponent<Image> ().enabled = true;
		quizModePanel.GetComponent<ButtonsController> ().AddButtonsToQuiz (); 
	}

	public void RefreshBrowseModeItemListTo (Item newItem)
	{
		ButtonsController browseModePanel = GameObject.Find ("BrowseModePanel").GetComponent<ButtonsController>();
		browseModePanel.itemList.RemoveAt(0); // убрать ?
		browseModePanel.itemList.Insert (0, newItem);
	}

	//на входе принимаем id желаемой категории, перенастраиваем  itemList у ObjectPicker
	public void RefreshObjectPickerItemListTo (int desiredCategoryId)
	{
		//ссылка на панель, которую будем тюнить
		ButtonsController objectPickerPanel = GameObject.Find ("ObjectPickerPanel").GetComponent<ButtonsController>();

		//копируем лист во временный
		List<Item> tempItemsList  = new List<Item>(itemList);

		//проходим циклом по двум листам, выбираем из общего объекты нужной категории, копируем в objectPickerPanel
		 for (int j = 0; j < 11;)
		{
			for (int i = 0; i < tempItemsList.Count;)
			{
				if (tempItemsList [i].Category == desiredCategoryId) 
				{
					objectPickerPanel.itemList [j] = tempItemsList [i];
					i++;
					j++;
				} 
				else 
				{
					i++;
				}
			}
		}
	}

	public void MakeBrowseVisible (Item newItem)
	{
		ButtonsController browseModePanel = GameObject.Find ("BrowseModePanel").GetComponent<ButtonsController>();
		browseModePanel.GetComponent<Image>().enabled =true;
		RefreshBrowseModeItemListTo (newItem);
		browseModePanel.GetComponent<ButtonsController> ().AddButtonToBrowse();
	}

	public void MakeBrowseInvisible ()
	{
		browseModePanel.GetComponent<Image>().enabled =false;
		browseModePanel.GetComponent<ButtonsController> ().RemoveAllButtons();
	}

	public void MakeMainVisible ()
	{
		mainMenuPanel.GetComponent<Image> ().enabled = true;
		objectPickerPanel.GetComponent<Image> ().enabled = true;
		categoryPickerPanel.GetComponent<Image> ().enabled = true;
		objectPickerPanel.GetComponent<ButtonsController> ().AddButtonsToObjectPicker (); 
		categoryPickerPanel.GetComponent<ButtonsController> ().AddButtonsToCategoryPicker ();
	}

	public void MakeMainInvisible ()
	{
		objectPickerPanel.GetComponent<ButtonsController> ().RemoveAllButtons();
		categoryPickerPanel.GetComponent<ButtonsController> ().RemoveAllButtons();
		objectPickerPanel.GetComponent<Image>().enabled =false;
		categoryPickerPanel.GetComponent<Image>().enabled =false;
		mainMenuPanel.GetComponent<Image>().enabled =false;
	}

}
