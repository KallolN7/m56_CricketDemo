using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace m56
{
    public class StumpsController : MonoBehaviour
    {
        [SerializeField]
        private Animator anim;
        [SerializeField]
        private GenericAnimHolder stumpsAnimHolder;

        private AnimatorOverrideController animOverrideController;

        #region Public Methods
        public void Init()
        {
            animOverrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
            anim.runtimeAnimatorController = animOverrideController;
        }

        public void ResetStumps()
        {
            anim.SetTrigger(StumpsData.hashIdle);
        }

        public void PlayBowledAnimation(int index)
        {
            animOverrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
            anim.runtimeAnimatorController = animOverrideController;
            //if index is -1 that mean player is not out. Added this check because the stumps collider is big and may be get the callback as ballhit stumps
            if (index != -1)
            {
                animOverrideController[StumpsData.STATE_STUMP_DISLODGE] = stumpsAnimHolder.clips[index];
                anim.SetTrigger(StumpsData.hashBowled);
            }
        }
        #endregion

        #region Private Methods

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Ball")
            {
                SetWicketAnimation(other.transform.localPosition.x);
                Config.didHitWicket = true;
                ScoreboardController.instance.AnimateBowledText();
            }
        }

        private void SetWicketAnimation(float posX)
        {
            if (posX <= StumpsData.ballMaxPosXForOffStump && posX >= StumpsData.ballMinPosXForOffStump)
                PlayBowledAnimation(0); //For Off Stump
            else if (posX <= StumpsData.ballMaxPosXForLegStump && posX >= StumpsData.ballMinPosXForLegStump)
                PlayBowledAnimation(2); //For Leg Stump
            else
                PlayBowledAnimation(1); //For Middle Stump
        }
        #endregion
    }
}

