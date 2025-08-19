using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMotion : MonoBehaviour
{
    private EnemySettings settings;
    private KillEnemy kill;
    private CharacterController enemyControl;
    private float yVelocity = 0;
    private float gravConstant, enemySpeed;
    private float timeBetweenDirectionChange = 0.0f;
    public float timeToWait = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyControl = GetComponent<CharacterController>();
        settings = GetComponent<EnemySettings>();
        kill = GetComponent<KillEnemy>();

        gravConstant = settings.gravConstant;
        enemySpeed = settings.enemySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //rotate the enemy a random amount, if 5 seconds have passed.
        if (timeBetweenDirectionChange >= timeToWait) {
            float rotationVal = Random.Range(-45.0f, 45.0f);
            transform.Rotate(0, rotationVal, 0);
            timeBetweenDirectionChange = 0.0f;
        } else {
            timeBetweenDirectionChange += Time.deltaTime;
        }

        //gravity handling. mostly unneeded on flat map but will be useful later.
        if (enemyControl.isGrounded) {
            yVelocity = 0.0f;
        } else {
            yVelocity -= gravConstant * Time.deltaTime;
        }

        //apply movement.
        Vector3 move = new Vector3(enemySpeed, yVelocity, 0);
        enemyControl.Move(transform.rotation * move * Time.deltaTime);

        //Check if we are below an acceptable height: if so, kill
        if (transform.position.y < -5) {
            kill.Kill();
        }
    }
}
