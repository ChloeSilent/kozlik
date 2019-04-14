using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GithubPlanViewer : MonoBehaviour
{
	public string githubPlanLink;

	public void ViewGithubPlan()
	{
		Application.OpenURL(githubPlanLink);
	}
}
