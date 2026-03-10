using UnityEngine;
using System.Collections;

public class EnnemiVol : MonoBehaviour
{
    public GameObject projectile;
    public float speed;
    public Vector2 SpawnPos;
    public GameObject barreVie;
    public float spawnTime = 3.5f;

    void Start()
    {
        StartCoroutine(LauchBlocksWait());
        barreVie = gameObject.transform.GetChild(0).GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
         barreVie.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, gameObject.transform.position.y+2,0);
    }

    public IEnumerator LauchBlocksWait()
    {
        while (true)
            {
                yield return new WaitForSeconds(spawnTime);
                SpawnPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                Instantiate(projectile, SpawnPos, Quaternion.identity);
            }
    }
}
