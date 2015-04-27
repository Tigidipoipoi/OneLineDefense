using UnityEngine;
using System.Collections;

[System.Serializable]
public class Base {
    #region Members
    public const int c_MaxHP = 100;
    public int m_HP;
    public int m_Defense;
    #endregion

    public Base() {
        m_HP = c_MaxHP;
        m_Defense = 2;
    }
}
