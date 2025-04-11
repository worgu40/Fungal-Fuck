using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (player != null)
        {
            // Offsets camera by +7 on the Y axis and -4 on the Z axis
            transform.position = new Vector3(player.position.x, player.position.y + 7f, player.position.z - 4f);
        }
    }
}
