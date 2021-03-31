// RevoErp.RestClient.ApiManager
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RevoErp.RestClient.Util;
using System;
using RevoErp.RestClient.Model;

internal class IncoloreAuthenticator: IAuthenticator
{
    private Token Token;

    public IncoloreAuthenticator(Token token)
    {
        this.Token = token;
    }


	public string Sign()
	{
		string data = JsonConvert.SerializeObject(new
		{
			createdAt = Time.Timestamp()
		});
		string key = Crypto.MD5(Token.Value.Substring(0, 16));
		string iv = Token.Value.Substring(16, 16).Trim();
		string sign = Token.Id + ">>" + Crypto.EncryptString(data, key, iv);
		Console.WriteLine("data: " + data + ", key: " + key + ", iv: " + iv);
		return sign.Trim();
	}
	public void Authenticate(IRestClient client, IRestRequest request)
    {
		var sign = Sign();
		request.AddHeader("Authorization", sign);
    }
}