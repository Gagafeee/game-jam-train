using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMotor : MonoBehaviour
{
    [SerializeField] private float health;

    public void ApplyDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Debug.Log("Entity has been killed!");
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        
        foreach(GameObject t in turrets)
        {
            t.GetComponent<TurretMotor>().ResetTarget();
        }
    }
}
