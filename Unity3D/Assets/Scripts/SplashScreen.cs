using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SplashScreen : MonoBehaviour {
    #region Members
    public GameObject m_BattleScenePanel;
    public GameObject m_LoginPanel;
    public Text m_Pseudo;
    public InputField m_LoginTB;
    public Button m_LoginButton;
    public Image[] m_Flags;
    #endregion

    public void Start() {
        UpdateFlagsAlpha();
    }

    public void Login() {
        MultiplayerManager.GetInstance.CreateOrConnectAsUser(m_Pseudo.text);
    }

    public void LoginAnonymous() {
        LaunchGame();
    }

    public void LaunchGame() {
        m_BattleScenePanel.SetActive(true);
        m_LoginPanel.SetActive(false);
        MultiplayerManager.GetInstance.GameLaunched();

        Application.LoadLevel(1);
    }

    public void EnableConnectionUI() {
        m_LoginTB.interactable = true;
        m_LoginButton.interactable = true;
    }

    public void ChangeLanguage(string language) {
        if (LocalizationManager.GetInstance.TryToChangeLanguage(language)) {
            UpdateFlagsAlpha();
        }
    }

    void UpdateFlagsAlpha() {
        for (int i = 0; i < m_Flags.Length; ++i) {
            Color newColor = m_Flags[i].color;

            if (m_Flags[i].gameObject.name ==
                LocalizationManager.GetInstance.m_CurrentLanguage + "Flag") {
                newColor.a = 1.0f;
            }
            else {
                newColor.a = 0.5f;
            }

            m_Flags[i].color = newColor;
        }
    }
}
