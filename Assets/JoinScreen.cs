using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinScreen : MonoBehaviour
{
    public Button registerButton;

    [Header("Inputs")]
    public InputField nicknameInput; //12345
    public InputField emailInput; //correct
    public InputField passwordInput; // 123456
    public Toggle toggle; // on

    public void ChangedInput()
    {
        // Check if all fields are correct
        bool shouldEnableButton = Check();

        registerButton.interactable = shouldEnableButton;
    }

    bool Check()
    {
        if (nicknameInput.text.Length < 3)
            return false;

        if (emailInput.text.Length < 5)
            return false;

        if (passwordInput.text.Length < 6)
            return false;

        if (toggle.isOn == false)
            return false;

        return true; // true = every field is fine/enough characters
    }

    int SquareArea(int side) // 5
    {
        int area = side * side;
        return area;
    }
}
