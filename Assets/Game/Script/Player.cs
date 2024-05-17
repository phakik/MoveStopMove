using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] FloatingJoystick joystick;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FindJoyStick());
        rb = GetComponent<Rigidbody>();
        GetWeapon();
        GetPant();
    }
    private void OnEnable()
    {
        isDead = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (joystick != null)
        {
            Moving();
        }

        CheckMoving();
        RemoveDeadEnemy();
        CheckAttack();
    }
    void Moving()
    {
        if (this.isDead)
        {
            return;
        }

        rb.velocity = new Vector3(joystick.Horizontal * speed, rb.velocity.y, joystick.Vertical * speed);


        if (isMoving)
        {
            if ((joystick.Vertical != 0 || joystick.Horizontal != 0) && rb.velocity != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(rb.velocity);
            }
        }
        else if (currentTarget != null)
        {
            Vector3 direction = currentTarget.transform.position - this.transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
        }

    }

    private IEnumerator FindJoyStick()
    {
        yield return new WaitForSeconds(0.4f);
        joystick = FindAnyObjectByType<FloatingJoystick>();
    }
    protected override void OnDespawn()
    {
        base.OnDespawn();
        Observer.Instance.Notify("Lose",0f);
    }
}
