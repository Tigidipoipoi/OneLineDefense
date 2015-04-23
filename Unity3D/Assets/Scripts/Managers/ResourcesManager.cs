using UnityEngine;
using System.Collections;

public class ResourcesManager : MonoBehaviour {
    #region Singleton
    static private ResourcesManager s_Instance;
    static public ResourcesManager GetInstance {
        get {
            return s_Instance;
        }
    }

    void Awake() {
        if (s_Instance == null) {
            s_Instance = this;
        }
        DontDestroyOnLoad(this);
    }
    #endregion

    #region Members
    public int m_CurrentGold = 100;
    public int m_CurrentMana = 100;
    public const int c_StartGold = 100;
    public const int c_StartMana = 100;
    #endregion

    public void Init() {
        m_CurrentGold = c_StartGold;
        m_CurrentMana = c_StartMana;
    }

    public bool HasEnoughResources(Cost cost) {
        bool enoughResources = m_CurrentGold >= cost.m_Gold
            && m_CurrentMana >= cost.m_Mana;
        Debug.Log(enoughResources
            ? "Spawning Creep."
            : "Not enough resources.");

        return enoughResources;
    }

    public void PayCost(Cost cost) {
        m_CurrentGold -= cost.m_Gold;
        m_CurrentMana -= cost.m_Mana;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            m_CurrentGold += 100;
        }
    }
}
