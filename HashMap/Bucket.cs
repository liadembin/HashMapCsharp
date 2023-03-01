using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashMap
{
    class Bucket<K,T>
    {
        K key;
        T value;
        public Bucket(K key, T value)
        {
            this.key = key;
            this.value = value;
        }
        public T getValue()
        {
            return value;
        }
        public K getKey()
        {
            return key;
        }
        public void setKey(K key)
        {
            this.key = key;
        }
        public void setValue(T value)
        {
            this.value = value;
        }
    }
}
