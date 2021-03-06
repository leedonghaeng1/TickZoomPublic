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

namespace TickZoom.Api
{
	public interface SymbolHandler {
		bool IsRunning { get; }
	    void Clear();
		void Start();
		void Stop();
		void SendQuote();
        void SendOptionPrice();
        void SendTimeAndSales();
		void SetPosition(double position);
		void AddPosition(double position);
		OrderAlgorithm LogicalOrderHandler {
			get;
			set;
		}
		double Position {
			get;
		}
		TimeStamp Time {
			get;
			set;
		}
		int AskSize {
			get;
			set;
		}
		int BidSize {
			get;
			set;
		}
		int LastSize {
			get;
			set;
		}
		double Ask {
			get;
			set;
		}
		double Bid {
			get;
			set;
		}
		double Last {
			get;
			set;
		}

	    double StrikePrice { get; set; }
	    TimeStamp UtcOptionExpiration { get; set; }
	    OptionType OptionType { get; set; }
	    long TickCount { get; }
	    SymbolInfo Symbol { get; }
	}
}
