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
        // Meet enemy's creep
        if (m_CreepScript.GetCurrentState() ==
                CreepScript.CREEP_STATE.AIMING_FOR_ENEMY_BASE
            && other.gameObject.layer == m_CreepScript.m_EnemyCreepLayer) {
            Debug.Log("Target spoted!");
            StartSeekAndDestroy(other.gameObject);
        }
        // Meet enemy's base
        if (other.gameObject == m_CreepMovement.m_EnemyBasePos) {
            StartCoroutine("Destroy", m_CreepMovement.m_EnemyBasePos
                .GetComponent<BaseScript>());
        }
    }

    private IEnumerator Seek(GameObject target) {
        CreepScript targetScript = target.GetComponent<CreepScript>();

        while (targetScript != null) {
            m_CreepMovement.ChangeTarget(target, true);
            bool targetIsAtAttackRange =
                m_CreepMovement.TargetIsAtAttackRange();

            CreepScript.CREEP_STATE currentState = m_CreepScript.GetCurrentState();
            #region Can Destroy ?
            if (currentState != CreepScript.CREEP_STATE.DESTROYING
                    && targetIsAtAttackRange) {
                m_CreepScript.SwitchToDestroyState();

                StartCoroutine("Destroy", targetScript);
            }
            else if (currentState != CreepScript.CREEP_STATE.SEEKING
                && !targetIsAtAttackRange) {
                m_CreepScript.SwitchToSeekState();
                StopCoroutine("Destroy");
            }
            #endregion

            yield return null;
        }

        StopSeekAndDestroy();
    }

    private IEnumerator Destroy(CreepScript targetScript) {
        while (targetScript != null) {
            // Attacking ennemy
            m_CreepScript.Attack(targetScript);
            if (m_CreepScript.m_CreepStats.m_Attribute.m_HP < 0) {
                StopSeekAndDestroy();
            }

            yield return new WaitForSeconds(m_CreepScript.m_CreepStats
                .m_Attribute.m_Attack.m_ReloadTime);
        }
    }

    private IEnumerator Destroy(BaseScript targetScript) {
        while (targetScript != null) {
            // Attacking ennemy
            m_CreepScript.Attack(targetScript);
            if (m_CreepScript.m_CreepStats.m_Attribute.m_HP < 0) {
                break;
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
        m_CreepScript.SwitchToDefaultState();
        m_CreepMovement.ChangeTarget(m_CreepMovement.m_EnemyBasePos);
        StopCoroutine("Seek");
        StopCoroutine("Destroy");
    }
}
