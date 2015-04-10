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

    #region Members
    public ATTACK_TYPES m_AtkType;
    public float m_Speed;
    public float m_ReloadTime;
    public int m_Power;

    #region Slow
    public float m_SlowDuration;
    public float m_SlowIntensity;
    #endregion

    #region AoE
    public int m_SplashReduction;
    public int m_AreaOfEffect;
    #endregion
    #endregion

    public Attack(ATTACK_TYPES atkType, float speed, float reloadTime,
        int power, float slowDuration = 0, float slowIntesity = 0,
        int splashReduction = 0, int aoe = 0) {
        m_AtkType = atkType;
        m_Speed = speed;
        m_ReloadTime = reloadTime;
        m_Power = power;
        m_SlowDuration = slowDuration;
        m_SlowIntensity = slowIntesity;
        m_SplashReduction = splashReduction;
        m_AreaOfEffect = aoe;
    }
}
