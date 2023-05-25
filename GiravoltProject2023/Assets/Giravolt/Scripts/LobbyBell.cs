using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyBell : MonoBehaviour
{
    public Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetAniamtionBool(bool b)
    {
        anim.SetBool("PlayAnimation", b);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.CompareTag("Player_Hand"))
        {
            SetAniamtionBool(true);
        }
    }
}
