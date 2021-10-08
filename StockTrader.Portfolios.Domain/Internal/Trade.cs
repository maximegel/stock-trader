using System;
using StockTrader.Portfolios.Domain.Payloads;

namespace StockTrader.Portfolios.Domain.Internal
{
    internal abstract class Trade
    {
        public interface ISellMatcher<TResult>
        {
            TResult WhenSell(Func<TResult> func);
        }

        private interface IBuyMatcher<TResult>
        {
            ISellMatcher<TResult> WhenBuy(Func<TResult> func);
        }

        public static Trade OfType(TradeType type) =>
            type switch
            {
                TradeType.Buy => new BuyTrade(),
                TradeType.Sell => new SellTrade(),
                _ => throw new ArgumentOutOfRangeException(nameof(type)),
            };

        public ISellMatcher<TResult> WhenBuy<TResult>(Func<TResult> func) =>
            Matcher<TResult>.For(this).WhenBuy(func);

        private class BuyTrade : Trade
        {
        }

        private class SellTrade : Trade
        {
        }

        private class Matcher<TResult> :
            IBuyMatcher<TResult>,
            ISellMatcher<TResult>
        {
            private readonly Trade _trade;
            private TResult? _result;

            private Matcher(Trade trade) =>
                _trade = trade;

            public static IBuyMatcher<TResult> For(Trade trade) =>
                new Matcher<TResult>(trade);

            ISellMatcher<TResult> IBuyMatcher<TResult>.WhenBuy(Func<TResult> func)
            {
                if (_trade is BuyTrade)
                {
                    _result = func();
                }

                return this;
            }

            TResult ISellMatcher<TResult>.WhenSell(Func<TResult> func)
            {
                if (_trade is SellTrade)
                {
                    _result = func();
                }

                return _result!;
            }
        }
    }
}
