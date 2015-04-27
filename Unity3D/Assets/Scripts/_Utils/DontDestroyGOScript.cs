using UnityEngine;
using System.Collections;

public class DontDestroyGOScript : MonoBehaviour {
    public void Awake() {
        DontDestroyOnLoad(gameObject);
    }
}
