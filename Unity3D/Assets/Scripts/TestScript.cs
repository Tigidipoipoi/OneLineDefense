using UnityEngine;
using System.Collections;

/// <summary>
/// This behaviour is meant only for tests.
/// Please remember to remove it from the scene when your tests are over.
/// </summary>
public class TestScript : MonoBehaviour {
    public string m_UserIdToLoad = "mH-TVrCMvkSYk05AwcC7oQ";
    public string m_UserTestToChange = "Changed!";

    void Update() {
        if (Input.GetKeyDown(KeyCode.S)) {
            Debug.Log("Asking Server to load user");
            MultiplayerManager.GetInstance.LoadUser(m_UserIdToLoad);
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            MultiplayerManager.GetInstance.UpdateUser("1st", m_UserTestToChange);
        }
    }
}
