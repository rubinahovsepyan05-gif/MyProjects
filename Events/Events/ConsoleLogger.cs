namespace Events;

public class ConsoleLogger
{
    private Auction _auction;

    public ConsoleLogger(Auction auction)
    {
        _auction = auction;
        _auction.BidPlaced += onBidPlaced;
        _auction.Outbid += onOutbid;
        _auction.Ended += onEnded;
    }

    public void onBidPlaced(object sender, BidEventArgs args)
    {
        Console.WriteLine($"[bid] {args.Bidder} {args.Amount}");
    }

    public void onOutbid(object sender, OutbidEventArgs args)
    {
        Console.WriteLine($"[outbid] {args.PreviousLeader} (was {args.PreviousAmount})");
    }

    public void onEnded(object sender, AuctionEndedEventArgs args)
    {
        Console.WriteLine($"[ended] {args.Item} -> {args.Winner} for {args.FinalPrice}");
    }
}