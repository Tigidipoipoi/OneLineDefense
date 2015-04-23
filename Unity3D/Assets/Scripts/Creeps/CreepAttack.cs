using UnityEngine;
using System.Collections;

public class CreepAttack : MonoBehaviour {
    #region Members
    // Area of aggro
    public SphereCollider m_AOA;
    public CreepScript m_CreepScript;
    public CreepMovement m_CreepMovement;
    #endregion

    public void Start() {
        m_AOA.radius = m_CreepScript.m_CreepStats.m_Attribute.m_Range;
    }

    public void OnTriggerEnter(Collider other) {
        if (m_CreepScript.GetCurrentState() ==
                CreepScript.CREEP_STATE.AIMING_FOR_ENEMY_BASE
            && other.gameObject.layer == m_CreepScript.m_EnemyCreepLayer) {
            Debug.Log("Target spoted!");
            StartSeekAndDestroy(other.gameObject);
        }
    }

    private IEnumerator Seek(GameObject target) {
        while (true) {
            if (target == null) {
                StopSeekAndDestroy();
                break;
            }

            m_CreepMovement.ChangeTarget(target, true);
            bool targetIsAtAttackRange =
                m_CreepMovement.TargetIsAtAttackRange();

            #region Can Destroy ?
            if (m_CreepScript.GetCurrentState() !=
                        CreepScript.CREEP_STATE.DESTROYING
                    && targetIsAtAttackRange) {
                m_CreepScript.SwitchToDestroyState();

                StartCoroutine("Destroy", target.GetComponent<CreepScript>());
            }
            else if (m_CreepScript.GetCurrentState() !=
                    CreepScript.CREEP_STATE.SEEKING
                && !targetIsAtAttackRange) {
                m_CreepScript.SwitchToSeekState();
                StopCoroutine("Destroy");
            }
            #endregion

            yield return null;
        }
    }

    private IEnumerator Destroy(CreepScript targetScript) {
        while (true) {
            // Attacking ennemy
            m_CreepScript.Attack(targetScript);
            if (m_CreepScript.m_CreepStats.m_Attribute.m_HP < 0) {
                StopSeekAndDestroy();
            }

            yield return new WaitForSeconds(m_CreepScript.m_CreepStats
                .m_Attribute.m_Attack.m_ReloadTime);
        }
    }

    public void StartSeekAndDestroy(GameObject target) {
        if (m_CreepScript.GetCurrentState() !=
                CreepScript.CREEP_STATE.AIMING_FOR_ENEMY_BASE)
            return;

        m_CreepScript.SwitchToSeekNDestroyStateLoop();
        StartCoroutine("Seek", target);
    }

    public void StopSeekAndDestroy() {
        if (m_CreepScript.GetCurrentState() ==
                CreepScript.CREEP_STATE.AIMING_FOR_ENEMY_BASE)
            return;

        m_CreepScript.SwitchToDefaultState();
        m_CreepMovement.ChangeTarget(m_CreepMovement.m_EnemyBasePos);
        StopCoroutine("Seek");
        StopCoroutine("Destroy");
    }
}
