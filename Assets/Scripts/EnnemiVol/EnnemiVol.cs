using UnityEngine;
using System.Collections;

public class EnnemiVol : MonoBehaviour
{
    public GameObject projectile;
    public Vector2 spawnPos;
    public GameObject barreVie;
    public float spawnTime;

    void Start()
    {
        StartCoroutine(LauchBlocksWait());
        barreVie = gameObject.transform.GetChild(0).GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        barreVie.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, gameObject.transform.position.y + 2, 0);
    }

    public IEnumerator LauchBlocksWait()
    {
        while (true)
            {
                spawnTime = Random.Range(Time.deltaTime, 1.7f);
                yield return new WaitForSeconds(spawnTime);
                
                spawnPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                Instantiate(projectile, spawnPos, Quaternion.identity);
            }
    }
}
