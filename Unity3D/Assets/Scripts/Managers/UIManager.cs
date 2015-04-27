using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {
    #region Singleton
    static private UIManager s_Instance;
    static public UIManager GetInstance {
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
    public GameObject m_PlayerInfosPanel;
    private Text m_BaseHPDisplay;
    private Text m_CurrentGoldDisplay;
    private Text m_CurrentManaDisplay;
    #endregion

    void Init() {
        m_BaseHPDisplay = m_PlayerInfosPanel
            .transform.FindChild("BaseHP").GetComponent<Text>();
        m_CurrentGoldDisplay = m_PlayerInfosPanel
            .transform.FindChild("CurrentGold").GetComponent<Text>();
        m_CurrentManaDisplay = m_PlayerInfosPanel
            .transform.FindChild("CurrentMana").GetComponent<Text>();
    }

    public void UpdateBaseHP(int baseHP) {
        m_BaseHPDisplay.text = string.Format("Base HP: {0}/{1}",
            baseHP.ToString(), Base.c_MaxHP.ToString());
    }

    public void UpdateCurrentGold(int gold) {
        m_CurrentGoldDisplay.text = string.Format("Gold: {0}", gold.ToString());
    }

    public void UpdateCurrentMana(int mana) {
        m_CurrentManaDisplay.text = string.Format("Mana: {0}", mana.ToString());
    }
}
