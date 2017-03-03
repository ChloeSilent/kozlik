using System.Collections;
using System.Collections.Generic; // нужно для  
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
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
		RefreshDisplay ();
	}




	public void RefreshDisplay()
	{
		AddButtons ();
	}

	private void AddButtons()
	{
		for (int i = 0; i < itemList.Count; i++) {
			Item item = itemList [i];
			GameObject newButton = buttonObjectPool.GetObject ();
			newButton.transform.SetParent (contentPanel);

			SampleButton sampleButton = newButton.GetComponent<SampleButton> ();
			sampleButton.Setup (item, this);

		}
	}

}
