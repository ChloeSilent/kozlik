using System.Collections;
using UnityEngine;
using System.Collections.Generic; // нужно для  [System.Serializable]

[System.Serializable] // выводит в инспектор
public class OrganizeData : MonoBehaviour 
{
	public List<Item> allItemsList;
	public List<Item> categoriesItemsList;
	public List<Item> itemsOfCategory;
	public PanelsController panelsController;

	void Start()
	{
		StartCoroutine("PrepareData");
		//start only when data is ready
		panelsController.DelayedStart (); 
	}

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
		for (int c = 0; c < categoriesItemsList.Count; c++)
		{
			categoriesItemsList[c].GetComponentsInChildren <Item> (itemsOfCategory); //all items of category
			itemsOfCategory.RemoveAt (0); //kill category item itself
			itemsOfCategory.RemoveAt (itemsOfCategory.Count-1); //kill quiz

			int r = Random.Range (0, itemsOfCategory.Count); //index of random item of category 
			categoriesItemsList [c].pictureList [0] = itemsOfCategory [r].pictureList [Random.Range (0, itemsOfCategory [r].pictureList.Count)];
		}
	}

	public void GenerateInitialLetter()
	{
		foreach (Item item in allItemsList)
		{
			item.initialLetter = item.itemName [0]; //TODO  exception if empty
		}

	}

}
