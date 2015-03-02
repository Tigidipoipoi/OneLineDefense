using UnityEngine;
using System.Collections;

[System.Serializable]
public class Stats {
    public Attack mAttack;
    public Cost   mCost;
    public int    mHP;
    public float  mSpawnTime;
    public float  mRange;
    public int    mDefense;

    public Stats (int hp, float spawnTime, float range, int defense,
        // Cost
        int gold, int mana,
        // Attack
        Attack.ATTACK_TYPES atkType, float speed, float reloadTime, int power,
        float slowDuration = 0, float slowIntesity = 0, int splashReduction = 0, int aoe = 0) {
        mHP = hp;
        mSpawnTime = spawnTime;
        mRange = range;
        mDefense = defense;

        mCost = new Cost (gold, mana);
        mAttack = new Attack (atkType, speed, reloadTime, power, slowDuration, slowIntesity, splashReduction, aoe);
    }
}
