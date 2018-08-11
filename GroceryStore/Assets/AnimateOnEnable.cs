using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateOnEnable : MonoBehaviour
{

    [System.Serializable]
    public struct ToAnimate
    {
        public Animator animator;
        public string trigger;
    }

    [SerializeField]
    private ToAnimate[] toAnimate;



    private void OnEnable()
    {
        foreach(ToAnimate thisStruct in toAnimate)
        {
            thisStruct.animator.SetTrigger(thisStruct.trigger);
        }
    }
}
