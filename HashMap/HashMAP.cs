using System;
using System.Runtime.InteropServices;

namespace HashMap
{
    internal class HashMap<K,T>
    {
        int size;
        int count;
        Bucket<K, T>[] values;
        const double loadFactorThreshHold = 0.7;
        public HashMap(int size)
        {
            this.size = size;
            values = new Bucket<K, T>[size];
        }
        public HashMap(){
            this.size = 16;
            values = new Bucket<K,T>[size];
        }
        public bool Exists(K s)
        {
            return this.values[Hash(s, size)] != null;
        }

        private int Hash(K key, int size)
        {
            //return Math.Abs(s.GetHashCode() % size;
            //credit: Yosi Zahavi
            //implementing the djb2  algorithem
            int hash = 5381;
            string s = key.ToString(); //For Any Type Of Key*/
            for (int i = 0; i < s.Length; i++)
            {
                hash = (int)(hash << 5) + hash + (int)s[i];
            }
            hash %= size;
            if (hash < 0) return hash + size;
            return hash;
        }
        public bool AddKey(K key, T value)
        {
            int indx = Hash(key, size);
            if (values[indx] == null) { 
                values[indx] = new Bucket<K, T>(key, value);
                count++;
                checkResize();
                return true; 
            }
            if (values[indx].getKey().Equals(key))
            {
                values[indx].setValue(value);
                return true;
            }
            else
            {
                int checking = (indx + 1) % size;
                while (values[checking] != null && !(values[checking].getKey().Equals(key)))
                {

                    if (values[checking].getKey().Equals(key))
                    {
                        values[checking].setValue(value);
                        count++;
                        checkResize();
                        return true;
                    }
                    checking++;
                    checking %= size;
                    if (checking == indx) //Made Full Cicle
                    {
                        Console.WriteLine("Cannot Add Key - No Empty key");
                        return false;
                    }
                }
                values[checking] = new Bucket<K, T>(key, value);
                count++;
                checkResize();
                return true;
            }

         }
        private void checkResize()
        {
            double alpha = count / (double)size;
            if ( alpha > loadFactorThreshHold)
            {
                Console.WriteLine("Resizing with α: " + alpha);
                Resize(size * 2);
            }
        }
        public bool UpdateKey(K key, T value)
        {
            int indx = Hash(key, size);
            int curChecking = indx;
            while(!(values[curChecking].getKey().Equals(key)))
            {
                curChecking++;
                curChecking %= size;
                if(curChecking == indx)
                {
                    Console.WriteLine("Key Dosent Exist");
                    return false;
                }
            
            }
            if(values[curChecking].getKey().Equals(key))
            {
                values[curChecking].setValue(value);
                return true;
            }
            return false;
        }
        public T GetValue(K key)
        {
            int indx = Hash(key,size);
            int searchingIndx = indx;
            while (!(values[searchingIndx].getKey().Equals(key)))
            {
                searchingIndx++;
                searchingIndx %= size;
                if(searchingIndx == indx)
                {
                    throw new Exception("Key Dosent Exists");
                }
            }
            return values[searchingIndx].getValue();
        }
        public bool includes(K key)
        {
            int indx = Hash(key, size);
            /*if (values[indx].getKey().Equals(key)) return true;
            */int checking = indx;
            
            while (values[checking] != null && !(values[checking].getKey().Equals(key)))
            {
                checking++;
                checking %= size;
                if (checking == indx) return false;
                
            }
            if (values[checking] == null) return false;
            return values[checking].getKey().Equals(key);
            /*return values[checking].getKey().Equals(key);*/
        }
        public bool DeleteValue(K key)
        {
            int indexRemove = Hash(key, size);
            Bucket<K, T> temp = values[indexRemove];
            if (temp != null && temp.getKey().Equals(key))
            {
                values[indexRemove] = null;
                count--;
                return true;
            }
            int realIndx =( indexRemove + 1 ) % size;
            while (!(values[realIndx].getKey().Equals(key)))
            {
                realIndx++;
                realIndx %= size;
                if (realIndx == indexRemove) return false;
            }
            values[realIndx] = null;
            count--;
            return true;
        }
        public bool Resize(int newSize)
        {
            if (newSize <= size)
            {
                return false; // can't resize to smaller size
            }
            Bucket<K, T>[] newArr = new Bucket<K, T>[newSize];
            for (int i = 0; i < values.Length; i++)
            {
                Bucket<K, T> bucket = values[i];
                if (bucket != null)
                {
                    int newIndx = Hash(bucket.getKey(), newSize);
                    while (newArr[newIndx] != null)
                    {
                        newIndx++;
                        newIndx %= newSize;
                    }
                    newArr[newIndx] = bucket;
                }
            }
            values = newArr;
            size = newSize;
            Console.WriteLine("Resized");
            return true;
        }
        public double GetFactor()
        {
            return count / (double)size;
        }


    }
}
