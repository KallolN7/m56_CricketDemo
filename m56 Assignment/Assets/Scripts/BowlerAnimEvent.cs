using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace m56
{
    public class BowlerAnimEvent : MonoBehaviour
    {
        public BowlerController bc;
        public Animator anim;

        public void RunupSound()
        {

        }

        public void BallRelease()
        {
            BallController.instance.DeliverBall();
        }

        public void RunupEnd()
        {

        }

        public void AcceptBatsmanInput()
        {

        }

        public void PauseMethod()
        {

        }
    }
}

