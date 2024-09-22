using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{

    public float speed = 10f;
    public float maxLifeTime = 3f;
    public Vector3 targetVector;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * targetVector * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.CompareTag("Enemy")) {
            IncreaseScore();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void IncreaseScore() {
        Player.score++;
        Debug.Log(Player.score);
        UpdateScoreText();
    }

    private void UpdateScoreText() {
        GameObject go = GameObject.FindGameObjectWithTag("Points");
        go.GetComponent<Text>().text = "Puntos: " + Player.score;
    }
}
