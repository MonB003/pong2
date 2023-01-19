using System.Net.Security;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using UnityEngine;


namespace pong2{
public class Packet  
{

        private const int DEFAULT_SIZE = 5;
        private const byte MAX_BYTE = 255;
        private const byte MIN_BYTE = 255;

        private byte[] data = new byte[DEFAULT_SIZE];


        public Packet()
        {
            // Set default values for each byte in the data
            // R random = new R();
            // for (int i = 0; i < DEFAULT_SIZE; i++)
            // {
            //     data[i] = (byte)random.Next(0, 255);
            // }
        }

        public Packet(byte user, byte action, byte x, byte y, byte z)
        {
                    data[0] = user;
                    data[1] = action;
                    data[2] = x;
                    data[3] = y;
                    data[4] = z;
        }

    

        public byte GetUser()
        {
            return data[0];
        }

        public void SetUser(byte userID)
        {
           
        }

        public byte GetFruit()
        {
            return data[1];
        }
        public void SetLocation(byte location)
        {
           data[1] = location;
        }

        public byte GetAction()
        {
            return data[2];
        }
        public void SetAction(byte action)
        {   
        }


        public override string ToString()
        {
            string result = "";

            for (int i = 0; i < data.Length; i++)
            {
                result += data[i] + " ";
            }

            return result;
        }

        public byte[] GetBuffer()
        {
            return this.data;
        }
}

}
