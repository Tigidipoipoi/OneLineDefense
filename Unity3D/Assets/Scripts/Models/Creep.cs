using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Creep {
    public enum CREEP_LIST {
        BASE_MELEE = 0,
        BASE_RANGE,

        COUNT
    }

    #region Members
    public Stats m_Attribute;
    public CREEP_LIST m_Type;
    public string m_Name;
    #endregion

    public Creep(CREEP_LIST type) {
        if (type != CREEP_LIST.COUNT)
            m_Type = type;
        else
            m_Type = CREEP_LIST.BASE_MELEE;
        this.InitCreep();
    }

    public Creep()
        : this(CREEP_LIST.BASE_MELEE) { }

    public void InitCreep() {
        switch (m_Type) {
            case CREEP_LIST.BASE_RANGE:
                m_Attribute = new Stats(
                    hp: 10, spawnTime: 3.0f, range: 3.5f, defense: 1,
                    gold: 15, mana: 0,
                    atkType: Attack.ATTACK_TYPES.RANGE, speed: 1.5f,
                    reloadTime: 1.0f, power: 2);
                break;

            case CREEP_LIST.BASE_MELEE:
            default:
                m_Attribute = new Stats(
                    hp: 15, spawnTime: 2.5f, range: 1.5f, defense: 2,
                    gold: 10, mana: 0,
                    atkType: Attack.ATTACK_TYPES.MELEE, speed: 1.0f,
                    reloadTime: 1.0f, power: 1);
                break;
        }
    }
}
