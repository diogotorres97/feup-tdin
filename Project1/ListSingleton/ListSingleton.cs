using System;
using System.Collections;
using System.Threading;

public class ListSingleton : MarshalByRefObject, IListSingleton {
  ArrayList itemsList;
  public event AlterDelegate alterEvent;
  int type = 2;

  public ListSingleton() {
    Console.WriteLine("Constructor called.");
    itemsList = new ArrayList();
    Item item = new Item(1, "Peter", "A comment");
    itemsList.Add(item);
  }

  public override object InitializeLifetimeService() {
    return null;
  }

  public ArrayList GetList() {
    Console.WriteLine("GetList() called.");
    return itemsList;
  }

  public int GetNewType() {
    return type++;
  }

  public void AddItem(Item item) {
    itemsList.Add(item);
    NotifyClients(Operation.New, item);
  }

  public void ChangeComment(int type, string comment) {
    Item nitem = null;

    foreach (Item it in itemsList) {
      if (it.Type == type) {
        it.Comment = comment;
        nitem = it;
        break;
      }
    }
    NotifyClients(Operation.Change, nitem); 
  }

  void NotifyClients(Operation op, Item item) {
    if (alterEvent != null) {
      Delegate[] invkList = alterEvent.GetInvocationList();

      foreach (AlterDelegate handler in invkList) {
        new Thread(() => {
          try {
            handler(op, item);
            Console.WriteLine("Invoking event handler");
          }
          catch (Exception) {
            alterEvent -= handler;
            Console.WriteLine("Exception: Removed an event handler");
          }
        }).Start();
      }
    }
  }
}