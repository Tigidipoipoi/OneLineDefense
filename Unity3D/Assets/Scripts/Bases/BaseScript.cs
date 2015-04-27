using UnityEngine;
using System.Collections;

public class BaseScript : MonoBehaviour {
    public Base m_BaseStats;

    public void Start() {
        if (name == "AllyBase") {
            UIManager.GetInstance.UpdateBaseHP(m_BaseStats.m_HP);
        }
    }

    public void GetDamage(int dammages) {
        dammages -= m_BaseStats.m_Defense;
        if (dammages <= 0) {
            return;
        }

        m_BaseStats.m_HP -= dammages;
        if (name == "AllyBase") {
            UIManager.GetInstance.UpdateBaseHP(m_BaseStats.m_HP);
        }
        if (m_BaseStats.m_HP > 0) {
            return;
        }

        Die();
    }

    private void Die() {
        Debug.Log(name + " has just died.");
        Destroy(this.gameObject);
    }
}
