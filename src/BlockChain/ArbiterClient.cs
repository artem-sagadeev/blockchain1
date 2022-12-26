using System.Text;
using Newtonsoft.Json;

namespace BlockChain;

public class ArbiterClient
{
    private const string GetPublicKeyUrl = "http://89.108.115.118/ts/public";
    private static readonly Func<string, string> GetSignatureUrl = digest => $"http://89.108.115.118/ts?digest={digest}";
    
    public static async Task<(byte[] data, byte[] signature)> GetSignature(byte[] hash)
    {
        var hex = Convert.ToHexString(hash);
        
        var httpClient = new HttpClient();
        
        var response = await httpClient.GetAsync(GetSignatureUrl(hex));
        var content = await response.Content.ReadAsStringAsync();
        var timeStampResponse = JsonConvert.DeserializeObject<TimeStampResponse>(content);

        if (timeStampResponse is null)
        {
            throw new InvalidOperationException();
        }
        
        var timeStamp = timeStampResponse.TimeStampToken.Ts;
        var signature = timeStampResponse.TimeStampToken.Signature;

        var timeStampBytes = Encoding.UTF8.GetBytes(timeStamp);
        
        var newData = timeStampBytes.Concat(hash).ToArray();
        var signatureBytes = Convert.FromHexString(signature);

        return (newData, signatureBytes);
    }

    public static async Task<byte[]> GetPublicKey()
    {
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(GetPublicKeyUrl);
        var content = await response.Content.ReadAsStringAsync();
        
        return GetKey(content);
    }
    
    private static byte[] GetKey(string hex)
    {
        var sb = new StringBuilder();
        for(var i = 0; i < hex.Length; i++)
        {
            var byteString = hex.Substring(i, 1);
            var b = Convert.ToByte(byteString, 16);
            sb.Append(Convert.ToString(b, 2).PadLeft(4, '0'));
        }
        var str = sb.ToString();
    
        var numOfBytes = str.Length / 8;
        var bytes = new byte[numOfBytes];
        for(var i = 0; i < numOfBytes; ++i)
        {
            bytes[i] = Convert.ToByte(str.Substring(8 * i, 8), 2);
        }

        return bytes;
    }
}