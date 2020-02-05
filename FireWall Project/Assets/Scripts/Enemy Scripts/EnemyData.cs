using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{

    [SerializeField]
    public float enemyBaseHealth = 10;
    public float enemyTempHealth = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemyTempHealth = enemyBaseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
