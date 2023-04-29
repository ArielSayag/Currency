using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using RestSharp;
using System.Net;

namespace CurrencyConverter
{
  class CallApi
  {
      const string FILEPATH = @"./db.json";
      const string APIKEY = "8M2zIVlReLxsWfLHk4wt3VyqFerHLoAV";  
      const string APIKEY_SECOND= "x718NExZ6gOUyYdHAgYfN0piguHCK91s1KRHuBxc";
      const int AMOUNT = 1;
      private readonly HttpClient _httpClient;
        
      public CallApi()
      {
          _httpClient = new HttpClient();
      }

      // Function with access key and url paremeters  and return Response 
      static IRestResponse GetResponse(string key , string url){

        RestClient client = new RestClient(url);
        client.Timeout = -1;

        RestRequest request = new RestRequest(Method.GET);
        request.AddHeader("apikey", key);          
        IRestResponse response = client.Execute(request);

        return response;
      }
      // Function that Get string key and PaiCoins object - 
      // and bu using api update the List<PairCoins> 
      public  void GetDataApi(string key , PairCoins pair){
        
        string fromCurrency = key.Split('/')[0];
        string toCurrency = key.Split('/')[1];

        string url =$"https://api.apilayer.com/exchangerates_data/convert?from={fromCurrency}&to={toCurrency}&amount={AMOUNT}";

        IRestResponse response =GetResponse(APIKEY,url);
       
        if (response.IsSuccessful) 
        {
          string responseContent = response.Content; // Convert the response content to a string and output it to the console
          // return response;
          JObject apiData = JObject.Parse(responseContent);  
          double result = double.Parse(apiData["result"].ToString());

          string dateString = apiData["date"].ToString().Substring(0, 10);
          DateTime date = DateTime.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
          string formattedDate = date.ToString("dd/MM/yyyy");

          pair.update(pair.Name , result , formattedDate);
        }
        else
        {
          // Bonus question
          string newUrl=$"https://api.freecurrencyapi.com/v1/latest?currencies={fromCurrency}&base_currency={toCurrency}";
          IRestResponse newResponse =GetResponse(APIKEY_SECOND,newUrl);
          
          if (newResponse.IsSuccessful)
          {
            string responseContent = newResponse.Content; 
            JObject apiData = JObject.Parse(responseContent);  
            double result = double.Parse(apiData["data"][fromCurrency].ToString());
            DateTime date = DateTime.Now;
            string formattedDate = date.ToString("dd/MM/yyyy");

            pair.update(pair.Name , result , formattedDate);
          }
          else{
            // Handle the case where the API call was not successful
            throw new Exception($"API call failed with status code {response.StatusCode}");
          }

        } 
      }

      // Function that Get PairCoins and save into a File by Key name
      public void saveToFile(PairCoins pair){

        string data= File.ReadAllText(FILEPATH);
        JObject dataJson = JObject.Parse(data);
       

        if( dataJson.ContainsKey(pair.Name)){
          JObject pairJson = JObject.FromObject(pair);
          dataJson[pair.Name]=pairJson;
        }
        else{
          //for the first time saving 
          dataJson.Add(new JProperty(pair.Name,pair));
        }
          
          string updateData = JsonConvert.SerializeObject(dataJson,Formatting.Indented);
          using (StreamWriter file = File.CreateText(FILEPATH))
          {
            file.Write(updateData);
          }
      }

      // Function that Read From File and Print the info 
      public void readFromFile(){
        string data= File.ReadAllText(FILEPATH);
         if(string.IsNullOrEmpty(data)){
            throw new Exception("Data is Empty");
        }
        JObject dataJson = JObject.Parse(data);
        foreach (var item in dataJson)
        { 
          if(item.Value !=null && item.Value["Name"] !=null && item.Value["Result"]!=null &&  item.Value["Date"]!=null ){
              string name = item.Value["Name"].ToString();
              string result = item.Value["Result"].ToString();
              string date = item.Value["Date"].ToString();

              Console.WriteLine("Name: " + name);
              Console.WriteLine("Result: " + result);
              Console.WriteLine("Date: " + date);
              Console.WriteLine();

          }
          else{
              Console.WriteLine($"For this Currency pair: {item.Key} Data is Empty");
          }
        }

      }
  }
}