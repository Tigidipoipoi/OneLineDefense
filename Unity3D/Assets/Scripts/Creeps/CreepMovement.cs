using UnityEngine;
using System.Collections;

public class CreepMovement : MonoBehaviour {
    #region Members
    Vector3 mEnemyBasePos;
    NavMeshAgent mNav;
    #endregion

    void Awake () {
        mNav = this.GetComponent<NavMeshAgent> ();
    }

    void Start () {
        mEnemyBasePos = GameObject.Find ("EnemyBase").transform.position;
        mNav.SetDestination (mEnemyBasePos);
    }
}
