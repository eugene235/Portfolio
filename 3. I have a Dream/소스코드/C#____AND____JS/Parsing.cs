using UnityEngine;
using System.Collections;

public class Parsing : MonoBehaviour {

    private string url = "http://192.168.100.245:8080/DimiGame/LogIn.jsp?userId=asdf&pwd=1234";
    public WWW www;
    private string text;

	// Use this for initialization
	void Start()
	{
		StartCoroutine (Test ());
	}

	IEnumerator Test(){

		www = new WWW(url);
		yield return www;
		Debug.Log(www.text);
		
		
		text = StripHtml (www.text);
		Debug.Log (text);
	}



	// Update is called once per frame
	void Update () {

	}

    //html의 태그를 다 떼어주는 함수                                                                                                                            
    public static string StripHtml(string Html)
    {
        string output;
        output = System.Text.RegularExpressions.Regex.Replace(Html, "<[^>]*>", string.Empty);
        output = System.Text.RegularExpressions.Regex.Replace(output, @"^\s*$\n", string.Empty, System.Text.RegularExpressions.RegexOptions.Multiline);
        return output;
    }
}
