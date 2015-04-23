using UnityEngine;
using System.Collections;

public class CreepMovement : MonoBehaviour {
    #region Members
    [HideInInspector]
    public GameObject m_EnemyBasePos;
    public NavMeshAgent m_NavMeshAgent;
    public CreepScript m_CreepScript;
    #endregion

    void Start() {
        m_EnemyBasePos = GameObject.Find(
            gameObject.layer == CreepManager.GetInstance.m_AllyCreepLayer
            ? "EnemyBase" : "AllyBase");

        ChangeTarget(m_EnemyBasePos);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.W)) {
            Debug.Log(string.Format("{0}: {1} remaining.",
                name, m_NavMeshAgent.remainingDistance.ToString()));
        }
    }

    public void ChangeTarget(GameObject target,
        bool targetHasSphereCollider = false) {

        m_NavMeshAgent.stoppingDistance =
            m_CreepScript.ComputeStoppingDistance(targetHasSphereCollider
                ? Utils.GetSphereColliderEnd(target)
                : Utils.GetBoxColliderEnd(target));
        m_NavMeshAgent.SetDestination(target.transform.position);
    }

    public bool TargetIsAtAttackRange() {
        return m_NavMeshAgent.remainingDistance
            <= m_NavMeshAgent.stoppingDistance;
    }
}
