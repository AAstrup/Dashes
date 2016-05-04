using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

    public float hitRange = 1f;
    public float damage = 1f;
    public float moveSpeed = 5f;
    Vector2 dir;

    float currentRotRAD;
    Vector3 vector;

    void Start()
    {
        currentRotRAD = transform.eulerAngles.z * Mathf.Deg2Rad;
        vector = new Vector3(Mathf.Cos(currentRotRAD), Mathf.Sin(currentRotRAD), 0);
    }

	// Update is called once per frame
	void Update ()
    {
        transform.position += vector * Time.deltaTime * moveSpeed;
        //        if (Vector2.Distance(transform.position, player) > hitRange)
        //            Hit();
    }

    void Hit()
    {
        print("Do damage to player or smth");
        Destroy(gameObject);
    }
}
