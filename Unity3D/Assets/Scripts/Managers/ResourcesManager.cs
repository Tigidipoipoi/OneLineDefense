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
    public float m_Income;
    public const float c_StartIncome = 1.5f;
    public const float c_IncomeDelay = 1.0f;
    #endregion

    public void Init() {
        m_CurrentGold = c_StartGold;
        m_CurrentMana = c_StartMana;
        m_Income = c_StartIncome;
        UIManager.GetInstance.UpdateCurrentGold(m_CurrentGold);
        UIManager.GetInstance.UpdateCurrentMana(m_CurrentMana);
        StartCoroutine("IncomeCoroutine");
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
        UIManager.GetInstance.UpdateCurrentGold(m_CurrentGold);
        UIManager.GetInstance.UpdateCurrentMana(m_CurrentMana);
    }

    private IEnumerator IncomeCoroutine() {
        while (true) {
            m_CurrentGold += (int)m_Income;
            UIManager.GetInstance.UpdateCurrentGold(m_CurrentGold);

            yield return new WaitForSeconds(c_IncomeDelay);
        }
    }

    public void UpdateIncomeFromCreepDeath() {
        m_Income += 0.1f;
    }
}
