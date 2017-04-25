using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic; // нужно для  [System.Serializable]

[System.Serializable] // выводит в инспектор
public class OrganizeData : MonoBehaviour 
{
	public List<Item> allItemsList;
	public List<Item> categoriesItemsList;
	public List<Item> itemsOfCategory;

	void Start()
	{
		LoadAllDataToList ();
		FindCategories ();
		GenerateCategorySprites ();
	}

	public void LoadAllDataToList()
	{
		this.GetComponentsInChildren <Item> (allItemsList); 
	}

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

	public void GenerateCategorySprites()
	{
		for (int c = 0; c < 5; c++)
		{
			categoriesItemsList[c].GetComponentsInChildren <Item> (itemsOfCategory); //all items of category
			itemsOfCategory.RemoveAt (0); //kill category item itself
			itemsOfCategory.RemoveAt (itemsOfCategory.Count-1); //kill quiz
			Debug.Log ("itemsOfCategory.Count " + itemsOfCategory.Count);
			int r = Random.Range (0, (itemsOfCategory.Count - 1)); //random item of category except quiz and categoryItem itself
			categoriesItemsList [c].pictureList [0] = itemsOfCategory [r].pictureList [Random.Range (0, itemsOfCategory [r].pictureList.Count)];
		}

	}

}
