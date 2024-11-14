using UnityEngine;

public class FollowXRRig : MonoBehaviour
{
    public Transform XRRig;

    private void Update()
    {
        if (XRRig != null)
        {
            // Align the position of the square with the XRRig's position
            transform.position = XRRig.position;

            // Align the rotation of the square with the XRRig's rotation
            transform.rotation = XRRig.rotation;

            // Adjust the position to be in front of the XRRig
            Vector3 offset = XRRig.forward * 2.0f; // Adjust this value to position the square at desired distance
            transform.position += offset;
        }
    }
}
