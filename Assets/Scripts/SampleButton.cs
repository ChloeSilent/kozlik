using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SampleButton : MonoBehaviour {

	public Button button;
	public Text nameLabel;
	public Image iconImage;
	private Item item;

	// Use this for initialization
	void Start () 
	{
		button.onClick.AddListener (HandleClick);
	}

	public void Setup (Item currentItem, ButtonsList currentButtonsList)
	{
		item = currentItem;
		nameLabel.text = item.itemName;
		iconImage.sprite = item.icon;
	}

	//обработка нажатий
	public void HandleClick()
	{

		//храним ссылку на родителя
		PanelsController panelsController = GameObject.Find ("MainCanvas").GetComponent<PanelsController> ();

		//выясняем какая кнопка нажата
		switch (transform.parent.name) 
		{

		case("CategoryPickerPanel"): 
			Debug.Log ("transform.parent.name:" + transform.parent.name);
			break;

		case("ObjectPickerPanel"): 
			//goto browse Mode
			panelsController.GoBrowseMode();
			break;

		case("BrowseModePanel"):
			panelsController.GoMainMode();
			break;

		default:
			Debug.LogError ("new or unknown category" + transform.parent.name);
			break;
		}


	}

}
