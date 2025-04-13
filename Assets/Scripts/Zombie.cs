
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    private NavMeshAgent navAI;
    public float distanceToPlayer;
    private bool canAttack = true;
    void Start()
    {
        navAI = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navAI.SetDestination(Player.instance.transform.position);
        distanceToPlayer = Vector3.Distance(Player.instance.transform.position, transform.position);
        if (Player.instance.health <= 0) {
            canAttack = false;
        }
        if (distanceToPlayer <= 1.1f && canAttack) {
            StartCoroutine(DealDamage(20f));
        }
    }

    private IEnumerator DealDamage(float damage) {
        canAttack = false;
        Player.instance.health -= damage;
        yield return new WaitForSeconds(1f); 
        canAttack = true;
    }
}
