using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class movimenta : MonoBehaviour
{
    public Rigidbody2D objeto;
    public float x, y;
    public int cont = 0;
    public float i = 15.0f;
    public SpriteRenderer objetorender;
    int contp = 0;
    int point = 0;
    private int contPulos = 0;
    public int maxPulos = 2; // Número máximo de pulos permitidos
    public float intensidadePulo = 10.0f; // Intensidade do pulo
    public Text pontos; // Referência ao componente Text
    public Transform objetotransf; // Declaração do campo Transform

    void Start()
    {
        this.objeto = gameObject.GetComponent<Rigidbody2D>();
        this.objetorender = gameObject.GetComponent<SpriteRenderer>();
        this.objeto.freezeRotation = true;
        if (pontos == null)
        {
            Debug.LogError("A referência ao componente Text não está atribuída!");
        }
        if (objetotransf == null)
        {
            Debug.LogError("A referência ao Transform não está atribuída!");
        }
    }

    void Update()
    {
        MovimentoH();
        MovimentoY();
        Pulo();
        EntradasMouse();
    }

    void Pulo()
    {
        float xp = objeto.velocity.x;
        float y = this.i;
        if (Input.GetKeyDown("space") && contp < 2)
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
        float y = objeto.velocity.y;
        Vector2 movimento = new Vector2(xn, y);
        this.objeto.velocity = movimento;
        if (this.x < 0) { this.objetorender.flipX = true; }
        if (this.x > 0) { this.objetorender.flipX = false; }
    }

    void MovimentoY()
    {
        this.y = Input.GetAxis("Vertical");
        float yn = this.y * this.i;
        float x = objeto.velocity.x;
        Vector2 movimento = new Vector2(x, yn);
        this.objeto.velocity = movimento;
        if (this.y > 0) { this.objetorender.flipY = false; }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Colidiu " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("key"))
        {
            Destroy(collision.gameObject);
            point++;
            this.pontos.text = point.ToString();
        }
        if (collision.gameObject.name == "door")
        {
            if ( point > 0 )
            {
                print("Você ganhou! " + collision.gameObject.name);
                SceneManager.LoadScene("segunda");
                PlayerPrefs.SetInt("point", point);
                this.contp++;
            }
        }
        if (collision.gameObject.name == "maze")
        {
            print("Perdeu " + collision.gameObject.name);
        }
        //if (collision.gameObject.CompareTag("chão"))
        //{
            //// Reiniciar contagem de pulos ao tocar o chão
            //contPulos = 0;
        //}
        //if (collision.gameObject.CompareTag("gatilho"))
        //{
            //// Encontrar todos os GameObjects com a tag "box01" e fazer as caixas caírem
            //GameObject[] vetorObjetos = GameObject.FindGameObjectsWithTag("box01");
            //foreach (GameObject obj in vetorObjetos)
            //{
                //Rigidbody2D rigid = obj.GetComponent<Rigidbody2D>();
                //if (rigid != null)
                //{
                    //rigid.gravityScale = 1;
                //}
            //}
        //}
        //if (collision.gameObject.CompareTag("box01"))
        //{
            //print("Perdeu " + collision.gameObject.name);
        //}
        if (collision.gameObject.CompareTag("Cresce"))
        {
            Vector3 tam = this.objetotransf.localScale; // Mudado para Vector3, pois Transform.localScale é um Vector3
            tam.x *= 2;
            tam.y *= 2;
            this.objetotransf.localScale = tam;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Diminui"))
        {
            Vector3 tam = this.objetotransf.localScale; // Mudado para Vector3
            tam.x /= 2;
            tam.y /= 2;
            this.objetotransf.localScale = tam;
            Destroy(collision.gameObject);
        }
    }

    void EntradasMouse()
    {
        if (Input.GetMouseButtonDown(1))
        {
            print("Apertou o botão direito");
        }

        if (Input.GetMouseButtonDown(0) && objeto.CompareTag("chão"))
        {
            // Aplicar força de pulo ao clicar com o botão esquerdo do mouse
            objeto.velocity = new Vector2(objeto.velocity.x, intensidadePulo);
            contPulos++;
        }
    }
}
