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

	void Start () 
	{
//		RefreshDisplay ();
		AddButtons ();
	}
		

	public void RefreshDisplay()
	{
		RemoveAllButtons ();
		AddButtons ();
	}

	//наплодить кнопок
	public void AddButtons()
	{
		//обойти лист, на каждой итерации доставать объект из пула, сетапить его
		for (int i = 0; i < itemList.Count; i++) 
		{
			Item currentItem = itemList [i];
			GameObject newButton = buttonObjectPool.GetObject ( contentPanel);

			//следы войны с уезжающим скейлингом, памятник одной бессонной ночи
//			Debug.Log ("transform.parent 2 is:" +  newButton.transform.parent.name);
//			newButton.transform.SetParent (contentPanel);
//			Debug.Log ("transform.parent 2 is:" +  newButton.transform.parent.name);

			SampleButton sampleButton = newButton.GetComponent<SampleButton> ();
			sampleButton.Setup (currentItem, this);

		}
	}

	//удалить все кнопки из панели, геймобжекты вернуть в пул
	public void RemoveAllButtons()
	{
		while (contentPanel.childCount > 0) 
		{
			GameObject toRemove = transform.GetChild (0).gameObject;
			buttonObjectPool.ReturnObject (toRemove);
		}
	}

}
