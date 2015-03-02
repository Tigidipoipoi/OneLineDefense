using UnityEngine;
using System.Collections;

public class CreepScript : MonoBehaviour {
    public int mCreepType = 0;
    public Creep mCreepStats;

    void Start () {
        switch (mCreepType) {
            case 1:
                mCreepStats = new Creep (Creep.CREEP_LIST.BASE_RANGE);
                break;
            case 0:
            default:
                mCreepStats = new Creep ();
                break;
        }
        Debug.Log ("Grr !");
    }

    // Temp: just for testing
    void Update () {
        // ToDo: Add navMesh handling
    }
}
