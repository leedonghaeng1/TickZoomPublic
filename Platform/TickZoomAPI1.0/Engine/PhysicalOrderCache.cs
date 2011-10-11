﻿using System;
using System.Collections.Generic;

namespace TickZoom.Api
{
    public struct PhysicalOrderLock : IDisposable
    {
        private PhysicalOrderCache lockedCache;
        public PhysicalOrderLock(PhysicalOrderCache cache)
        {
            lockedCache = cache;
        }
        public void Dispose()
        {
            lockedCache.Unlock();
        }
    }
    public interface PhysicalOrderCache : IDisposable
    {
        void SetOrder(CreateOrChangeOrder order);
        CreateOrChangeOrder RemoveOrder(string clientOrderId);
        Iterable<CreateOrChangeOrder> GetActiveOrders(SymbolInfo symbol);
        bool HasCancelOrder(PhysicalOrder order);
        bool HasCreateOrder(CreateOrChangeOrder order);
        PhysicalOrderLock Lock();
        void Unlock();
        void ResetLastChange();
    }

    public interface PhysicalOrderStore : PhysicalOrderCache
    {
        string DatabasePath { get; }
        long SnapshotRolloverSize { get; set; }
        int RemoteSequence { get; }
        int LocalSequence { get; }
        void TrySnapshot();
        void ForceSnapShot();
        void WaitForSnapshot();
        bool Recover();
        void Clear();
        bool TryGetOrderById(string brokerOrder, out CreateOrChangeOrder order);
        bool TryGetOrderBySequence(int sequence, out CreateOrChangeOrder order);
        CreateOrChangeOrder GetOrderById(string brokerOrder);
        bool TryGetOrderBySerial(long logicalSerialNumber, out CreateOrChangeOrder order);
        CreateOrChangeOrder GetOrderBySerial(long logicalSerialNumber);
        void UpdateLocalSequence(int localSequence);
        void UpdateRemoteSequence(int remoteSequence);
        void SetSequences(int remoteSequence, int localSequence);
        List<CreateOrChangeOrder> GetOrders(Func<CreateOrChangeOrder, bool> select);
        string LogOrders();
        int Count();
    }
}