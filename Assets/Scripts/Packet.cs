using System.Net.Security;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using UnityEngine;


namespace pong2
{
    public class Packet
    {

        private const int DEFAULT_SIZE = 5;
        private const float MAX_float = 255;
        private const float MIN_float = 255;

        private float[] data = new float[DEFAULT_SIZE];


        public Packet()
        {

        }

        public Packet(float user, float action, float x, float y, float z)
        {
            data[0] = user;
            data[1] = action;
            data[2] = x;
            data[3] = y;
            data[4] = z;
        }



        public float GetUser()
        {
            return data[0];
        }

        public void SetUser(float userID)
        {

        }

        public float GetFruit()
        {
            return data[1];
        }
        public void SetLocation(float location)
        {
            data[1] = location;
        }

        public float GetAction()
        {
            return data[2];
        }
        public void SetAction(float action)
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

            List<byte> bytes = new List<byte>();
            foreach (float f in data)
            {
                byte[] floatBytes = BitConverter.GetBytes(f);
                foreach (byte b in floatBytes)
                {
                    Debug.Log(b);
                }
                
                bytes.AddRange(BitConverter.GetBytes(f));
            }
            return bytes.ToArray();
        }
    }

}
