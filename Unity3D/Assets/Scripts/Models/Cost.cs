using UnityEngine;
using System.Collections;

[System.Serializable]
public class Cost {
    public int mGold;
    public int mMana;

    public Cost (int gold, int mana) {
        mGold = gold;
        mMana = mana;
    }
}
