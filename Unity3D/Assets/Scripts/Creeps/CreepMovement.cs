using UnityEngine;
using System.Collections;

public class CreepMovement : MonoBehaviour {
    #region Members
    GameObject mEnemyBasePos;
    NavMeshAgent mNav;
    float m_OwnColliderEnd;
    #endregion

    void Awake() {
        mNav = this.GetComponent<NavMeshAgent>();
    }

    void Start() {
        mEnemyBasePos = GameObject.Find("EnemyBase");
        mNav.SetDestination(mEnemyBasePos.transform.position);
        m_OwnColliderEnd = Utils.GetSphereColliderEnd(gameObject);

        ComputeStoppingDistance();
    }

    void ComputeStoppingDistance() {
        float targetColliderEnd = Utils.GetBoxColliderEnd(mEnemyBasePos);
        float attackRange = GetComponent<CreepScript>()
            .mCreepStats.mAttribute.mRange;

        mNav.stoppingDistance = m_OwnColliderEnd + attackRange + targetColliderEnd;
    }
}
