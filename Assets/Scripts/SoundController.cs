using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
	public QuizController quizController;
	public AudioClip showMeWhere;
	public AudioClip rightAnswer;
	public AudioClip tellRight;
	public AudioClip tellWrong;
	private AudioSource audioSource;
	private AudioClip buttonName;
	public AudioClip wordLetterClip;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	// озвучивает "покажи, где"  потом название правильного варианта
	public void AskQiuzAudioQuestion(int winnerId)
	{
		rightAnswer = quizController.ReturnWinnersAudioClip();
		StartCoroutine(playTwoClipsSequentially(showMeWhere, rightAnswer));
	}

	IEnumerator playSequentially()
	{
		audioSource.clip = showMeWhere;
		audioSource.Play();

		yield return new WaitForSeconds(audioSource.clip.length);

		rightAnswer = quizController.ReturnWinnersAudioClip();
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

	public void TellButtonName(AudioClip buttonName)
	{
		audioSource.clip = buttonName;
		audioSource.Play();
	}

	public void TellButtonLetter (AudioClip buttonLetter)
	{
		StartCoroutine(playTwoClipsSequentially(wordLetterClip, buttonLetter));
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
