using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class OrganizeData : MonoBehaviour 
{
	public List<Item> allItemsList;
	public List<Item> categoriesItemsList;
	public List<Item> itemsOfCategory;
	private PanelsController panelsController;

	void Start()
	{
		StartCoroutine("PrepareData");
		//корутина отработала, стартуем
		panelsController = FindObjectOfType<PanelsController>();
		// категория по умолчанию на старте имеет индекс ноль
		panelsController.GoMainMode (0); 
	}

	//корутина готовит данные, когда она закончит работу - можно стартовать
	IEnumerator PrepareData()
	{
		LoadAllDataToList ();
		FindCategories ();
		GenerateCategorySprites ();
		GenerateInitialLetter ();
		yield return null;
	}

	public void LoadAllDataToList()
	{
		this.GetComponentsInChildren <Item> (allItemsList); 
	}

	// соберем категории в отдельный лист
	public void FindCategories()
	{
		for (int i = 0; i < allItemsList.Count; i++)
		{
			if (allItemsList[i].isACategory == true)
			{
				categoriesItemsList.Add (allItemsList[i]);
			}
		}
	}

	// выберем  случайный спрайт для всей категории среди спрайтов предметов принадлежащих этой категории
	public void GenerateCategorySprites()
	{
		for (int c = 0; c < categoriesItemsList.Count; c++)
		{
			// сначала соберем в один лист все предметы, принадлежащие данной категории
			categoriesItemsList[c].GetComponentsInChildren <Item> (itemsOfCategory); 

			// первым идёт сама категория, уберем её
			itemsOfCategory.RemoveAt (0); 

			// последним идёт предмет-квиз, уберем его тоже
			itemsOfCategory.RemoveAt (itemsOfCategory.Count-1); 

			// теперь в листе только преметы, принадлежащие данной категории, выберем из них случайный
			int r = Random.Range (0, itemsOfCategory.Count); 

			// у предмета из шага выше выберем рандомный спрайт и назначим его спрайтом для всей категории
			categoriesItemsList [c].pictureList [0] = itemsOfCategory [r].pictureList [Random.Range (0, itemsOfCategory [r].pictureList.Count)];
		}
	}

	//вычленим первую букву из названия
	public void GenerateInitialLetter()
	{
		foreach (Item item in allItemsList)
		{
			item.initialLetter = item.itemName [0]; 
		}
	}
}
