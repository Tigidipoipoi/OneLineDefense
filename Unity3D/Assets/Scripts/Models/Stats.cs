using UnityEngine;
using System.Collections;

[System.Serializable]
public class Stats {
    #region Members
    public Attack m_Attack;
    public Cost m_Cost;
    public int m_HP;
    public float m_SpawnTime;
    public float m_Range;
    public int m_Defense;
    #endregion

    public Stats(int hp, float spawnTime, float range, int defense,
        // Cost
        int gold, int mana,
        // Attack
        Attack.ATTACK_TYPES atkType, float speed, float reloadTime, int power,
        // Slow
        float slowDuration = 0, float slowIntesity = 0,
        // Splash
        int splashReduction = 0, int aoe = 0) {
        m_HP = hp;
        m_SpawnTime = spawnTime;
        m_Range = range;
        m_Defense = defense;

        m_Cost = new Cost(gold, mana);
        m_Attack = new Attack(atkType, speed, reloadTime,
            power, slowDuration, slowIntesity, splashReduction, aoe);
    }
}
