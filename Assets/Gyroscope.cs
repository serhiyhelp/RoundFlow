using UnityEngine;

public class Gyroscope : MonoBehaviour
{
    private void FixedUpdate()
    {
#if UNITY_EDITOR
        transform.Rotate(0, 0, Input.GetAxis("Horizontal") * Time.fixedDeltaTime * 130f);
#else
        var trueGravity = Input.gyro.gravity;
        trueGravity.y *= -1;

        transform.LookAt(Vector3.forward, trueGravity);
#endif
    }
}
