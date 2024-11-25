using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControl : MonoBehaviour
{

    private bool Press;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Press);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // åàÇ‹Ç¡ÇΩÉLÅ[à»äOÇÕÇÕÇ∂Ç≠
        if (!context.performed) return;

        if (Press) { Press = false; }
        else       { Press = true;  }

        
    }
}
