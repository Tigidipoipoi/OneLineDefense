using UnityEngine;
using System.Collections;

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

    public void Init() {
        // Parse: m_CSVFile.text;
    }
}
