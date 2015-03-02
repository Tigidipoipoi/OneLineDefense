using UnityEngine;
using System.Collections;

public class ResourcesManager : MonoBehaviour {
    #region Singleton
    static private ResourcesManager sInstance;
    static public ResourcesManager instance {
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

    public int mCurrentGold = 100;
    public int mCurrentMana = 100;
    public const int cStartGold = 100;
    public const int cStartMana = 100;

    public void Init () {
        mCurrentGold = cStartGold;
        mCurrentMana = cStartMana;
    }

    public bool HasEnoughResources (Cost cost) {
        bool enoughResources = mCurrentGold >= cost.mGold
                            && mCurrentMana >= cost.mMana;
        Debug.Log (enoughResources
            ? "Spawning Creep"
            : "Not enough resources");

        return enoughResources;
    }

    public void PayCost (Cost cost) {
        mCurrentGold -= cost.mGold;
        mCurrentMana -= cost.mMana;
        Debug.Log (string.Format ("{0} & {1}", mCurrentGold, mCurrentMana));
    }
}
