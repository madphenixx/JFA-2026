using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{ 
    public InputActionReference distanceRef;

    public GameObject projectile;
    
    public float speed = 10;
    public Vector2 spawnPos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        distanceRef.action.started += DistanceAttack;
        distanceRef.action.canceled += DistanceAttack;
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    void DistanceAttack(InputAction.CallbackContext ctx)
    { 
        if (!ctx.canceled)
        {
            spawnPos = new Vector2(gameObject.transform.position.x + 1, gameObject.transform.position.y);
            Instantiate(projectile, spawnPos, Quaternion.identity);
        } 
    }
}
