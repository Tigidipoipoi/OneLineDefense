using UnityEngine;
using System.Collections;

public class CreepScript : MonoBehaviour {
    public enum CREEP_STATE {
        AIMING_FOR_ENEMY_BASE = 0,
        SEEKING,
        DESTROYING,

        COUNT
    }

    #region Members
    public Creep m_CreepStats;
    public Animator m_FSM;

    [HideInInspector]
    public int m_AllyCreepLayer;
    [HideInInspector]
    public int m_EnemyCreepLayer;
    [HideInInspector]
    public float m_OwnColliderEnd;
    #endregion

    public void Start() {
        m_OwnColliderEnd = Utils.GetSphereColliderEnd(gameObject);

        if (gameObject.layer == CreepManager.GetInstance.m_AllyCreepLayer) {
            m_AllyCreepLayer = CreepManager.GetInstance.m_AllyCreepLayer;
            m_EnemyCreepLayer = CreepManager.GetInstance.m_EnemyCreepLayer;
        }
        else {
            m_AllyCreepLayer = CreepManager.GetInstance.m_EnemyCreepLayer;
            m_EnemyCreepLayer = CreepManager.GetInstance.m_AllyCreepLayer;
        }
    }

    public float ComputeStoppingDistance(float targetColliderEnd) {
        float attackRange = m_CreepStats.m_Attribute.m_Range;

        return m_OwnColliderEnd + attackRange + targetColliderEnd;
    }

    public CREEP_STATE GetCurrentState() {
        AnimatorStateInfo stateInfo = m_FSM.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Seeking")) {
            return CREEP_STATE.SEEKING;
        }
        else if (stateInfo.IsName("Destroying")) {
            return CREEP_STATE.DESTROYING;
        }
        else {
            return CREEP_STATE.AIMING_FOR_ENEMY_BASE;
        }
    }

    #region Switch State Methods
    public void SwitchToSeekNDestroyStateLoop() {
        m_FSM.SetBool("TargetIsAtRange", false);
        m_FSM.SetTrigger("TargetSpotted");
    }

    public void SwitchToSeekState() {
        m_FSM.SetBool("TargetIsAtRange", false);
    }

    public void SwitchToDestroyState() {
        m_FSM.SetBool("TargetIsAtRange", true);
    }

    public void SwitchToDefaultState() {
        m_FSM.SetTrigger("TargetDied");
    }
    #endregion

    public void Attack(CreepScript target) {
        target.GetDamage(m_CreepStats.m_Attribute.m_Attack.m_Power);
    }

    public void Attack(BaseScript target) {
        target.GetDamage(m_CreepStats.m_Attribute.m_Attack.m_Power);
    }

    public void GetDamage(int dammages) {
        dammages -= m_CreepStats.m_Attribute.m_Defense;
        if (dammages <= 0) {
            return;
        }

        m_CreepStats.m_Attribute.m_HP -= dammages;
        if (m_CreepStats.m_Attribute.m_HP > 0) {
            return;
        }

        Die();
    }

    private void Die() {
        Debug.Log(name + " has just died.");
        
        // Will generate a random error if not previously destroy
        Destroy(GetComponent<NavMeshAgent>());

        Destroy(gameObject);
    }
}
