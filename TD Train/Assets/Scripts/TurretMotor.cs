using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMotor : MonoBehaviour
{
    [Header("Turret stats")]
    [SerializeField] private float damage;
    [SerializeField] private float cooldown;
    [SerializeField] private float range;
    [SerializeField] private bool perceShield;

    [Header("Animation part")]
    [SerializeField] private Transform partToRotate;
    [SerializeField] private float turnSpeed = 6.5f;
    [SerializeField] private string fireAnimationName;

    private Animator anim;

    public Transform target;
    private string targetName;

    private bool isInFight = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(UpdateTarget());
    }

    private IEnumerator UpdateTarget()
    {
        WaitForSeconds delay1 = new WaitForSeconds(2);
        WaitForSeconds delay2 = new WaitForSeconds(.8f);

        while (true)
        {
            if (isInFight)
            {
                yield return delay1;
            }
            else
            {
                GameObject[] ennemies = FindEnemy();
                float shortestDistance = Mathf.Infinity;
                GameObject nearestEnemy = null;

                if (ennemies.Length == 0)
                {
                    yield return delay1;
                }
                else
                {
                    foreach (GameObject enemy in ennemies)
                    {
                        float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

                        if (distanceToEnemy < shortestDistance)
                        {
                            shortestDistance = distanceToEnemy;
                            nearestEnemy = enemy;
                        }
                    }

                    if (nearestEnemy != null)
                    {
                        if (this.range >= shortestDistance)
                        {
                            target = nearestEnemy.transform;
                            targetName = target.name;
                        }
                    }
                    else
                    {
                        target = null;
                        targetName = "";
                    }

                    yield return delay2;
                }
            }
        }
    }

    private GameObject[] FindEnemy()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        return gos;
    }

    private void Update()
    {
        if (target == null) return;

        Vector3 dir = target.position - transform.position;

        if (dir != new Vector3())
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }

        if (!isInFight)
        {
            isInFight = true;
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        WaitForSeconds delay = new WaitForSeconds(cooldown);
        EntityMotor motor = target.GetComponent<EntityMotor>();

        while (target != null)
        {
            if (target == null) break;

            if (Vector3.Distance(transform.position, target.position) > range)
            {
                ResetTarget();
                break;
            }

            motor.ApplyDamage(damage);

            yield return delay;
        }

        isInFight = false;
        StartCoroutine(UpdateTarget());
    }

    public void SetTarget(Transform target)
    {
        StopCoroutine(Attack());
        isInFight = false;

        this.target = target;
        targetName = this.target.name;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void ResetTarget()
    {
        StopCoroutine(Attack());
        isInFight = false;
        target = null;
        targetName = "";
    }
}