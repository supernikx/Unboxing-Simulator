using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableController : MonoBehaviour
{
    [Header("Finger for grab")]
    [SerializeField]
    private bool thumb;
    [SerializeField]
    private bool index;
    [SerializeField]
    private bool middle;
    [SerializeField]
    private bool ring;
    [SerializeField]
    private bool pinky;

    private LayerMask grabLayer;
    private LayerMask defaultLayer;
    private Rigidbody rb;
    private bool handIn;
    private GrabberController grabCtrl;
    private IEnumerator followBehaviourRoutine;
    private List<FingerStatus.Finger> fingersToCheck = new List<FingerStatus.Finger>();

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabLayer = LayerMask.NameToLayer("Grabbed");
        defaultLayer = gameObject.layer;

        if (thumb)
            fingersToCheck.Add(FingerStatus.Finger.thumb);
        if (index)
            fingersToCheck.Add(FingerStatus.Finger.index);
        if (middle)
            fingersToCheck.Add(FingerStatus.Finger.middle);
        if (ring)
            fingersToCheck.Add(FingerStatus.Finger.ring);
        if (pinky)
            fingersToCheck.Add(FingerStatus.Finger.pinky);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabber"))
        {
            GrabberController grab = other.GetComponent<GrabberController>();
            if (grab != null)
            {
                grabCtrl = grab;
                if (!grabCtrl.CheckFingers(fingersToCheck))
                {
                    handIn = true;
                }
            }
        }
    }

    private void Update()
    {
        if (handIn)
        {
            if (grabCtrl.CheckFingers(fingersToCheck))
            {
                followBehaviourRoutine = FollowBehaviourCoroutine(grabCtrl);
                StartCoroutine(followBehaviourRoutine);
            }
        }
    }

    private IEnumerator FollowBehaviourCoroutine(GrabberController _grab)
    {
        WaitForEndOfFrame wf = new WaitForEndOfFrame();
        rb.useGravity = false;
        rb.freezeRotation = true;
        gameObject.layer = grabLayer;

        while (_grab != null && _grab.CheckFingers(fingersToCheck))
        {
            Vector3 followDir = (_grab.transform.position - transform.position).normalized;
            float followDistance = Vector3.Distance(transform.position, _grab.transform.position);
            transform.position = followDir * followDistance + transform.position;
            yield return wf;
        }

        rb.freezeRotation = false;
        rb.velocity = Vector3.zero;
        rb.useGravity = true;
        gameObject.layer = defaultLayer;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabber"))
        {
            GrabberController grab = other.GetComponent<GrabberController>();
            if (grab != null)
            {
                handIn = false;
                grabCtrl = null;
            }
        }
    }
}
