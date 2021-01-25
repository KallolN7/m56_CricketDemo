using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace m56
{
    /// <summary>
    /// Controls the bowler animation. Switches bowler type on user input 
    /// </summary>
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
            ///StopBowlerRunUpCoroutine();
            AssignRandomIdleAnimation();
            isBowlerRunning = false;
            animBowler.SetTrigger("Stance");
        }

        /// <summary>
        ///  Starts Bowler animation
        /// </summary>
        public void StartBowling()
        {
            ResetToRunupPosition();
            StartRunup("");
            Debug.Log("BowlerController, IsSpinner: " + Config.IS_BOWLER_SPINNER);
        }

        /// <summary>
        /// Sets the Bowler's current position to it's default position 
        /// </summary>
        public void SetDefaultPos()
        {
            AssignIdleAnimationOnDeliveryBeingSelected();

            if (Config.IS_BOWLER_SPINNER == 1)
                RepositionBowler(BowlerAnimData.bowlerDefaultPosForSpinner);
            else
                RepositionBowler(BowlerAnimData.bowlerDefaultPos);
        }

        /// <summary>
        ///  Sets Idle animation to Bowler animator 
        /// </summary>
        public void AssignIdleAnimationOnDeliveryBeingSelected()
        {
            animOverride[BowlerAnimData.STATE_STANCE] = GetBowlerAnimationIdle();
            animBowler.SetTrigger("Stance");
        }

        #endregion

        
        #region PrivateMethods

        /// <summary>
        ///  Starts Bowler runup animation
        /// </summary>
        /// <param name="deliveryKey"></param>
        private void StartRunup(string deliveryKey)
        {
          //StopBowlerRunUpCoroutine();
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
        /// </summary>
        void SetSpeedOfBowlerAnimation()
        {
            animBowler.speed = 1.0f * BowlerAnimData.speedFactor;
        }
      

        /// <summary>
        /// This is currently being used to delay the runup 
        /// </summary>
        void RunupDelayed()
        {
            RepositionBowler(BowlerAnimData.bowlerStartPos);
            animBowler.SetTrigger(runupHash);

           isBowlerRunning = true;
        }

        /// <summary>
        /// Assigns random Idle Animation clip to animator
        /// </summary>
        private void AssignRandomIdleAnimation()
        {
            animOverride[BowlerAnimData.STATE_STANCE] = GetRandleBowlerAnimationIdle();
        }

        /// <summary>
        /// Assigns proper Bowler delivery animation
        /// </summary>
        void AssignRunupAnimation()
        {
            animOverride[BowlerAnimData.STATE_RUNUP] = GetBowlerAnimForBowler(GetBowlerIndexInBowlerAnimHolder(), releaseIdList[0]);
        }



        ////IEnumerator StartBowlerRunUpCoroutine(float time)
        ////{
        ////    yield return new WaitForSeconds(time);
        ////    StartRunup("");
        ////}

        ////private void StopBowlerRunUpCoroutine()
        ////{
        ////    if (bowlerRunUpStartOnIdleAnimComplete != null)
        ////        StopCoroutine(bowlerRunUpStartOnIdleAnimComplete);
        ////}

        /// <summary>
        ///  Get Index of the Character to Use Animations based on the Character Id.
        /// </summary>
        /// <returns></returns>
        private int GetBowlerIndexInBowlerAnimHolder()
        {
            return Config.IS_BOWLER_SPINNER;
        }


        #region Bowlers Method

        /// <summary>
        /// Get Bowler animation clip from animHolder scriptable object based on animSet and animIndex
        /// </summary>
        /// <param name="animSet"></param>
        /// <param name="animIndex"></param>
        /// <returns></returns>
        private AnimationClip GetBowlerAnimForBowler(int animSet, int animIndex)
        {
            return bowlerAnimHolderForBowler.GetBowlerAnim(animSet, animIndex);
        }

        /// <summary>
        /// Get Bowler Idle Animatio clip
        /// </summary>
        /// <returns></returns>
        private AnimationClip GetBowlerAnimationIdle()
        {
            return bowlerAnimHolderForBowler.GetIdleAnim();
        }

        /// <summary>
        /// Get random bowler idle animation clip
        /// </summary>
        /// <returns></returns>
        private AnimationClip GetRandleBowlerAnimationIdle()
        {
            return bowlerAnimHolderForBowler.GetRandomAnimIdleClip();
        }

        #endregion

        #endregion

    }

}
