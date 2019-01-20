using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class OrganizeData : MonoBehaviour 
{
	public List<Item> allItemsList;
	public List<Item> categoriesItemsList;
	public List<Item> itemsOfCategory;

	public void StartDataProcessing()
	{
		StartCoroutine("PrepareData");
	}

	//корутина готовит данные, когда она закончит работу - можно стартовать
	IEnumerator PrepareData()
	{
		LoadAllDataToList ();
		FindCategories ();
		GenerateCategorySprites ();
		GenerateInitialLetter ();
		//корутина отработала, стартуем
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

	// выберем  случайный спрайт для всей категории среди спрайтов предметов, принадлежащих этой категории
	public void GenerateCategorySprites()
	{
		// для каждой категории 
		for (int c = 0; c < categoriesItemsList.Count; c++)
		{
			// сначала соберем в лист itemsOfCategory все предметы, принадлежащие данной категории
			categoriesItemsList[c].GetComponentsInChildren <Item> (itemsOfCategory); 

			// в этом листе первой идёт сама категория, уберем её
			itemsOfCategory.RemoveAt (0); 

			// последним идёт предмет-квиз, уберем его тоже
			itemsOfCategory.RemoveAt (itemsOfCategory.Count-1);

			// Готово. Теперь в листе itemsOfCategory только предметы, принадлежащие данной категории. 
			// Пройдёмся циклом по листу itemsOfCategory.
			for (int ioc=0; ioc < itemsOfCategory.Count; ioc++)
			{
				// выберем один случайный спрайт из каждого предмета
				int randomSpriteNumber = Random.Range(0, itemsOfCategory[ioc].pictureList.Count);
				// и добавим его в лист спрайтов категории
				categoriesItemsList[c].pictureList.Add(itemsOfCategory[ioc].pictureList[randomSpriteNumber]);
			}
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
