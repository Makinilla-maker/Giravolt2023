using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyBell : MonoBehaviour
{
    private Animator anim;
    public bool isAnimationDone;
    private GameManager gameManager;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        isAnimationDone = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetAnimationDone()
    {
        isAnimationDone = true;
        gameManager.OnVotationBegin();
    }
    public void SetAniamtionBool(bool b)
    {
        anim.SetBool("PlayAnimation", b);
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.CompareTag("Player_Hand"))
        {
            SetAniamtionBool(true);
        }
    }
}
