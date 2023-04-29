using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CurrencyConverter
{
  class CoinsList
  {
    string[] keys = { "USD/ILS","GBP/EUR","EUR/JPY","EUR/USD" };
    public List<PairCoins> coins;
    public CoinsList(){
      coins = new List<PairCoins>();
    }

    public List<PairCoins> GetCoins() {
        return coins;
    }
    public void Add(PairCoins pair){
      coins.Add(pair);
    }
  

    public void  SetUp(){
      foreach (var name in keys)
      {
        PairCoins newPair = new PairCoins(name);
        coins.Add(newPair);
      }
      // this.fetchDate();
    }

    public void fetchDate(){
      CallApi api=new CallApi();
    
      foreach (PairCoins item in coins)
      {
        try{
          api.GetDataApi(item.Name,item);
        }
        catch(Exception ex){
          Console.WriteLine(ex.Message);
        }
      }
    }
    
    public void save(){
      CallApi api=new CallApi();
      foreach (PairCoins item in coins)
      {
        api.saveToFile(item);
      }
    }
    public void print(){
      
      foreach (var item in coins)
      {
        item.print();
      }
    }
  
  }
}