using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public class BallDoor : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.Ball))
            {
                other.transform.parent.parent.SendMessage("HitBallDoor", SendMessageOptions.RequireReceiver);
            }
        }
    }
}
