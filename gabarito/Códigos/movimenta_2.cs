using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class movimenta_2 : MonoBehaviour
{
    public Rigidbody2D objeto;
    public Animator objetoani;
    public float x, y;
    public int cont = 0;
    public float i = 15.0f;
    public SpriteRenderer objetorender;
    int contp = 0;
    int point = 0;
    //private int contPulos = 0;
    public int maxPulos = 2; // Número máximo de pulos permitidos
    public float intensidadePulo = 10.0f; // Intensidade do pulo
    public Text pontos; // Referência ao componente Text
    public Transform objetotransf; // Declaração do campo Transform
    public float veltiro = 100f;
    public GameObject tiro;
    //variável para contar o tempo decrescente
    public float timeleft = 10.0f;
    //varíavel para inicializar a contagem
    public bool BonusAtivado = false;
    void Start()
    {
        this.objeto = gameObject.GetComponent<Rigidbody2D>();
        this.objetorender = gameObject.GetComponent<SpriteRenderer>();
        this.objetoani = gameObject.GetComponent<Animator>();
        this.objeto.freezeRotation = true;
        if (pontos == null)
        {
            Debug.LogError("A referência ao componente Text não está atribuída!");
        }
        if (objetotransf == null)
        {
            Debug.LogError("A referência ao Transform não está atribuída!");
        }
        point = PlayerPrefs.GetInt("point");
    }

    void Update()
    {
        MovimentoH();
        Pulo();
        Disparar();
        ContadorTempoBonusVelocidade();
        this.pontos.text=point.ToString();
    }

    void Pulo()
    {
        float xp = objeto.velocity.x;
        float y = this.i;
        if (Input.GetKeyDown("space") && contp < 2) //Input.GetKeyDown(KeyCode.Space)
        {
            Vector2 movimento = new Vector2(xp, y);
            this.objeto.velocity = movimento;
            this.contp++;
        }
    }

    void MovimentoH()
    {
        this.x = Input.GetAxis("Horizontal");
        float xn = this.x * this.i;
        if (xn != 0)
        {
            float y = objeto.velocity.y;
            Vector2 movimento = new Vector2(xn, y);
            this.objeto.velocity = movimento;
            objetoani.SetBool("correndo", true);
            objetoani.SetBool("respirando", false);
            if (this.x < 0) { this.objetorender.flipX = true; }
            if (this.x > 0) { this.objetorender.flipX = false; }
        }
        else
        {
            objetoani.SetBool("correndo", false);
            objetoani.SetBool("respirando", true);
        }
    }

    void Disparar()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float xp = transform.position.x;
            float yp = transform.position.y;
            GameObject copiatiro = (GameObject)Instantiate(tiro, new Vector3(xp + 3.0f, yp, 0), Quaternion.identity);
            copiatiro.GetComponent<Rigidbody2D>().velocity = new Vector2(veltiro, yp);
            Destroy(copiatiro, 0.5f);
            point++;
        }
    }

    void ContadorTempoBonusVelocidade()
    {
        if (this.BonusAtivado == true)
        {
            this.timeleft = this.timeleft - Time.deltaTime;
            if (this.timeleft <= 0)
            {
                this.BonusAtivado = false;
                this.timeleft = 10.0f;
                this.i = 5;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Colidiu " + collision.gameObject.name);
       

        if (collision.gameObject.tag == "chão")
        {
            // Reiniciar contagem de pulos ao tocar o chão
            contp = 0;
        }
        if (collision.gameObject.tag == "bonus")
        {
            this.i = 30;
            this.BonusAtivado = true;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "box01")
        {
            print("Perdeu " + collision.gameObject.name);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "gatilho")
        {
            // Encontrar todos os GameObjects com a tag "box01" e fazer as caixas caírem
            GameObject[] vetorObjetos = GameObject.FindGameObjectsWithTag("box01");
            foreach (GameObject obj in vetorObjetos)
            {
                Rigidbody2D rigid = obj.GetComponent<Rigidbody2D>();
                if (rigid != null)
                {
                    rigid.gravityScale = 1;
                }
            }
        }
    }

}
