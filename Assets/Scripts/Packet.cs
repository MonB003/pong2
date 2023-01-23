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

        private const int DEFAULT_SIZE = 8;
        private const float MAX_float = 255;
        private const float MIN_float = 255;

        private float[] data = new float[DEFAULT_SIZE];


        public Packet()
        {

        }

        public Packet(float user, float action, float state, float host, float coordinateID, float x, float y, float z)
        {
            data[0] = user;
            data[1] = action;
            data[2] = state;
            data[3] = host;
            data[4] = coordinateID;
            data[5] = x;
            data[6] = y;
            data[7] = z;
        }

        public Packet(float[] data)
        {
            this.data = data;
        }

        
    public float GetID() {
        return data[0];
    }

        public float GetCoordinateID()
        {
            return data[4];
        }

        public float GetUser()
        {
            return data[0];
        }

        public void SetUser(float userID)
        {

        }

        public float GetState()
        {
            return data[2];
        }

     
        public void SetLocation(float location)
        {
            data[1] = location;
        }

        public float GetAction()
        {
            return data[1];
        }
        public void SetAction(float action)
        {
        }

        public float GetHost()
        {
            return data[3];
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
                
                bytes.AddRange(BitConverter.GetBytes(f));
            }
            return bytes.ToArray();
        }

        public float ConvertByteToFloat(int index)
        {
            return data[index];

        }
    }

    enum Actions {
        JOIN, 
        START, 
        MOVE,
        LEAVE,
        SERVER,
        ACKNOWLEDGMENT
    }

}
