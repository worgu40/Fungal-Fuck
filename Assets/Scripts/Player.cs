
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float health;
    public Image healthBar;
    [SerializeField] float designerSpeed;
    float Speed { 
        get { 
            return designerSpeed; 
        } 
    }
    private CharacterController controller;
    public static Player instance;
    void Start()
    {
        instance = this;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    
    {
        // Movement
        if (controller != null) {
            Vector3 movementCreation = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            movementCreation = movementCreation.normalized * Mathf.Clamp01(movementCreation.magnitude); //clamps magnitude of the vector between 0 and 1
            controller.Move(movementCreation * Speed * Time.deltaTime);
        }
        // Health
        healthBar.fillAmount = health / 100f;
    }
}
