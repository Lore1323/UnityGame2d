using UnityEngine;
using UnityEngine.Rendering;

public class FollowCamera : MonoBehaviour
{
    public Transform Target;

    private float TargetPositionX;
    private float TargetPositionY;

    private float PositionX;
    private float PositionY;

    [Header("References")]
    public float RightMax;
    public float LeftMax;
    public float TopMax;
    public float BottomMax;

    
    public bool On= true;

    private void Awake()
    {
        PositionX = TargetPositionX;
        PositionY = TargetPositionY;
        transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, -1); 
    }

    void MoveCam()
    {
        if (On)
        {
            if (Target)
            {
                TargetPositionX = Target.transform.position.x;
                TargetPositionY = Target.transform.position.y;

                if (PositionX > RightMax && TargetPositionX < LeftMax)
                {
                    PositionX = TargetPositionX;
                }
                if (PositionY > TopMax && TargetPositionY < BottomMax)
                {
                    PositionY = TargetPositionY;
                }
            }
            transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, -1);
        }
    }
    private void Update()
    {
        MoveCam();
    }
}
