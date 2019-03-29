using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject naveInimigaPrefab;
    [SerializeField]
    private GameObject[] powerups;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    //Criar uma Coroutine para o inimigo

    IEnumerator EnemySpawnRoutine()
    {
        while (true)
        {      
            Instantiate(naveInimigaPrefab, new Vector3(Random.Range(-10.58f, 10.33f), 5.66f, 0), Quaternion.identity);
            yield return new WaitForSeconds(3.0f);
        }
    }

    IEnumerator PowerupSpawnRoutine()
    {
        while (true)
        {      
            int randomPowerup = Random.Range(0,3);
            Instantiate(powerups[randomPowerup], new Vector3(Random.Range(-10.58f, 10.33f), 5.66f, 0), Quaternion.identity);
            yield return new WaitForSeconds(3.0f);
        }
    }

}
