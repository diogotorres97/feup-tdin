using System;
using System.Collections;

[Serializable]
public class Item {
  int type;
  public string Name {get; set;}
  public string Comment {get; set;}

  public Item() : this(0, "", "") {
  }

  public Item(int type, string name, string comment) {
    Type = type;
    Name = name;
    Comment = comment;
  }

  public int Type {
    get {
      return type;
    }
    set {
      if (value < 0)
        type = 999;
      else
        type = value;
    }
  }
}

public enum Operation { New, Change };

public delegate void AlterDelegate(Operation op, Item item);

public interface IListSingleton {
  event AlterDelegate alterEvent;

  ArrayList GetList();
  int GetNewType();
  void AddItem(Item item);
  void ChangeComment(int type, string comment);
}

public class AlterEventRepeater : MarshalByRefObject {
  public event AlterDelegate alterEvent;

  public override object InitializeLifetimeService() {
    return null;
  }

  public void Repeater(Operation op, Item item) {
    if (alterEvent != null)
      alterEvent(op, item);
  }
}