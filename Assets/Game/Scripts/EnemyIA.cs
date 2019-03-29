using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour {


    private float _speed = 4.0f;

    [SerializeField]
    private GameObject _enemyExplosionPrefab;
    private UIManager _uiManager;

    // Use this for initialization
    void Start ()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager != null)
        {
            _uiManager.updateScore();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.down * _speed  * Time.deltaTime);

        if (transform.position.y < -5.4f)
        {
            float randomX = Random.Range(-10.3f, 10.3f);
            transform.position = new Vector3(randomX, 5.4f, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            if(other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }
            Destroy(other.gameObject); // Destruição do Laser
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity); // Explosão instanciada
            _uiManager.updateScore();
            Destroy(this.gameObject); // Destruição do Inimigo
        }
        else if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
           
            if(player != null)
            {
                player.Damage();
            }

            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            
        }
    }
}

