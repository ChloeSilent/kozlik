using System.Collections;
using System.Collections.Generic; // нужно для  [System.Serializable]
using UnityEngine;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour 
{
	public List<Item> itemList;
	public Transform currentController;
	public SimpleObjectPool buttonObjectPool;

	//взять кнопку из пула и просетапить ее переданным аргументом someItem
	public void TakeOneButtonFromPoolAndSetupWith(Item someItem)
	{
		//для правильного скейлинга при извлечении из пула нужно утанавливать parent. для этого передаем currentController
		GameObject newButton = buttonObjectPool.GetObject (currentController);
		SampleButton sampleButton = newButton.GetComponent<SampleButton> ();
		sampleButton.Setup (someItem, this);
	}

	//сетапим все кнопки из itemList в цикле
	public void AddButtons()
	{
		for (int i = 0; i < itemList.Count; i++) {
			Item currentItem = itemList [i];
			TakeOneButtonFromPoolAndSetupWith (currentItem);
		}
	}

	//наплодить 1 кнопку для BrowseModePanel . принимает 1 аргумент типа Item
	public void AddButtonToBrowse()
	{
		ButtonsController browseModePanel = GameObject.Find ("BrowseModePanel").GetComponent<ButtonsController>();
		browseModePanel.AddButtons ();

	}

	//наплодить кнопок для ObjectPicker 
	public void AddButtonsToObjectPicker ()
	{
			ButtonsController objectPicker = GameObject.Find ("ObjectPickerPanel").GetComponent<ButtonsController>();
			objectPicker.AddButtons ();

	}

	//наплодить кнопок для CategoryPicker
	public void AddButtonsToCategoryPicker ()
	{
		ButtonsController categoryPicker = GameObject.Find ("CategoryPickerPanel").GetComponent<ButtonsController>();
		categoryPicker.AddButtons ();
	}

	//переключиться на другую категорию
	public void ChangeCategory (Item chosenItem)
	{
		PanelsController panelsController = GameObject.Find ("MainCanvas").GetComponent<PanelsController> ();
		RemoveAllButtonsFromObjectPicker();
		panelsController.RefreshObjectPickerItemListTo (chosenItem.Category); //подтягиваем в objectPicker нужные item
		AddButtonsToObjectPicker ();
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
		while (currentController.childCount > 0) 
		{
			ReturnOneButtonToPool ();
		}
	}

	// убрать в пул все кнопки из ObjectPicker
	public void RemoveAllButtonsFromObjectPicker ()
	{
		ButtonsController objectPicker = GameObject.Find ("ObjectPickerPanel").GetComponent<ButtonsController>();
		objectPicker.RemoveAllButtons ();
	}
}
