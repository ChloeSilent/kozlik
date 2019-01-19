using UnityEngine;
using System.Collections.Generic;

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
