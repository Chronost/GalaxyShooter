using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // variável para saber se o tiro foi coletado
    public bool tiroTriplo = true;
    // Variável de inicio do powerUp
    public bool velocidade = false;
    // Variável para coleta do Shield
    public bool escudo = false;

    //Variável de vidas
    [SerializeField]
    private int vidas = 3;

    [SerializeField]
    private GameObject _prefabLaser;

    [SerializeField]
    private GameObject _TiroTriploPrefab;

    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private GameObject _shieldGameObject;

    [SerializeField]
    private GameObject _shieldPrefab;

    [SerializeField]
    private float _descansoTiro = 0.2f;

    private float _podeTiro = 0.0f;

    [SerializeField] // Se houver uma váriavel privada abaixo do atributo, podemos editá-la no inspector
    private float _speed = 5.0f;

    private UIManager _uiManager;


    void Start()
    {
        transform.position = new Vector3(0, -4.0f, 0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager != null)
        {
            _uiManager.updateLives(vidas);
        }
    }

    //Update é a chamada do método a cada Frame.
    void Update()
    {
        Movimento();

        // Se a tecla de espaço for pressionada
        // Surgirá um laser na posição da nave

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Tiro();
        }

    }

    private void Tiro()
    {
        if (Time.time > _podeTiro) //Verifica se o tempo real está em 0 segundos.
        {
            //Instantiate(_prefabLaser, transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity); //Instancia o objeto do Laser
            _podeTiro = Time.time + _descansoTiro; // Verifica uma condição falsa para fazê-lo retornar ao Loop adicionando 0.35 segundos.

            if (tiroTriplo == true)
            {
                Instantiate(_TiroTriploPrefab, transform.position, Quaternion.identity);
                //Destroy(_TiroTriploPrefab.gameObject);
            }
            else
            {
                Instantiate(_prefabLaser, transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity); //Instancia o objeto do Laser
                _podeTiro = Time.time + _descansoTiro; // Verifica uma condição falsa para fazê-lo retornar ao Loop adicionando 0.35 segundos.
            }
        }
    }
    public void Movimento() // Toda a lógica de movimentação e colisão da Nave, está contida aqui.
    {

        float eixoHorizontal = Input.GetAxis("Horizontal");
        float eixoVertical = Input.GetAxis("Vertical");

        // Se o boost estiver ativado   
        // se moverá 2 vezes mais rápido que a velocidade normal
        // senão
        // ficará com a velocidade normal
        if (velocidade == true)
        {
            transform.Translate(Vector3.up * _speed * eixoVertical * Time.deltaTime);
            transform.Translate(Vector3.right * _speed * eixoHorizontal * Time.deltaTime);
        }
        else
        {
            //Converte os 60 Frames Por Segundo para o tempo comum, "Time.deltaTime" é quem faz a conversão
            // Com o adicional de dobro da velocidade para o caso do Power-Up ter sido coletado
            transform.Translate(Vector3.up * (_speed * _speed) * eixoVertical * Time.deltaTime);
            transform.Translate(Vector3.right * (_speed * _speed) * eixoHorizontal * Time.deltaTime);
        }

        // Lógica de colisão com as paredes, nesse caso, a nave teletransporta para outra posição
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.4f)
        {
            transform.position = new Vector3(transform.position.x, -4.4f, 0);
        }

        if (transform.position.x > 10.25f)
        {
            transform.position = new Vector3(-10.25f, transform.position.y, 0);
        }
        else if (transform.position.x < -10.25f)
        {
            transform.position = new Vector3(10.25f, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        if (escudo == true)
        {
            
        }
        else if (vidas < 1)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else
        {
            vidas--;
            _uiManager.updateLives(vidas);
        }     

    }

    //====================================================================================================
    // HABILITAÇÃO DO ESCUDO
    public void escudoHabilitado()
    {
        escudo = true;
        _shieldGameObject.SetActive(true); // Determina que a animação na Layer será ativada.
        StartCoroutine(escudoRecarga());
    }

    public IEnumerator escudoRecarga()
    {
        yield return new WaitForSeconds(5.0f);
        _shieldGameObject.SetActive(false); // Determina que a animação na Layer será desativada.
        escudo = false;
    }
    //====================================================================================================
    // MUDANÇA DE VELOCIDADE DA NAVE
    public void velocidadeHabilitada()
    {
        velocidade = false;
        StartCoroutine(velocidadeRecarga());
    }
    public IEnumerator velocidadeRecarga()
    {
        yield return new WaitForSeconds(5.0f);
        velocidade = true;
    }


    //====================================================================================================
    // método para ativar o power up
    // Coroutine Method é responsavel por dar o Cooldown para o Power-Up
    // Método IEnumerator para desativar o boost
    //====================================================================================================


    //====================================================================================================
    // HABILITAÇÃO DO TIRO TRIPLO E SEU COOLDOWN
    public void TiroTriploHabilitado()
    {
        tiroTriplo = true;
        StartCoroutine(TiroTriploTempoRecarga());
    }

    public IEnumerator TiroTriploTempoRecarga()
    {
        yield return new WaitForSeconds(7.0f);
        tiroTriplo = false; 

    }
}