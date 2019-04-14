using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelegramIssueReporter : MonoBehaviour
{
	public string issueReportLink;

	public void ReportAnIssue()
	{
		Application.OpenURL(issueReportLink);
	}
}
