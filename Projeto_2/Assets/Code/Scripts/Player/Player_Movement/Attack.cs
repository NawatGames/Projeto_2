using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Attack : MonoBehaviour
{
    public int healthPlayer = 15;
    private int activeHealthPlayer;
    private bool isPressed;

    public HealthBar healthBar;
    public Shield shield;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        activeHealthPlayer = healthPlayer;
        healthBar.SetHealthPlayer(healthPlayer);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            animator.SetBool("Attacking", true);
            //StartCoroutine(TimerRoutine());
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
            animator.SetBool("Attacking", false);
            //StartCoroutine(TimerRoutine());
        }
        
      
    }
 
    private IEnumerator TimerRoutine()
    {
      //code can be executed anywhere here before this next statement 
      yield return new WaitForSeconds(5); //code pauses for 5 seconds
     //code resumes after the 5 seconds and exits if there is nothing else to run
 
    }
    

    //public void OnUpdateSelected(BaseEventData data)
    //      {
    //          if (isPressed)
    //          {
    //           attack = true;
    //          }
    //      }
    //      public void OnPointerDown(PointerEventData data)
    //      {
    //          isPressed = true;
    //      }
    //      public void OnPointerUp(PointerEventData data)
    //      {
    //          isPressed = false;
    //      }

    public void TakeDamage(int damage)
    {
        activeHealthPlayer -= damage;
        healthBar.SetHealth(activeHealthPlayer);

        if(activeHealthPlayer <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(((collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent)) && (shield.shieldPlayer == true)))
        {
            enemyComponent.TakeDamage(5);
        }
    }
}
