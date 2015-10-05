using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace BaoXin.Utility
{
    public class TableFramework : ICollection
    {
        string _tableName;
        public string TableName { get { return _tableName; } set { _tableName = value; } }
        public TableFramework(string tableName)
        {
            _tableName = tableName;
        }
        public TableFramework()
        {
        }
        public class Column
        {
            public Column(string name, Type cType, object value)
            {
                Name = name;
                CType = cType;
                Value = value;
            }
            public Column(string name, string value)
            {
                Name = name;
                CType = typeof(string);
                Value = value;
            }
            public Column(string name, int value)
            {
                Name = name;
                CType = typeof(int);
                Value = value;
            }
            public Column(string name, double value)
            {
                Name = name;
                CType = typeof(double);
                Value = value;
            }
            public Column(string name, long value)
            {
                Name = name;
                CType = typeof(long);
                Value = value;
            }
            public Column(string name, float value)
            {
                Name = name;
                CType = typeof(float);
                Value = value;
            }
            public Column(string name, decimal value)
            {
                Name = name;
                CType = typeof(decimal);
                Value = value;
            }
            public string Name;
            public Type CType;
            public object Value;
        }
        ArrayList list = new ArrayList();

        public void Add(string name, Type cType, object value)
        {
            Column col = new Column(name, cType, value);
            list.Add(col);
        }
        public void Add(string name, string value)
        {
            Column col = new Column(name, value);
            list.Add(col);
        }
        public void Add(string name, DateTime dt)
        {
            Column col = new Column(name, typeof(DateTime), dt);
            list.Add(col);
        }
        public void Add(string name, DateTime? dt)
        {
            Column col = new Column(name, typeof(DateTime), dt);
            list.Add(col);
        }
        public void Add(string name, int value)
        {
            Column col = new Column(name, value);
            list.Add(col);
        }
        public void Add(string name, double value)
        {
            Column col = new Column(name, value);
            list.Add(col);
        }
        public void Add(string name, decimal value)
        {
            Column col = new Column(name, value);
            list.Add(col);
        }
        public void Add(string name, float value)
        {
            Column col = new Column(name, value);
            list.Add(col);
        }
        public void Add(string name, long value)
        {
            Column col = new Column(name, value);
            list.Add(col);
        }
        public void Add(string name, bool value)
        {
            Column col = new Column(name, typeof(bool), value);
            list.Add(col);
        }
        public void Add(string name, bool? value)
        {
            Column col = new Column(name, typeof(bool?), value);
            list.Add(col);
        }
        public Column this[int index]
        {
            get { return list[index] as Column; }
        }
        public Column this[string name]
        {
            get
            {
                foreach (Column c in this)
                {
                    if (name == c.Name)
                        return c;
                }
                return null;
            }
        }

        public void CopyTo(Array array, int index)
        {
            list.CopyTo(array, index);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsSynchronized
        {
            get { return list.IsSynchronized; }
        }

        public object SyncRoot
        {
            get { return list.SyncRoot; }
        }


        public IEnumerator GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
