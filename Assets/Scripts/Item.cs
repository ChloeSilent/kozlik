using System.Collections;
using UnityEngine;
using UnityEngine.UI; 
using System.Collections.Generic; // нужно для  [System.Serializable]

[System.Serializable] // выводит в инспектор
public class Item : MonoBehaviour
{
	public string itemName;
	public char initialLetter;
	public List<Sprite> pictureList;
	public int Category;
	public int savedNumberOfSelectedPicture;
	public bool spriteWasSelected;
	public bool isACategory;
}  
