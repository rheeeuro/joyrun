using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayer : MonoBehaviour
{
    public static MenuPlayer instance;
    RuntimeAnimatorController animIdle;
    Animator animator;

    void Awake() { instance = this; }

    void Start()
    {
        animator = GetComponent<Animator>();
        GetComponent<Animation>().wrapMode = WrapMode.Loop;
        animIdle = Resources.Load("3DResources/AnimationControllers/JoyRunIdle") as RuntimeAnimatorController;
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
        transform.position = new Vector3(ConstInfo.center, ConstInfo.playerInitialPositionY, ConstInfo.playerInitialPositionZ);
    }

    void HandleMenuPlayerAnimtaion()
    {
        if (Setting.GetCurrentAnimationState() == AnimationState.animation)
            animator.runtimeAnimatorController = animIdle;
        else if (Setting.GetCurrentAnimationState() == AnimationState.kinect)
            animator.runtimeAnimatorController = null;
    }
}
