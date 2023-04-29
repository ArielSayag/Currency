using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace CurrencyConverter
{
  class Program
  {
    static void Main(string[] args){
      
     CoinsList coins = new CoinsList();
     CallApi api=new CallApi();
      //1)
     coins.SetUp();
     coins.fetchDate();
     coins.save();

      //2)
      api.readFromFile();
      // coins.print();

      /*Note 1: 

      I can automate the process of calling `fetchData` and `save` the data by invoking `fetchData` in
      the setup method and calling the `save` method within `fetchData` */

      /*I chose to save the data into a file because for a small amount of data,
      it is a simple and efficient way to store the information.
      The result from the API returns as a JSON object,
      which makes it easy to select the values I want and insert them into a file with the key name of pair coins.
      If I had chosen to use a database,
      it would have required more overhead and assistance from the CoinsList object
      and I would have needed to use SQL to store the data.  */
      
      /*Note 2:

      There are two ways to print the data: one is to print from the object `coins`,
      or to read from the file directly.
      A third way is to read from the file and insert the data into a new `List<PairCoins>`,
      and then call the `print` function from there.
      */

      /*
        For the Bonus question,
        
        I extract the parts of the `GetDateApi` function that relate to the `RestClient`,
        up until the `response`, into a separate function.
        This new function would take a URL string and access key as parameters,
        and would return the response. If the response  status is NOT status OK, 
        I would have the function try another URL with a different access key.
      */

      //output example:
      /*Name: USD/ILS
        Result: 3.63729
        Date: 28/04/2023

        Name: GBP/EUR
        Result: 1.134036
        Date: 28/04/2023

        Name: EUR/JPY
        Result: 149.162771
        Date: 28/04/2023

        Name: EUR/USD
        Result: 1.098756
        Date: 28/04/2023*/

   
    }
  }
}