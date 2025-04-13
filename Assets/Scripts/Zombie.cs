
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    private NavMeshAgent navAI;
    public float distanceToPlayer;
    private bool canAttack = true;
    private Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;
    public LayerMask groundLayer, playerLayer;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private bool touchingPlayer = false;
    void Start()
    {
        navAI = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        navAI.SetDestination(Player.instance.transform.position);
        distanceToPlayer = Vector3.Distance(Player.instance.transform.position, transform.position);
        if (!playerInSightRange && !playerInAttackRange) {
            Patroling();
        }
        if (playerInSightRange && !playerInAttackRange) {
            ChasePlayer();
        }
        if (playerInAttackRange && playerInSightRange) {
            AttackPlayer();
        }
        if (Player.instance.health <= 0) {
            canAttack = false;
        }
    }
    private void Patroling() {
        if (!walkPointSet) {
            SearchWalkPoint();
        }
        if (walkPointSet) {
            navAI.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        // Point reached
        if (distanceToWalkPoint.magnitude < 1f) {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint() {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer)) {
            walkPointSet = true;
        }
    }
    private void AttackPlayer() {
        if (!touchingPlayer) {
            navAI.SetDestination(Player.instance.transform.position);
        }
        if (touchingPlayer) {
            navAI.SetDestination(transform.position);
        }

        if (!touchingPlayer) {
            Vector3 direction = Player.instance.transform.position - transform.position;
            direction.y = 0;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
        }
        if (distanceToPlayer <= 1.1f && canAttack) {
            StartCoroutine(DealDamage(20f));
        }
    }
    private void ChasePlayer() {
        navAI.SetDestination(Player.instance.transform.position);
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    private IEnumerator DealDamage(float damage) {
        canAttack = false;
        Player.instance.health -= damage;
        yield return new WaitForSeconds(1f); 
        canAttack = true;
    }
}
