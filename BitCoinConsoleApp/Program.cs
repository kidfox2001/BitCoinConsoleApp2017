using NBitcoin;
using System;


// privateKey => publicKey => ย่นรูปให้สั้นลงเป็น publicKeyHash (Bitcoin Address)
// Bitcoin Wallet ไว้เก็บ  Bitcoin Address และ Private Key
// รางวัล Bitcoin Subsidy นี้จะถูกมอบให้กับผู้สร้าง Block สำเร็จในรูปแบบ Transaction ที่ใส่ไว้ใน Block ปกตินี่แหละ เพียงแต่ว่า Transaction นี้ได้รับการยกเว้นไม่ต้องอ้างอิง inputs กับ Transaction ใด ๆ ก่อนหน้านี้ และด้วยความพิเศษของมัน Transaction นี้เลยมีชื่อเรียกเฉพาะด้วยว่า Coinbase Transaction หรือ Generation Transactionและเราจะใส่เจ้า Coinbase Transaction ตัวนี้ไว้เป็น Transaction แรกของ Block เสมอครับ 

// จะโอนได้ต้องรู้ address ชองผู้รับติด ScriptPubKey ไปในส่วนของ out address => hash => ScriptPubKey (แนวคิด)/
// ??? ScriptPubKey , โครงสร้างใน block
namespace BitCoinConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Coin!");


            Console.WriteLine("======================= privateKey");
            Key privateKey = new Key(); // generate a random private key
            Console.WriteLine("privateKey : " + privateKey);


            Console.WriteLine("======================= privateKey => publicKey");
            PubKey publicKey = privateKey.PubKey;
            Console.WriteLine("publicKey : " +  publicKey);


            Console.WriteLine("======================= publicKey => GetAddress");
            Console.WriteLine("addressMain : " + publicKey.GetAddress(Network.Main));
            Console.WriteLine("addressTestNet : " + publicKey.GetAddress(Network.TestNet));


            // hash คือการลดรูปของ publicKey
            Console.WriteLine("======================= publicKey => publicKeyHash => GetAddress");
            var publicKeyHash = publicKey.Hash;
            Console.WriteLine("publicKeyHash : " +  publicKeyHash); 
            var mainNetAddress = publicKeyHash.GetAddress(Network.Main);
            var testNetAddress = publicKeyHash.GetAddress(Network.TestNet);
            Console.WriteLine("mainNetAddress : " + mainNetAddress);
            Console.WriteLine("testNetAddress : " + testNetAddress);


            // เอาส่วนแรกก่อนละกัน ส่วนแรกที่เป็นการเขียนโน้ตทิ้งไว้ใน out ฝากไว้ให้ผู้รับ Script ส่วนนี้มีชื่อเรียกอย่างเป็นทางการว่า Public Key Script หรือ scriptPubKey ซึ่งก็คือ Script ฝั่งที่ใช้ Public Key ครับ
            Console.WriteLine("======================= ScriptPubKey from Address");
            var publicKeyHash1 = new KeyId("14836dbe7f38c5ac3d49e8d790af808a4ee9edcf");
            Console.WriteLine("publicKeyHash1 : " + publicKeyHash1);
            var testNetAddress1 = publicKeyHash1.GetAddress(Network.TestNet);
            var mainNetAddress1 = publicKeyHash1.GetAddress(Network.Main);
            Console.WriteLine("mainNetAddress : " + mainNetAddress1);
            Console.WriteLine("testNetAddress : " + testNetAddress1);
            Console.WriteLine("mainNetAddress1ScriptPubKey : " + mainNetAddress1.ScriptPubKey); 
            Console.WriteLine("testNetAddress1ScriptPubKey : " + testNetAddress1.ScriptPubKey);


            Console.WriteLine("======================= Address ที่ได้จาก ScriptPubKey ที่มาจาก hash เดียวกัน จะเท่ากับ hash ที่ GetAddress (ScriptPubKey หน้าจะมีความสัมพันกคับ publicKeyHash)");
            var paymentScript = publicKeyHash1.ScriptPubKey;
            var sameMainNetAddress = paymentScript.GetDestinationAddress(Network.Main);
            Console.WriteLine(mainNetAddress1 == sameMainNetAddress); // True
            Console.WriteLine(mainNetAddress1.ScriptPubKey == publicKeyHash1.ScriptPubKey); // True

            Console.ReadLine();
        }
    }
}
