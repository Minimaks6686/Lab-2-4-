using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RailwayStation.Models;

namespace RailwayStation
{
    public class MultyThreads
    {
        private AutoResetEvent waitHandler = new(true);
        private Mutex mutex = new();
        private Semaphore semaphore = new(0, 1);
        private DbContextOptions options;
        public MultyThreads(DbContextOptions options)
        {
            this.options = options;
        }

        public void LockExample()
        {
            for (int i = 0; i < 100; i++)
            {
                using (RailwayStationContext context = new(options))
                {
                    Thread myThread = new(() =>
                    {
                        object? locker = new object();
                        lock (locker!) 
                        {
                            context.Stations.AddAsync(new Station { Address = "Address " + i, Station_name = "Station_name " + i});
                            context.SaveChanges();
                        }  
                    });
                    myThread.Start();
                }
            }
        }

        public void MonitorExample()
        {
            object? locker = new();

            for (int i = 101; i < 200; i++)
            {
                using (RailwayStationContext context = new(options))
                {
                    Thread myThread = new(() =>
                    {
                        bool acquiredLock = false;
                        try
                        {
                            Monitor.Enter(locker, ref acquiredLock);

                            context.Stations.AddAsync(new Station { Address = "Address " + i, Station_name = "Station_name " + i });
                            context.SaveChanges();
                        }
                        finally
                        {
                            if (acquiredLock)
                            {
                                Monitor.Exit(locker);
                            }
                        }
                    });
                    myThread.Start();
                }

            }
        }

        public void AutoResetEventExample()
        {
            for (int i = 201; i < 300; i++)
            {
                using (RailwayStationContext context = new(options))
                {
                    Thread myThread = new(() =>
                    {
                        waitHandler.WaitOne();
                        context.Stations.AddAsync(new Station { Address = "Address " + i, Station_name = "Station_name " + i });

                        context.SaveChanges();
                        waitHandler.Set();
                    });
                    myThread.Start();
                }

            }
        }

        public void MutexExample()
        {
            for (int i = 301; i < 400; i++)
            {
                using (RailwayStationContext context = new(options))
                {
                    Thread myThread = new(() =>
                    {
                        mutex.WaitOne();
                        context.Stations.AddAsync(new Station { Address = "Address " + i, Station_name = "Station_name " + i });
                        context.SaveChanges();
                        mutex.ReleaseMutex();
                    });
                    myThread.Start();
                }

            }
        }

        public void SemaphoreExample()
        {
            for (int i = 401; i < 410; i++)
            {
                using (RailwayStationContext context = new(options))
                {
                    Thread myThread = new(() =>
                    {
                        semaphore.WaitOne();
                        context.Stations.AddAsync(new Station { Address = "Address " + i, Station_name = "Station_name " + i });
                        context.SaveChanges();
                        semaphore.Release();
                    });
                    myThread.Start();
                }
            }
        }
    }
}
