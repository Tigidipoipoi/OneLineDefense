﻿using UnityEngine;
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
        m_BaseHPDisplay.text = string.Format("{0}: {1}/{2}",
            LocalizationManager.GetInstance
                .GetLocalizedText(Utils.LocalizedTexts.c_BaseHp),
            baseHP.ToString(), Base.c_MaxHP.ToString());
    }

    public void UpdateCurrentGold(int gold) {
        m_CurrentGoldDisplay.text = string.Format("{0}: {1}",
            LocalizationManager.GetInstance
                .GetLocalizedText(Utils.LocalizedTexts.c_Gold),
            gold.ToString());
    }

    public void UpdateCurrentMana(int mana) {
        m_CurrentManaDisplay.text = string.Format("{0}: {1}",
            LocalizationManager.GetInstance
                .GetLocalizedText(Utils.LocalizedTexts.c_Mana),
            mana.ToString());
    }

    //public RectTransform NewHealthBar(RectTransform healthBar) {
    //    RectTransform instanciatedHB = Instantiate<RectTransform>(healthBar);
    //    instanciatedHB.SetParent(m_CreepsInfosPanel.GetComponent<RectTransform>());
    //    // When instanciated the scale goes mad so we fix it here
    //    instanciatedHB.localScale = new Vector3(1.0f, 1.0f, 1.0f);

    //    return instanciatedHB;
    //}
}
