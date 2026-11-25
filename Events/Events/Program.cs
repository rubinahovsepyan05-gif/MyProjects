namespace Events;

class Program
{
    static void Main(string[] args)
    {
        var auction = new Auction("Vintage guitar", 100, 10);
        var logger = new ConsoleLogger(auction);
        var anna = new AutoBidder("Anna", 180, auction);
        var ben=new AutoBidder("Ben", 150, auction);

        auction.PlaceBid("Anna", 100);
        auction.PlaceBid("Ben", 110);
        auction.Close();
    }
    
}

public delegate void BidHandler(object sender, BidEventArgs e);
public delegate void OutbidHandler(object sender, OutbidEventArgs e);
public delegate void EndedHandler(object sender, AuctionEndedEventArgs e);

public class BidEventArgs : EventArgs
{
    public string Bidder { get; }
    public decimal Amount { get; }

    public BidEventArgs(string bidder, decimal amount)
    {
        Bidder = bidder;
        Amount = amount;
    }
}

public class OutbidEventArgs : EventArgs
{
    public string PreviousLeader { get; }
    public decimal PreviousAmount { get; }
    public decimal NewAmount { get; }

    public OutbidEventArgs(string previousLeader, decimal previousAmount, decimal newAmount)
    {
        PreviousLeader = previousLeader;
        PreviousAmount = previousAmount;
        NewAmount = newAmount;
    }
}

public class AuctionEndedEventArgs : EventArgs
{
    public string Item{ get; }
    public string? Winner { get; }
    public decimal FinalPrice { get; }

    public AuctionEndedEventArgs(string item, string? winner, decimal finalPrice)
    {
        Item = item;
        Winner = winner;
        FinalPrice = finalPrice;
    }
}
