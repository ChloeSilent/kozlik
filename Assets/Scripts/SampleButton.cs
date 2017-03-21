using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SampleButton : MonoBehaviour 
{
	public Button button;
	public Text nameLabel;
	public Image iconImage;
	private Item item;

	void Start () 
	{
		button.onClick.AddListener (HandleClick);
	}

	public void Setup (Item currentItem, ButtonsController currentButtonsController)
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
		ButtonsController currentButtonsController = this.transform.parent.GetComponent<ButtonsController> ();

		//выясняем кто parent нажатой кнопки, реагируем соответственно
		switch (transform.parent.name) 
		{
		case("CategoryPickerPanel"): 
			currentButtonsController.ChangeCategory (item);
			break;

		case("ObjectPickerPanel"): 
			if(this.item.itemName == "QuizButton") //12 кнопка quiz
			{
				currentPanelsController.GoQuizMode ();
			}
			else //1-11 кнопка
			{
				currentPanelsController.GoBrowseMode(item);	
			}

			break;

		case("BrowseModePanel"):
			currentPanelsController.GoMainMode(item); 
			break;

		case("QuizButtonsPanel"):
			currentPanelsController.CheckIfWinner(item); 
			break;

		default:
			Debug.LogError ("new or unknown category" + transform.parent.name);
			break;
		}
	}
}
