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
    public const float ballPitchTimeForSpinner = 0.8f;
    public const float ballEndTimeForSpinner = 1;
    public const float deviationFactor = 3;
    public const float deviationConstant = 1.5f;
    public const float minYPos = 0.1f;
    public const float maxYPos = 3f;
    public const float heightConstant = 3;
}
