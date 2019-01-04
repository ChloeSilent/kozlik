using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public QuizController quizController;
	public AudioClip showMeWhere;
	public AudioClip rightAnswer;
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
}
