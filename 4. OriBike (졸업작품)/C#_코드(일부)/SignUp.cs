using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

namespace SignUp {
    public class SignUp : SignUpField {
        InputField nameF, idF, pwdF, cfmF;
        Text reviewField;
        Button signUpBtn;
        string name, id, pwd, cfm;
        string[] jVal, uVal;
        int NOF; //number of field

        // Use this for initialization
        void Start () {
            url = "http://203.252.218.18:8080/OriBike/SignUp.jsp";

            NOF = 3;

            nameF = returnField(nameF, "nameField");
            idF = returnField(idF, "IDField");
            pwdF = returnField(pwdF, "PasswordField");
            cfmF = returnField(cfmF, "ConfirmField");

            name = id = pwd = cfm = null;

            jVal = new string[] {"name", "id", "pwd"};
            uVal = new string[NOF];
            
            reviewField = returnField(reviewField, "review");
            signUpBtn = returnField(signUpBtn, "SignUpButton");

            nameF.onEndEdit.AddListener(onChangeName);
            idF.onEndEdit.AddListener(onChangeID);
            pwdF.onEndEdit.AddListener(onChangePWD);
            cfmF.onEndEdit.AddListener(onChangeCFM);

            signUpBtn.onClick.AddListener(() => StartCoroutine(signUpAndLogIn(jVal,uVal,NOF)));
        }
	
	    // Update is called once per frame
	    void Update () {

        }

        protected override void onChangeName(string str)
        {
            name = str;
            uVal[0] = name;
            Debug.Log(str);
        }

        protected override void onChangeID(string str)
        { 
            id = str;
            uVal[1] = id;
        }

        protected override void onChangePWD(string str)
        {
            pwd = str;
            uVal[2] = pwd;
        }

        protected override void onChangeCFM(string str)
        {
            cfm = str;
        }


        protected override string isEqualPwd()
        {
            if (pwd!=null && cfm!=null && pwd.Equals(cfm))
            {
                return "match";
            }
            return "";
        }

        protected override void nullField()
        {
            reviewField.color = new Color(254.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f);
            reviewField.text = "모든 항목을 입력해주세요";
            Debug.Log("모든 항목을 입력해주세요");
        }

        protected override void notMatch()
        {
            reviewField.color = new Color(254.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f);
            reviewField.text = "비밀번호가 일치하지 않습니다";
            Debug.Log("비밀번호가 일치하지 않습니다");
        }

        protected override void error()
        {
            reviewField.color = new Color(254.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f);
            reviewField.text = "이미 존재하는 ID입니다";
            Debug.Log("이미 존재하는 ID입니다");
        }

        protected override void afterWWW()
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}