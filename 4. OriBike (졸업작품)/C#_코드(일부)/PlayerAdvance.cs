using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;

[RequireComponent(typeof(NetworkView))]
public class PlayerAdvance : MonoBehaviour
{
    public static SerialPort sp = new SerialPort("COM6", 9600);
    float timer, speed, inter_timer;
    static float pi;
    static float radius;
    int sense;

    /* SerialPort형 변수 sp는 아두이노에서 읽어 들인 
     * 적외선 감지 센서와 자이로 센서 값을 컴퓨터의 
     * 시리얼 포트를 통해 읽어오고, 
     * int형 변수 sense에 그 값을 저장합니다. 
     * 나머지 변수 timer, inter_timer, pi, radius는 
     * 자전의 속력을 구하기 위해 필요한 변수들이고 
     * 아래의 코드에서 더 자세히 설명 드리겠습니다. */

    float Z = 0f;  //camera rotation value
    float Z0 = 100;
    float angle = 0.5f;

    /* 위의 변수들은 자이로 센서에서 받아온 
     * 방향 값을 처리하기 위해 설정한 변수입니다. 
     * Z0 값을 100으로 초기화 했는데 
     * 이는 캐릭터가 정면을 보고 있을 때의 값으로, 
     * 방향을 결정하는 기준 값으로 정했습니다. */

    int IR;
    /* 변수 IR은 아까 적외선 센서를 통해 구한 
     * 0, 1 값을 담을 변수입니다. */

    NetworkView nView;

    // Use this for initialization
    void Start()
    {
        nView = gameObject.GetComponent<NetworkView>();

        sp.Open();
        sp.ReadTimeout = 30;
        /* 포트를 열고 sp 변수의 ReadTimeout 속성을 정해줍니다. 
         * ReadTimeout 속성은 읽기 작업을 마쳐야 하는 제한 시간(밀리초)을
         * 뜻하며 30 밀리초로 정해주었습니다. */


        timer = 2.0f;
        pi = 3.14f;

        radius = 0.3f;
        speed = 0.0f;

        /* 각 변수들을 초기화 시킵니다. 
         * timer의 값은 후에 speed 변수의 분모가 될 것이므로 
         * 0.0f로 초기화 하지 않았습니다. 
         * radius는 직접 자전거 바퀴의 길이를 재서 나온 값(30cm)입니다. */

        IR = 3;
        /* IR 변수 값은 초기화할 때 캐릭터의 움직임과 상관없는 값으로 설정했습니다. 
         * (0 혹은 1이 되면 플레이어는 전혀 조작하지 않았음에도 캐릭터가 움직이게 됩니다.) */
    }


    // Update is called once per frame
    void Update()
    {
        if (!nView.isMine)
            return;

        try
        {
            sense = int.Parse(sp.ReadLine());
            //아두이노에서 보내준 센서의 값을 한 줄씩 읽어옵니다.
        }
        catch (TimeoutException)
        {
        }

        if (sense == 0)
        {
            IR = 0;
        }

        if (sense == 1)
        {
            IR = 1;
        }
        /* 
         * 만약 아두이노에서 읽어온 값이 0이면
         * (적외선 감지 센서에 아무것도 감지되지 않았음) 
         * IR 변수의 값을 0으로 만들고 1이면
         * (적외선 감지 센서에 자전거 휠의 돌출된 부분이 감지 됨)
         * IR 변수의 값을 1로 만들어줍니다.
         * 원래 적외선 감지 센서 코드만 있었을 때에는 sense 값으로 모든 것을 해결했지만,
         * 자이로 센서와 코드를 합치면서 IR 변수를 만들게 되었습니다. */

        if (IR == 0)
        {
            timer += Time.deltaTime;
            /* 적외선 감지 센서에 아무것도 감지가 되지 않을 때,
             * Time.deltaTime을 계속 더해주어 한 바퀴 돌 때 
             * 얼마큼의 시간이 걸렸는지를 측정합니다. */
        }

        if (IR == 1)
        {
            inter_timer = Time.deltaTime;
            timer = Mathf.Lerp(timer, inter_timer, 0.5f);

            IR = 0;
        }

        /* 
         * 적외선 감지 센서에 물체가 감지 됐을 경우 
         * timer의 값을 초기화 해주는데 위에서 말했던 것과 동일한 원리로 
         * 분모에 0이 들어가면 안 되기 때문에 Time.deltaTime을 대신해서 넣어줍니다. 
         * 그러나 실제로 실행을 했을 때 timer 변수에 time.deltaTime을 그대로 넣어주면 
         * 캐릭터의 움직임이 부자연스럽게 갑자기 움직이는 듯한 현상이 나타나기 때문에 
         * inter_timer라는 변수를 두었고 이전의 timer 값과 보간 함으로써 
         * 부드러운 움직임을 표현하고자 했습니다. 
         * 그리고 적외선 감지 센서에 물체가 감지되고 바로 IR 값을 0으로 만들어주어 
         * 다음 바퀴가 도는 시간을 측정합니다.
         */

        speed = (2 * pi * radius) / (timer);
        /*
         * speed 변수는 각속도를 나타내는 변수입니다. 
         * 방금 전에 구한 자전거가 한 바퀴 돌 때의 시간을 가지고 
         * 자전거 바퀴의 실질적인 속도를 표현합니다.
         */

        if (IR == 0 || IR == 1)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime * 7.5f);
        }
        /*
         * 이 부분은 시작과 동시에 캐릭터가 움직이는 것을 방지하기 위해
         * 적외선 감지 센서가 동작하고 있을 때만 캐릭터가 움직이도록 구현한 코드입니다.
         * (IR을 3으로 초기화 한 것도 마찬가지의 이유입니다.)
         */

        if (sense > 50) //변수 sense의 값이 50이 넘어가는 것은 자이로 센서의 영역입니다.
        {
            if (sense == Z0)
                transform.Rotate(0, 0, 0);
            //만약 자이로센서의 값이 Z0(정면을 보고 있다) 라면 회전을 하지 않습니다.

            if (sense > Z0)
            {
                transform.Rotate(0, angle, 0);
            }

            else if (sense < Z0)
            {
                transform.Rotate(0, -angle, 0);
            }
            //Z0의 값이 100보다 작으면 왼쪽, 크면 오른쪽으로 회전하도록 작성한 코드입니다.
        }
    }

    void OnApplicationQuit()
    {
        sp.Close();
    }
    //프로그램이 끝날 때 시리얼 포트를 닫아줍니다.
}