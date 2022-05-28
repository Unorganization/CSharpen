using System; 
using System.Text;
using System.Collections.Generic;

public class Metric {
  readonly int amount;

  public Metric(int amount = 0) => this.amount = amount;
    
  public Metric Measure() {    
    var newAmount = amount + 1;
    return new Metric(newAmount);
  }
  
  public string Dump() => $"Amount: {amount}";
}
