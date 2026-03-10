using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{ 
    public GameObject projectile;
    public float speed = 10;
    public Vector2 SpawnPos;
    public InputActionReference DistanceRef;

    void Start()
    {

    }

    void FixedUpdate()
    {
        DistanceRef.action.started += DistanceAttack;
        //DistanceRef.action.performed += DistanceAttack;
        DistanceRef.action.canceled += DistanceAttack;
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    void DistanceAttack(InputAction.CallbackContext ctx)
    { 
        if (!ctx.canceled)
        {
            SpawnPos = new Vector2(gameObject.transform.position.x +1, gameObject.transform.position.y);
            Instantiate(projectile, SpawnPos, Quaternion.identity);
        } 
    }
}
