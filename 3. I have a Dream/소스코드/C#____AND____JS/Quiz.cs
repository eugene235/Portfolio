using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Quiz : MonoBehaviour {

	public GUISkin textSkin;
	private Text t_quiz = null;

	private string url = "http://192.168.100.246:8080/GameDB/Quiz.jsp";
	public WWW www;
	private string text;
	private string[] QuizTable;
	private string[] m_quiz;
	private string[] ranQuiz;
	private float timer;
	private float interval;

	private Button Btn;
	private int wayNum;

	private int rndAnswer = 0;
	private int count = 0;

	// Use this for initialization
	void Start () {
		interval = 10.0f;
		timer = 10.0f;

		m_quiz = new string[4];
		t_quiz = GameObject.Find("QuizPannel").GetComponent<Text>();
		Btn = GameObject.Find("BoosterBtn").GetComponent<Button>();

		Btn.onClick.AddListener (Booster);

	}

	void Booster(){
		timer -= 0.5f;
	}

	IEnumerator getQuiz(){
		www = new WWW(url);
		yield return www;
		text = StripHtml (www.text);
		QuizTable = text.Split( new string[]{ System.Environment.NewLine }, System.StringSplitOptions.RemoveEmptyEntries);

		//Debug.Log (QuizTable[0].Trim());	//ㅁㅜㄴㅈㅔ
		//Debug.Log (QuizTable[1].Trim());	//ㄷㅏㅂ
		//Debug.Log (QuizTable[2].Trim());	//ㅇㅗㄷㅏㅂ1
		//Debug.Log (QuizTable[3].Trim());	//ㅇㅗㄷㅏㅂ2

		m_quiz [0] = QuizTable [0].Trim ();
		m_quiz [1] = QuizTable [1].Trim ();
		m_quiz [2] = QuizTable [2].Trim ();
		m_quiz [3] = QuizTable [3].Trim ();

		t_quiz.text = m_quiz[0];

		rndAnswer = Random.Range (1, 4);
		bool first = true;

		for (int i = 1; i < 4; i++) {
			string way = i.ToString ();


			if (i == rndAnswer) {
				GameObject.Find (way).SendMessage ("IsAnswer", true);
				GameObject.Find (way).SendMessage ("DisplayAnswer", m_quiz [1]);
			} 

			else {
				GameObject.Find (way).SendMessage ("IsAnswer", false);

				if (first) {
					GameObject.Find (way).SendMessage ("DisplayAnswer", m_quiz [2]);
					first = false;
				} else if (!first) {
					GameObject.Find (way).SendMessage ("DisplayAnswer", m_quiz [3]);
				}
			}
			
		}
	}

	// Update is called once per frame
	void Update () {
		//t_quiz.text = text;
		timer -= Time.deltaTime;

		if (timer <= 0.0f) {
			StartCoroutine(getQuiz());
     		timer = interval;
			count++;
		}

		if (count == 6) {
			Application.LoadLevel ("End");
		}
			
	}

	//text에는 html태그가 다 떼어진 정보가 들어옴

	//html의 태그를 다 떼어주는 함수                                                                                                                            
	public static string StripHtml(string Html)
	{
		string output;
		output = System.Text.RegularExpressions.Regex.Replace(Html, "<[^>]*>", string.Empty);
		output = System.Text.RegularExpressions.Regex.Replace(output, @"^\s*$\n", string.Empty, System.Text.RegularExpressions.RegexOptions.Multiline);
		return output;
	}
}