using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundController : MonoBehaviour
{
	public QuizModeController quizModeController;
	public AudioClip showMeWhere;
	public AudioClip rightAnswer;
	public AudioClip tellRight;
	public AudioClip tellWrong;
	private AudioSource audioSource;
	private AudioClip buttonName;
	public AudioClip wordLetterClip;
	private UnityAction<ItemButton> TellButtonNameListener;
	private UnityAction<ItemButton> TellButtonLetterListener;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void OnEnable()
	{
		TellButtonNameListener = new UnityAction<ItemButton> (TellButtonName);
		TellButtonLetterListener = new UnityAction<ItemButton> (TellButtonLetter);

		EventManager.StartListening ("CategoryPickerButtonClicked", TellButtonNameListener);
		EventManager.StartListening ("ObjectPickerButtonClicked", TellButtonNameListener);
		EventManager.StartListening ("FirstClickInBrowseMode", TellButtonLetterListener);
	}

	private void OnDisable()
	{
		EventManager.StopListening ("CategoryPickerButtonClicked", TellButtonNameListener);
		EventManager.StopListening ("ObjectPickerButtonClicked", TellButtonNameListener);
		EventManager.StopListening ("FirstClickInBrowseMode", TellButtonLetterListener);
	}

	// озвучивает "покажи, где"  потом название правильного варианта
	public void AskQiuzAudioQuestion(int winnerId)
	{
		rightAnswer = quizModeController.ReturnWinnersAudioClip();
		StartCoroutine(playTwoClipsSequentially(showMeWhere, rightAnswer));
	}

	IEnumerator playSequentially()
	{
		audioSource.clip = showMeWhere;
		audioSource.Play();

		yield return new WaitForSeconds(audioSource.clip.length);

		rightAnswer = quizModeController.ReturnWinnersAudioClip();
		audioSource.clip = rightAnswer;
		audioSource.Play();
	}

	public void TellRight()
	{
		audioSource.clip = tellRight;
		audioSource.Play();
	}

	public void TellWrong()
	{
		audioSource.clip = tellWrong;
		audioSource.Play();
	}

	public void TellButtonName(ItemButton itemButton)
	{
		audioSource.clip = itemButton.nameClip;
		audioSource.Play();
	}

	public void TellButtonLetter (ItemButton itemButton)
	{
		StartCoroutine(playTwoClipsSequentially(wordLetterClip, itemButton.letterClip));
	}

	IEnumerator playTwoClipsSequentially(AudioClip firstClip, AudioClip secondClip)
	{
		audioSource.clip = firstClip;
		audioSource.Play();

		yield return new WaitForSeconds(audioSource.clip.length);

		audioSource.clip = secondClip;
		audioSource.Play();
	}
}
