using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMotion : MonoBehaviour
{
    private EnemySettings settings;
    private KillEnemy kill;
    private CharacterController enemyControl;
    private float yVelocity = 0;
    private float gravConstant, enemySpeed;
    // private float timeBetweenDirectionChange = 0.0f;
    // [SerializeField] private float timeToWait = 3.0f;
    [SerializeField] private UIHandling pauseCheck;
    [SerializeField] private GameObject player;
    [SerializeField] private NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        enemyControl = GetComponent<CharacterController>();
        settings = GetComponent<EnemySettings>();
        kill = GetComponent<KillEnemy>();
        //agent = GetComponent<NavMeshAgent>();

        gravConstant = settings.gravConstant;
        enemySpeed = settings.enemySpeed;

        agent.updatePosition = false;
        agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        //This check has been added to all code: it stops activity if "paused" is true.
        if (pauseCheck.paused)
        {
            return;
        }

        /* RANDOM MOVEMENT HAS BEEN PHASED OUT.
        //rotate the enemy a random amount, if 5 seconds have passed.
        if (timeBetweenDirectionChange >= timeToWait)
        {
            float rotationVal = Random.Range(-45.0f, 45.0f);
            transform.Rotate(0, rotationVal, 0);
            timeBetweenDirectionChange = 0.0f;
        }
        else
        {
            timeBetweenDirectionChange += Time.deltaTime;
        }
        */

        agent.SetDestination(player.transform.position);
        
        //gravity handling. mostly unneeded on flat map but will be useful later.
        if (enemyControl.isGrounded)
        {
            yVelocity = 0.0f;
        }
        else
        {
            yVelocity -= gravConstant * Time.deltaTime;
        }

        //Set location of navmesh agent - zombie movement.
        

        
        //apply movement.
        Vector3 move = new Vector3(0, yVelocity, 0);
        move += agent.desiredVelocity;
        enemyControl.Move(move * Time.deltaTime);

        //sync agent location
        agent.nextPosition = transform.position;

        //Check if we are below an acceptable height: if so, kill
        if (transform.position.y < -5)
        {
            kill.Kill();
        }
    }
}
