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

		//берем в цикле 4 раза item из objectPickerPanel.itemList , помещаем в лист вариантов 
		for (int i = 0; i < 4; i++) 
		{
			fourVariantsItemsList [i]  = objectPickerPanel.itemList [i];
		}

		//наконец передаем 4 варианта в quiz
		for (int i = 0; i < 4; i++) 
		{
			quizModePanel.itemList [i] = fourVariantsItemsList [i];
		}


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
