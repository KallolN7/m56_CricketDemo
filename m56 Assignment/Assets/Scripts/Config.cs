using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Config
{
    public  const  int BOWLER_RELEASE_POINT = 1;
    public static int IS_BOWLER_SPINNER;
    public static float[] SPEED_FACTORS_SLABS = { 0.2f, 0.35f, 0.4f };
    public const bool IS_RIGHTY_BOWLER = true;
    public static int wicketHitCount;
    public static int ballsBowledCount;
    public static bool didHitWicket;
    public static bool canBowl;
    public static int InputIndex;
    public const float shotDelayForPacer = 1;
    public const float shotDelayForSpinner = 1.5f;
}

public static class PitchIndicatorData
{
    public const float pitchIndicatorSpeed = 3;
    public const float xMinClampPos = -915.73f;
    public const float xMaxClampPos = -913.05f;
    public const float zMinClampPos = 398.65f;
    public const float zMaxClampPos = 406.45f;
    public static Vector3 defaultPos = new Vector3(-914.4f, -1880.062f, 402.3f);
}

public static class BallAnimationData
{
    public const float ballPitchTimeForFastBowler = 0.5f;
    public const float ballEndTimeForFastBowler = 0.7f;
    public const float ballPitchTimeForSpinner = 0.9f;
    public const float ballEndTimeForSpinner = 1.1f;
    public const float deviationFactor = 3;
    public const float deviationConstant = 1.5f;
    public const float minYPos = 0.1f;
    public const float maxYPos = 3f;
    public const float heightConstant = 3;
}

public static class BowlerAnimData
{
    public const float speedFactor = 1.5f;
    public const string STATE_STANCE = "1_Ball Juggle";
    public const string STATE_RUNUP = "0_-1_3";
    public static Vector3 bowlerStartPos = new Vector3(0.5f, 0, 13.2f); //new Vector3(1.43f, 0, 11.26f); 
    public static Vector3 bowlerDefaultPos = new Vector3(0.5f, 0, 20);
    public static Vector3 bowlerDefaultPosForSpinner = new Vector3(1.73f, 0, 14.95f);
    public static Vector3 resetPosForPacer = new Vector3(0.5f, 2, 21.4f);
    public static Vector3 resetPosForSpinner = new Vector3(1.8f, 2, 5.4f);
    public static Vector3 runUpDelayPos = new Vector3(0.5f, 0, 13.2f);
}
 
public static class BatsmanAnimData
{
    public const string STATE_SHOT = "1_11_0_1";
    public static int triggerIdle = Animator.StringToHash("Idle");
    public static int triggerShot = Animator.StringToHash("Shot");
}

public static class StumpsData
{
    public static int hashBowled = Animator.StringToHash("Bowled");
    public static int hashIdle = Animator.StringToHash("Idle");
    public const string STATE_STUMP_DISLODGE = "OffStumpRebound";
    public const float ballMaxPosXForOffStump = -0.15f;
    public const float ballMinPosXForOffStump = -0.224f;
    public const float ballMaxPosXForLegStump = -0.55f;
    public const float ballMinPosXForLegStump = -0.452f;
}
