using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace m56
{
    public class BowlerController : MonoBehaviour
    {
        //public static Action<float> OnBowlerRunupStarts;
        [SerializeField]
        private Animator animBowler;
        [SerializeField]
        private AnimatorOverrideController animOverride;
        [SerializeField]
        private GameObject bowlerBody;
        [SerializeField]
        private Transform bowlerParent, bowlerHip;

        private Vector3 bowlerStartPos = new Vector3(1.43f, 0, 11.26f);
        private int runupHash = Animator.StringToHash("Runup");
        private int triggerReaction = Animator.StringToHash("reaction");
        private bool isBowlerRunning;

        private List<int> releaseIdList = new List<int> { 0, 1, 2, 3, 4, 5, 1, 2, 1, 6, 5, 9, 1, 2, 1, 2, 4, 5, 6, 6, 4, 5, 5, 4, 0, 1, 2, 2, 1 };

       private Coroutine bowlerRunUpStartOnIdleAnimComplete = null;

        [Header("Animations Scriptables")]
        [SerializeField]
        private BowlerAnimHolder bowlerAnimHolderForBowler;
        [SerializeField]
        private BowlerAnimHolder bowlerAnimHolderForBattingClient;


        #region PublicMethods

        /// <summary>
        /// THis is called when state get changed to OnUpdatingScores
        /// </summary>
        public void ResetToRunupPosition()
        {
            animBowler.ResetTrigger(runupHash);
            StopBowlerRunUpCoroutine();
            AssignRandomIdleAnimation();
            //RepositionBowler(AllCoordinates.releasePointsDictionary[Config.BOWLER_RELEASE_POINT].GetReleasePoint() - GetRpOffset());
            isBowlerRunning = false;
            animBowler.SetTrigger("Stance");
        }


        public void StartBowling()
        {
            ResetToRunupPosition();
            StartRunup("");
            Debug.Log("OnboardingBowlerController, IsSpinner: " + Config.IS_BOWLER_SPINNER);
        }

        public void SetDefaultPos()
        {
            AssignIdleAnimationOnDeliveryBeingSelected();

            if (Config.IS_BOWLER_SPINNER == 1)
                RepositionBowler(BowlerAnimData.bowlerDefaultPosForSpinner);
            else
                RepositionBowler(BowlerAnimData.bowlerDefaultPos);
        }


        public void AssignIdleAnimationOnDeliveryBeingSelected()
        {
            animOverride[BowlerAnimData.STATE_STANCE] = GetBowlerAnimationIdle();
            animBowler.SetTrigger("Stance");
        }

        #endregion


        #region PrivateMethods


        private void StartRunup(string deliveryKey)
        {
            StopBowlerRunUpCoroutine();
            if (!isBowlerRunning)
            {
                AssignRunupAnimation();
                SetSpeedOfBowlerAnimation();
                RunupDelayed();
            }
        }

        /// <summary>
        /// To be called at the start, to reposition bowler on either OTW or RTW side
        /// </summary>
        void RepositionBowler(Vector3 startPos)
        {
            bowlerParent.position = new Vector3(startPos.x, bowlerParent.position.y, startPos.z); //Pivot is aligned with the release point
        }

        /// <summary>
        /// Sets the speed of bowler animation. Can be used to make the delivery look easier
        /// TODO - This method needs to take into account the delay in delivery of events
        /// </summary>
        void SetSpeedOfBowlerAnimation()
        {
            animBowler.speed = 1.0f * BowlerAnimData.speedFactor;
        }
      

        /// <summary>
        /// This is currently being used to delay the runup by DeftouchConfig.BOWLING_CLIENT_RUNUP_DELAY on the Bowling Client in case of real player 
        /// </summary>
        void RunupDelayed()
        {
            RepositionBowler(BowlerAnimData.bowlerStartPos);
            animBowler.SetTrigger(runupHash);

           isBowlerRunning = true;

            //if (OnBowlerRunupStarts != null)
            //{
            //    OnBowlerRunupStarts(bowlerTimingIndicatorColorTimer[0] + bowlerTimingIndicatorColorTimer[1] + bowlerTimingIndicatorColorTimer[2]);
            //}
        }
        private void AssignRandomIdleAnimation()
        {
            animOverride[BowlerAnimData.STATE_STANCE] = GetRandleBowlerAnimationIdle();
        }

        void AssignRunupAnimation()
        {
            animOverride[BowlerAnimData.STATE_RUNUP] = GetBowlerAnimForBowler(GetBowlerIndexInBowlerAnimHolder(), releaseIdList[0]);
        }

        private void StartBowlerRunUp()
        {
            StopBowlerRunUpCoroutine();
            bowlerRunUpStartOnIdleAnimComplete = StartCoroutine(StartBowlerRunUpCoroutine(GetBowlerAnimationIdle().length * animBowler.speed));
        }

        IEnumerator StartBowlerRunUpCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            StartRunup("");
        }

        private void StopBowlerRunUpCoroutine()
        {
            if (bowlerRunUpStartOnIdleAnimComplete != null)
                StopCoroutine(bowlerRunUpStartOnIdleAnimComplete);
        }

        /// <summary>
        ///  Get Index of the Character to Use Animations based on the Character Id.
        /// </summary>
        /// <returns></returns>
        private int GetBowlerIndexInBowlerAnimHolder()
        {
            return Config.IS_BOWLER_SPINNER;
        }


        #region Bowlers Method

        private AnimationClip GetBowlerAnimForBowler(int animSet, int animIndex)
        {
            return bowlerAnimHolderForBowler.GetBowlerAnim(animSet, animIndex);
        }
        private AnimationClip GetBowlerAnimationIdle()
        {
            return bowlerAnimHolderForBowler.GetIdleAnim();
        }
        private AnimationClip GetRandleBowlerAnimationIdle()
        {
            return bowlerAnimHolderForBowler.GetRandomAnimIdleClip();
        }

        #endregion

        #endregion

    }

}
