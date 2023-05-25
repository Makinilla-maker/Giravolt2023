using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyBell : MonoBehaviour
{
    private Animator anim;
    public bool isAnimationDone;
    [SerializeField]private GameManager gameManager;
    private AudioSource src;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        isAnimationDone = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        src = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            SetBellAniamtionBool(true);
        }
    }
    public void SetAnimationDone()
    {
        isAnimationDone = true;
        SetBellAniamtionBool(false);
        gameManager.OnVotationBegin();
    }
    public void SetBellAniamtionBool(bool b)
    {
        anim.SetBool("PlayAnimation", b);
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.CompareTag("Player_Hand"))
        {
            SetBellAniamtionBool(true);
        }
    }
    public void PlayBellSound()
    {
        src.PlayOneShot(src.clip);
    }
}
