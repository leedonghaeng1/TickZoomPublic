#region Copyright
/*
 * Software: TickZoom Trading Platform
 * Copyright 2009 M. Wayne Walter
 * 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * Business use restricted to 30 days except as otherwise stated in
 * in your Service Level Agreement (SLA).
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, see <http://www.tickzoom.org/wiki/Licenses>
 * or write to Free Software Foundation, Inc., 51 Franklin Street,
 * Fifth Floor, Boston, MA  02110-1301, USA.
 * 
 */
#endregion

using System;
using System.Collections.Generic;
using System.Threading;

namespace TickZoom.Api
{
	public class PositionChangeDetail {
		private SymbolInfo symbol;
		private int position;
		private long utcTime;
	    private long counter;
		private Iterable<LogicalOrder> orders;
	    private Iterable<StrategyPosition> strategyPositions;
        private static long counterNext = 0;

        public PositionChangeDetail(SymbolInfo symbol, int position, Iterable<LogicalOrder> orders, Iterable<StrategyPosition> strategyPositions, long utcTime) : this( symbol, position, orders, strategyPositions, utcTime, Interlocked.Increment(ref counterNext))
        {
		}

        public PositionChangeDetail(SymbolInfo symbol, int position, Iterable<LogicalOrder> orders, Iterable<StrategyPosition> strategyPositions, long utcTime, long counter)
        {
            this.symbol = symbol;
            this.position = position;
            this.orders = orders;
            this.strategyPositions = strategyPositions;
            this.utcTime = utcTime;
            this.counter = counter;
        }

        public int Position
        {
			get { return position; }
		}
		
		public Iterable<LogicalOrder> Orders {
			get { return orders; }
		}
		
		public SymbolInfo Symbol {
			get { return symbol; }
		}
		
		public long UtcTime {
			get { return utcTime; }
		}

	    public Iterable<StrategyPosition> StrategyPositions
	    {
	        get { return strategyPositions; }
	    }

	    public long Counter
	    {
	        get { return counter; }
	        set { counter = value; }
	    }

        public override string ToString()
        {
            return "Counter " + counter + ", "  + symbol + ", Position " + position + ", Orders " + orders.Count;
        }
	}
}
