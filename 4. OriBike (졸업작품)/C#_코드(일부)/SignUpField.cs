using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
* 템플릿 메소드 패턴 사용(부모 클래스)
*/
abstract public class SignUpField : MonoBehaviour {
    abstract protected string isEqualPwd();
    abstract protected void nullField();
    abstract protected void notMatch();
    abstract protected void error();
    abstract protected void afterWWW();
    abstract protected void onChangeName(string s); 
    //회원가입&로그인 시 이름을 입력하는 Input Field에 바뀌는 내용 감지 및 변경
    abstract protected void onChangeID(string s);
    abstract protected void onChangePWD(string s);
    abstract protected void onChangeCFM(string s);
    //회원가입 체계에서 필요한 검사들을 하기 위한 메서드들의 정의입니다.

    protected WWW w;
    protected string url;

    // Use this for initialization (Empty)
    void Start() {
        
    }

    // Update is called once per frame (Empty)
    void Update()
    {

    }

    /* 유니티 Scene에서 
     * text field, input field, Button의 
     * 객체를 찾아주는 역할을 합니다. */
    public T returnField<T>(T tmp, string fieldName)
    {
        tmp = GameObject.Find(fieldName).GetComponent<T>();

        return tmp;
    }


    protected string nullCheck(string[] arr, int NOF)
    {
        for (int i = 0; i < NOF; i++)
        {
            if (arr[i] == "" || arr[i] == null) 
            {
                Debug.Log("null");
                return null;
            }
        }
        return "NOT NULL";
    }

    protected IEnumerator signUpAndLogIn(string[] jspVal, string[] unityVal, int nof)
    {
        /*
         * 템플릿 메서드입니다. 위에서 정의한 추상 메서드들로 처리의 흐름을 정해놓고 
         * 실질적인 구현과 작동은 자식 클래스 SignUp.cs, LogIn.cs에서 당담하게 됩니다.
         * 회원가입과 로그인을 처리하는 과정이 비슷하다고 생각하여
         * 템플릿 메서드 패턴을 사용하게 되었습니다.
         * 리턴 형식은 IEnumerator로 로그인이나 회원가입 버튼을 누를 때 작동하게 됩니다. 
         * 이 메서드 안에서 WWWform 객체를 통해 톰캣 서버와 통신을 하고 
         * 통신 결과가 오류가 없는지 확인한 뒤 로그인 혹은 회원가입 처리를 하게 됩니다.
         */

        if (nullCheck(unityVal, nof) != null && isEqualPwd() == "match")
        {
            /* isEqualPwd 메서드의 경우 
             * 회원가입에서 비밀번호를 두 번 입력하게 만든 뒤,
             * 비밀번호와 확인용 비밀번호의 입력이 다를 경우
             * 회원가입을 할 수 없도록 만든 것이기 때문에
             * 로그인 단계에선 불필요한 메서드입니다.
             * 따라서 회원가입 클래스(SignUp.cs)의 isEqulPwd()는
             * 검사를 시행하게 되지만 로그인 클래스(Login.cs)의 isEqualPwd()는
             * 무조건 "match"를 리턴하도록 만들었습니다. */
            
            WWWForm form = new WWWForm();
            // 새로운 WWWForm 객체 form을 만들어줍니다.

            for (int i = 0; i< nof; i++)
            {
                form.AddField(jspVal[i], unityVal[i]);
            }
            w = new WWW(url, form);

            yield return w;
            /* 위에서 null 체크 등을 마친 오류가 없는 string형 배열과
             * url 주소를 가지고 톰캣 서버와 통신을 합니다. */

            if (w.error != null)
            {
                Debug.Log(w.error);
                error();
            }
            /* 오류가 있다면 에러를 출력하게 되고 그렇지 않다면 afterWWW()를 통해
             * SignUp.cs, Login.cs 소스코드에서 각각 어떤 처리를 할 것인지 정하게 됩니다.
             * Login.cs의 경우에는 톰캣 서버를 통해 DB에 있는 ID와 Password가 같은지 검사를 하게 됩니다.
             * 그렇게 받은 결과 페이지의 HTML 태그를 다 떼고 html body의 내용을 보고
             * 로그인이 성공했는지 실패했는지 검사한 뒤,
             * 로그인에 성공했으면 다음 Scene으로 넘어가도록 되어있습니다.
             * SignUp.cs의 경우는 DB에 새로운 정보를 저장하는 것이기 때문에
             * 별도의 검사 없이 바로 처음 Scene으로 넘어가도록 만들었습니다.*/

            else
            {
                afterWWW();
            }
        }
        else if (nullCheck(unityVal, nof) == null)
        {
            nullField();
        }
        /* 위의 returnField 메서드를 통해 각 개체에 접근하여 얻어온 text 값이
         * null인지 아닌지 확인하는 메서드입니다.
         * 회원가입이나 로그인 시에 빈 칸이 없어야 하기 때문에 구현하게 됐습니다. */

        else if (isEqualPwd()== "" && nullCheck(unityVal, nof) == "NOT NULL")
        {
            notMatch();
        }
    }
}