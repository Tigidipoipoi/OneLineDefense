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

    public struct Languages {
        public const int c_Count = 2;

        public const string c_English = "EN";
        public const string c_French = "FR";
    }

    public struct LocalizedTexts {
        public const int c_Count = 3;

        public const string c_Gold = "GOLD";
        public const string c_Mana = "MANA";
        public const string c_BaseHp = "BASE_HP";
    }
}
