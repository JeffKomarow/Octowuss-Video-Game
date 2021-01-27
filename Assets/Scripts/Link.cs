using UnityEngine;
using UnityEngine.UI;

public class Link : MonoBehaviour 
{

	public void OpenLinkToTwitter()
	{
		Application.OpenURL("https://twitter.com/JeffKomarow");
	}

	public void OpenLinkToInstagram()
	{
		Application.OpenURL("https://www.instagram.com/jeffkomarow/");
	}

	public void OpenLinkToWebsite()
	{
		Application.OpenURL("https://jeffkomarow.com/");
	}

	public void OpenLink(string link)
    {
		Application.OpenURL(link);
    }

}