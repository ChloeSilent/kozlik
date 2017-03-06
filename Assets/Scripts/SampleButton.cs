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

		//храним ссылку на родителей
		PanelsController currentPanelsController = GameObject.Find ("MainCanvas").GetComponent<PanelsController> ();
		ButtonsList currentButtonsList = this.transform.parent.GetComponent<ButtonsList> ();

		//выясняем кто parent нажатой кнопки, реагируем соответственно
		switch (transform.parent.name) 
		{

		case("CategoryPickerPanel"): 
			currentButtonsList.ChangeCategoryTo (item);
			break;

		case("ObjectPickerPanel"): 
			currentPanelsController.GoBrowseMode(item);
			break;

		case("BrowseModePanel"):
			currentPanelsController.GoMainMode(); //TODO category id here
//			currentButtonsList.ChangeCategoryTo (item);
			break;

		default:
			Debug.LogError ("new or unknown category" + transform.parent.name);
			break;
		}


	}

}
