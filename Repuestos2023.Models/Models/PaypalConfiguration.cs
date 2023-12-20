using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repuestos2023.Models.Models
{
	public static class PaypalConfiguration
	{
		//Variables for storing the clientID and clientSecret key  

		//Constructor  

		static PaypalConfiguration()
		{

		}
		
		private static string GetAccessToken(string ClientId, string ClientSecret)
		{
			// getting accesstocken from paypal  
			string accessToken = new OAuthTokenCredential(ClientId, ClientSecret).GetAccessToken();
			return accessToken;
		}
		public static APIContext GetAPIContext(string clientId, string clientSecret)
		{
			// return apicontext object by invoking it with the accesstoken  
			APIContext apiContext = new APIContext(GetAccessToken(clientId, clientSecret));
			return apiContext;
		}
	}
}
