using System; 
using System.Text;
using System.Collections.Generic;

public class Logger {
  readonly LinkedList<string> log;

  public Logger(LinkedList<string> log = null)
      => this.log = log ?? new LinkedList<string>();
    
  public Logger Log(string msg) {    
    var newLog = new LinkedList<string>(log);
    newLog.AddLast(msg);
    return new Logger(newLog);
  }
  
  public string Dump() => string.Join("\n", log);
}
