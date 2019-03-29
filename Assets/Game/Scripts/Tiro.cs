using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro : MonoBehaviour
{

    [SerializeField]
    private float _speed = 6.0f;
    private float _vidaTiro = 6.0f;
    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        // Translação do Laser
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        Player triploPrefab = GetComponent<Player>();

        //Se o laser chegar na posição Y = 6.0, os clones gerados são destruídos

        if (transform.position.y >= _vidaTiro)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject); 

        }
    }
}
