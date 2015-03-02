using UnityEngine;
using System.Collections;

[System.Serializable]
public class Attack {
    public enum ATTACK_TYPES {
        MELEE = 0,
        RANGE,
        SPELL,

        COUNT
    }

    public ATTACK_TYPES mAtkType;
    public float        mSpeed;
    public float        mReloadTime;
    public int          mPower;

    #region Slow
    public float mSlowDuration;
    public float mSlowIntensity;
    #endregion

    #region AoE
    public int   mSplashReduction;
    public int  mAreaOfEffect;
    #endregion

    public Attack (ATTACK_TYPES atkType, float speed, float reloadTime, int power,
        float slowDuration = 0, float slowIntesity = 0, int splashReduction = 0, int aoe = 0) {
        mAtkType = atkType;
        mSpeed = speed;
        mReloadTime = reloadTime;
        mPower = power;
        mSlowDuration = slowDuration;
        mSlowIntensity = slowIntesity;
        mSplashReduction = splashReduction;
        mAreaOfEffect = aoe;
    }
}
