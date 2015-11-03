﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace eSuitLibraryNET45
{
    public class eSuit
    {
        private eSuit_Connection eSuitCon;
        public eSuit()
        {
        }
        public void Start()
        {
            eSuitCon = new eSuit_Connection();        
        }
        public void ExecuteHit(HitPlaces hit, int volts, int duration)
        {
            Contract.Requires(volts > 0, "Volt must be bigger than zero");
            Contract.Requires(volts <= 60, "Volt must be smaller or equil to 60");

            Contract.Requires(duration >= 500, "duration must be bigger or equal than 500 milliseconds");
            Contract.Requires(duration <= 3000, "duration must be smaller or equal than 3000 milliseconds");

            Thread hitThread = new Thread(() => eSuitCon.ExecuteHit(hit, volts, duration));
            hitThread.IsBackground = true;
            hitThread.Start();            
        }
        public bool connected()
        {
            return eSuitCon.connected;
        }

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