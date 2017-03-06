using System.Collections;
using System.Collections.Generic; // нужно для  [System.Serializable]
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] // выводит в инспектор
public class Item 
{
	public string itemName;
	public Sprite  icon;

}

public class ButtonsList : MonoBehaviour {

	public List<Item> itemList;
	public Transform contentPanel;
	public SimpleObjectPool buttonObjectPool;


	//наплодить кнопок для MainMenuPanel. не принимает  аргументов 
	public void AddButtons2Main()
	{
		//обойти лист, на каждой итерации доставать объект из пула, сетапить его
		for (int i = 0; i < itemList.Count; i++) 
		{
			Item currentItem = itemList [i];
			GameObject newButton = buttonObjectPool.GetObject (contentPanel);
			SampleButton sampleButton = newButton.GetComponent<SampleButton> ();
			sampleButton.Setup (currentItem, this);
		}

	}



	//наплодить 1 кнопку для BrowseModePanel . принимает 1 аргумент типа Item
	public void AddButtons2Browse(Item itemToBrowse)
	{
			GameObject newButton = buttonObjectPool.GetObject (contentPanel);
			SampleButton sampleButton = newButton.GetComponent<SampleButton> ();
			sampleButton.Setup (itemToBrowse, this);
	}

	//удалить все кнопки из панели, геймобжекты вернуть в пул
	public void RemoveAllButtons()
	{
		//убираем кнопки в пул пока не кончатся
		while (contentPanel.childCount > 0) 
		{
			GameObject toRemove = transform.GetChild (0).gameObject;
			buttonObjectPool.ReturnObject (toRemove);
		}
	}



}
