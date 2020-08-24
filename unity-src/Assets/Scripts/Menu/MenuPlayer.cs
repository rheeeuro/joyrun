using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayer : MonoBehaviour
{
    RuntimeAnimatorController animIdle;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        GetComponent<Animation>().wrapMode = WrapMode.Loop;
        animIdle = Resources.Load("BasicMotions/AnimationControllers/BasicMotions@Idle") as RuntimeAnimatorController;
    }

    void Update()
    {
        HandleMenuPlayer();
    }

    void HandleMenuPlayer() {
        HandleMenuPlayerPosition();
        HandleMenuPlayerAnimtaion();
    }

    void HandleMenuPlayerPosition() 
    { 
        transform.position = new Vector3(ConstInfo.center, ConstInfo.playerStartPositionY, ConstInfo.playerStartPositionZ);
    }

    void HandleMenuPlayerAnimtaion()
    {
        if (Setting.GetCurrentAnimationState() == AnimationState.animation)
            animator.runtimeAnimatorController = animIdle;
        else if (Setting.GetCurrentAnimationState() == AnimationState.kinect)
            animator.runtimeAnimatorController = null;
    }
}
