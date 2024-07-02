using UnityEngine;
using ExitGames.Client.Photon;

public static class PhotonCustomTypes
{
    public static void Register()
    {
        PhotonPeer.RegisterType(typeof(Color), (byte)'C', SerializeColor, DeserializeColor);
    }

    private static short SerializeColor(StreamBuffer outStream, object customobject)
    {
        Color color = (Color)customobject;
        byte[] bytes = new byte[16];
        int index = 0;

        Protocol.Serialize(color.r, bytes, ref index);
        Protocol.Serialize(color.g, bytes, ref index);
        Protocol.Serialize(color.b, bytes, ref index);
        Protocol.Serialize(color.a, bytes, ref index);

        outStream.Write(bytes, 0, 16);
        return 16;
    }

    private static object DeserializeColor(StreamBuffer inStream, short length)
    {
        byte[] bytes = new byte[16];
        inStream.Read(bytes, 0, 16);

        int index = 0;
        Protocol.Deserialize(out float r, bytes, ref index);
        Protocol.Deserialize(out float g, bytes, ref index);
        Protocol.Deserialize(out float b, bytes, ref index);
        Protocol.Deserialize(out float a, bytes, ref index);

        return new Color(r, g, b, a);
    }
}
