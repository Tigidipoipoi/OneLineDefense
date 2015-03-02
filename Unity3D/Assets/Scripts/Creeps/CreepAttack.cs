using UnityEngine;
using System.Collections;

public class CreepAttack : MonoBehaviour {
    #region Members
    // Area of aggro
    public SphereCollider mAOA;
    public CreepScript mCreepScript;
    int mAllyCreepMask;
    int mEnemyCreepMask;
    #endregion

    void Start () {
        mAOA.radius = mCreepScript.mCreepStats.mAttribute.mRange;

        if (this.gameObject.layer == CreepManager.instance.mAllyCreepMask) {
            mAllyCreepMask = CreepManager.instance.mAllyCreepMask;
            mEnemyCreepMask = CreepManager.instance.mEnemyCreepMask;
        }
        else {
            mAllyCreepMask = CreepManager.instance.mEnemyCreepMask;
            mEnemyCreepMask = CreepManager.instance.mAllyCreepMask;
        }
    }

    void OnTriggerEnter (Collider other) {
        if (other.gameObject.layer == mEnemyCreepMask) {
            Debug.Log ("Target spoted!");
        }
    }
}
