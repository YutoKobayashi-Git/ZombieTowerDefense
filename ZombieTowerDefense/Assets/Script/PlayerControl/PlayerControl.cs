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
        MoveSoldier();
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
                // 地面のPlaneを定義（通常はy=0の平面）
                Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

                // マウス位置のスクリーン座標からRayを生成
                Ray raya = mainCamera.ScreenPointToRay(Input.mousePosition);

                // レイと地面の交点を計算
                if (groundPlane.Raycast(raya, out float distance))
                {
                    currentPosition = raya.GetPoint(distance);  // レイと地面の交点
                    currentPosition.y = 0;  // y座標を0に設定
                }
            }

            // 兵士をRayでとる
            var SoldierHits = raycastHitList
               .Where(hit => hit.collider.GetComponent<Soldier>() != null)
               .ToList();

            if(SoldierHits.Any()) 
            {
                // SoldierHitsの最初のヒットを取得
                var firstSoldier = SoldierHits.First();

                // BaseSoldierコンポーネントを取得
                currentPlayer = firstSoldier.collider.gameObject.GetComponent<BaseSoldier>();
            }
        }



    }

    private void MoveSoldier()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (currentPlayer == null) return;

            currentPlayer.OnWalkMove(currentPosition);

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
