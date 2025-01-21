using UnityEngine;

public class KeypadButton : MonoBehaviour
{
    public KeypadInteraction keypad;
    public string digit;

    public void PressButton()
    {
        if (keypad != null)
        {
            keypad.EnterDigit(digit);
        }
    }
}
