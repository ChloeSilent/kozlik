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

	public void Setup (Item currentItem)
	{
		nameLabel.text = currentItem.itemName;
		int numberOfRandomPictureOfItem = Random.Range (0, currentItem.pictureList.Capacity);
		iconImage.sprite = currentItem.pictureList[numberOfRandomPictureOfItem];
	}

	//обработка нажатий
	public void HandleClick()
	{
		//храним ссылку на родителей
		PanelsController currentPanelsController = GameObject.Find ("MainCanvas").GetComponent<PanelsController> (); //todo fix find
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
				Debug.Log ("1");
			}

			break;

		case("BrowseModePanel"):
			currentPanelsController.GoMainMode(item); 
			break;

		case("QuizButtonsPanel"):
			currentPanelsController.CheckIfWinner(item, button.gameObject); 
			break;

		default:
			Debug.LogError ("new or unknown category" + transform.parent.name);
			break;
		}
	}

	public void MoveRedCrossForward()
	{
		this.transform.FindChild ("WrongImage").SetAsLastSibling ();
	}

	public void MoveRedCrossBackward()
	{
		
		this.transform.FindChild ("WrongImage").SetAsFirstSibling ();
	}
}
