using UnityEngine;

public static class Utils {
    public static float GetSphereColliderEnd(GameObject go) {
        return go.GetComponent<SphereCollider>().radius
            * go.transform.localScale.x * 0.5f;
    }

    public static float GetBoxColliderEnd(GameObject go) {
        return go.GetComponent<BoxCollider>().size.x
            * go.transform.localScale.x * 0.5f;
    }
}
