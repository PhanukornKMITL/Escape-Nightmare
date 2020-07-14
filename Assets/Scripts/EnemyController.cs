using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public UnityEngine.AI.NavMeshAgent agent;
    private Vector3 targetPoint, startPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ทำให้มันติดพื้นไม่ตามขึ้นบนอากาศ
        targetPoint = PlayerController.instance.transform.position;
        targetPoint.y = transform.position.y;

        agent.destination = targetPoint;
    }
}
