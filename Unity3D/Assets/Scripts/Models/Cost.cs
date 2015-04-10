using UnityEngine;
using System.Collections;

[System.Serializable]
public class Cost {
    #region Members
    public int m_Gold;
    public int m_Mana;
    #endregion

    public Cost(int gold, int mana) {
        m_Gold = gold;
        m_Mana = mana;
    }
}
