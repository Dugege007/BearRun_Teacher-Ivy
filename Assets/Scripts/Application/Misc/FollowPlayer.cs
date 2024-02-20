using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public class FollowPlayer : MonoBehaviour
    {
        private Transform mPlayer;
        private Vector3 mOffset;
        private float mSpeed = 20f;

        private void Awake()
        {
            mPlayer = GameObject.FindWithTag(Tags.Player).transform;
            mOffset = transform.position - mPlayer.position;
        }

        private void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, mOffset + mPlayer.position, mSpeed * Time.deltaTime);
        }
    }
}
