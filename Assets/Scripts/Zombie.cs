using UnityEngine;

public class Zombie : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent navAI;
    void Start()
    {
        navAI = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navAI.SetDestination(Player.instance.transform.position);
    }
}
