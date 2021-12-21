using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.CloudScriptModels;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class PlayfabManager : MonoBehaviour
    {


        [SerializeField]  private Text debug;
        public static PlayfabManager Instance;
        public ExecuteFunctionResult _result;
        public PlayFabError _error;
        public void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }   
        }

        public void PlayerLogin()
        {
            var id = Game.PlayerPersistentData.GetID();
            var request = new LoginWithCustomIDRequest
            {
                CustomId = id,
                CreateAccount = true
            };
            PlayFabClientAPI.LoginWithCustomID(request, OnCustomLoginSuccess, OnError);

        }

        public void LoginWithGooglePlay()
        {
            //TODO:: Develop Google Play login
            //var request = new LoginWithGoogleAccountRequest();
            //PlayFabClientAPI.LoginWithGoogleAccount(request, OnGoogleLogin, OnError);
        }

        private static void OnCustomLoginSuccess(LoginResult loginResult)
        {
            Debug.Log("Logged as " + loginResult.PlayFabId);
            Instance.debug.text = "Session Ticket : " + loginResult.SessionTicket;
            Instance.debug.text += "\nSession EntityToken : " + loginResult.EntityToken.EntityToken;
            Instance.debug.text += "\nSession Id : " + loginResult.PlayFabId;
           
        }
        private static void OnGoogleLogin(LoginResult loginResult)
        {
            //TODO:: Develop Google Play login
        }

        private static void OnError(PlayFabError playFabError)
        {
            Debug.LogError("Request got error : " + playFabError.Error);
        }


        public static void CallFunction(string function, Dictionary<string, object> parameters)
        {
            Debug.Log(function);
            PlayFabCloudScriptAPI.ExecuteFunction(new ExecuteFunctionRequest()
            {
                Entity = new PlayFab.CloudScriptModels.EntityKey()
                {
                    Id = PlayFabSettings.staticPlayer.EntityId, //Get this from when you logged in,
                    Type = PlayFabSettings.staticPlayer.EntityType, //Get this from when you logged in
                },
                FunctionName = function, //This should be the name of your Azure Function that you created.
                FunctionParameter = parameters, //This is the data that you would want to pass into your function.
                GeneratePlayStreamEvent = false //Set this to true if you would like this call to show up in PlayStream
            }, (ExecuteFunctionResult result) =>
            {
                Instance._result = result;
                if (result.FunctionResultTooLarge ?? false)
                {
                    Debug.Log("This can happen if you exceed the limit that can be returned from an Azure Function, See PlayFab Limits Page for details.");
                    return;
                }
                Debug.Log($"The {result.FunctionName} function took {result.ExecutionTimeMilliseconds} to complete");
                Debug.Log($"Result: {result.FunctionResult.ToString()}");
            }, (PlayFabError error) =>
            {
                Debug.Log($"Opps Something went wrong: {error.GenerateErrorReport()}");
                Instance._error = error;
            });        }
    }
}