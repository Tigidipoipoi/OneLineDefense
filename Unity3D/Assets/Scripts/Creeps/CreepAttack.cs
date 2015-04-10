using UnityEngine;
using System.Collections;

public class CreepAttack : MonoBehaviour {
    #region Members
    // Area of aggro
    public SphereCollider m_AOA;
    public CreepScript m_CreepScript;
    int m_AllyCreepMask;
    int m_EnemyCreepMask;
    #endregion

    void Start() {
        m_AOA.radius = m_CreepScript.m_CreepStats.m_Attribute.m_Range;

        if (this.gameObject.layer == CreepManager.GetInstance.mAllyCreepMask) {
            m_AllyCreepMask = CreepManager.GetInstance.mAllyCreepMask;
            m_EnemyCreepMask = CreepManager.GetInstance.mEnemyCreepMask;
        }
        else {
            m_AllyCreepMask = CreepManager.GetInstance.mEnemyCreepMask;
            m_EnemyCreepMask = CreepManager.GetInstance.mAllyCreepMask;
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == m_EnemyCreepMask) {
            Debug.Log("Target spoted!");
        }
    }
}
