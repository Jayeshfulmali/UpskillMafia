//using UnityEngine;
//using Agora.Rtc;
//using UnityEngine.UI;
//using System.Collections.Generic;
//using Agora_RTC_Plugin.API_Example;

//public class AgoraManager : MonoBehaviour
//{
//    public static AgoraManager Instance { get; private set; }

//    [SerializeField] private string appID;
//    [SerializeField] private GameObject canvas;
//    [SerializeField] private string tokenBase = "https://agora-token-server-qrv9.onrender.com";

//    private IRtcEngine RtcEngine;
//    private string token = "";
//    private string channelName = "Sample";
//    private PlayerInfo mainPlayerInfo;

//    public CONNECTION_STATE_TYPE connectionState = CONNECTION_STATE_TYPE.CONNECTION_STATE_DISCONNECTED;
//    public Dictionary<string, List<uint>> usersJoinedInAChannel;

//    private void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject);
//            usersJoinedInAChannel = new Dictionary<string, List<uint>>();
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }

//    private void Start()
//    {
//        InitRtcEngine();
//        SetBasicConfiguration();
//    }

//    #region Configuration Functions

//    private void InitRtcEngine()
//    {
//        RtcEngine = Agora.Rtc.RtcEngine.CreateAgoraRtcEngine();
//        UserEventHandler handler = new UserEventHandler(this);
//        RtcEngineContext context = new RtcEngineContext
//        {
//            appId = appID,
//            channelProfile = CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_LIVE_BROADCASTING,
//            audioScenario = AUDIO_SCENARIO_TYPE.AUDIO_SCENARIO_DEFAULT,
//            areaCode = AREA_CODE.AREA_CODE_GLOB
//        };

//        RtcEngine.Initialize(context);
//        RtcEngine.InitEventHandler(handler);
//    }

//    private void SetBasicConfiguration()
//    {
//        RtcEngine.EnableAudio();
//        RtcEngine.EnableVideo();

//        // Setting up Video Configuration
//        VideoEncoderConfiguration config = new VideoEncoderConfiguration
//        {
//            dimensions = new VideoDimensions(640, 360),
//            frameRate = 15,
//            bitrate = 0
//        };
//        RtcEngine.SetVideoEncoderConfiguration(config);

//        RtcEngine.SetChannelProfile(CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_LIVE_BROADCASTING);
//        RtcEngine.SetClientRole(CLIENT_ROLE_TYPE.CLIENT_ROLE_BROADCASTER);
//    }

//    #endregion

//    #region Channel Join/Leave Handler Functions

//    public void JoinChannel(PlayerInfo player1Info, PlayerInfo player2Info)
//    {
//        string player1ChannelName = player1Info.GetChannelName();
//        string player2ChannelName = player2Info.GetChannelName();

//        mainPlayerInfo = player1Info;

//        // If both players have not joined a channel
//        if (string.IsNullOrEmpty(player1ChannelName) && string.IsNullOrEmpty(player2ChannelName))
//        {
//            string newChannelName = GenerateChannelName();
//            channelName = newChannelName;
//            mainPlayerInfo.SetChannelName(channelName);
//        }
//        // If both players are already in a channel
//        else if (!string.IsNullOrEmpty(player1ChannelName) && !string.IsNullOrEmpty(player2ChannelName))
//        {
//            return;
//        }
//        // If the other player has joined a channel, join their channel
//        else if (!string.IsNullOrEmpty(player2ChannelName))
//        {
//            UpdatePropertiesForPlayer(mainPlayerInfo, player2ChannelName, player2Info.GetToken());
//        }

//        JoinChannel();
//    }

//    private void JoinChannel()
//    {
//        // If a token is not yet generated, generate one and then join the channel
//        if (token.Length == 0)
//        {
//            StartCoroutine(HelperClass.FetchToken(tokenBase, channelName, 0, UpdateToken));
//            return;
//        }

//        RtcEngine.JoinChannel(token, channelName, "", 0);
//        UpdateUsersInAChannelTable(channelName, 0);
//        RtcEngine.StartPreview();
//        MakeVideoView(0);
//    }

//    public void LeaveChannel()
//    {
//        UpdatePropertiesForPlayer(mainPlayerInfo, "", "");

//        RtcEngine.StopPreview();
//        DestroyVideoView(0);
//        RtcEngine.LeaveChannel();
//    }

//    public void LeaveChannelIfNoOtherUsersPresent()
//    {
//        string channel = mainPlayerInfo.GetChannelName();
//        if (!usersJoinedInAChannel.ContainsKey(channel) || usersJoinedInAChannel[channel].Count != 1) return;

//        RemoveAllTheUsersFromChannel(channel);
//        LeaveChannel();
//    }

//    #endregion

//    #region Helper Functions

//    private void DestroyVideoView(uint uid)
//    {
//        GameObject videoView = GameObject.Find(uid.ToString());
//        if (videoView != null)
//        {
//            Destroy(videoView);
//        }
//    }

//    private void UpdateUsersInAChannelTable(string channel, uint uid)
//    {
//        if (usersJoinedInAChannel.ContainsKey(channel))
//        {
//            usersJoinedInAChannel[channel].Add(uid);
//        }
//        else
//        {
//            usersJoinedInAChannel.Add(channel, new List<uint> { uid });
//        }
//    }

