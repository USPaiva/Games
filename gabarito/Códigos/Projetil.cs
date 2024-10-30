using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour
{
    int point = 0;
    public AudioSource som;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D objetoTocado)
    {
        if (objetoTocado.gameObject.tag == "Inimigo")
        {
            try
            {
                som.Play();
            }
            catch
            {
                print("Erro de som");
            }
            print("Acertou");
            Destroy(objetoTocado.gameObject);
            point++;
        }
    }
}
