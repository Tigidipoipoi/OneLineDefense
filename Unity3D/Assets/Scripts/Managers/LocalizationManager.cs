using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LocalizationManager : MonoBehaviour {
    #region Singleton
    static private LocalizationManager s_Instance;
    static public LocalizationManager GetInstance {
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

    public TextAsset m_CSVFile;
    public string m_CurrentLanguage = Utils.Languages.c_French;

    // First dictionnary contains all languages,
    // the second contains localized texts
    private Dictionary<string,
        Dictionary<string, string>> m_LocalizedTexts;

    public void Init() {
        if (m_CurrentLanguage != Utils.Languages.c_French
            && m_CurrentLanguage != Utils.Languages.c_English) {
            m_CurrentLanguage = Utils.Languages.c_French;
        }

        m_LocalizedTexts = new Dictionary<string,
            Dictionary<string, string>>(Utils.Languages.c_Count);

        ParseLocCSVFile(m_CSVFile);
    }

    public string GetLocalizedText(string key) {
        return m_LocalizedTexts[m_CurrentLanguage][key];
    }

    private void ParseLocCSVFile(TextAsset locFile) {
        string[] splitStrings = new string[2];
        splitStrings[0] = "\r\n";
        splitStrings[1] = "\n";

        string[] rows = locFile.text.Split(splitStrings,
            System.StringSplitOptions.RemoveEmptyEntries);

        string[] languages = rows[0].Split(';');

        // first element is "ID" so we skip it
        for (int i = 1; i < languages.Length; ++i) {
            m_LocalizedTexts.Add(languages[i],
                new Dictionary<string, string>(Utils.LocalizedTexts.c_Count));

            for (int j = 1; j < rows.Length; ++j) {
                string[] translatedTexts = rows[j].Split(';');

                m_LocalizedTexts[languages[i]].Add(translatedTexts[0],
                    translatedTexts[i]);
            }
        }
    }

    public bool TryToChangeLanguage(string language) {
        switch (language) {
            case Utils.Languages.c_English:
            case Utils.Languages.c_French:
                m_CurrentLanguage = language;

                return true;
            default:
                return false;
        }
    }
}
