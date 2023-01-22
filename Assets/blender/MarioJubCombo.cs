using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioJubCombo : StateMachineBehaviour          //スマッシュwikiのマリオの弱フレーム参照
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("MarioJub", false);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetBool("MarioJub", true);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("MarioJub", false);
    }
}
