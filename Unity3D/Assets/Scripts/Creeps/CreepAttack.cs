using UnityEngine;
using System.Collections;

public class CreepAttack : MonoBehaviour {
    #region Members
    // Area of aggro
    public SphereCollider mAOA;
    CreepScript mCreepScript;
    #endregion

    void Awake () {
        mCreepScript = this.GetComponent<CreepScript> ();
    }

    void Start () {
        mAOA.radius = mCreepScript.mCreepStats.mAttribute.mRange;
    }
}
