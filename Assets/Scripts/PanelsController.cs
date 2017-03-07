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
	private Item item; // а нужно ли это ?
	public List<Item> itemList;
	private List<Item> tempItemsList;

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

	public void RefreshBrowseModeItemListTo (Item newItem)
	{
		ButtonsController browseModePanel = GameObject.Find ("BrowseModePanel").GetComponent<ButtonsController>();
		browseModePanel.itemList.RemoveAt(0);
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
