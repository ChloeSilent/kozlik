using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailIssueReporter : MonoBehaviour
{
	public string issueReportLink;

	public void ReportAnIssue()
	{
		Application.OpenURL(issueReportLink);
	}
}
