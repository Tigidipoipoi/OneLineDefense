using UnityEngine;
using System.Collections;

[System.Serializable]
public class Spell {
    public enum SPELL_TYPES {
        FIRE = 0,
        HEAL,

        COUNT
    }

    #region Members
    public SPELL_TYPES m_SpellType;
    public Attack m_AttackData;
    public Cost m_Cost;
    #endregion
}
