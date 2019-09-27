using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class HandInputController : MonoBehaviour
{
    [SerializeField]
    private KeyCode thumbKey;
    [SerializeField]
    private KeyCode indexKey;
    [SerializeField]
    private KeyCode middleKey;
    [SerializeField]
    private KeyCode ringKey;
    [SerializeField]
    private KeyCode pinkyKey;

    private HandAnimationController animCtrl;
    private FingerStatus fingerStatus;

    private void Awake()
    {
        animCtrl = GetComponentInChildren<HandAnimationController>();
        fingerStatus = new FingerStatus();
    }

    private void Update()
    {
        CheckFingers();
        animCtrl.UpdateAnim(fingerStatus);
    }

    private void CheckFingers()
    {
        fingerStatus.SetFingerStatus(FingerStatus.Finger.thumb, Input.GetKey(thumbKey));
        fingerStatus.SetFingerStatus(FingerStatus.Finger.index, Input.GetKey(indexKey));
        fingerStatus.SetFingerStatus(FingerStatus.Finger.middle, Input.GetKey(middleKey));
        fingerStatus.SetFingerStatus(FingerStatus.Finger.ring, Input.GetKey(ringKey));
        fingerStatus.SetFingerStatus(FingerStatus.Finger.pinky, Input.GetKey(pinkyKey));
    }

    public FingerStatus GetFingerStatus()
    {
        return fingerStatus;
    }
}

public class FingerStatus
{
    public enum Finger
    {
        thumb,
        index,
        middle,
        ring,
        pinky,
    }
    private class Status
    {
        public Finger finger;
        public bool isDown;

        public Status(Finger _finger, bool _isDown)
        {
           finger = _finger;
           isDown = _isDown;
        }

        public void SetIsDown(bool _isDown)
        {
            isDown = _isDown;
        }

        public bool GetIsDown()
        {
            return isDown;
        }
    }
    private List<Status> fingerStatus = new List<Status>();

    public FingerStatus()
    {
        fingerStatus.Add(new Status(Finger.thumb, false));
        fingerStatus.Add(new Status(Finger.index, false));
        fingerStatus.Add(new Status(Finger.middle, false));
        fingerStatus.Add(new Status(Finger.ring, false));
        fingerStatus.Add(new Status(Finger.pinky, false));
    }

    public void SetFingerStatus(Finger _finger, bool _isDown)
    {
        fingerStatus.Where(f => f.finger == _finger).FirstOrDefault().SetIsDown(_isDown);        
    }

    public bool GetFingerStatus(Finger _finger)
    {
        return fingerStatus.Where(f => f.finger == _finger).FirstOrDefault().GetIsDown();
    }
}
