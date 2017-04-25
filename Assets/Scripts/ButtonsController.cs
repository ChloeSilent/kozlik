using System.Collections;
using System.Collections.Generic; // нужно для  [System.Serializable]
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class ButtonsController : MonoBehaviour 
{
	public List<Item> currentItemList; //здесь хранится отфильтрованный из allItemsList контент 
	private ButtonsController currentController;
	public SimpleObjectPool buttonObjectPool;

	public ButtonsController objectPickerButtonsController;
	public ButtonsController categoryPickerButtonsController;

	public GameObject dataContainer;
	public List<Item> allItemsList; //здесь хранится весь контент загруженный из контейнера

	void OnEnable () 
	{
		//загружаем данные из контейнера в контроллер
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
	public void AddButtonsFromCurrentItemList()
	{
		for (int i = 0; i < currentItemList.Count; i++) 
		{
			Item currentItem = currentItemList [i];
			TakeOneButtonFromPoolAndSetupWith (currentItem);
		}
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
	public void UnTuneButtonsForQuiz ()
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
		
	public void FilterCategoryPickerItemList()
	{
		foreach (Item sortedItem in allItemsList) 
		{
			if (sortedItem.isACategory == true) 
			{ 
				categoryPickerButtonsController.currentItemList.Add (sortedItem);
			}
		}
	}

	public void FilterObjectPickerItemListTo (int desiredCategoryId)
	{
		foreach (Item sortedItem in allItemsList)
		{
			if (sortedItem.Category == desiredCategoryId &&  sortedItem.isACategory ==false)
			{
				objectPickerButtonsController.currentItemList.Add (sortedItem);
			}
		}
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
}
