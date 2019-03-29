using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    // Velocidade de transição do Power Up
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID; // 0 = Tiro Triplo || 1 = Velocidade de Movimento || 2 = Escudo || 

	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colidiu com: " + other.name);

        if (other.tag == "Player")
        {
            //Acessando o Player
            Player powerUp = other.GetComponent<Player>();

            if (powerUp != null)
            {
                if (powerupID == 0)
                {
                    // Ativando o tiro Triplo
                    powerUp.TiroTriploHabilitado();
                }
                else if(powerupID == 1)
                {
                    //Ativando a velocidade
                    powerUp.velocidadeHabilitada();
                }
                else if(powerupID == 2)
                {
                    powerUp.escudoHabilitado();
                }               
            }
            StartCoroutine(powerUp.velocidadeRecarga());
            StartCoroutine(powerUp.TiroTriploTempoRecarga());
            // Destruindo o objeto pós coleta
            Destroy(this.gameObject);
        }
    }     
}
