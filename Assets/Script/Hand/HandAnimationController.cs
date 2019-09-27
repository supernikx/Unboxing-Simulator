using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class HandAnimationController : MonoBehaviour
{
    private Animator anim = null;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void UpdateAnim(FingerStatus _fingerStatus)
    {
        anim.SetBool("ThumbDown", _fingerStatus.GetFingerStatus(FingerStatus.Finger.thumb));
        anim.SetBool("IndexDown", _fingerStatus.GetFingerStatus(FingerStatus.Finger.index));
        anim.SetBool("MiddleDown", _fingerStatus.GetFingerStatus(FingerStatus.Finger.middle));
        anim.SetBool("RingDown", _fingerStatus.GetFingerStatus(FingerStatus.Finger.ring));
        anim.SetBool("PinkyDown", _fingerStatus.GetFingerStatus(FingerStatus.Finger.pinky));
    }
}
