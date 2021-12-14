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

    GameObject loginInfoUI,
        findGameRoomInfoUI,
        ticTacToeRoomInfoUI,
        makeMoveButton,
        findGameButton;

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
            else if(go.name == "FindGameButton")
            {
                findGameButton = go;
            }
            else if(go.name == "MakeMoveButton")
            {
                makeMoveButton = go;
            }
            else if(go.name == "LoginInfo")
            {
                loginInfoUI = go;
            }
            else if(go.name == "FindGameInfo")
            {
                findGameRoomInfoUI = go;
            }
            else if(go.name == "TicTacToeRoomInfo")
            {
                ticTacToeRoomInfoUI = go;
            }
        }

        ChangeGameState(GameStates.LoginState);

        createToggle.GetComponent<Toggle>().onValueChanged.AddListener(CreateToggle);

        loginToggle.GetComponent<Toggle>().onValueChanged.AddListener(LoginToggle);  

        submitButton.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);

        //findGameButton.GetComponent<Button>().onClick.AddListener(FindGameButtonPressed);
        //makeMoveButton.GetComponent<Button>().onClick.AddListener(MakeMoveButtonPressed);

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

    private void SubmitButtonPressed()
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


    private void FindGameButtonPressed()
    {

    }

    private void MakeMoveButtonPressed()
    {

    }

    public void ChangeGameState(int newState)
    {
        loginInfoUI.SetActive(false);
        findGameRoomInfoUI.SetActive(false);
        ticTacToeRoomInfoUI.SetActive(false);

        if (newState == GameStates.LoginState)
        {
            loginInfoUI.SetActive(true);
        }
        else if(newState == GameStates.FindGameRoom)
        {
            findGameRoomInfoUI.SetActive(true);
        }
        else if(newState == GameStates.WaitingForGameRoom)
        {

        }
        else if(newState == GameStates.PlayingTicTacToe)
        {
            ticTacToeRoomInfoUI.SetActive(true);
        }
    }
}

public static class GameStates
{
    public const int LoginState = 1;

    public const int FindGameRoom = 2;
    
    public const int WaitingForGameRoom = 3;
    
    public const int PlayingTicTacToe = 4;
}