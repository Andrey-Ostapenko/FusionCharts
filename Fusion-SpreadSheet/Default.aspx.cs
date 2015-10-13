﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.GData.Client;
using Google.GData.Spreadsheets;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ////////////////////////////////////////////////////////////////////////////
      
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        // STEP 1: Configure how to perform OAuth 2.0
        ////////////////////////////////////////////////////////////////////////////

        // TODO: Update the following information with that obtained from
        // https://code.google.com/apis/console. After registering
        // your application, these will be provided for you.

        string CLIENT_ID = "641157605077-rr2l9ma85ps6n5grp031js3pccehfv5u.apps.googleusercontent.com";

        // This is the OAuth 2.0 Client Secret retrieved
        // above.  Be sure to store this value securely.  Leaking this
        // value would enable others to act on behalf of your application!
        string CLIENT_SECRET = "Gq-ekOvi9grFi9K3rznIpoT9";

        // Space separated list of scopes for which to request access.
        string SCOPE = "https://spreadsheets.google.com/feeds https://docs.google.com/feeds";

        // This is the Redirect URI for installed applications.
        // If you are building a web application, you have to set your
        // Redirect URI at https://code.google.com/apis/console.
        string REDIRECT_URI = "http://localhost:1918/oauth2callback";

        string TOKEN_TYPE = "refresh";

        ////////////////////////////////////////////////////////////////////////////
        // STEP 2: Set up the OAuth 2.0 object
        ////////////////////////////////////////////////////////////////////////////

        // OAuth2Parameters holds all the parameters related to OAuth 2.0.
        OAuth2Parameters parameters = new OAuth2Parameters();

        // Set your OAuth 2.0 Client Id (which you can register at
        // https://code.google.com/apis/console).
        parameters.ClientId = CLIENT_ID;

        // Set your OAuth 2.0 Client Secret, which can be obtained at
        // https://code.google.com/apis/console.
        parameters.ClientSecret = CLIENT_SECRET;

        // Set your Redirect URI, which can be registered at
        // https://code.google.com/apis/console.
        parameters.RedirectUri = REDIRECT_URI;

        ////////////////////////////////////////////////////////////////////////////
        // STEP 3: Get the Authorization URL
        ////////////////////////////////////////////////////////////////////////////

        // Set the scope for this particular service.
        parameters.Scope = SCOPE;

        parameters.AccessType = "offline"; // IMPORTANT and was missing in the original

        parameters.TokenType = TOKEN_TYPE; // IMPORTANT and was missing in the original

      
        string authorizationUrl = OAuthUtil.CreateOAuth2AuthorizationUrl(parameters);
        Response.Redirect(authorizationUrl);
        Console.WriteLine("Please visit the URL above to authorize your OAuth "
          + "request token.  Once that is complete, type in your access code to "
          + "continue...");
        parameters.AccessCode = Console.ReadLine();

        ////////////////////////////////////////////////////////////////////////////
        // STEP 4: Get the Access Token
        ////////////////////////////////////////////////////////////////////////////

        // Once the user authorizes with Google, the request token can be exchanged
        // for a long-lived access token.  If you are building a browser-based
        // application, you should parse the incoming request token from the url and
        // set it in OAuthParameters before calling GetAccessToken().
        OAuthUtil.GetAccessToken(parameters);
        string accessToken = parameters.AccessToken;
        Console.WriteLine("OAuth Access Token: " + accessToken);

        ////////////////////////////////////////////////////////////////////////////
        // STEP 5: Make an OAuth authorized request to Google
        ////////////////////////////////////////////////////////////////////////////

        // Initialize the variables needed to make the request
        GOAuth2RequestFactory requestFactory =
            new GOAuth2RequestFactory(null, "MySpreadsheetIntegration-v1", parameters);
        SpreadsheetsService service = new SpreadsheetsService("MySpreadsheetIntegration-v1");
        service.RequestFactory = requestFactory;

        // Make the request to Google
       
    }
}