// RevoErp.RestClient.ApiManager
using Pluvet.Service.Application.Utils;
using RestSharp;
using RevoErp.RestClient.Api;
using RevoErp.RestClient.Model;
using RevoErp.RestClient.Util;
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class ApiManager
{
	public RestClient Client
	{
		get;
		set;
	}

	public BasicInfo BasicInfo
	{
		get;
		set;
	}

	public ApiManager(string baseUrl)
	{
		Client = new RestClient(baseUrl);
		Client.Authenticator = new IncoloreAuthenticator(new Token
		{
			CreatedAt = Time.Timestamp(),
			Id = -1,
			Value = "00000001000000010000000100000001",
			IsLogin = false
		});
		BasicInfo = new BasicInfo(this);
	}

	public async Task<T> ExecuteAsync<T>(RestRequest request) where T : new()
	{
		var response = await Client.ExecuteAsync<T>(request);

		if (response.ErrorException != null)
		{
			const string message = "Error retrieving response.  Check inner details for more info.";
			var twilioException = new Exception(message, response.ErrorException);
			throw twilioException;
		}
		Logger.Debug(response.Content);
		return response.Data;
	}
}
