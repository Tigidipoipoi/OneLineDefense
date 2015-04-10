using UnityEngine;
using System.Collections;

public class CreepScript : MonoBehaviour {
    public Creep m_CreepStats;

    void Start() {
        Debug.Log(string.Format(
            "CreepScript::Start => {0} just spawned.",
            name));
    }
}