//    private string GenerateChannelName()
//    {
//        return GetRandomChannelName(10);
//    }

//    private string GetRandomChannelName(int length)
//    {
//        const string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
//        char[] channelNameChars = new char[length];

//        for (int i = 0; i < length; i++)
//        {
//            channelNameChars[i] = characters[Random.Range(0, characters.Length)];
//        }

//        return new string(channelNameChars);
//    }

//    private void UpdateToken(string newToken)
//    {
//        token = newToken;
//        mainPlayerInfo.SetToken(token);

//        if (connectionState == CONNECTION_STATE_TYPE.CONNECTION_STATE_DISCONNECTED ||
//            connectionState == CONNECTION_STATE_TYPE.CONNECTION_STATE_FAILED)
//        {
//            JoinChannel();
//        }
//    }

//    private void RemoveAllTheUsersFromChannel(string userChannel)
//    {
//        if (!usersJoinedInAChannel.ContainsKey(userChannel)) return;

//        foreach (uint uid in usersJoinedInAChannel[userChannel])
//        {
//            DestroyVideoView(uid);
//        }
//        usersJoinedInAChannel.Remove(userChannel);
//    }

//    private void UpdatePropertiesForPlayer(PlayerInfo player, string channelName, string token)
//    {
//        player.SetChannelName(channelName);
//        player.SetToken(token);

//        if (player == mainPlayerInfo)
//        {
//            this.channelName = channelName;
//            this.token = token;
//        }
//    }

//    #endregion

//    #region Video View Rendering Logic

//    private void MakeVideoView(uint uid, string channelId = "")
//    {
//        GameObject videoView = GameObject.Find(uid.ToString());
//        if (videoView != null)
//        {
//            // Video view for this user id already exists
//            return;
//        }

//        // Create a video surface game object and assign it to the user
//        VideoSurface videoSurface = MakeImageSurface(uid.ToString());
//        if (videoSurface == null) return;

//        // Configure videoSurface
//        if (uid == 0)
//        {
//            videoSurface.SetForUser(uid, channelId);
//        }
//        else
//        {
//            videoSurface.SetForUser(uid, channelId, VIDEO_SOURCE_TYPE.VIDEO_SOURCE_REMOTE);
//        }

//        videoSurface.OnTextureSizeModify += (int width, int height) =>
//        {
//            RectTransform transform = videoSurface.GetComponent<RectTransform>();
//            if (transform)
//            {
//                // If render in RawImage, just set rawImage size.
//                transform.sizeDelta = new Vector2(width / 2, height / 2);
//                transform.localScale = Vector3.one;
//            }
//            else
//            {
//                // If render in MeshRenderer, just set localSize with MeshRenderer
//                float scale = (float)height / (float)width;
//                videoSurface.transform.localScale = new Vector3(-1, 1, scale);
//            }
//            Debug.LogError("OnTextureSizeModify: " + width + "  " + height);
//        };

//        videoSurface.SetEnable(true);
//    }

//    private VideoSurface MakeImageSurface(string goName)
//    {
//        GameObject gameObject = new GameObject();
//        gameObject.name = goName;

//        // To be rendered onto
//        gameObject.AddComponent<RawImage>();
//        // Make the object draggable
//        gameObject.AddComponent<UIElementDrag>();
//        if (canvas != null)
//        {
//            // Add the video view as a child of the canvas
//            gameObject.transform.parent = canvas.transform;
//        }
//        else
//        {
//            Debug.LogError("Canvas is null video view");
//        }

//        // Set up transform
//        gameObject.transform.Rotate(0f, 0.0f, 180.0f);
//        gameObject.transform.localPosition = Vector3.zero;
//        gameObject.transform.localScale = Vector3.one;
//        gameObject.transform.localRotation = Quaternion.identity;
//        gameObject.layer = LayerMask.NameToLayer("UI");

//        return gameObject.AddComponent<VideoSurface>();
//    }

//    #endregion

//    private class UserEventHandler : IRtcEngineEventHandler
//    {
//        private AgoraManager agoraManager;

//        public UserEventHandler(AgoraManager manager)
//        {
//            agoraManager = manager;
//        }

//        public override void OnUserJoined(RtcConnection connection, uint uid, int elapsed)
//        {
//            agoraManager.MakeVideoView(uid);
//            agoraManager.UpdateUsersInAChannelTable(connection.channelId, uid);
//        }

//        public override void OnUserOffline(RtcConnection connection, uint uid, USER_OFFLINE_REASON_TYPE reason)
//        {
//            if (agoraManager.usersJoinedInAChannel.ContainsKey(connection.channelId))
//            {
//                agoraManager.usersJoinedInAChannel[connection.channelId].Remove(uid);
//            }

//            agoraManager.DestroyVideoView(uid);
//            agoraManager.LeaveChannelIfNoOtherUsersPresent();
//        }

//        public override void OnJoinChannelSuccess(RtcConnection connection, int elapsed)
//        {
//            agoraManager.connectionState = CONNECTION_STATE_TYPE.CONNECTION_STATE_CONNECTED;
//            agoraManager.MakeVideoView(0, connection.channelId);
//            agoraManager.UpdateUsersInAChannelTable(connection.channelId, 0);
//        }
//    }
//}
