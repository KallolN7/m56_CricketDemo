using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using System;
using Deftouch;
//using Deftouch.EventSystem;
using System.Threading.Tasks;

namespace Deftouch.Asc.OnBoarding
{
    public class OnboardingBowlerController : MonoBehaviour
    {

        public static Action<Quaternion> OnBowlerRunupEnded;
        public static Action<float> OnBowlerRunupStarts;
        public Animator animBowler;
        public AnimatorOverrideController animOverride;
        public GameObject bowlerBody;
        public Transform bowlerParent, bowlerHip;
        public float speedFactor = 1.5f;
        private const string STATE_STANCE = "1_Ball Juggle";
        private const string STATE_RUNUP = "0_-1_3";


        [SerializeField]
        private Vector3 bowlerStartPos = new Vector3(1.43f, 0, 11.26f); //new Vector3(0.48f, 0, 21.35f);
        [SerializeField]
        private Vector3 bowlerDefaultPos = new Vector3(0.5f, 0, 20);
        [SerializeField]
        private Vector3 bowlerDefaultPosForSpinner = new Vector3(1.73f, 0, 14.95f);

        private Transform rightHand;
        private Transform rightHandWrist; // I am using this for position ball in hand because of new rig there was issues if i use finger.
        public GameObject fakeBowlerIdle;


        [SerializeField]
        private Color[] bowlerTimingIndicatorColors;
        [SerializeField]
        private float[] bowlerTimingIndicatorColorTimer = new float[3];

        private Vector3 rpOffsetSpinRightie = new Vector3(-1.3f, 0, -2.16f);
        private Vector3 rpOffsetSpinLeftie = new Vector3(1.3f, 0, -2.16f);
        private Vector3 rpOffsetFastRightie = new Vector3(0, 0, -8.09f);//new Vector3(0, 0, -7.5f);
        private Vector3 rpOffsetFastLeftie = new Vector3(0, 0, -8.09f);//new Vector3(0, 0, -7.5f);

        private int runupHash = Animator.StringToHash("Runup");
        private int triggerReaction = Animator.StringToHash("reaction");
        private bool isBowlerRunning;
        private Vector3 bowlerfilderDefaultpos;

        private List<int> releaseIdList = new List<int> { 0, 1, 2, 3, 4, 5, 1, 2, 1, 6, 5, 9, 1, 2, 1, 2, 4, 5, 6, 6, 4, 5, 5, 4, 0, 1, 2, 2, 1 };

        private float animSpeed;
        private bool canReleaseBall = false;

        private int totalFrameNumber = 151;
        private int releaseFrameNumber = 90;
        private float runupStartFrameOnInputRecieved = 60f;//67.5f;
        private int totalFramesAfterBallReleaseEvent = 61;

        private Coroutine bowlerRunUpStartOnIdleAnimComplete = null;

        [Header("Animations Scriptables")]
        public BowlerAnimHolder bowlerAnimHolderForBowler;
        public BowlerAnimHolder bowlerAnimHolderForBattingClient;

        #region Unity Methods

        private void OnEnable()
        {

            //DeftEventHandler.AddListener(EventID.ONBOARDING_EVENT_START_NEW_DELIVERY, EventOnReceivedDeliverySelected);
            //OnboardingBall3Controller_C.onForcedOut += OnForcedOutCallback;
            //OnboardingWicketkeeper_C.OnKeeperCollectsBall += OnWicketkeeperCollectsBall;
            //DeftEventHandler.AddListener(EventID.ONBOARDING_EVENT_BOWLER_ANIMATION_AT_RELEASE_FRAME, EventBowlerAnimationAtReleaseFrame);
            //DeftEventHandler.AddListener(EventID.ONBOARDING_EVENT_BALL2_ANIMATION_AT_STUMPS_FRAME, EventBall2AnimationAtStumpsFrame);


        }
        private void OnDisable()
        {
            //DeftEventHandler.RemoveListener(EventID.ONBOARDING_EVENT_START_NEW_DELIVERY, EventOnReceivedDeliverySelected);
            //OnboardingBall3Controller_C.onForcedOut -= OnForcedOutCallback;
            //OnboardingWicketkeeper_C.OnKeeperCollectsBall -= OnWicketkeeperCollectsBall;
            //DeftEventHandler.RemoveListener(EventID.ONBOARDING_EVENT_BOWLER_ANIMATION_AT_RELEASE_FRAME, EventBowlerAnimationAtReleaseFrame);
            //DeftEventHandler.RemoveListener(EventID.ONBOARDING_EVENT_BALL2_ANIMATION_AT_STUMPS_FRAME, EventBall2AnimationAtStumpsFrame);
        }


        #endregion


