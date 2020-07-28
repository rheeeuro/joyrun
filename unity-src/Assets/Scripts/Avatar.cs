using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    public static GameObject avatar;
    public static GameObject left;
    public static GameObject center;
    public static GameObject right;
    public GameObject leftFoot;
    public GameObject rightFoot;
    public static GameObject playerPosition;

    public static bool isJumping;
    public static bool stepSide;
    public static float countStep;

    // Start is called before the first frame update
    void Start()
    {

        avatar = GameObject.Find("avatar");
        left = GameObject.Find("floorTile-left");
        center = GameObject.Find("floorTile-center");
        right = GameObject.Find("floorTile-right");
        playerPosition = GameObject.Find("playerPosition");
        countStep = 0;

        isJumping = false;
        stepSide = false;
    }

    private void FixedUpdate()
    {
        countStep += Time.fixedDeltaTime;
    }

    void Update()
    {
        HandleUI();
        HandleJump();
    }

    void HandleUI() {
        GameObject playerPosition = Player.playerPosition;

        if (IsInside(left, leftFoot) && IsInside(left, rightFoot))
        {
            playerPosition.transform.position = new Vector3(Tile.left, playerPosition.transform.position.y, playerPosition.transform.position.z);
        }
        if (IsInside(center, leftFoot) && IsInside(center, rightFoot))
        {
            playerPosition.transform.position = new Vector3(Tile.center, playerPosition.transform.position.y, playerPosition.transform.position.z);
        }
        if (IsInside(right, leftFoot) && IsInside(right, rightFoot))
        {
            playerPosition.transform.position = new Vector3(Tile.right, playerPosition.transform.position.y, playerPosition.transform.position.z);
        }
    }

    void HandleStep() {
        if (stepSide == true && (leftFoot.transform.position.y > 0.1 && rightFoot.transform.position.y < 0.1)) {
            stepSide = false;
            Tile.extraSpeed = 4 / countStep;
            countStep = 0;
        }

        if (stepSide == false && (leftFoot.transform.position.y < 0.1 && rightFoot.transform.position.y > 0.1))
        {
            stepSide = true;
            Tile.extraSpeed = 4 / countStep;
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
        isJumping =  avatar.transform.position.y > 0.15;
    }
}
