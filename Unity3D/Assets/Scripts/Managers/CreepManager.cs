﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreepManager : MonoBehaviour {
    #region Singleton
    static private CreepManager sInstance;
    static public CreepManager instance {
        get {
            return sInstance;
        }
    }

    void Awake () {
        if (sInstance == null)
            sInstance = this;
        GameObject.DontDestroyOnLoad (this);
    }
    #endregion

    public int mCreepCount = 0;
    public Transform mAllySpawn;
    public List<GameObject> mCreepList = new List<GameObject> ();

    void Update () {
        if (Input.GetKeyDown (KeyCode.Alpha1)
            || Input.GetKeyDown (KeyCode.Alpha2)) {
            GameObject creepToSummon = Input.GetKeyDown (KeyCode.Alpha1)
                ? mCreepList[0]
                : Input.GetKeyDown (KeyCode.Alpha2)
                    ? mCreepList[1]
                    : mCreepList[0];
            Cost creepCost = creepToSummon
                .GetComponent<CreepScript> ()
                .mCreepStats.mAttribute.mCost;
            if (ResourcesManager.instance.HasEnoughResources (creepCost)) {
                ResourcesManager.instance.PayCost (creepCost);
                GameObject newCreep = GameObject.Instantiate (creepToSummon, mAllySpawn.position, Quaternion.identity) as GameObject;
                newCreep.transform.parent = mAllySpawn;
                ++mCreepCount;
            }
        }
    }

    public void FindAllySpawn () {
        mAllySpawn = GameObject.Find ("AllySpawn").transform;
    }
}