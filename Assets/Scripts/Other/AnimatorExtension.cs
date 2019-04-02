using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorExtension
{

    public static bool CheckCurState (this Animator anim, string stateName, int layerIndex)
    {
        return anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
    }

    public static bool CheckCurTransition(this Animator anim, string stateName, int layerIndex)
    {
        return anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
    }

}
