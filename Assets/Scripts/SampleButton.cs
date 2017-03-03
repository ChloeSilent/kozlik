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
	void Start () 
	{
		button.onClick.AddListener (HandleClick);
	}

	public void Setup (Item currentItem, ButtonsList currentButtonslist)
	{
		item = currentItem;
		nameLabel.text = item.itemName;
		iconImage.sprite = item.icon;

		buttonsList = currentButtonslist; 

	}

	public void HandleClick()
	{
		// выводим в лог какая кнопка кликнута
		Debug.Log ("button clicked:" + nameLabel.text);	
	}

}
