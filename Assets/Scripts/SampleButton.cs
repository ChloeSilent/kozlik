using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleButton : MonoBehaviour {

	public Button button;
	public Text nameLabel;
	public Image iconImage;

	private Item item;
	private ButtonsList buttonsList;

	// Use this for initialization
	void Start () {
		
	}

	public void Setup (Item currentItem, ButtonsList currentButtonslist)
	{
		item = currentItem;
		nameLabel.text = item.itemName;
		iconImage.sprite = item.icon;

		buttonsList = currentButtonslist; 
		  

	}
	

}
