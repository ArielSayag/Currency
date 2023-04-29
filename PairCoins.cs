using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CurrencyConverter
{
  class PairCoins
  {
    public string Name {get;}
    public double Result {get; set;}
    public string Date {get; set;}

    public PairCoins(string name, double result = 0.0,string date = ""){
      Name = name;
      Result = result;
      Date = date;
    }
    public void update(string name , double result , string date){
      if(Name == name){
        Result = result;
        Date = date;
      }
    }

    public void print(){
      Console.WriteLine("Name: " + Name);
      Console.WriteLine("Result: " + Result);
      Console.WriteLine("Date: " + Date);
      Console.WriteLine();
    }


  }
}