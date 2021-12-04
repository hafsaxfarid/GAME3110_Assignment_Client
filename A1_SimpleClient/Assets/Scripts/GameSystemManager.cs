using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSystemManager : MonoBehaviour
{
    GameObject usernameInputField,
        passwordInputField,
        createToggle,
        loginToggle,
        submitButton,
        networkClient;

    void Start()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach(GameObject go in allObjects)
        {
            if(go.name == "UsernameInput")
            {
                usernameInputField = go;
            }
            else if(go.name == "PasswordInput")
            {
                passwordInputField = go;
            }
            else if(go.name == "LoginToggle")
            {
                loginToggle = go;
            }
            else if(go.name == "CreateToggle")
            {
                createToggle = go;
            }
            else if(go.name == "SubmitButton")
            {
                submitButton = go;
            }
            else if(go.name == "NetworkedClient")
            {
                networkClient = go;
            }
        }

        submitButton.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);

        createToggle.GetComponent<Toggle>().onValueChanged.AddListener(CreateToggle);

        loginToggle.GetComponent<Toggle>().onValueChanged.AddListener(LoginToggle);  
    }

    void Update()
    {

    }

    public void CreateToggle(bool newValue)
    {
        loginToggle.GetComponent<Toggle>().SetIsOnWithoutNotify(!newValue);
    }

    public void LoginToggle(bool newValue)
    {
        createToggle.GetComponent<Toggle>().SetIsOnWithoutNotify(!newValue);
    }

    public void SubmitButtonPressed()
    {
        string n = usernameInputField.GetComponent<TMP_InputField>().text;
        string p = passwordInputField.GetComponent<TMP_InputField>().text;

        if(loginToggle.GetComponent<Toggle>().isOn)
        {
            networkClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToSeverSignifiers.Login + "," + n + "," + p);
        }
        else if (createToggle.GetComponent<Toggle>().isOn)
        {
            networkClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToSeverSignifiers.CreateAccount + "," + n + "," + p);
        }

        Debug.Log(ClientToSeverSignifiers.CreateAccount + ", Name: " + n + " Password: " + p);
    }
}

public static class ClientToSeverSignifiers
{
    public const int Login = 1;
    
    public const int CreateAccount = 2;
}

public static class SeverToClientSignifiers
{
    public const int LoginResponse = 1;
}