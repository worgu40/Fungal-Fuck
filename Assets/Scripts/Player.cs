
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
    [SerializeField] GameObject playerModel;
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
            if(movementCreation.x == 0 && movementCreation.z != 0)
            {
                playerModel.transform.localRotation = Quaternion.Euler(0,movementCreation.z < 0 ? -90 : 90,0);
            }
            else if(movementCreation.x != 0)
            {
                playerModel.transform.localRotation = Quaternion.Euler(0, (Mathf.Atan(-movementCreation.z / movementCreation.x) + (movementCreation.x<0? 0 : Mathf.PI)) * 180 / Mathf.PI, 0);
            }
        }
        // Health
        healthBar.fillAmount = health / 100f;
    }
}
