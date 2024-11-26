using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControl : MonoBehaviour
{
    //----------------------------------------------
    // マウス系処理

    [SerializeField] private Transform player;
    private Camera mainCamera;

    private Vector3 currentPosition = Vector3.zero;

    //----------------------------------------------
    // キーボード

    private bool Press;

    //----------------------------------------------
    // Playerの保存
    private BaseSoldier currentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        MousePosition();
    }

    private void MousePosition()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            var raycastHitList = Physics.RaycastAll(ray).ToList();

            // 地面をRayでとる
            var baseFieldHits = raycastHitList
               .Where(hit => hit.collider.GetComponent<Field>() != null)
               .ToList();

            if (baseFieldHits.Any())
            {
                var distance = Vector3.Distance(mainCamera.transform.position, raycastHitList.First().point);
                var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);

                currentPosition = mainCamera.ScreenToWorldPoint(mousePosition);
                currentPosition.y = 0;
            }

            // 兵士をRayでとる
            var SoldierHits = raycastHitList
               .Where(hit => hit.collider.GetComponent<Soldier>() != null)
               .ToList();

            if(SoldierHits.Any()) 
            {
                currentPlayer = raycastHitList.First().collider.gameObject.GetComponent<BaseSoldier>();
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // 決まったキー以外ははじく
        if (!context.performed) return;

        if (Press) { Press = false; }
        else       { Press = true;  }



        
    }

    void OnDrawGizmos()
    {
        if (currentPosition != Vector3.zero)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(currentPosition, 0.5f);
        }
    }
}
