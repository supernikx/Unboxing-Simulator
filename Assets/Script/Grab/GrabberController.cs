using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberController : MonoBehaviour
{
    private HandInputController handInputCtrl;

    public void Awake()
    {
        handInputCtrl = GetComponentInParent<HandInputController>();
    }

    public bool CheckFingers(List<FingerStatus.Finger> _fingers)
    {
        FingerStatus fingerStatus = handInputCtrl.GetFingerStatus();
        for (int i = 0; i < _fingers.Count; i++)
        {
            if (fingerStatus.GetFingerStatus(_fingers[i]))
                continue;

            return false;
        }

        return true;
    }
}
