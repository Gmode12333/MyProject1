using UnityEngine;
using Pathfinding;
using CongTDev.ObjectPooling;
using System;

public class EnemyMovingSystem : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform targer;
    public float activateDistance = 10f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float moveSpeed = 10f;
    public float nextWayPointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;

    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;

    private Path path;
    private int currentWaypoint = 0;
    Seeker seeker;
    Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;
    [SerializeField] private LayerMask jumpableGround;

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        targer = PlayerPosition.Instance.transform;
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void FixedUpdate()
    {
        if (TargetInDistance() && followEnabled)
        {
            PathFollow();
        }
    }
    private void UpdatePath()
    {
        if (followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, targer.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, targer.transform.position) < activateDistance;
    }
    private void PathFollow()
    {
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }
        RaycastHit2D isGrounded = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, 0.1f, jumpableGround);


        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * moveSpeed * Time.deltaTime;
        if(this.tag != "Lucid")
        {
            anim.SetFloat("Move", Mathf.Abs(force.x));
        }
        //jump
        if (isGrounded && jumpEnabled)
        {
            if (direction.y > jumpNodeHeightRequirement)
            {
                rb.AddForce(Vector2.up * moveSpeed * jumpModifier);
            }
        }
        //movement
        rb.AddForce(force);
        //next waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWayPointDistance)
        {
            currentWaypoint++;
        }
        //direction graphics handling
        if (directionLookEnabled)
        {
            if (rb.velocity.x > 0.05f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }else if (rb.velocity.x < -0.05f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }

    }
}
