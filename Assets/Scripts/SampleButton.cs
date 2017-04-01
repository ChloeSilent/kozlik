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
		item = currentItem; //TODO это хуйня. переделать
		nameLabel.text = item.itemName;

		//если номер спрайта был выбран ранее
		if (item.spriteWasSelected == true) 
		{
			// то используем выбранный ранее
			iconImage.sprite = item.pictureList [item.savedNumberOfSelectedPicture];
		} 
		else 
		{
			// то выберем новый спрайт
			int numberOfRandomPicture = Random.Range (0, item.pictureList.Capacity);

			//пропишем выбранный спрайт в кнопку 
			iconImage.sprite = item.pictureList [numberOfRandomPicture];

			//пропишем выбранный спрайт в айтем 
			item.savedNumberOfSelectedPicture = numberOfRandomPicture;

			// и отметим, что у этого айтема спрайт уже выбран
			item.spriteWasSelected = true;
		}
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
			if(item.itemName == "quiz") //12 кнопка quiz
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
