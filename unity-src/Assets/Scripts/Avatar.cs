using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    public static GameObject avatar;

    public static GameObject left;
    public static GameObject center;
    public static GameObject right;

    public GameObject leftFoot;
    public GameObject rightFoot;

    public GameObject leftFootPrint;
    public GameObject rightFootPrint;

    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject head;

    public GameObject startButton;

    public float footPrintY = 0.01f;

    public static bool isJumping;
    public static bool stepSide;
    public static float countStep;
    public static List<float> steps;

    public static bool isPause;

    public static float userX;
    public static float userY;
    public static float userZ;

    // Start is called before the first frame update
    void Start()
    {

        avatar = GameObject.Find("avatar");
        left = GameObject.Find("floorTile-left");
        center = GameObject.Find("floorTile-center");
        right = GameObject.Find("floorTile-right");

        leftFootPrint = GameObject.Find("footprint-left");
        rightFootPrint = GameObject.Find("footprint-right");
        countStep = 0;
        startButton.transform.gameObject.SetActive(false);

        isJumping = false;
        stepSide = false;
        isPause = false;

        userX = 0;
        userY = 0;
        userZ = 0;
        InitialExtraSpeed();
    }

    public static void InitialExtraSpeed() {
        steps = Enumerable.Repeat<float>(0, 10).ToList<float>();

    }

    private void FixedUpdate()
    {
        countStep += Time.fixedDeltaTime;
    }

    void Update()
    {
        Debug.Log(userX + "/" + userY + "/" + userZ);
        HandleFootPrint();
        HandleUI();
        HandleJump();
        HandleStep();
        HandlePause();
    }

    void HandleUI() {
        GameObject playerPosition = Player.playerPosition;

        if (IsInside(left, leftFootPrint) && IsInside(left, rightFootPrint))
        {
            playerPosition.transform.position = new Vector3(Tile.left, playerPosition.transform.position.y, playerPosition.transform.position.z);
        }
        if (IsInside(center, leftFootPrint) && IsInside(center, rightFootPrint))
        {
            playerPosition.transform.position = new Vector3(Tile.center, playerPosition.transform.position.y, playerPosition.transform.position.z);
        }
        if (IsInside(right, leftFootPrint) && IsInside(right, rightFootPrint))
        {
            playerPosition.transform.position = new Vector3(Tile.right, playerPosition.transform.position.y, playerPosition.transform.position.z);
        }
    }

    void HandleFootPrint() {
        leftFootPrint.transform.position = new Vector3(leftFoot.transform.position.x, footPrintY, leftFoot.transform.position.z-10);
        rightFootPrint.transform.position = new Vector3(rightFoot.transform.position.x, footPrintY, rightFoot.transform.position.z-10);
    }

    void HandleStep() {
        if (stepSide == true && (leftFoot.transform.position.y > 0.1 && rightFoot.transform.position.y < 0.1)) {
            stepSide = false;
            steps.RemoveAt(0);
            steps.Add(10 / countStep);
            Tile.extraSpeed = steps.Average();
            countStep = 0;
        }

        if (stepSide == false && (leftFoot.transform.position.y < 0.1 && rightFoot.transform.position.y > 0.1))
        {
            stepSide = true;
            steps.RemoveAt(0);
            steps.Add(10 / countStep);
            Tile.extraSpeed = steps.Average();
            countStep = 0;
        }
    }

    bool IsInside (GameObject button, GameObject obj) {
        float objX = obj.transform.position.x;
        float objZ = obj.transform.position.z;

        bool horizontal = (objX > button.transform.position.x - (button.transform.localScale.x / 2))
            && (objX < button.transform.position.x + (button.transform.localScale.x / 2));
        bool vertical = (objZ > button.transform.position.z - (button.transform.localScale.z / 2))
            && (objZ < button.transform.position.z + (button.transform.localScale.z / 2));

        return horizontal && vertical;
    }

    void HandleJump() {
        isJumping =  leftFoot.transform.position.y > 0.2 && rightFoot.transform.position.y > 0.2;
    }

    void HandlePause() {
        if (isPause)
        {
            UIinGame.instance.bePause = true;
            startButton.transform.gameObject.SetActive(true);
        }
        else {
            if ((leftHand.transform.position.y > head.transform.position.y && rightHand.transform.position.y > head.transform.position.y)
                && (IsInside(startButton, leftFootPrint) && IsInside(startButton, rightFootPrint))) {
                UIinGame.instance.bePause = false;
                startButton.transform.gameObject.SetActive(false);
            }
        }

    }
}
