using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float health;
    public Image healthBar;
    public Vector3 offset = new Vector3(2f, 0, 0);    [SerializeField] float designerSpeed;
    float Speed { 
        get { 
            return designerSpeed; 
        } 
    }
    private bool canAttack = true;
    private CharacterController controller;
    public static Player instance;
    [SerializeField] GameObject playerModel; // Bald Guy
    [SerializeField] Animator modelAnimator;
    public GameObject attackHitbox;
    private BoxCollider hitboxCollider;
    void Start()
    {
        instance = this;
        controller = GetComponent<CharacterController>();
        hitboxCollider = attackHitbox.GetComponent<BoxCollider>();
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
            modelAnimator.SetFloat("WalkSpeed",movementCreation.magnitude);
        }
        attackHitbox.transform.position = transform.position + playerModel.transform.rotation * offset;
        if (Input.GetButtonDown("Fire1") && canAttack) {
            StartCoroutine(Attack());
        }

        
        // Health
        healthBar.fillAmount = health / 100f;
    }
    private IEnumerator Attack() {
        canAttack = false;
        hitboxCollider.enabled = true;
        modelAnimator.SetTrigger("AttackTrigger");
        yield return new WaitForSeconds(0.5f);
        hitboxCollider.enabled = false;
        canAttack = true;
    } 
    void FixedUpdate()
    {
    }
}
