using UnityEngine;

[System.Serializable]
public struct ScreenInput
{
    public bool isSet;
    public Vector2 coords;
}
public class PlayerInput : MonoBehaviour
{
    const string LeftMouseButton = "Fire1";
    const string RightMouseButton = "Fire2";

    public PlayerMovement movement;
    public PlayerBehaviour behaviour;

    public ScreenInput[] leftInputCords = new ScreenInput[3];
    public Vector2 leftDirectionVector;

    public ScreenInput[] rightInputCords = new ScreenInput[3];
    public Vector2 rightDirectionVector;

    void Update()
    {
        if (Input.GetButtonDown(LeftMouseButton))
            SetInputCoords(true, true);
        if (Input.GetButtonUp(LeftMouseButton))
            SetInputCoords(true, false);
        if (Input.GetButton(LeftMouseButton))
            SetUpdateInputCoords(leftInputCords, true);

        if (Input.GetButtonDown(RightMouseButton))
            SetInputCoords(false, true);
        if (Input.GetButtonUp(RightMouseButton))
            SetInputCoords(false, false);
        if (Input.GetButton(RightMouseButton))
            SetUpdateInputCoords(rightInputCords, true);
    }

    void SetInputCoords(bool leftInput, bool initial)
    {
        ScreenInput[] screenInput = leftInput ? leftInputCords : rightInputCords;

        screenInput[2].isSet = !initial;
        screenInput[2].coords = !initial ? Input.mousePosition : Vector3.zero;

        if (!initial)
        {
            SetUpdateInputCoords(screenInput, false);
            CalculateDirectionVector(leftInput, screenInput);
        }

        screenInput[0].isSet = initial;
        screenInput[0].coords = initial ? Input.mousePosition : Vector3.zero;
    }

    void SetUpdateInputCoords(ScreenInput[] inputCoords, bool set)
    {
        if (inputCoords[0].isSet)
            inputCoords[1].coords = set ? Input.mousePosition : Vector3.zero;
    }

    void CalculateDirectionVector(bool leftInput, ScreenInput[] inputCoords)
    {
        if (leftInput)
        {
            leftDirectionVector = inputCoords[2].coords - inputCoords[0].coords;
            CallPlayerMove();
        }
        else
        {
            rightDirectionVector = inputCoords[2].coords - inputCoords[0].coords;
            CallPlayerAttack();
        }

    }

    void CallPlayerMove()
    {
        movement.Move(leftDirectionVector);
    }
    void CallPlayerAttack()
    {
        behaviour.Attack(rightDirectionVector);
    }
}
