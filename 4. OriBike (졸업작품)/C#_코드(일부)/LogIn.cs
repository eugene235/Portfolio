using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class LogIn : SignUpField {
    private string htmlText;
    private string[] loginInfo;
    private Button LoginBtn;
    private InputField idField, pwdField;
    private Text reviewField;
    private string id, pwd;
    private string[] jVal, uVal;
    private int NOF;

    // Use this for initialization
    void Start () {

        url = "http://203.252.218.18:8080/OriBike/LogIn.jsp";

        NOF = 2;

        jVal = new string[] { "id", "pwd" };
        uVal = new string[NOF];

        LoginBtn = returnField(LoginBtn, "LogInButton");
        LoginBtn.onClick.AddListener(() => StartCoroutine(signUpAndLogIn(jVal,uVal,NOF)));

        idField = returnField(idField,"IDField");
        pwdField = returnField(pwdField,"PasswordField");

        idField.onEndEdit.AddListener(onChangeID);
        pwdField.onEndEdit.AddListener(onChangePWD);

        reviewField = returnField(reviewField, "review");
    }
	
	// Update is called once per frame
	void Update () {

    }

    protected override void onChangeID(string str)
    {
        id = str;

        try
        {
            uVal[0] = id;
        }
        catch (System.IndexOutOfRangeException e)
        {
            //throw new System.ArgumentOutOfRangeException("index parameter is out of range.", e);
        }
    }

    protected override void onChangePWD(string str)
    {
        pwd = str;

        try
        {
            uVal[1] = pwd;
        }
        catch (System.IndexOutOfRangeException e)
        {
            //throw new System.ArgumentOutOfRangeException("index parameter is out of range.", e);
        }
    }

    //html의 태그를 다 떼어주는 함수                                                                                                                            
    public static string StripHtml(string Html)
    {
        string output;
        output = System.Text.RegularExpressions.Regex.Replace(Html, "<[^>]*>", string.Empty);
        output = System.Text.RegularExpressions.Regex.Replace(output, @"^\s*$\n", string.Empty, System.Text.RegularExpressions.RegexOptions.Multiline);
        return output;
    }

    protected override void notMatch()
    {
        reviewField.color = new Color(254.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f);
        reviewField.text = "ID와 비밀번호가 일치하지 않습니다";
    }

    protected override string isEqualPwd()
    {
        return "match";
    }

    protected override void nullField()
    {
        reviewField.color = new Color(254.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f);
        reviewField.text = "모든 항목을 입력해주세요";
        Debug.Log("모든 항목을 입력해주세요");
    }

    protected override void error()
    {
        notMatch ();
    }

    protected override void afterWWW()
    {
        htmlText = StripHtml(w.text);
        loginInfo = htmlText.Split(new string[] { System.Environment.NewLine }, System.StringSplitOptions.RemoveEmptyEntries);

        if (loginInfo[0].Trim() == "success")
        {
            reviewField.color = new Color(254.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f);
            reviewField.text = "로그인 성공!";
            PlayerPrefs.SetString("playerId", id);
            SceneManager.LoadScene("SelectPlayerNumber");
        }
        else
        {
            Debug.Log(w.text);
            error();
        }
    }

    //후크
    protected override void onChangeName(string s)
    {
        throw new NotImplementedException();
    }

    protected override void onChangeCFM(string s)
    {
        throw new NotImplementedException();
    }
}