        #region PublicMethods
        public void Init()
        {
            rightHand = this.transform.Find("Player_IK_Bowler/Hip_Root/Spine1/Spine2/Clavical_R/Shoulder_JNT_IK_R/Elbow_JNT_IK_R/Wrist_JNT_IK_R/R_Index_Finger1/R_Index_Finger2/R_Index_Finger3");
            rightHandWrist = this.transform.Find("Player_IK_Bowler/Hip_Root/Spine1/Spine2/Clavical_R/Shoulder_JNT_IK_R/Elbow_JNT_IK_R/Wrist_JNT_IK_R");

            // ResetToRunupPosition();
            bowlerfilderDefaultpos = new Vector3(bowlerParent.transform.position.x, bowlerParent.transform.position.y, 11.9f);

        }

        /// <summary>
        /// THis is called when state get changed to OnUpdatingScores
        /// </summary>
        public void ResetToRunupPosition()
        {
            animBowler.ResetTrigger(runupHash);
            StopBowlerRunUpCoroutine();
            AssignRandomIdleAnimation();
            RepositionBowler(AllCoordinates.releasePointsDictionary[Config.BOWLER_RELEASE_POINT].GetReleasePoint() - GetRpOffset());
            ShowBowlerHideFielder(true);
            isBowlerRunning = false;
            animBowler.SetTrigger("Stance");
        }

        public void SwitchBowlerType()
        {

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
                RepositionBowler(bowlerDefaultPosForSpinner);
            else
                RepositionBowler(bowlerDefaultPos);
        }

        public void SetAnimSpeedOnPause(float animSpeed)
        {
            this.animSpeed = animSpeed;
        }
        public bool CanReleaseBall()
        {
            return canReleaseBall;
        }

        public void ToggleCanReleaseBall(bool flag)
        {
            canReleaseBall = flag;
        }
        #region AnimEvents
        public void RunupSound()
        {

        }
        public void BallRelease()
        {
            
        }
        public void RunupEnd()
        {
            ShowBowlerHideFielder(false);
        }


        public void AssignIdleAnimationOnDeliveryBeingSelected()
        {
            animOverride[STATE_STANCE] = GetBowlerAnimationIdle();
            animBowler.SetTrigger("Stance");
        }

        #endregion


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
        void UpdateIndicatorTimings()
        {
            float t = GetRunupDuration();
            for (int i = 0; i < Config.SPEED_FACTORS_SLABS.Length; i++)
            {
                if (i == 0)
                {
                    bowlerTimingIndicatorColorTimer[i] = Config.SPEED_FACTORS_SLABS[i] * t;
                }
                else
                {
                    bowlerTimingIndicatorColorTimer[i] = (Config.SPEED_FACTORS_SLABS[i] - Config.SPEED_FACTORS_SLABS[i - 1]) * t;
                }
            }
        }
        //void RepositionBowler()
        //{
        //    Vector3 bowlerPos = AllCoordinates.releasePointsDictionary[OnboardingTeams.instance.GetReleasePoint()].GetReleasePoint();
        //    bowlerParent.position = new Vector3(bowlerPos.x, bowlerParent.position.y, bowlerPos.z); //Pivot is aligned with the release point
        //}
        /// <summary>
        /// To be called at the start, to reposition bowler on either OTW or RTW side
        /// </summary>
        void RepositionBowler(Vector3 startPos)
        {
            bowlerParent.position = new Vector3(startPos.x, bowlerParent.position.y, startPos.z); //Pivot is aligned with the release point
        }

        private Vector3 GetRpOffset()
        {
            if (Config.IS_RIGHTY_BOWLER)
            {
                if (Config.IS_BOWLER_SPINNER == 1)
                {
                    return rpOffsetSpinRightie;
                }
                //Fast bowler
                return rpOffsetFastRightie;
            }
            //Leftie
            if (Config.IS_BOWLER_SPINNER == 1)
            {
                return rpOffsetSpinLeftie;
            }
            //Fast bowler
            return rpOffsetFastLeftie;
        }
        private void SetBallInBowlersHand()
        {
            //Check if Bowler is right handed or left
            //ball.transform.localScale = new Vector3(5.2f, 5.2f, 5.2f);
            //ball.transform.SetParent(rightHandWrist);
            //ball.transform.localPosition = new Vector3(0.11f, 0.044f, -0.007f);//new Vector3(0, -0.1f, 0.06f);//Old position:new Vector3(0, -0.11f, 0.08f) when scale was 6.799999
        }
        private void UnhideBall(bool status)
        {
            //ball.SetActive(status);
        }
        /// <summary>
        /// Sets the speed of bowler animation. Can be used to make the delivery look easier
        /// TODO - This method needs to take into account the delay in delivery of events
        /// </summary>
        void SetSpeedOfBowlerAnimation()
        {
            ////anim.ForceStateNormalizedTime(0.4f); // Playing the clip from in between
            //if (OnboardingScene_M.instance.IsPlayerBatting())
            //{
            //    animBowler.speed = 1f;
            //}
            //else
                animBowler.speed = 1.0f * speedFactor;
        }
        void ShowBowlerHideFielder(bool status)
        {
            if (status)
            {
                bowlerfilderDefaultpos = new Vector3(bowlerBody.transform.position.x, bowlerBody.transform.position.y, 11.9f);
            }
            else
            {


                if (OnBowlerRunupEnded != null)
                    OnBowlerRunupEnded(bowlerParent.transform.rotation);
            }
        }

