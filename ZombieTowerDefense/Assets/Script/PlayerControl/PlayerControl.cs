using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControl : MonoBehaviour
{
    //----------------------------------------------
    // �}�E�X�n����

    [SerializeField] private Transform player;
    private Camera mainCamera;

    private Vector3 currentPosition = Vector3.zero;

    //----------------------------------------------
    // �L�[�{�[�h

    private bool Press;

    //----------------------------------------------
    // Player�̕ۑ�
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

            // �n�ʂ�Ray�łƂ�
            var baseFieldHits = raycastHitList
               .Where(hit => hit.collider.GetComponent<Field>() != null)
               .ToList();

            if (baseFieldHits.Any())
            {
                // �n�ʂ�Plane���`�i�ʏ��y=0�̕��ʁj
                Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

                // �}�E�X�ʒu�̃X�N���[�����W����Ray�𐶐�
                Ray raya = mainCamera.ScreenPointToRay(Input.mousePosition);

                // ���C�ƒn�ʂ̌�_���v�Z
                if (groundPlane.Raycast(raya, out float distance))
                {
                    currentPosition = raya.GetPoint(distance);  // ���C�ƒn�ʂ̌�_
                    currentPosition.y = 0;  // y���W��0�ɐݒ�
                }
            }

            // ���m��Ray�łƂ�
            var SoldierHits = raycastHitList
               .Where(hit => hit.collider.GetComponent<Soldier>() != null)
               .ToList();

            if(SoldierHits.Any()) 
            {
                // SoldierHits�̍ŏ��̃q�b�g���擾
                var firstSoldier = SoldierHits.First();

                // BaseSoldier�R���|�[�l���g���擾
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
        // ���܂����L�[�ȊO�͂͂���
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
