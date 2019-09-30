using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endAnimation : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        Debug.Log("animaatio loppui");

        FindObjectOfType<GameController>().GetComponent<GameController>().disableAnimator();
    }
}
