using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreepManager : MonoBehaviour {
    #region Singleton
    static private CreepManager s_Instance;
    static public CreepManager GetInstance {
        get {
            return s_Instance;
        }
    }

    void Awake() {
        if (s_Instance == null) {
            s_Instance = this;
        }
        DontDestroyOnLoad(this);
        Init();
    }
    #endregion

    #region Members
    public int m_CreepCount = 0;
    public Transform m_AllySpawn;
    public List<GameObject> m_CreepList = new List<GameObject>();
    [HideInInspector]
    public int m_AllyCreepLayer;
    [HideInInspector]
    public int m_EnemyCreepLayer;
    #endregion

    void Init() {
        m_AllyCreepLayer = LayerMask.NameToLayer("AllyCreep");
        m_EnemyCreepLayer = LayerMask.NameToLayer("EnemyCreep");
    }

    void Update() {
        bool spawn1stCreep = Input.GetKeyDown(KeyCode.Alpha1);
        bool spawn2ndCreep = Input.GetKeyDown(KeyCode.Alpha2);

        if (spawn1stCreep) {
            SpawnCreep(Creep.CREEP_LIST.BASE_MELEE);
        }
        if (spawn2ndCreep) {
            SpawnCreep(Creep.CREEP_LIST.BASE_RANGE);
        }
    }

    public void FindAllySpawn() {
        m_AllySpawn = GameObject.Find("AllySpawn").transform;
    }

    public void ChangeLayersRecursively(Transform parent, int layerName) {
        parent.gameObject.layer = layerName;
        int childCount = parent.childCount;
        for (int i = 0; i < childCount; ++i) {
            ChangeLayersRecursively(parent.GetChild(i), layerName);
        }
    }

    private void SpawnCreep(Creep.CREEP_LIST creepTypeToSummon) {
        GameObject creepToSummon = null;
        string creepName = "";
        switch (creepTypeToSummon) {
            case Creep.CREEP_LIST.BASE_RANGE:
                creepToSummon = m_CreepList[1];
                creepName = "RangeCreep" + m_CreepCount;
                break;

            // Melee creep is the default creep
            case Creep.CREEP_LIST.BASE_MELEE:
            default:
                creepToSummon = m_CreepList[0];
                creepName = "MeleeCreep" + m_CreepCount;
                break;
        }

        Cost creepCost = creepToSummon
            .GetComponent<CreepScript>()
            .m_CreepStats.m_Attribute.m_Cost;
        if (ResourcesManager.GetInstance.HasEnoughResources(creepCost)) {
            ResourcesManager.GetInstance.PayCost(creepCost);
            GameObject newCreep = GameObject.Instantiate(creepToSummon,
                m_AllySpawn.position, Quaternion.identity) as GameObject;
            newCreep.transform.parent = m_AllySpawn;
            newCreep.name = creepName;

            newCreep.layer = m_AllyCreepLayer;
            ++m_CreepCount;
        }
    }
}
