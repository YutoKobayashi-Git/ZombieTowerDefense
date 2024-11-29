using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class BaseSoldier : MonoBehaviour , Soldier , SelectObject
{
    private bool MoveOn;

   private Vector3 target;
    private NavMeshAgent myAgent;

    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        // Nav Mesh Agent を取得します。
        myAgent = GetComponent<NavMeshAgent>();
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        // targetに向かって移動します。
        myAgent.SetDestination(target);
    }

    public void OnWalkMove(Vector3 positon)
    {
        
        Debug.Log("もじばけ");
        MoveOn = true;

        target = positon;
    }
}

interface Soldier
{

}
interface SelectObject
{

}
