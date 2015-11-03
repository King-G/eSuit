﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace eSuitLibraryNET35
{
    public class eSuit
    {
        private eSuit_Connection eSuitCon;
        public eSuit()
        {
        }
        //Start eSuit Connection
        public void Start()
        {
            eSuitCon = new eSuit_Connection();
        }
        //Execute a Hit on the eSuit
        //Volts (min. 1, max. 60) (volts)
        //Duration (min. 10, max 3000) (milliseconds)
        public void ExecuteHit(HitPlaces hit, int volts, int duration)
        {
            if (hit == null)
            {
                throw new Exception("Hit can't be null");
            }
            else if (volts > 60 || volts < 1)
            {
                throw new Exception("volts must have a value between 1 and 60");
            }
            else if (duration < 10 || duration > 3000)
            {
                throw new Exception("duration must have a value between 10 and 3000");
            }
            else
            {
                Thread hitThread = new Thread(() => eSuitCon.ExecuteHit(hit, volts, duration));
                hitThread.IsBackground = true;
                hitThread.Start();
            }     
        }
        // Check the connection status.
        public bool connected()
        {
            return eSuitCon.connected;
        }
        // Check the current port used by the eSuit.
        public string currentPort()
        {
            return eSuitCon.currentPort.PortName;
        }

        public void Dispose()
        {
            eSuitCon.Dispose();
            GC.Collect();
        }

        
    }
}