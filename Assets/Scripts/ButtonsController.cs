using System.Collections;
using System.Collections.Generic; // нужно для  [System.Serializable]
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class ButtonsController : MonoBehaviour 
{
	public List<Item> itemList;
	private ButtonsController currentController;
	public SimpleObjectPool buttonObjectPool;

	public ButtonsController browseModePanel; //TODO fix name
	public ButtonsController objectPicker;//TODO fix name
	public ButtonsController categoryPicker;//TODO fix name
	public ButtonsController quizButtonsPanel;//TODO fix name
	public PanelsController panelsController;

	public GameObject dataContainer;
	public List<Item> allItemsList;

	void Start () 
	{
		//загружаем данные в лист
		dataContainer.GetComponentsInChildren <Item> (allItemsList);
	}

	//взять кнопку из пула и просетапить ее полученным аргументом itemToSetupWith
	public void TakeOneButtonFromPoolAndSetupWith(Item itemToSetupWith)
	{
		currentController = this;

		//для правильного скейлинга при извлечении из пула нужно утанавливать parent. для этого передаем currentController
		GameObject newButton = buttonObjectPool.GetObject (currentController.transform);
		SampleButton sampleButton = newButton.GetComponent<SampleButton> ();
		sampleButton.Setup (itemToSetupWith);

		//смена парента при возвращении из пула  ломает не только скейлинг, но и якоря и оффсеты. жестко фиксим  
		RectTransform buttonRectTransform = sampleButton.GetComponent<RectTransform>();

		buttonRectTransform.anchorMax = new Vector2(1, 1);
		buttonRectTransform.anchorMin = new Vector2(0, 0);

		buttonRectTransform.offsetMin = new Vector2(buttonRectTransform.offsetMin.x, 0);
		buttonRectTransform.offsetMax = new Vector2(buttonRectTransform.offsetMax.x, 0);
		buttonRectTransform.offsetMin = new Vector2(buttonRectTransform.offsetMin.y, 0);
		buttonRectTransform.offsetMax = new Vector2(buttonRectTransform.offsetMax.y, 0);
	}

	//сетапим все кнопки из itemList в цикле
	public void AddButtons()
	{
		for (int i = 0; i < itemList.Count; i++) 
		{
			Item currentItem = itemList [i];
			TakeOneButtonFromPoolAndSetupWith (currentItem);
		}
	}

	//наплодить 1 кнопку для BrowseModePanel . принимает 1 аргумент типа Item
	public void AddButtonToBrowse()
	{
		browseModePanel.AddButtons ();
	}

	//наплодить кнопок для ObjectPicker 
	public void AddButtonsToObjectPicker ()
	{
			objectPicker.AddButtons ();
	}

	//наплодить кнопок для CategoryPicker
	public void AddButtonsToCategoryPicker ()
	{
		categoryPicker.AddButtons ();
	}

	public void AddButtonsToQuiz ()
	{
		quizButtonsPanel.AddButtons ();
		quizButtonsPanel.TuneButtonsForQuiz ();
	}

	// модифицируем кнопки для квиза, убираем надпись и подложку
	public void TuneButtonsForQuiz ()
	{
		//перебираем кнопки в квизе
		foreach (Transform quizButton in transform) 
		{
			GameObject currentTextPanel = quizButton.FindChild ("TextPanel").gameObject;
			currentTextPanel.SetActive (false);
		}
	}

	//откатываем изменения, которые вносились в кнопки для квиз режима
	public void unTuneButtonsForQuiz ()
	{
		//перебираем кнопки в квизе
		foreach (Transform quizButton in transform) 
		{
			//включаем обратно плашку с текстом
			GameObject currentTextPanel = quizButton.FindChild ("TextPanel").gameObject;
			currentTextPanel.SetActive (true);
			//прячем красный крест на второй план
			quizButton.GetComponent <SampleButton> ().MoveRedCrossBackward ();
		}
	}

	//переключиться на другую категорию
	public void ChangeCategory (Item chosenItem)
	{
		RemoveAllButtonsFromObjectPicker();
		RemoveItemsFrom (objectPicker);
		RefreshObjectPickerItemListTo (chosenItem.Category); //подтягиваем в objectPicker нужные item
		AddButtonsToObjectPicker ();
	}

	public void RefreshObjectPickerItemListTo (int desiredCategoryId)
	{
		foreach (Item sortedItem in allItemsList)
		{
			if (sortedItem.Category == desiredCategoryId && sortedItem.transform.parent.name != "DataContainer")
			{
				objectPicker.itemList.Add (sortedItem);
			}
		}
	}

	public void RemoveItemsFrom (ButtonsController controllerToClear)
	{
		controllerToClear.itemList.Clear ();
	}

	//убрать в пул одну кнопку 
	public void ReturnOneButtonToPool ()
	{	
		GameObject toRemove = transform.GetChild (0).gameObject;
		buttonObjectPool.ReturnObject (toRemove);
	}

	//убирать в пул все кнопки  пока не кончатся
	public void RemoveAllButtons() 
	{
		currentController = this;
		while (currentController.transform.childCount > 0) 
		{
			ReturnOneButtonToPool ();
		}
	}

	// убрать в пул все кнопки из ObjectPicker
	public void RemoveAllButtonsFromObjectPicker ()
	{
		objectPicker.RemoveAllButtons ();
	}

	public void RemoveAllButtonsFromQuiz ()
	{
		//приведем кнопку в порядок перед возвращением в пул
		unTuneButtonsForQuiz ();

		quizButtonsPanel.RemoveAllButtons ();
	}


}
