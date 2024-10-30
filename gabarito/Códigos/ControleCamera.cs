using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleCamera : MonoBehaviour
{
    public GameObject player;
	private Vector3 distancia;
	void Start () {//Calcula a distância entre o jogador e a câmera
		distancia = transform.position - player.transform.position;
	}
	// Método chamado após o Update primeiramente atualiza os componentes e depois a câmera
	void LateUpdate () {
		transform.position = player.transform.position + distancia;
	}
}
