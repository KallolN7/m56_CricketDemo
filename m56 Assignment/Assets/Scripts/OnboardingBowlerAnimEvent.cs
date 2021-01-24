using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deftouch.Asc.OnBoarding
{
    public class OnboardingBowlerAnimEvent : MonoBehaviour
    {

        public OnboardingBowlerController bc;
        public Animator anim;

        public void RunupSound()
        {
            bc.RunupSound();
        }

        public void BallRelease()
        {
            //bc.BallRelease();
            BallController.instance.DeliverBall();
        }

        public void RunupEnd()
        {
            bc.RunupEnd();
        }

        public void AcceptBatsmanInput()
        {
            //bc.AcceptBatsmanInput();
        }

        public void PauseMethod()
        {
            //if (OnboardingScene_M.instance.IsPlayerBatting())
            //{
            //    DeftouchUtils.Log("PauseMethod");
            //    //bc.SetAnimSpeedOnPause(anim.speed);
            //    anim.speed = 0f;
            //    bc.ToggleCanReleaseBall(true);
            //    //DeftouchNetworkManager.instance?.ResetReconnectingEvent();

            //}
        }
    }
}