        /// <summary>
        /// This is currently being used to delay the runup by DeftouchConfig.BOWLING_CLIENT_RUNUP_DELAY on the Bowling Client in case of real player 
        /// </summary>
        void RunupDelayed()
        {
            RepositionBowler(bowlerStartPos);
            animBowler.SetTrigger(runupHash);
            StartIndicatorAnimation();

            isBowlerRunning = true;
            //            GameTimer_M.instance.StartNewCountdown(GetTimeTillFinalDeliveryInput());
            //if (bowling_M.isPlayerBowling)
            //{
            //    StartIndicatorAnimation();
            //}
            if (OnBowlerRunupStarts != null)
            {
                OnBowlerRunupStarts(bowlerTimingIndicatorColorTimer[0] + bowlerTimingIndicatorColorTimer[1] + bowlerTimingIndicatorColorTimer[2]);
            }
            //inningsManager_M.PlaySound("OnBowlingStart");
            //            DeftSoundManager.instance.PlaySound(DeftSoundManager.SoundType.ON_BOWLING_START);
        }
        private void AssignRandomIdleAnimation()
        {
            //animOverride[STATE_STANCE] = bowlerIdleAnimClips[UnityEngine.Random.Range(0, bowlerIdleAnimClips.Length)];
            //animOverride[STATE_STANCE] = bowlerAnimationsHolder.bowlerIdleAnimations[UnityEngine.Random.Range(0, bowlerAnimationsHolder.bowlerIdleAnimations.Length)];

            animOverride[STATE_STANCE] = GetRandleBowlerAnimationIdle();
        }

        void AssignRunupAnimation()
        {
            animOverride[STATE_RUNUP] = GetBowlerAnimForBowler(GetBowlerIndexInBowlerAnimHolder(), releaseIdList[0]);
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

        //Starts the PlayerIndicator Color Animation On Bowler Client
        private void StartIndicatorAnimation()
        {
            //UpdateIndicatorTimings();
            //playerIndicator.StartIndicatorAnimation(bowlerTimingIndicatorColorTimer.ToList(), bowlerTimingIndicatorColors.ToList());
        }
        private float GetRunupDuration()
        {
            RuntimeAnimatorController ac = animBowler.runtimeAnimatorController;
            for (int i = 0; i < ac.animationClips.Length; i++)              
            {
                if (ac.animationClips[i].name == GetBowlerAnimForBowler(GetBowlerIndexInBowlerAnimHolder(), releaseIdList[0]).name) 
                {
                    return ac.animationClips[i].length / speedFactor;
                }
                //if (OnboardingScene_M.instance.IsPlayerBatting())
                //{
                //    if (ac.animationClips[i].name == GetBowlerAnimForBattingClient(GetBowlerIndexInBowlerAnimHolder(), releaseIdList[0]).name) 
                //    {
                //        return ac.animationClips[i].length / speedFactor;
                //    }
                //}
                //else
                //{
                //    if (ac.animationClips[i].name == GetBowlerAnimForBowler(GetBowlerIndexInBowlerAnimHolder(), releaseIdList[0]).name) 
                //    {
                //        return ac.animationClips[i].length / speedFactor;
                //    }
                //}
            }

            //This shoud ideal never get executed
            Debug.LogError("MAJOR ISSUE, BOWLER CONTROLLER, INVESTIGATE");
            Debug.Break();
            return (5.33f / speedFactor);
        }


        #region Bowlers Method
        public AnimationClip GetBowlerAnimForBattingClient(int animSet, int animIndex)
        {
            return bowlerAnimHolderForBattingClient.GetBowlerAnim(animSet, animIndex);
        }
        public AnimationClip GetBowlerAnimForBowler(int animSet, int animIndex)
        {
            return bowlerAnimHolderForBowler.GetBowlerAnim(animSet, animIndex);
        }
        public AnimationClip GetBowlerAnimationIdle()
        {
            return bowlerAnimHolderForBowler.GetIdleAnim();
        }
        public AnimationClip GetRandleBowlerAnimationIdle()
        {
            return bowlerAnimHolderForBowler.GetRandomAnimIdleClip();
        }

        #endregion

        #endregion

        #region GameEventListeners

        private void EventBowlerAnimationAtReleaseFrame(object arg)
        {
            UnhideBall(false);
            //bowling_M.BallReleasedFromBowlerHand(rightHand.localPosition);
        }

        private void EventBall2AnimationAtStumpsFrame(object arg)
        {
            if (Config.IS_BOWLER_SPINNER != 1)
                bowlerParent.transform.localPosition = new Vector3(bowlerHip.position.x, bowlerParent.transform.position.y, bowlerHip.position.z); //  bowlerHip.position;
            animBowler.speed = 1f;
            //animOverride[STATE_REACTION] = GetAnimationClipForBowlerCelebration();//bowlerParent.GetComponent<OnboardingFieldingAnimation>().fielderAnimHolder.bowlerCelebration;
            animBowler.SetTrigger(triggerReaction);
        }
        #endregion
    }
}