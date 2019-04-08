using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleButton : MonoBehaviour 
{
	public Button button;
	public GameObject letterPanel;
	public GameObject namePanel;
	public Text nameLabel;
	public Text initialLetter;
	public Image iconImage;
	private Item item;
	private bool isDislpayingPictureNow;
	private AudioClip letterClip;
	private AudioClip nameClip;
	private SoundController soundController;
	private Mask maskComponent;
	private GameModeController gameModeController;

	private void Awake()
	{
		//соберем ссылки на контроллеры 
		soundController = FindObjectOfType<SoundController>();
		gameModeController = FindObjectOfType<GameModeController>();
		// через инспектор нельзя назначить значение больше трёхсот, поэтому назначим тут
		initialLetter.fontSize = 710;
		maskComponent = transform.GetComponent<Mask>();
	}

	void Update()
	{
		// на десктопе можно стрелками листать спрайты
		if (transform.parent.name == "BrowseModePanel") 
		{
			if (Input.GetKeyUp("right"))
			{
				SwitchToNextSprite ();
			}

			if (Input.GetKeyUp("left"))
			{
				SwitchToPreviousSprite ();
			}	
		}
	}

	public void Setup (Item itemToSetupWith)
	{
		item = itemToSetupWith;
		nameLabel.text = item.itemName;
		initialLetter.text = item.initialLetter.ToString ();
		SetupSprite ();
		button.gameObject.name = item.itemName; // использовать this ?
		AudioSource[] allAudioSourcesOfButton = item.GetComponents<AudioSource>();
		if (allAudioSourcesOfButton.Length>0)
		{
			nameClip = allAudioSourcesOfButton[0].clip;
		}
		if (allAudioSourcesOfButton.Length > 1)
		{
			letterClip = allAudioSourcesOfButton[1].clip;
		}
	}

	public void SetupSprite ()
	{
		if (item.spriteWasSelected == false) //если номер спрайта не был выбран ранее
		{
			// то выберем новый спрайт
			int numberOfRandomPicture = Random.Range (0, item.pictureList.Count);	
		
			// отметим флажком, что у этого айтема спрайт уже выбран
			item.spriteWasSelected = true;

			//пропишем выбранный спрайт в айтем 
			item.savedNumberOfSelectedPicture = numberOfRandomPicture;
		} 

		//пропишем выбранный спрайт в кнопку 
		iconImage.sprite = item.pictureList [item.savedNumberOfSelectedPicture];
	}

	public void SwitchToNextSprite()
	{
		// Если выбран последний спрайт, то переключаем на первый, т.к. листаем по кругу
		if(item.savedNumberOfSelectedPicture==(item.pictureList.Count-1))
		{
			item.savedNumberOfSelectedPicture = 0;
		}
        // Для всех остальных кроме последнего переключимся на следующий, увеличив номер на единицу
		else
		{
			item.savedNumberOfSelectedPicture++;			
		}
		Setup (item); 
	}

	public void SwitchToPreviousSprite()
	{
        //если выбран первый спрайт, то переключим на последний, т.к. листаем по кругу
        if (item.savedNumberOfSelectedPicture==0)
		{
			item.savedNumberOfSelectedPicture = (item.pictureList.Count-1);
		}
        // Для всех остальных кроме первого перенключимся на предыдущий, уменьшив номер на единицу
        else
        {
			item.savedNumberOfSelectedPicture--;			
		}
		Setup (item);
	}

	//обработка нажатий
	public void HandleClick()
	{
		QuizController quizController = GameObject.Find ("QuizController").GetComponent<QuizController> (); // TODO переместить

		//выясняем кто parent нажатой кнопки, реагируем соответственно
		switch (transform.parent.name) 
		{
            //если нажатая кнопка находится в панели выбора категории
		case("CategoryPickerPanel"): 
			soundController.TellButtonName(nameClip);  // TODO переместить
            gameModeController.SwitchCategory(item);
			break;

		case("ObjectPickerPanel"): 
			//если нажата кнопка квиза, то гоу в квиз
			if(item.gameObject.name == "Quiz")
			{
				gameModeController.SwitchFromChoiseToQuiz(item); 
			}
			// если нажата НЕ кнопка квиза, то это кнопка предмета, гоу в  BrowseMode
			else
			{
				gameModeController.SwitchFromChoiseToBrowse(item);
				soundController.TellButtonName(nameClip);  //  TODO переместить
			}
			break;

		case("BrowseModePanel"):
				if (isDislpayingPictureNow==true) // TODO переименовать
				{
					// если это первый экран для browseMode , то 
					// переключаемся на второй экран с буквицей
					DisplayInitialLetterFullscreeen();
					soundController.TellButtonLetter(letterClip);
					// и ставим флаг, сигнализирующий, что мы уже не на первом экране
					isDislpayingPictureNow = false;
					break;
				}
				else
				{
					// если это второй экран для browseMode , то возвращаемся на главный экран
					gameModeController.SwitchFromBrowseToChoise(item);
					break;
				}

		case("QuizButtonsPanel"):
			// угадал?
			bool guessWasSuccessfull = quizController.CheckIfWinner (item); 
			// да, угадал
			if (guessWasSuccessfull == true) 
			{
				gameModeController.SwitchFromQuizToBrowse(item);
			}
			// нет, не угадал
			else 
			{
				this.MoveRedCrossForward (); //TODO CHECK IF THIS needed
			}
			break;

		default:
			Debug.LogError ("Ошибка. У кнопки неверный или несуществующий parent" + transform.parent.name);
			break;
		}
	}

    public void TuneButtonForPool()
    {
        namePanel.SetActive(false);
        letterPanel.SetActive(false);
        MoveRedCrossBackward();
    }

    public void TuneButtonForMain()
	{
		maskComponent.enabled = true;
		namePanel.SetActive (false);
		letterPanel.SetActive (false);
		MoveRedCrossBackward ();
		DisableARF();
	}

	public void TuneButtonForBrowse()
	{
		maskComponent.enabled = false;
		namePanel.SetActive (false);
		letterPanel.SetActive (false);
		MoveRedCrossBackward ();
		isDislpayingPictureNow = true;
		EnableARF();
	}

	public void TuneButtonForQuiz()
	{
		maskComponent.enabled = true;
		namePanel.SetActive (false);
		letterPanel.SetActive (false);
		MoveRedCrossBackward ();
		DisableARF();
	}

	public void MoveRedCrossForward()
	{
		this.transform.Find ("WrongImage").SetAsLastSibling (); 
	}

	public void MoveRedCrossBackward()
	{
		this.transform.Find ("WrongImage").SetAsFirstSibling (); 
	}

	private void DisplayInitialLetterFullscreeen()
	{
		namePanel.SetActive(true);
		letterPanel.SetActive(true);
	}

	// ресайзит картинку под текущее соотношение сторон экрана
	private void EnableARF ()
	{
		AspectRatioFitter arf = GetComponent<AspectRatioFitter>();
		arf.enabled = true;
	}

	// отключаем управление ratio, дальше этим будет заниматься layout group
	private void DisableARF()
	{
		AspectRatioFitter arf = GetComponent<AspectRatioFitter>();
		arf.enabled = false;
	}
}
