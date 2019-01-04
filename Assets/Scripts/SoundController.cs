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

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	// озвучивает "покажи, где"  потом название правильного варианта
	public void AskQiuzAudioQuestion(int winnerId)
	{
		StartCoroutine(playSequentially());
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
}
