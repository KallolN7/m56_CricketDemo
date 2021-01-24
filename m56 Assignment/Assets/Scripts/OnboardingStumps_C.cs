using UnityEngine;

namespace Deftouch.Asc.OnBoarding
{
    public class OnboardingStumps_C : MonoBehaviour
    {
        public Collider collider;
        //Public varibales
        public Animator anim;

        public GenericAnimHolder stumpsAnimHolder;
        private AnimatorOverrideController animOverrideController;
        //public GameObject stumpsCollider;


        //Animations
        private int hashBowled = Animator.StringToHash("Bowled");

        private int hashIdle = Animator.StringToHash("Idle");

        private const string STATE_STUMP_DISLODGE = "OffStumpRebound";

        #region Public Methods
        public void Init()
        {
            animOverrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
            anim.runtimeAnimatorController = animOverrideController;
        }

        public void ResetStumps()
        {
            anim.SetTrigger(hashIdle);
        }

        public void AcativateDeactiveStumpsCollider(bool status)
        {

        }
        public void PlayBowledAnimation(int index)
        {
            animOverrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
            anim.runtimeAnimatorController = animOverrideController;
            //if index is -1 that mean player is not out. Added this check because the stumps collider is big and may be get the callback as ballhit stumps
            if (index != -1)
            {
                animOverrideController[STATE_STUMP_DISLODGE] = stumpsAnimHolder.clips[index];
                anim.SetTrigger(hashBowled);
            }
        }
        #endregion

        #region Private Methods

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Ball")
            {
                SetWicketAnimation(other.transform.localPosition.x);
                Config.didHitWicket = true;
                ScoreboardController.instance.AnimateBowledText();
            }
        }

        private void SetWicketAnimation(float posX)
        {
            if (posX <= -0.15 && posX >= -0.224)
                PlayBowledAnimation(0);
            else if(posX <= -0.55 && posX >= -0.452)
                PlayBowledAnimation(2);
            else
                PlayBowledAnimation(1);
        }
        #endregion
    }
}