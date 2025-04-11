using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public float speed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    
    {
        // Movement
        if (!controller != null) {
        controller.Move(new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed) * Time.deltaTime);
        }
    }
}
