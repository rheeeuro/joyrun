using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewBehaviourScript : MonoBehaviour
{

    public GameObject[] balls = new GameObject[9];
    public GameObject user;

    public float z = 55.4f;

    public float scale = 6.266f;
    public int percent = 90;

    public float left = 118.45f;
    public float center = 132.7f;
    public float right = 147.2f;


    private void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            balls[i] = GameObject.Find((i + 1).ToString());
        }


        left = balls[0].transform.position.x;
        center = balls[1].transform.position.x;
        right = balls[2].transform.position.x;
        user = GameObject.Find("user");
        user.transform.position = new Vector3(center, user.transform.position.y, user.transform.position.z);

    }
    void Update()
    {
        for (int i = 0; i < 9; i += 3)
        {
            for (int j = i; j < i + 3; j++) {
                balls[j].transform.Translate(Vector3.back * 30 * Time.deltaTime);
                if (balls[j].transform.position.z - 3 < user.transform.position.z
                    && balls[j].transform.localScale.x == scale 
                    && balls[j].transform.position.x == user.transform.position.x) {
                        Debug.Log("hit!");
                        balls[j].transform.localScale = new Vector3(0, 0, 0);
                }
            }
            if (balls[i].transform.position.z < -45)
            {
                balls[i].transform.position = new Vector3(balls[i].transform.position.x, balls[i].transform.position.y, z);
                balls[i + 1].transform.position = new Vector3(balls[i + 1].transform.position.x, balls[i + 1].transform.position.y, z);
                balls[i + 2].transform.position = new Vector3(balls[i + 2].transform.position.x, balls[i + 2].transform.position.y, z);
                int open = Random.Range(0, 3);
                switch (open)
                {
                    case 0:
                        balls[i].transform.localScale = new Vector3(scale, scale, scale);
                        balls[i + 1].transform.localScale = new Vector3(0, 0, 0);
                        balls[i + 2].transform.localScale = new Vector3(0, 0, 0);
                        break;
                    case 1:
                        balls[i].transform.localScale = new Vector3(0, 0, 0);
                        balls[i + 1].transform.localScale = new Vector3(scale, scale, scale);
                        balls[i + 2].transform.localScale = new Vector3(0, 0, 0);
                        break;
                    case 2:
                        balls[i].transform.localScale = new Vector3(0, 0, 0);
                        balls[i + 1].transform.localScale = new Vector3(0, 0, 0);
                        balls[i + 2].transform.localScale = new Vector3(scale, scale, scale);
                        break;
                }
                if (Random.Range(0, 100) > percent)
                {
                    balls[i + 1].transform.localScale = new Vector3(scale, scale, scale);
                    percent -= 2;
                }
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            user.transform.position = new Vector3(left, user.transform.position.y, user.transform.position.z);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            user.transform.position = new Vector3(center, user.transform.position.y, user.transform.position.z);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            user.transform.position = new Vector3(right, user.transform.position.y, user.transform.position.z);
        }




    }

    // 아래의 코드 생략
}
