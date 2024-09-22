using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

public float thrustForce = 100f;
public float rotationSpeed = 120f;
public float xBorderLimit = 7f;
public float yBorderLimit = 7f;

public GameObject gun, bulletPrefab;

private Rigidbody _rigid;

public GameObject pauseMenu;
public GameObject pauseButton;
private bool paused = false;

public static int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        pauseMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
    
        Vector3 newPos = transform.position;
        if(newPos.x > xBorderLimit)
            newPos.x = -xBorderLimit + 1;
        else if(newPos.x < -xBorderLimit)
            newPos.x = xBorderLimit - 1;
        else if(newPos.y > yBorderLimit)
            newPos.y = -yBorderLimit + 1;
        else if(newPos.y < -yBorderLimit)
            newPos.y = yBorderLimit - 1;
        transform.position = newPos;

        float rotation = Input.GetAxis("Horizontal") * Time.deltaTime;
        float thrust = Input.GetAxis("Vertical") * Time.deltaTime;

        Vector3 thrustDirection = transform.right;

        _rigid.AddForce(thrustDirection * thrust * thrustForce);

        transform.Rotate(Vector3.forward, -rotation * rotationSpeed);

        if(Input.GetKeyDown(KeyCode.Space)) {
            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);

            Bullet balaScript = bullet.GetComponent<Bullet>();

            balaScript.targetVector = transform.right;

        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            paused = !paused;
            if(paused){
                Pause();
            } else {
                Resume();
            }
        }

    }

    public void Resume(){
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void Pause(){
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void Quit(){
        Application.Quit();
    }

    private void OnCollisionEnter(Collision collision) {
        
        if(collision.gameObject.CompareTag("Enemy")) {
            score = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } else {
            Debug.Log(message:"He colisionado con otra cosa");
            //Physics.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider>());
        }

    }

}
