using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    public class PlayerMove : View
    {
        public float Speed = 10f;
        private CharacterController mCC;

        public override string Name => Consts.V_PlayerMove;

        public override void HandleEvent(string eventName, object data)
        {
            
        }

        private void Awake()
        {
            mCC = GetComponent<CharacterController>();
        }

        private void Update()
        {
            mCC.Move(transform.forward * Speed * Time.deltaTime);
        }
    }
}
