using UnityEngine;
using System.Collections;

public class BattleSceneStartScript : MonoBehaviour {
    public void Start() {
        ResourcesManager.GetInstance.Init();
        CreepManager.GetInstance.FindAllySpawn();
    }
}
