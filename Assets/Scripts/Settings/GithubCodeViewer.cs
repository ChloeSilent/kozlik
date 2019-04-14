using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GithubCodeViewer : MonoBehaviour
{
	public string githubCodeLink;

	public void ViewGithubCode()
	{
		Application.OpenURL(githubCodeLink);
	}
}
