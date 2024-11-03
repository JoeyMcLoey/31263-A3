using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    private Animator ghostAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        ghostAnim = GetComponent<Animator>();
    }

    public void SetNormalGhost(){
        ghostAnim.SetInteger("State", 0);
    }
    
    public void SetScaredGhost(){
        ghostAnim.SetInteger("State", 1);
    }

    public void SetRecoveringGhost(){
        ghostAnim.SetInteger("State", 2);
    }


}
