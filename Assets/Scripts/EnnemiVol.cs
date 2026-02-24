using UnityEngine;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class EnnemiVol : MonoBehaviour
{
    public GameObject projectile;
    public float speed;
    public Vector2 SpawnPos;

    void Start()
    {
        StartCoroutine(LauchBlocksWait());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LauchBlocksWait()
    {
        while (true)
            {
                yield return new WaitForSeconds(3.5f);
                SpawnPos = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
                Instantiate(projectile, SpawnPos, Quaternion.identity);
            }
    }
}
