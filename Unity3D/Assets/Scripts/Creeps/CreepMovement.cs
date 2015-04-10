using UnityEngine;
using System.Collections;

public class CreepMovement : MonoBehaviour {
    #region Members
    GameObject m_EnemyBasePos;
    NavMeshAgent m_NavMeshAgent;
    float m_OwnColliderEnd;
    #endregion

    void Awake() {
        m_NavMeshAgent = this.GetComponent<NavMeshAgent>();
    }

    void Start() {
        m_EnemyBasePos = GameObject.Find("EnemyBase");
        m_NavMeshAgent.SetDestination(m_EnemyBasePos.transform.position);
        m_OwnColliderEnd = Utils.GetSphereColliderEnd(gameObject);

        ComputeStoppingDistance();
    }

    void ComputeStoppingDistance() {
        float targetColliderEnd = Utils.GetBoxColliderEnd(m_EnemyBasePos);
        float attackRange = GetComponent<CreepScript>()
            .m_CreepStats.m_Attribute.m_Range;

        m_NavMeshAgent.stoppingDistance = m_OwnColliderEnd
            + attackRange + targetColliderEnd;
    }
}
