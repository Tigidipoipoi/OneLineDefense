using UnityEngine;
using System.Collections;

[System.Serializable]
public class Spell {
    public enum SPELL_TYPES {
        FIRE = 0,
        HEAL,

        COUNT
    }

    protected SPELL_TYPES   mSpellType;
    protected Attack        mAttackData;
    protected Cost          mCost;

    /*
     *  protected Cost mCost;
     *  protected int mPower;
     *  protected float mReloadSpeed;
     * */
}
