using System;

namespace EventExample
{
    public delegate void StockControl();
    class Program
    {
        #region Product Class Tanımlaması
        public class Product
        {
            private short _stockAmount;
            public Product(short stockAmount)
            {
                _stockAmount = stockAmount;
            }

            public event StockControl StockKontrolEvent;
          
            public string ProductName { get; set; }

            public short StockAmount
            {
                get
                {
                    return _stockAmount;
                }
                set
                {
                    _stockAmount = value;
                    if (_stockAmount < 10 && StockKontrolEvent != null)
                    {
                        //stock sayısı 10 dan küçük olan ve event a üye olan her instance için uyarı verecektir.
                        StockKontrolEvent();
                    }
                    
                }
            }
            public void Sell(short amount)
            {
                StockAmount -= amount;
                Console.WriteLine("ProductName: {0} - Stock Amount: {1} ",ProductName,StockAmount);
            }

        }
        #endregion
        static void Main(string[] args)
        {
            Product hdd = new Product(50);
            hdd.ProductName = "Samsung";
            //harddisk için event a üye olma işlemi gerçekleştirildi
            hdd.StockKontrolEvent += Hdd_StockKontrolEvent;

            Product ram = new Product(50);
            ram.ProductName = "Kingston";
            //ram için event a üye olma işlemi gerçekleştirildi
            ram.StockKontrolEvent += Ram_StockKontrolEvent;

            for (int i = 0; i < 10; i++)
            {
                hdd.Sell(10); // 10 tane satma işlemi
                ram.Sell(10); // 10 tane satma işlemi
                Console.ReadLine();
            }
        }
        #region Event Metotları
        private static void Ram_StockKontrolEvent()
        {
            //event ram için tetiklendiği zaman çalışmasını istediğimiz kodlar
            Console.WriteLine("Ram bitiyor!!!");
        }

        private static void Hdd_StockKontrolEvent()
        {
            //event harddisk için tetiklendiği zaman çalışmasını istediğimiz kodlar
            Console.WriteLine("Hdd bitiyor!!!");
        }
        #endregion
    }



}

