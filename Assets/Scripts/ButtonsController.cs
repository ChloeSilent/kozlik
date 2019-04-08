using System.Collections.Generic; 
using UnityEngine;

public class ButtonsController : MonoBehaviour 
{
	public List<Item> currentItemList; //здесь хранится отфильтрованный из allItemsList контент 
	private SimpleObjectPool buttonObjectPool;
	private OrganizeData dataContainer;

	private void Awake()
	{
		buttonObjectPool = FindObjectOfType<SimpleObjectPool>();
		dataContainer = FindObjectOfType<OrganizeData>();
	}

	//взять кнопку из пула и просетапить ее полученным аргументом itemToSetupWith
	public void TakeOneButtonFromPoolAndSetupWith(Item itemToSetupWith)
	{
        GameObject returningGameObject = buttonObjectPool.GetObject (this.transform);
		ItemButton buttonUnderConstruction = returningGameObject.GetComponent<ItemButton> ();
		buttonUnderConstruction.Setup (itemToSetupWith);

		// фиксим якоря и оффсеты при возвращении из пула
		RectTransform buttonRectTransform = buttonUnderConstruction.GetComponent<RectTransform>();

		buttonRectTransform.anchorMax = new Vector2(1, 1);
		buttonRectTransform.anchorMin = new Vector2(0, 0);

		buttonRectTransform.offsetMin = new Vector2(buttonRectTransform.offsetMin.x, 0);
		buttonRectTransform.offsetMax = new Vector2(buttonRectTransform.offsetMax.x, 0);
		buttonRectTransform.offsetMin = new Vector2(buttonRectTransform.offsetMin.y, 0);
		buttonRectTransform.offsetMax = new Vector2(buttonRectTransform.offsetMax.y, 0);
	}

	public void AddButtonsFromCurrentItemList()
	{
		for (int i = 0; i < currentItemList.Count; i++) 
		{
			Item currentItem = currentItemList [i];
			TakeOneButtonFromPoolAndSetupWith (currentItem);
		}
	}

	public void TuneButtonsForMain ()
	{
        foreach (Transform mainButton in transform) 
		{
			mainButton.GetComponent <ItemButton> ().TuneButtonForMain ();
		}
	}

	public void TuneButtonsForBrowse ()
	{
        foreach (Transform browseButton in transform) 
		{
			browseButton.GetComponent <ItemButton> ().TuneButtonForBrowse ();
        }
	}

	public void TuneButtonsForQuiz ()
	{
		foreach (Transform quizButton in transform) 
		{
			quizButton.GetComponent <ItemButton> ().TuneButtonForQuiz ();
		}
	}

    // из общего списка объектов отфильтруем тех, кто принадлежит нужной категории
    public void FilterObjectPickerItemListTo (Item desiredItem)
	{
		foreach (Item sortedItem in dataContainer.allItemsList)
		{
			if (sortedItem.Category == desiredItem.Category && sortedItem.isACategory ==false)
			{
				this.currentItemList.Add (sortedItem);
			}
		}
	}

	// из общего списка объектов отфильтруем тех, кто не объект, а категория объектов
	public void FilterCategoryPickerItemList()
	{
		foreach (Item sortedItem in dataContainer.allItemsList)
		{
			if (sortedItem.isACategory == true)
			{
				this.currentItemList.Add(sortedItem);
			}
		}
	}

	//убрать в пул одну кнопку 
	public void ReturnOneButtonToPool ()
	{	
		GameObject toBeRemoved = transform.GetChild (0).gameObject;
		buttonObjectPool.ReturnObject (toBeRemoved);
	}

	//убирать в пул все кнопки  пока не кончатся
	public void RemoveAllButtons() 
	{
		while (this.transform.childCount > 0)
        {
            ReturnOneButtonToPool ();
		}
	}
}
